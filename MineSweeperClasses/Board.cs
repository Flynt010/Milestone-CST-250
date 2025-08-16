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

            // Set up your bombs, rewards, and bomb neighbors
            SetupBombs();
            SetupRewards();
            CalculateNumberOfBombNeighbors();
            StartTime = DateTime.Now;
        }

        // I can't remember what this was for but it was in the tutorial
        // So i ain't touching it
        public void UseSpecialBonus()
        {

        }

        // We don't care about score yet
        public int DetermineFinalScore()
        {
            return 0;
        }

        public bool IsCellOnBoard(int row, int col)
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
            // So say you had 0.15 difficulty, its just 15% of the tiles
            // Are bombs

            // Just put the bombs in the bag bro
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
            // How many rewards it can place on the board
            // Self explanatory
            int rewardsToPlace = 1;
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

        public GameStatus DetermineGameState()
        {
            bool allNonBombsRevealed = true;

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Cell cell = Cells[r, c];

                    // You hit a bomb
                    // Stupid
                    if (cell.IsVisited && cell.IsBomb)
                        return GameStatus.Lost;

                    if (!cell.IsBomb && !cell.IsVisited)
                        allNonBombsRevealed = false;
                }
            }

            // You didn't hit a bomb
            // Hoorah
            if (allNonBombsRevealed)
                return GameStatus.Won;

            // You still have spots to mark
            // Stupid
            return GameStatus.InProgress;
        }

        public void RevealAdjacentZeros(int row, int col)
        {
            // Safety checks
            if (!IsCellOnBoard(row, col)) return;

            Cell cell = Cells[row, col];

            if (cell.IsVisited || cell.IsBomb || cell.IsFlagged) return;

            cell.IsVisited = true;

            // If this cell has neighbors with bombs, stop here
            if (cell.NumberOfBombNeighbors > 0) return;

            // Otherwise, visit all surrounding cells
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue; // Skip self
                    RevealAdjacentZeros(row + dr, col + dc);
                }
            }
        }
    }
}
