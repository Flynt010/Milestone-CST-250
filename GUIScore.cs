using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeperGUI
{
    public partial class GUIScore : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private static List<GameStat> statList = new List<GameStat>();

        public GUIScore(string name, int score, TimeSpan gameTime)
        {
            InitializeComponent();

            LoadScores();

            GameStat record = new GameStat(
                statList.Count + 1,
                name,
                score,
                DateTime.Now,
                gameTime
            );

            statList.Add(record);

            bindingSource.DataSource = statList;
            dataGridViewScores.DataSource = bindingSource;
        }

        private void SaveScores()
        {
            var lines = statList.Select(s => $"{s.Id},{s.PlayerName},{s.Score},{s.Date},{s.GameTime}");
            File.WriteAllLines("minesweeper_scores.txt", lines);
        }

        private void LoadScores()
        {
            if (File.Exists("minesweeper_scores.txt"))
            {
                var lines = File.ReadAllLines("minesweeper_scores.txt");
                statList = lines.Select(line =>
                {
                    var parts = line.Split(',');
                    return new GameStat
                    {
                        Id = int.Parse(parts[0]),
                        PlayerName = parts[1],
                        Score = int.Parse(parts[2]),
                        Date = DateTime.Parse(parts[3]),
                        GameTime = TimeSpan.Parse(parts[4])
                    };
                }).ToList();
                bindingSource.DataSource = statList;
                dataGridViewScores.DataSource = bindingSource;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScores();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadScores();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statList = statList.OrderBy(s => s.PlayerName).ToList();
            bindingSource.DataSource = statList;
            dataGridViewScores.DataSource = bindingSource;
        }

        private void byScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statList = statList.OrderByDescending(s => s.Score).ToList();
            bindingSource.DataSource = statList;
            dataGridViewScores.DataSource = bindingSource;
        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statList = statList.OrderByDescending(s => s.Date).ToList();
            bindingSource.DataSource = statList;
            dataGridViewScores.DataSource = bindingSource;
        }
    }
}
