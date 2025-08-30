using MineSweeperClasses;

namespace MineSweeperGUI
{
    public partial class GUIGame : Form
    {
        private Board board;
        private Button[,] buttonGrid;
        private bool rewardMode = false;

        public GUIGame()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            using (GUISetup setupForm = new GUISetup())
            {
                if (setupForm.ShowDialog() == DialogResult.OK)
                {
                    int size = setupForm.BoardSize;
                    int bombPercent = setupForm.BombPercent;

                    board = new Board(size, bombPercent / 100f);
                    CreateBoard(size);

                    board.StartTime = DateTime.Now;
                    lblStartTime.Text = board.StartTime.ToString("T");

                    lblRewards.Text = $"Rewards: {board.RewardsRemaining}";
                    scoreTimer.Start();
                }
            }
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            if (board != null)
                lblScore.Text = $"Score: {CalculateScore()}";
        }

        private void CreateBoard(int size)
        {
            panelBoard.Controls.Clear();
            buttonGrid = new Button[size, size];

            int cellSize = Math.Min(panelBoard.ClientSize.Width, panelBoard.ClientSize.Height) / size;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var btn = new Button
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Location = new Point(col * cellSize, row * cellSize),
                        Tag = new Point(row, col),
                        Margin = Padding.Empty
                    };
                    btn.MouseUp += Cell_MouseUp;
                    panelBoard.Controls.Add(btn);
                    buttonGrid[row, col] = btn;
                }
            }
        }

        private void GameOver()
        {
            foreach (Button btn in panelBoard.Controls.OfType<Button>())
                btn.Enabled = false;

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Cell cell = board.Cells[row, col];
                    if (cell.IsBomb)
                        buttonGrid[row, col].Text = "B";
                    else if (cell.HasSpecialReward)
                        buttonGrid[row, col].Text = "R"; // reveal hidden reward
                }
            }

            scoreTimer.Stop();
            MessageBox.Show("Game Over! You hit a bomb.");
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void CheckWin()
        {
            int revealed = 0;
            int totalSafe = board.Size * board.Size - board.Cells.Cast<Cell>().Count(c => c.IsBomb);

            foreach (Cell cell in board.Cells)
            {
                if (cell.IsVisited && !cell.IsBomb)
                    revealed++;
            }

            if (revealed == totalSafe)
            {
                scoreTimer.Stop();
                MessageBox.Show($"You Win! Score: {CalculateScore()}");
            }
        }

        private void Cell_MouseUp(object sender, MouseEventArgs e)
        {
            var clicked = (Button)sender;
            var pos = (Point)clicked.Tag;
            int row = pos.X, col = pos.Y;
            var cell = board.Cells[row, col];

            if (e.Button == MouseButtons.Left)
            {
                if (rewardMode)
                {
                    UseRewardOnCell(row, col);
                    rewardMode = false;
                    return;
                }

                OpenCell(row, col);
                CheckWin();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!cell.IsVisited)
                {
                    if (clicked.Text == "F") clicked.Text = "";
                    else clicked.Text = "F";
                    cell.IsFlagged = !cell.IsFlagged;
                }
            }
        }

        private void UseRewardOnCell(int row, int col)
        {
            var cell = board.Cells[row, col];

            if (!cell.IsBomb)
            {
                MessageBox.Show("No bomb here to remove!");
                return;
            }

            cell.IsBomb = false;
            board.RewardsRemaining--;
            lblRewards.Text = $"Rewards: {board.RewardsRemaining}";

            board.UpdateNumbers();

            foreach (var btn in panelBoard.Controls.OfType<Button>())
            {
                Point pos = (Point)btn.Tag;
                Cell c = board.Cells[pos.X, pos.Y];
                if (c.IsVisited && !c.IsBomb)
                    btn.Text = c.NumberOfBombNeighbors == 0 ? "" : c.NumberOfBombNeighbors.ToString();
            }

            MessageBox.Show("Bomb successfully removed!");
        }

        private void OpenCell(int row, int col)
        {
            if (row < 0 || col < 0 || row >= board.Size || col >= board.Size) return;

            var cell = board.Cells[row, col];
            var btn = buttonGrid[row, col];

            if (cell.IsVisited || cell.IsFlagged) return;

            cell.IsVisited = true;
            btn.Enabled = false;

            if (cell.IsBomb)
            {
                btn.Text = "B";
                GameOver();
                return;
            }

            if (cell.HasSpecialReward)
            {
                board.RewardsRemaining++;
                lblRewards.Text = $"Rewards: {board.RewardsRemaining}";
                cell.HasSpecialReward = false;

                btn.Text = "R";
            }
            else
            {
                btn.Text = cell.NumberOfBombNeighbors == 0 ? "" : cell.NumberOfBombNeighbors.ToString();
            }

            if (cell.NumberOfBombNeighbors == 0)
                FloodRevealZeros(row, col);
        }

        private void FloodRevealZeros(int sr, int sc)
        {
            var q = new Queue<Point>();
            q.Enqueue(new Point(sr, sc));

            while (q.Count > 0)
            {
                var p = q.Dequeue();

                for (int dr = -1; dr <= 1; dr++)
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0) continue;
                        int r = p.X + dr, c = p.Y + dc;
                        if (r < 0 || c < 0 || r >= board.Size || c >= board.Size) continue;

                        var cell = board.Cells[r, c];
                        var btn = buttonGrid[r, c];
                        if (cell.IsVisited || cell.IsFlagged) continue;
                        if (cell.IsBomb) continue;

                        cell.IsVisited = true;
                        btn.Enabled = false;
                        btn.Text = cell.NumberOfBombNeighbors == 0 ? "" : cell.NumberOfBombNeighbors.ToString();

                        if (cell.NumberOfBombNeighbors == 0)
                            q.Enqueue(new Point(r, c));
                    }
            }
        }

        private int CalculateScore()
        {
            TimeSpan elapsed = DateTime.Now - board.StartTime;

            int revealed = board.Cells.Cast<Cell>().Count(c => c.IsVisited && !c.IsBomb);
            int flaggedCorrect = board.Cells.Cast<Cell>().Count(c => c.IsFlagged && c.IsBomb);

            int baseScore = revealed * 10 + flaggedCorrect * 50;

            double multiplier = Math.Max(0.2, 1.0 - elapsed.TotalMinutes * 0.1);

            return (int)(baseScore * multiplier);
        }

        private void btnUseReward_Click(object sender, EventArgs e)
        {
            if (board.RewardsRemaining > 0)
            {
                rewardMode = true;
                MessageBox.Show("Select a cell to remove its bomb (if present).");
            }
            else
            {
                MessageBox.Show("No rewards available!");
            }
        }
    }
}
