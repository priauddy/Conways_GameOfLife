﻿
using Conways_Game_of_Life;


Game game = GetUserInput();

game.Run();

//Game game = new Game(40, 40, 400);
//Game game = new Game(5, 5, 13);
//Game loadedGame = new Game("00100\n01110\n11111\n01110\n00100");
//Game game = new Game(3, 3, 5);
//Game loadedGame = new Game("010\n111\n001");
//Game loadedGame = new Game("10101010100101010101\n10101010100101010101\n01010101010101010101\n10101010100101010101\n" +
//    "01010101010101010101\n10101010100101010101\n01010101011010101010\n01010101010101010101\n01010101011010101010\n01010101010101010101" +
//    "10101010100101010101\n01010101011010101010\n10101010100101010101\n1010101010\n10101010100101010101\n10101010100101010101\n1010101010" +
//    "\n10101010100101010101\n01010101011010101010\n01010101010101010101\n10101010100101010101\n10101010100101010101\n01010101010101010101" +
//    "\n10101010100101010101\n01010101010101010101\n10101010100101010101\n01010101011010101010\n01010101010101010101" +
//    "\n01010101011010101010\n01010101010101010101\n10101010100101010101\n01010101011010101010\n10101010100101010101\n" +
//    "10101010100101010101\n10101010100101010101\n10101010100101010101\n10101010100101010101\n10101010100101010101" +
//    "\n01010101011010101010\n01010101010101010101");




Game GetUserInput()
{

    Console.WriteLine("Welcome to Conway's Game of Life!");
    try
    {
        Console.Write("Enter the number of rows: ");
        bool rowsBool = int.TryParse(Console.ReadLine(), out int rows);

        Console.Write("Enter the number of columns: ");
        bool colsBool = int.TryParse(Console.ReadLine(), out int cols);

        Console.Write("Enter the initial number of living cells (or density as a percentage): ");
        string input = Console.ReadLine();
        if (input.Contains("%"))
        {
            double density = double.Parse(input.TrimEnd('%')) / 100.0;
            return new Game(rows, cols, density);
        }
        else
        {
            int livingCellsCount = int.Parse(input);
            return new Game(rows, cols, livingCellsCount);
        }


    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving game state: {ex.Message}");
    }


    return new Game("10101010100101010101\n10101010100101010101\n01010101010101010101\n10101010100101010101\n" +
    "01010101010101010101\n10101010100101010101\n01010101011010101010\n01010101010101010101\n01010101011010101010\n01010101010101010101" +
    "10101010100101010101\n01010101011010101010\n10101010100101010101\n1010101010\n10101010100101010101\n10101010100101010101\n1010101010" +
    "\n10101010100101010101\n01010101011010101010\n01010101010101010101");
}

