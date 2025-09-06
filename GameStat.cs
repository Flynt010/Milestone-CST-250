using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperGUI
{
    public class GameStat
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan GameTime { get; set; }

        public GameStat() { }

        public GameStat(int id, string playerName, int score, DateTime date, TimeSpan gameTime)
        {
            Id = id;
            PlayerName = playerName;
            Score = score;
            Date = date;
            GameTime = gameTime;
        }
    }
}
