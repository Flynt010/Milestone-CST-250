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
    public partial class GUISetup : Form
    {
        public GUISetup()
        {
            InitializeComponent();

            trackSize.Minimum = 6; trackSize.Maximum = 20; trackSize.Value = 10; trackSize.TickFrequency = 1;
            trackBomb.Minimum = 5; trackBomb.Maximum = 40; trackBomb.Value = 15; trackBomb.TickFrequency = 5;

            trackSize.ValueChanged += (_, __) => UpdatePreview();
            trackBomb.ValueChanged += (_, __) => UpdatePreview();
            UpdatePreview();
        }

        public int BoardSize { get; private set; }
        public int BombPercent { get; private set; }

        private void UpdatePreview()
        {
            int n = trackSize.Value;
            int pct = trackBomb.Value;
            int bombs = (int)Math.Round(n * n * (pct / 100.0));

            lblSize.Text = $"{n} × {n}";
            lblBomb.Text = $"{pct}%";
            lblSummary.Text = $"Board: {n}×{n}  |  Bombs: ~{bombs}";
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            BoardSize = trackSize.Value;
            BombPercent = trackBomb.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
