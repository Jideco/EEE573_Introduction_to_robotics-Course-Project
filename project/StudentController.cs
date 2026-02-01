using System;
using System.IO;
using System.Collections.Generic;

namespace PULSR_3
{
    /// <summary>
    /// STUDENT PROJECT - KINEMATICS & TRAJECTORY CONTROL
    /// 
    /// TASK 1: Implement Forward Kinematics
    ///         Calculate the end-effector position (X, Y) from motor angles.
    ///         
    /// TASK 2: Load and Apply Motor Trajectory
    ///         Read pre-computed motor commands from files and apply them.
    /// </summary>
    public class StudentController
    {
        // Robot Constants (Do not modify)
        private const double L1 = 26.0;   // Upper link length
        private const double L2 = 52.0;   // Lower link + effector length
        private const double SCALER = 25.0;
        private const double OFFSET_ANGLE = 20.0;

        // TASK 2: Store the loaded motor commands here
        private List<int> upperCommands;
        private List<int> lowerCommands;

        public StudentController()
        {
            // TODO: Initialize the command lists
            upperCommands = new List<int>();
            lowerCommands = new List<int>();
            
            // TODO: Call your file loading method here
            // LoadTrajectoryFiles();
        }

        /// <summary>
        /// TASK 1: Forward Kinematics
        /// Given the motor angles, calculate the end-effector screen position.
        /// Use the PULSR Parallelogram Arm Robot configuration in README.md for reference.
        /// </summary>
        /// <param name="theta1">Upper motor angle (degrees)</param>
        /// <param name="theta2">Lower motor angle (degrees)</param>
        /// <returns>Array {screenX, screenY}</returns>
        public int[] ForwardKinematics(double theta1, double theta2)
        {
            // TODO Step 1: Convert degrees to radians
            // double t1_rad = ...
            // double t2_rad = ...

            // TODO Step 2: Calculate link positions using trig
            // e2 = L1 * cos(t1) + L2 * cos(t2)
            // e1 = L1 * sin(t1) + L2 * sin(t2)
            double e2 = 0; // REPLACE
            double e1 = 0; // REPLACE

            // TODO Step 3: Apply coordinate transformation (20 degree offset)
            // x_screen =   (e1 * cos(20)) - (e2 * sin(20))
            // y_screen =  -(e2 * cos(20)) - (e1 * sin(20))
            double x_screen = 0; // REPLACE
            double y_screen = 0; // REPLACE

            // TODO Step 4: Apply scaler (multiply by SCALER)

            return new int[] { (int)x_screen, (int)y_screen };
        }

        /// <summary>
        /// TASK 2A: Load Trajectory Files
        /// Read "Nupper_targets.txt" and "Nlower_targets.txt"
        /// Parse each line as an integer and store in the lists.
        /// </summary>
        private void LoadTrajectoryFiles()
        {
            // TODO: Read "Nupper_targets.txt" 
            // - Use File.ReadAllLines() to get all lines
            // - Parse each line as int using int.TryParse()
            // - Add to upperCommands list

            // TODO: Read "Nlower_targets.txt"
            // - Same process, add to lowerCommands list

            // TODO: Print how many commands were loaded (for debugging)
            // Console.WriteLine($"Loaded {upperCommands.Count} commands");
        }

        /// <summary>
        /// TASK 2B: Get Motor Commands  
        /// Return the motor speeds for the current orbit position.
        /// </summary>
        /// <param name="robotX">Current robot X position </param>
        /// <param name="robotY">Current robot Y position </param>
        /// <param name="orbitAngle">Current target angle on the orbit (270 to -90)</param>
        /// <returns>Array {upperSpeed, lowerSpeed}</returns>
        public int[] CalculateControl(int robotX, int robotY, double orbitAngle)
        {
            // TODO Step 1: Calculate which step we are on
            // The orbit starts at 270 degrees and goes to -90 degrees
            // Step 0 = angle 270
            // Step 1 = angle 269
            // Step 360 = angle -90
            // Formula: step = (int)(270 - orbitAngle)
            int step = 0; // REPLACE

            // TODO Step 2: Make sure step is in valid range
            // if (step < 0) step = 0;
            // if (step >= upperCommands.Count) step = upperCommands.Count - 1;

            // TODO Step 3: Get the motor speeds from the loaded lists
            int speedUpper = 0; // REPLACE with upperCommands[step]
            int speedLower = 0; // REPLACE with lowerCommands[step]

            return new int[] { speedUpper, speedLower };
        }

        // Helper: Convert degrees to radians
        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
