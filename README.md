# Student Project: PULSR Kinematics & Control
## Introduction to Robotics 

---

## Objective
Implement a controller for the PULSR parallelogrma link robot to:
1. Calculate the robot's end-effector position using **Forward Kinematics**
2. Control the robot's motion by loading and applying **pre-computed motor trajectories**

---

<img width="274" height="350" alt="image" src="https://github.com/user-attachments/assets/4189c334-0921-4f2d-a358-247b7471a2f2" />

PULSR Parallelogram Arm Robot.
(*Adapted from K. P. Ayodele et al., 2023.*)



---

## Robot Parameters

| Parameter | Value | Description |
|-----------|-------|-------------|
| L1 | 26 units | Upper link length |
| L2 | 52 units | Lower link + effector length |
| SCALER | 25 | Pixels per unit |
| OFFSET_ANGLE | 20° | Physical mounting offset |

---

## Task 1: Forward Kinematics

**Goal:** Given the motor angles (θ1, θ2), calculate the end-effector position (X, Y).

### Steps:
1. Convert angles from degrees to radians
2. Apply the kinematics equations:
   ```
   (Hint: Sample Kinematics equation for a planar robot)
   e2 = La × cos(θp) + Lb × cos(θq)
   e1 = La × sin(θp) + Lb × sin(θq)
   ```
3. Apply coordinate transformation (20° rotation):
   ```
   x_screen =  (e1 × cos(20°)) - (e2 × sin(20°))
   y_screen = -(e2 × cos(20°)) - (e1 × sin(20°))
   ```
4. Scale by SCALER for screen display

### Deliverable:
Complete the `ForwardKinematics(double theta1, double theta2)` method.

---

## Task 2: Trajectory Loading and Execution 

**Goal:** Read pre-computed motor commands from files and apply them during robot operation.

### Part A: Load Trajectory Files 
Complete the `LoadTrajectoryFiles()` method to:
1. Read `Nupper_targets.txt` - contains upper motor speeds
2. Read `Nlower_targets.txt` - contains lower motor speeds
3. Parse each line as an integer
4. Store values in the provided lists

**Hints:**
- Use `File.ReadAllLines()` to read all lines from a file
- Use `int.TryParse()` to safely convert strings to integers
- Handle potential file errors with try/catch

### Part B: Apply Motor Commands 
Complete the `CalculateControl()` method to:
1. Map the current orbit angle to a step index:
   - Angle 270° = Step 0
   - Angle 269° = Step 1
   - Angle -90° = Step 360
   - Formula: `step = (int)(270 - orbitAngle)`
2. Clamp the step to valid array bounds
3. Return the corresponding motor speeds from your loaded lists

---

## Files Provided

| File | Location | Description |
|------|----------|-------------|
| StudentController.cs | Project folder | Template to complete |
| Nupper_targets.txt | bin/Debug/ | Upper motor trajectory (360 values) |
| Nlower_targets.txt | bin/Debug/ | Lower motor trajectory (360 values) |

---

## Testing

1. Open the PULSR application
2. Select **Mode 9: Student** at startup
3. Click **Start** to begin the orbit
4. Verify:
   - The **red dot** (your FK calculation) aligns with the robot position
   - The robot tracks the **green target** around the circle

---

## Grading Criteria

| Component | Points |
|-----------|--------|
| FK: Correct angle conversion | 10 |
| FK: Correct kinematics math | 15 |
| FK: Correct coordinate transform | 15 |
| Trajectory: File reading works | 15 |
| Trajectory: Proper parsing | 15 |
| Trajectory: Correct step mapping | 15 |
| Trajectory: Valid bounds checking | 15 |
| **Total** | **100** |

---

## Submission
Submit your completed `StudentController.cs` file.

Good luck!
