# Testing Student Assignments
## Instructor Guide for PULSR Robot Control Project

---

## Overview

This document explains how to test student-submitted `StudentController.cs` files 
against the PULSR robot system to verify their implementations are correct.

---

## Prerequisites

1. PULSR robot connected and powered on
2. Visual Studio with the PULSR3 project open
3. Student's `StudentController.cs` file

---

## Step-by-Step Testing Procedure

### Step 1: Backup the Existing File

Before testing, backup the current `StudentController.cs`:

```
1. Navigate to: PULSR3\pulsr\
2. Rename StudentController.cs → StudentController_backup.cs
```

### Step 2: Add Student's File

```
1. Copy the student's submitted StudentController.cs to: PULSR3\pulsr\
2. Ensure the file has:
   - namespace PULSR_3
   - class StudentController
   - public int[] ForwardKinematics(double theta1, double theta2)
   - public int[] CalculateControl(int robotX, int robotY, double orbitAngle)
```

### Step 3: Verify Data Files Exist

Ensure these files are in the `bin\Debug\` folder:
```
- Nupper_targets.txt (360 lines of motor commands)
- Nlower_targets.txt (360 lines of motor commands)
```

### Step 4: Build the Project

```
1. In Visual Studio: Build → Build Solution (Ctrl+Shift+B)
2. Check for compilation errors:
   - If errors exist, note them for the student (partial credit)
   - If no errors, proceed to runtime testing
```

### Step 5: Run the Application

```
1. Start the application (F5 or Debug → Start Debugging)
2. When prompted for mode, select: 9 (Student Mode)
3. Click "Start" to begin the orbit test
```

---

## Evaluation Criteria

### Task 1: Forward Kinematics (40 points)

**Visual Check:**
- Look at the **RED DOT** on screen
- It should align closely with the actual robot position (Blue Rectangle in other modes)

| Result | Points | Description |
|--------|--------|-------------|
| Perfect alignment | 40 | Red dot tracks robot exactly |
| Close alignment (±20 pixels) | 30 | Minor calculation errors |
| Visible but offset | 20 | Partial implementation |
| Wrong position or missing | 10 | Significant errors |
| Code doesn't compile | 0 | Syntax errors |

**Code Review Checklist:**
- [ ] Degrees converted to radians correctly
- [ ] L1 (26) and L2 (52) used correctly
- [ ] cos/sin applied to correct angles
- [ ] 20° offset rotation applied
- [ ] SCALER (25) multiplied at the end

### Task 2: Trajectory Loading (60 points)

#### Part A: File Loading (30 points)

**Console Check:**
- Open Output window in Visual Studio (View → Output)
- Look for: `"Loaded X upper, Y lower commands"`
- X and Y should both be 360

| Result | Points | Description |
|--------|--------|-------------|
| 360 commands loaded | 30 | Files read correctly |
| Partial loading | 20 | Some parsing errors |
| Zero commands | 10 | File path wrong but code structure okay |
| Crashes on load | 5 | No error handling |
| No loading attempt | 0 | Method not implemented |

**Code Review Checklist:**
- [ ] File.ReadAllLines() or equivalent used
- [ ] int.TryParse() or int.Parse() used
- [ ] Both files read
- [ ] Values stored in lists
- [ ] Try/catch for error handling (bonus)

#### Part B: Motor Control (30 points)

**Visual Check:**
- Watch if the robot tracks the green target around the circle
- The motion should be smooth and follow the orbit path

| Result | Points | Description |
|--------|--------|-------------|
| Full orbit completed | 30 | Robot tracks target smoothly |
| Partial tracking | 20 | Works for part of orbit |
| Moves but wrong direction | 10 | Index mapping incorrect |
| No movement | 5 | Method returns zeros |
| Code doesn't compile | 0 | Syntax errors |

**Code Review Checklist:**
- [ ] Step calculated as `(int)(270 - orbitAngle)`
- [ ] Bounds checking (step >= 0 and step < count)
- [ ] Correct array indexing
- [ ] Returns array of two values


---

## Sample Grading Form

```
Student Name: _________________________
Date: _________________________________

TASK 1: Forward Kinematics          ___/40
  - Radians conversion              ___/10
  - Kinematics equations            ___/15  
  - Coordinate transform            ___/15

TASK 2A: File Loading               ___/30
  - File reading                    ___/15
  - Parsing                         ___/15

TASK 2B: Motor Control              ___/30
  - Step calculation                ___/15
  - Array indexing                  ___/15

TOTAL                               ___/100

Notes:
_________________________________________
_________________________________________
```

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| "File not found" error | Copy data files to bin\Debug\ folder |
| Robot doesn't move | Check motor connection, verify Start was clicked |
| Red dot missing | Check if ForwardKinematics returns valid array |
| Compilation error | Check namespace is PULSR_3, class is public |
| Index out of range | Student forgot bounds checking |

---

## Files Location Summary

```
PULSR3/
├── pulsr/
│   ├── StudentController.cs     ← Replace with student file
│   ├── Form1.cs
│   └── pulsrAPI.cs
├── bin/
│   └── Debug/
│       ├── Nupper_targets.txt   ← Motor data file
│       └── Nlower_targets.txt   ← Motor data file
```
