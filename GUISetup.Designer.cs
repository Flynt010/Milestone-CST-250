namespace MineSweeperGUI
{
    partial class GUISetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            trackSize = new TrackBar();
            trackBomb = new TrackBar();
            btnPlay = new Button();
            lblSize = new Label();
            lblBomb = new Label();
            lblSummary = new Label();
            ((System.ComponentModel.ISupportInitialize)trackSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBomb).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 14);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 0;
            label1.Text = "Play MineSweeper";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 44);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 1;
            label2.Text = "Size";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 125);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 2;
            label3.Text = "Bomb Percent";
            // 
            // trackSize
            // 
            trackSize.Location = new Point(40, 62);
            trackSize.Name = "trackSize";
            trackSize.Size = new Size(202, 45);
            trackSize.TabIndex = 3;
            // 
            // trackBomb
            // 
            trackBomb.Location = new Point(40, 143);
            trackBomb.Name = "trackBomb";
            trackBomb.Size = new Size(202, 45);
            trackBomb.TabIndex = 4;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(106, 194);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 23);
            btnPlay.TabIndex = 5;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(84, 44);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(38, 15);
            lblSize.TabIndex = 6;
            lblSize.Text = "label4";
            // 
            // lblBomb
            // 
            lblBomb.AutoSize = true;
            lblBomb.Location = new Point(143, 125);
            lblBomb.Name = "lblBomb";
            lblBomb.Size = new Size(38, 15);
            lblBomb.TabIndex = 7;
            lblBomb.Text = "label4";
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Location = new Point(127, 14);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(38, 15);
            lblSummary.TabIndex = 8;
            lblSummary.Text = "label4";
            // 
            // GUISetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 233);
            Controls.Add(lblSummary);
            Controls.Add(lblBomb);
            Controls.Add(lblSize);
            Controls.Add(btnPlay);
            Controls.Add(trackBomb);
            Controls.Add(trackSize);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "GUISetup";
            Text = "GUISetup";
            ((System.ComponentModel.ISupportInitialize)trackSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBomb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TrackBar trackSize;
        private TrackBar trackBomb;
        private Button btnPlay;
        private Label lblSize;
        private Label lblBomb;
        private Label lblSummary;
    }
}