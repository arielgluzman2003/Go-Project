using System;

namespace Go
{
    partial class MainForm
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
            this.boardPanel = new System.Windows.Forms.Panel();
            this.PlayerALabel = new System.Windows.Forms.Label();
            this.PlayerBLabel = new System.Windows.Forms.Label();
            this.PlayerAScoreLabel = new System.Windows.Forms.Label();
            this.PlayerBScoreLabel = new System.Windows.Forms.Label();
            this.scorePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.Location = new System.Drawing.Point(0, 100);
            this.boardPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(1125, 1125);
            this.boardPanel.TabIndex = 0;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            this.boardPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.boardPanel_MouseClick);
            // 
            // PlayerALabel
            // 
            this.PlayerALabel.AutoSize = true;
            this.PlayerALabel.Font = new System.Drawing.Font("Copperplate Gothic Bold", 20F);
            this.PlayerALabel.Location = new System.Drawing.Point(100, 10);
            this.PlayerALabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlayerALabel.Name = "PlayerALabel";
            this.PlayerALabel.Size = new System.Drawing.Size(147, 30);
            this.PlayerALabel.TabIndex = 1;
            this.PlayerALabel.Text = "Player 1";
            // 
            // PlayerBLabel
            // 
            this.PlayerBLabel.AutoSize = true;
            this.PlayerBLabel.Font = new System.Drawing.Font("Copperplate Gothic Bold", 20F);
            this.PlayerBLabel.Location = new System.Drawing.Point(823, 10);
            this.PlayerBLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlayerBLabel.Name = "PlayerBLabel";
            this.PlayerBLabel.Size = new System.Drawing.Size(147, 30);
            this.PlayerBLabel.TabIndex = 2;
            this.PlayerBLabel.Text = "Player 2";
            // 
            // PlayerAScoreLabel
            // 
            this.PlayerAScoreLabel.AutoSize = true;
            this.PlayerAScoreLabel.Font = new System.Drawing.Font("Copperplate Gothic Bold", 30F);
            this.PlayerAScoreLabel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.PlayerAScoreLabel.Location = new System.Drawing.Point(120, 50);
            this.PlayerAScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlayerAScoreLabel.Name = "PlayerAScoreLabel";
            this.PlayerAScoreLabel.Size = new System.Drawing.Size(48, 44);
            this.PlayerAScoreLabel.TabIndex = 2;
            this.PlayerAScoreLabel.Text = "0";
            // 
            // PlayerBScoreLabel
            // 
            this.PlayerBScoreLabel.AutoSize = true;
            this.PlayerBScoreLabel.Font = new System.Drawing.Font("Copperplate Gothic Bold", 30F);
            this.PlayerBScoreLabel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.PlayerBScoreLabel.Location = new System.Drawing.Point(843, 50);
            this.PlayerBScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlayerBScoreLabel.Name = "PlayerBScoreLabel";
            this.PlayerBScoreLabel.Size = new System.Drawing.Size(48, 44);
            this.PlayerBScoreLabel.TabIndex = 2;
            this.PlayerBScoreLabel.Text = "0";
            // 
            // scorePanel
            // 
            this.scorePanel.Location = new System.Drawing.Point(0, 0);
            this.scorePanel.Name = "scorePanel";
            this.scorePanel.Size = new System.Drawing.Size(1125, 100);
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.scorePanel_Paint);
            this.scorePanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, this.scorePanel.Height + this.boardPanel.Height);


            this.Controls.Add(this.boardPanel);

            this.Controls.Add(this.scorePanel);

            this.scorePanel.Controls.Add(this.PlayerBLabel);
            this.scorePanel.Controls.Add(this.PlayerALabel);
            this.scorePanel.Controls.Add(this.PlayerAScoreLabel);
            this.scorePanel.Controls.Add(this.PlayerBScoreLabel);

            this.Font = new System.Drawing.Font("Consolas", 10F);
            this.Icon = global::Go.Properties.Images.default_icon;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Go.. Game of the Ancients!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Label PlayerALabel;
        private System.Windows.Forms.Label PlayerBLabel;
        private System.Windows.Forms.Label PlayerAScoreLabel;
        private System.Windows.Forms.Label PlayerBScoreLabel;
        private System.Windows.Forms.Panel scorePanel;
    }
}

