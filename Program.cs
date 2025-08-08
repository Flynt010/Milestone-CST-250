using MineSweeperClasses;

namespace MineSweeperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper");

            Board board10 = new Board(10, 0.1f);
            Console.WriteLine("Here is the answer key for the first board");
            PrintAnswers(board10);

            Board board15 = new Board(15, 0.15f);
            Console.WriteLine("Here is the answer key for the second board");
            PrintAnswers(board15);
        }

        static void PrintAnswers(Board board)
        {
            Console.Write("    ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write($"{col,3} ");
            }
            Console.WriteLine();

            // Top border to make it more like the example image
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

                    if (cell.IsBomb)
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
                        Console.ResetColor();
                        Console.Write("   ");
                    }

                    Console.ResetColor();
                }

                Console.WriteLine("|");

                // Row separator, again, like the example image
                Console.Write("    ");
                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }
        }

    }
}
