using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    internal class Generation
    {
        private int rows;
        private int cols;

        char aliveCellChar = 'X'; // Character to represent alive cells
        char deadCellChar = '.';  // Character to represent dead cells


        //game board
        private Cell[,] gameBoard;

        //constructor creating a random board with given dimensions and number of living cells
        public Generation(int rows, int cols, int numberOfLivingCells)
        {
            this.rows = rows;
            this.cols = cols;
            gameBoard = new Cell[this.rows, this.cols];

            // Initializes a board with all cells dead.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gameBoard[i, j] = new Cell(false);
                }
            }
            InitializeRandomBoard(numberOfLivingCells);
        }

        //constructor creating a random board with given dimensions and population density
        public Generation(int rows, int cols, double populationDensity)
            : this(rows, cols, (int)Math.Round(rows * cols * populationDensity))
        {

        }

        //constructor creating a board based on a string of characters
        public Generation(string boardString)
        {
            // Assuming boardString contains only '0' and '1' characters 
            // representing empty and alive cells, respectively
            rows = boardString.Split('\n').Length - 1;
            cols = boardString.Split('\n')[0].Length;

            //Console.WriteLine(rows);
            //Console.WriteLine(cols);
            //if (boardString.Length != rows * (cols + 1))
            //{
            //    throw new ArgumentException("Invalid boardString format or size.");
            //}

            gameBoard = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gameBoard[i, j] = new Cell(boardString[i * (cols + 1) + j] == '1');
                }
            }
        }

        // Initializes a random board with a given number of living cells.
        private void InitializeRandomBoard(int livingCells)
        {
            Random random = new Random();

            for (int i = 0; i < livingCells; i++)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);
                if (gameBoard[row, col].IsAlive)
                {
                    i--;
                }
                else
                {
                    gameBoard[row, col] = new Cell(true);
                }

            }
        }


        // Simulates the game for a given number of steps.
        public Generation Next()
        {

            // Create a copy of the current board
            Generation next = new Generation(this.ToString());

            Cell[,] nextGeneration = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(i, j);

                    if (gameBoard[i, j].IsAlive)
                    {

                        // Any live cell with fewer than two live neighbors dies (underpopulation)
                        // Any live cell with two or three live neighbors lives on to the next generation
                        // Any live cell with more than three live neighbors dies (overpopulation)
                        nextGeneration[i, j] = new Cell(liveNeighbors == 2 || liveNeighbors == 3);
                    }
                    else
                    {
                        // Any dead cell with exactly three live neighbors becomes a live cell (reproduction)
                        nextGeneration[i, j] = new Cell(liveNeighbors == 3);
                    }
                }
            }
            // Update the board with the temporary values
            next.gameBoard = nextGeneration;

            return next;
        }

        // Checks if two boards are equal.
        public bool Equals(Generation other)
        {
            if (other == null || other.rows != rows || other.cols != cols)
                return false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (gameBoard[i, j].IsAlive != other.gameBoard[i, j].IsAlive)
                        return false;
                }
            }
            return true;
        }


        // Counts the number of live neighbors for a given cell
        private int CountLiveNeighbors(int row, int col)
        {
            int liveNeighbors = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < rows && j >= 0 && j < cols && !(i == row && j == col))
                    {
                        liveNeighbors += gameBoard[i, j].IsAlive ? 1 : 0;
                    }
                }
            }

            return liveNeighbors;
        }

        public override string ToString()
        {
            //Console.Clear();
            string board = "";


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board += gameBoard[i, j].IsAlive ? aliveCellChar : deadCellChar;
                }
                board += "\n";
            }

            return board;

        }

    }
}
