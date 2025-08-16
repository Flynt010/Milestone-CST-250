using MineSweeperClasses;
using static MineSweeperClasses.Board;

namespace MineSweeperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper");

            Board board = new Board(8, 0.10f);
            Console.WriteLine("Here is the answer key for the board");
            PrintAnswers(board);

            bool victory = false;
            bool death = false;
            bool rewardAvailable = false;

            while (!victory && !death)
            {
                PrintBoard(board);

                Console.Write("Enter row: ");
                int row = int.Parse(Console.ReadLine());

                Console.Write("Enter col: ");
                int col = int.Parse(Console.ReadLine());

                // Had to split this into two because you can either have the
                // reward or not, so it selects the option whether or not you
                // have the reward and uses the one with the Use Reward option
                string options = rewardAvailable ? "Flag / Visit / Use Reward" : "Flag / Visit";
                Console.Write($"Choose action ({options}): ");
                string action = Console.ReadLine().ToLower();

                Cell chosenCell = board.Cells[row, col];

                // Flags the tile if chosen tile isn't flagged yet
                if (action == "flag")
                {
                    chosenCell.IsFlagged = !chosenCell.IsFlagged;
                }
                // Had to implement a method of showing the nearby zeros like in
                // normal minesweeper, thats what RevealAdjascentZeros is about
                else if (action == "visit")
                {
                    // If the tile isn't one that has a number on it or isn't a bomb
                    // Reveal itself and all other zeros it's connected to
                    if (chosenCell.NumberOfBombNeighbors == 0 && !chosenCell.IsBomb)
                    {
                        board.RevealAdjacentZeros(row, col);
                    }
                    else
                    {
                        chosenCell.IsVisited = true;
                    }

                    // If the tile is the reward, you grab the bomb and sets the
                    // reward to true and makes it so the tile isn't a reward tile anymore
                    if (chosenCell.HasSpecialReward)
                    {
                        Console.WriteLine("You've found a bomb defuse kit!");
                        rewardAvailable = true;
                        chosenCell.HasSpecialReward = false;
                    }
                }
                else if (action == "use reward" && rewardAvailable)
                {
                    // If you have the reward, checks the spot and determines whether or not
                    // it's a bomb, turns the reward off once used
                    Console.WriteLine($"Is it a bomb at ({row}, {col})? " + board.Cells[row, col].IsBomb);
                    rewardAvailable = false;
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }

                var gameState = board.DetermineGameState();
                if (gameState == GameStatus.Won) victory = true;
                if (gameState == GameStatus.Lost) death = true;
            }

            Console.WriteLine(victory ? "You win!" : "You hit a bomb! Game over.");

        }

        static void PrintAnswers(Board board)
        {
            Console.Write("    ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write($"{col,3} ");
            }
            Console.WriteLine();

            Console.Write("    ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write($"{row,3} ");

                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write("|");
                    var cell = board.Cells[row, col];

                    if (cell.HasSpecialReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" R ");
                    }
                    else if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" B ");
                    }
                    else if (cell.NumberOfBombNeighbors > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.ResetColor();
                }

                Console.WriteLine("|");

                Console.Write("    ");
                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }
        }

        static void PrintBoard(Board board)
        {
            // Had to copy the formatting of the board, so most of this is the same
            Console.Write("    ");
            for (int col = 0; col < board.Size; col++)
                Console.Write($"{col,3} ");
            Console.WriteLine();

            Console.Write("    ");
            for (int col = 0; col < board.Size; col++)
                Console.Write("+---");
            Console.WriteLine("+");

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write($"{row,3} ");

                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write("|");
                    var cell = board.Cells[row, col];

                    if (!cell.IsVisited && !cell.IsFlagged)
                    {
                        Console.Write(" ? ");
                    }
                    else if (cell.IsFlagged)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(" F ");
                        Console.ResetColor();
                    }
                    else if (cell.HasSpecialReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" R ");
                        Console.ResetColor();
                    }
                    else if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" B ");
                        Console.ResetColor();
                    }
                    else if (cell.NumberOfBombNeighbors > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }

                Console.WriteLine("|");
                Console.Write("    ");
                for (int col = 0; col < board.Size; col++)
                    Console.Write("+---");
                Console.WriteLine("+");
            }
        }
    }
}
