# PULSR3 Software Analysis Report

## 1. Project Overview
This software is a control interface for the **PULSR3 rehabilitation robot**. It is a Windows Forms application written in C# that communicates with three hardware components via serial (COM) ports to control a 2-link robotic arm. The system provides multiple modes for user rehabilitation exercises, visualizes motion in real-time, and logs session data.

## 2. Architecture & Components

### 2.1. Frontend (WinForms)
**File:** `Form1.cs`
-   **Role:** The main entry point and user interface.
-   **Responsibilities:**
    -   Initializes the `pulsr` driver.
    -   Handles user input (Start, Reset, Mode Selection).
    -   **Game Loop:** Uses a `System.Windows.Forms.Timer` (`timer_Tick`) to update the game state (angle of the orbiting target).
    -   **Rendering:** Uses GDI+ in `orbitPanelPaint` to draw the robot's path, target, and current end-effector position.
    -   **Control Implementation:** The "Assistive" mode control loop logic resides directly inside the painting loop (calculating motor speeds based on force thresholds).
    -   **Data Logging:** Writes sensor data (angles, forces, scores) to CSV files in `sessions_files/`.

### 2.2. PULSR API (Backend Logic)
**File:** `pulsrAPI.cs` (Active), `pulsr2API.cs` (Legacy/Unused)
-   **Role:** Hardware abstraction layer and kinematics engine.
-   **Namespace:** `PULSR_3`
-   **Key Class:** `pulsr`
-   **Responsibilities:**
    -   **Serial Communication:** Manages 3 `SerialPort` objects (`load_cell_coms`, `encoder_coms`, `pulsr2_coms`).
    -   **Device Identification:** Reads `usb_ser.txt` to map COM ports to specific devices (Load Cell, Encoder, Motor).
    -   **Kinematics:** precise forward kinematics calculations.
    -   **Motor Commands:** Sends byte-level commands to the motor controller (speed, direction, enable/disable).


## 3. Hardware Interfacing

 The software interfaces with 3 distinct hardware boards:

1.  **Motor Controller (`pulsr2_coms`)**
    -   **Baud Rate:** 2,000,000
    -   **Protocol:** Sends a control byte followed by two speed bytes (Upper Speed, Lower Speed).
    -   **Control Byte:** Bitmask flags likely control enable/disable (bits 1 & 3) and direction (bits 2 & 4).

2.  **Encoder Board (`encoder_coms`)**
    -   **Polling:** Sends `0` to request data.
    -   **Response:** Reads 5 bytes. 
        -   Bytes 1-2: Upper Motor Angle.
        -   Bytes 3-4: Lower Motor Angle.
    -   **Conversion:** `angle = (high_byte << 8) + low_byte`.

3.  **Load Cell Board (`load_cell_coms`)**
    -   **Polling:** Sends `1` (Upper) or `2` (Lower) to request specific sensor data.
    -   **Response:** Reads 4 bytes.
    -   **Data:** Processes bytes to construct a 16-bit force value.

## 4. Kinematics & Geometry

The robot is modeled as a 2-link arm.
-   **Parameters:**
    -   `ll` (Lower Link Length): 26
    -   `lu` (Upper Link Length): 26
    -   `le` (Effector Length): 26
    -   `offset_angle`: 20 degrees
-   **Forward Kinematics (`pulsrAPI.cs` -> `ForwardKinematics`)**:
    -   Calculates the `(e1, e2)` coordinates of the end-effector based on the joint angles `u` (upper) and `l` (lower).
    -   Uses trigonometric transformation to map these links to a Cartesian plane.
-   **Coordinate Mapping (`ComputeXY`)**:
    -   Rotates the kinematic output by the `offset_angle`.
    -   Scales the result by a factor of **25** (screen scaler) to match the UI resolution.

## 5. Operation Modes

1.  **Passive Mode (Mode 0)**
    -   **Behavior:** Motors are disabled (`Speed = 0`). The user moves the arm freely.
    -   **Feedback:** The system tracks the motion and visualizes it on screen, logging the data.

2.  **Assistive Mode (Mode 4)**
    -   **Behavior:** The robot assists or resists the user based on force input.
    -   **Logic:**
        -   Dynamic thresholds (`threshold_upper`, `threshold_lower`) are calculated based on the current angle.
        -   The system defines a "dead zone" around the desired force.
        -   If force < threshold: Motors move *towards* the direction (Assist).
        -   If force > threshold: Motors move *against* (Resist).
        -   **Target:** Keeps the user's force within a specific "tunnel".

3.  **Active Mode (Mode 8)**
    -   **Behavior:** Similar to Assistive but loads pre-defined target trajectories from text files (`Bupper_targets.txt`, `Blower_targets.txt`).
    -   **Logic:** Follows specific force/position profiles loaded from files rather than just dynamic thresholds.

