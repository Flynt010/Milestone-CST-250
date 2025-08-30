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

        private readonly Random random = new Random();

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
                for (int col = 0; col < Size; col++)
                    Cells[row, col] = new Cell { Row = row, Column = col };

            SetupBombs();
            SetupRewards();
            UpdateNumbers();
            StartTime = DateTime.Now;
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
            int safeCells = Size * Size - (int)(Size * Size * Difficulty);
            int rewardsToPlace = Math.Max(1, safeCells / 100);

            RewardsRemaining = rewardsToPlace;

            while (rewardsToPlace > 0)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                if (!Cells[row, col].IsBomb && !Cells[row, col].HasSpecialReward)
                {
                    Cells[row, col].HasSpecialReward = true;
                    rewardsToPlace--;
                }
            }
        }

        public bool IsCellOnBoard(int row, int col) =>
            row >= 0 && row < Size && col >= 0 && col < Size;

        public void UpdateNumbers()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Cells[row, col].IsBomb)
                    {
                        Cells[row, col].NumberOfBombNeighbors = -1;
                        continue;
                    }

                    int count = 0;
                    for (int dr = -1; dr <= 1; dr++)
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            if (dr == 0 && dc == 0) continue;
                            int nr = row + dr, nc = col + dc;
                            if (IsCellOnBoard(nr, nc) && Cells[nr, nc].IsBomb)
                                count++;
                        }
                    Cells[row, col].NumberOfBombNeighbors = count;
                }
            }
        }

        public GameStatus DetermineGameState()
        {
            bool allNonBombsRevealed = true;

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Cell cell = Cells[r, c];

                    if (cell.IsVisited && cell.IsBomb)
                    {
                        EndTime = DateTime.Now;
                        return GameStatus.Lost;
                    }

                    if (!cell.IsBomb && !cell.IsVisited)
                        allNonBombsRevealed = false;
                }
            }

            if (allNonBombsRevealed)
            {
                EndTime = DateTime.Now;
                return GameStatus.Won;
            }

            return GameStatus.InProgress;
        }
    }
}
