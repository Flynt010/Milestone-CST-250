namespace MineSweeperClasses
{
    public class Board
    {
        public int Size { get; set; }
        public float Difficulty { get; set; }
        public Cell[,] Cells { get; set; }
        public int RewardsRemaining { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public enum GameStatus { InProgress, Won, Lost }

        Random random = new Random();

        public Board(int size, float difficulty)
        {
            Size = size;
            Difficulty = difficulty;
            Cells = new Cell[size, size];
            RewardsRemaining = 0;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Cells[row, col] = new Cell
                    {
                        Row = row,
                        Column = col
                    };
                }
            }

            SetupBombs();
            SetupRewards();
            CalculateNumberOfBombNeighbors();
            StartTime = DateTime.Now;
        }

        public void UseSpecialBonus()
        {

        }

        public int DetermineFinalScore()
        {
            return 0;
        }

        private bool IsCellOnBoard(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        private void CalculateNumberOfBombNeighbors()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (!Cells[row, col].IsBomb)
                    {
                        Cells[row, col].NumberOfBombNeighbors = GetNumberOfBombNeighbors(row, col);
                    }
                }
            }
        }

        private int GetNumberOfBombNeighbors(int row, int col)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    if (IsCellOnBoard(neighborRow, neighborCol) && !(i == 0 && j == 0))
                    {
                        if (Cells[neighborRow, neighborCol].IsBomb)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private void SetupBombs()
        {
            int totalCells = Size * Size;
            int bombCount = (int)(totalCells * Difficulty);

            int bombsPlaced = 0;
            while (bombsPlaced < bombCount)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                if (!Cells[row, col].IsBomb)
                {
                    Cells[row, col].IsBomb = true;
                    bombsPlaced++;
                }
            }
        }

        private void SetupRewards()
        {
            RewardsRemaining = 0;
        }

        public GameStatus DetermineGameState()
        {
            return GameStatus.InProgress;
        }

    }
}
