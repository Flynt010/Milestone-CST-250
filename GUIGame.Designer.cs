namespace MineSweeperGUI
{
    partial class GUIGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelBoard = new Panel();
            label1 = new Label();
            lblStartTime = new Label();
            label3 = new Label();
            lblScore = new Label();
            btnRestart = new Button();
            lblRewards = new Label();
            btnUseReward = new Button();
            scoreTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // panelBoard
            // 
            panelBoard.AutoSize = true;
            panelBoard.Location = new Point(12, 12);
            panelBoard.Name = "panelBoard";
            panelBoard.Size = new Size(636, 426);
            panelBoard.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(669, 39);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 1;
            label1.Text = "Start Time";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(669, 66);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(38, 15);
            lblStartTime.TabIndex = 2;
            lblStartTime.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(669, 118);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 3;
            label3.Text = "Score";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(669, 148);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(38, 15);
            lblScore.TabIndex = 4;
            lblScore.Text = "label4";
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(669, 290);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(75, 23);
            btnRestart.TabIndex = 5;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // lblRewards
            // 
            lblRewards.AutoSize = true;
            lblRewards.Location = new Point(669, 200);
            lblRewards.Name = "lblRewards";
            lblRewards.Size = new Size(38, 15);
            lblRewards.TabIndex = 6;
            lblRewards.Text = "label2";
            // 
            // btnUseReward
            // 
            btnUseReward.Location = new Point(669, 218);
            btnUseReward.Name = "btnUseReward";
            btnUseReward.Size = new Size(82, 23);
            btnUseReward.TabIndex = 7;
            btnUseReward.Text = "Use Reward";
            btnUseReward.UseVisualStyleBackColor = true;
            btnUseReward.Click += btnUseReward_Click;
            // 
            // scoreTimer
            // 
            scoreTimer.Enabled = true;
            scoreTimer.Tick += scoreTimer_Tick;
            // 
            // GUIGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUseReward);
            Controls.Add(lblRewards);
            Controls.Add(btnRestart);
            Controls.Add(lblScore);
            Controls.Add(label3);
            Controls.Add(lblStartTime);
            Controls.Add(label1);
            Controls.Add(panelBoard);
            Name = "GUIGame";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBoard;
        private Label label1;
        private Label lblStartTime;
        private Label label3;
        private Label lblScore;
        private Button btnRestart;
        private Label lblRewards;
        private Button btnUseReward;
        public System.Windows.Forms.Timer scoreTimer;
    }
}
