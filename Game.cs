using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    internal class Game
    {
        private Generation currentGeneration;
        private bool isPaused = false;
        private int remainingSteps = 0; // New member to store remaining steps

        // Constructor for generating a game with a given population density.
        public Game(int rows, int cols, double populationDensity)
        {
            currentGeneration = new Generation(rows, cols, populationDensity);
        }

        public Game(int rows, int cols, int numOfLivingCells)
        {
            currentGeneration = new Generation(rows, cols, numOfLivingCells);
        }

        // Constructor for loading a game from a string representation of the board.
        public Game(string boardString)
        {
            currentGeneration = new Generation(boardString);
        }


        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Conway's Game of Life");
                Console.WriteLine("1. Start New Simulation");
                Console.WriteLine("2. Load Simulation from File");
                Console.WriteLine("3. Pause Simulation");
                Console.WriteLine("4. Resume Simulation");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        StartSimulation();
                        break;
                    case "2":
                        LoadGameFromFile();
                        break;
                    case "3":
                        PauseSimulation();
                        break;
                    case "4":
                        ResumeSimulation();
                        break;
                    case "5":
                        SaveToFile("saved_board.txt");
                        return; // Exit the application
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void StartSimulation()
        {
            // Start or restart the simulation

            Console.Write("Enter the number of simulation steps: ");
            int steps = int.Parse(Console.ReadLine());

            Simulate(steps);
            Console.ReadKey();
        }

        private void PauseSimulation()
        {
            isPaused = true;
            Console.WriteLine("Simulation paused. Press any key to continue.");
            Console.ReadKey();
            // Remaining steps are already updated in the Simulate method
        }

        private void ResumeSimulation()
        {
            isPaused = false;
            Console.WriteLine("Resuming simulation...");
            Simulate(remainingSteps); // Continue the simulation with the remaining steps
        }

        // Simulates the game for a given number of steps.
        public void Simulate(int steps)
        {
            remainingSteps = steps; // Set the initial step count

            // CancellationTokenSource is used to signal cancellation from the keyPressTask.
            var cancellationTokenSource = new CancellationTokenSource();

            // keyPressTask listens for a key press. Once a key is pressed, the cancellation token is triggered.
            var keyPressTask = Task.Run(() =>
            {
                Console.ReadKey();
                cancellationTokenSource.Cancel();
            });

            for (int i = 0; i < steps; i++)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    remainingSteps -= i; // Updating remaining steps when paused
                    PauseSimulation();
                    break; // Exit the loop if a key is pressed
                }

                Console.Clear();
                Console.WriteLine(currentGeneration.ToString());
                currentGeneration = currentGeneration.Next();
                System.Threading.Thread.Sleep(400);
            }

            if (!keyPressTask.IsCompleted)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true); // Wait for any additional key press before returning to the menu
            }

        }
        private void LoadGameFromFile()
        {
            Console.Write("Enter the file name to load the game from: ");
            string fileName = Console.ReadLine();

            try
            {
                LoadFromFile(fileName);
                Console.WriteLine("Game loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game from file: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }



        // Saves the current game state to a file.
        public void SaveToFile(string fileName)
        {
            try
            {
                System.IO.File.WriteAllText(fileName, currentGeneration.ToString());
                Console.WriteLine("Game state saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game state: {ex.Message}");
            }

        }

        // Loads the game state from a file.
        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found.");
                return;
            }
            string fileContent = System.IO.File.ReadAllText(fileName);
            string boardString = ConvertFileContentToBoardString(fileContent);
            currentGeneration = new Generation(boardString);
        }

        // converts the '■' and ' ' characters in fileContent to '1' and '0', respectively.
        // This format is compatible with the Generation constructor.
        private string ConvertFileContentToBoardString(string fileContent)
        {
            return fileContent.Replace('■', '1').Replace(' ', '0');
        }
    }

}
