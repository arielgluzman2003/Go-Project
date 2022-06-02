namespace Go.src.View
{
    partial class Settings
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
            this.ChooseBoardGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NineteenByNineteenRadioButton = new System.Windows.Forms.RadioButton();
            this.ThirteenByThirteenRadioButton = new System.Windows.Forms.RadioButton();
            this.NineByNineRadioButton = new System.Windows.Forms.RadioButton();
            this.ChooseModeGroupBox = new System.Windows.Forms.GroupBox();
            this.PlayAgainstComputerRadioButton = new System.Windows.Forms.RadioButton();
            this.OneVsOneRadioButton = new System.Windows.Forms.RadioButton();
            this.ConfirmSettingsButton = new System.Windows.Forms.Button();
            this.ChooseBoardGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ChooseModeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseBoardGroupBox
            // 
            this.ChooseBoardGroupBox.Controls.Add(this.pictureBox3);
            this.ChooseBoardGroupBox.Controls.Add(this.pictureBox2);
            this.ChooseBoardGroupBox.Controls.Add(this.pictureBox1);
            this.ChooseBoardGroupBox.Controls.Add(this.NineteenByNineteenRadioButton);
            this.ChooseBoardGroupBox.Controls.Add(this.ThirteenByThirteenRadioButton);
            this.ChooseBoardGroupBox.Controls.Add(this.NineByNineRadioButton);
            this.ChooseBoardGroupBox.Location = new System.Drawing.Point(30, 30);
            this.ChooseBoardGroupBox.Name = "ChooseBoardGroupBox";
            this.ChooseBoardGroupBox.Size = new System.Drawing.Size(700, 360);
            this.ChooseBoardGroupBox.TabIndex = 0;
            this.ChooseBoardGroupBox.TabStop = false;
            this.ChooseBoardGroupBox.Text = "Choose Board";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Go.Properties.Resources._19X19_Preview;
            this.pictureBox3.Location = new System.Drawing.Point(471, 85);
            this.pictureBox3.Name = "19X19PreviewPictureBox";
            this.pictureBox3.Size = new System.Drawing.Size(200, 200);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Go.Properties.Resources._13X13_Preview;
            this.pictureBox2.Location = new System.Drawing.Point(247, 85);
            this.pictureBox2.Name = "13X13PreviewPictureBox";
            this.pictureBox2.Size = new System.Drawing.Size(200, 200);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Go.Properties.Resources._9X9_Preview;
            this.pictureBox1.Location = new System.Drawing.Point(22, 85);
            this.pictureBox1.Name = "9X9PreviewPictureBox";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // NineteenByNineteenRadioButton
            // 
            this.NineteenByNineteenRadioButton.AutoSize = true;
            this.NineteenByNineteenRadioButton.Location = new System.Drawing.Point(546, 321);
            this.NineteenByNineteenRadioButton.Name = "NineteenByNineteenRadioButton";
            this.NineteenByNineteenRadioButton.Size = new System.Drawing.Size(56, 17);
            this.NineteenByNineteenRadioButton.TabIndex = 2;
            this.NineteenByNineteenRadioButton.TabStop = true;
            this.NineteenByNineteenRadioButton.Text = "19X19";
            this.NineteenByNineteenRadioButton.UseVisualStyleBackColor = true;
            // 
            // ThirteenByThirteenRadioButton
            // 
            this.ThirteenByThirteenRadioButton.AutoSize = true;
            this.ThirteenByThirteenRadioButton.Location = new System.Drawing.Point(314, 321);
            this.ThirteenByThirteenRadioButton.Name = "ThirteenByThirteenRadioButton";
            this.ThirteenByThirteenRadioButton.Size = new System.Drawing.Size(56, 17);
            this.ThirteenByThirteenRadioButton.TabIndex = 1;
            this.ThirteenByThirteenRadioButton.TabStop = true;
            this.ThirteenByThirteenRadioButton.Text = "13X13";
            this.ThirteenByThirteenRadioButton.UseVisualStyleBackColor = true;
            // 
            // NineByNineRadioButton
            // 
            this.NineByNineRadioButton.AutoSize = true;
            this.NineByNineRadioButton.Checked = true;
            this.NineByNineRadioButton.Location = new System.Drawing.Point(99, 321);
            this.NineByNineRadioButton.Name = "NineByNineRadioButton";
            this.NineByNineRadioButton.Size = new System.Drawing.Size(44, 17);
            this.NineByNineRadioButton.TabIndex = 0;
            this.NineByNineRadioButton.TabStop = true;
            this.NineByNineRadioButton.Text = "9X9";
            this.NineByNineRadioButton.UseVisualStyleBackColor = true;
            // 
            // ChooseModeGroupBox
            // 
            this.ChooseModeGroupBox.Controls.Add(this.PlayAgainstComputerRadioButton);
            this.ChooseModeGroupBox.Controls.Add(this.OneVsOneRadioButton);
            this.ChooseModeGroupBox.Location = new System.Drawing.Point(30, 411);
            this.ChooseModeGroupBox.Name = "ChooseModeGroupBox";
            this.ChooseModeGroupBox.Size = new System.Drawing.Size(700, 77);
            this.ChooseModeGroupBox.TabIndex = 1;
            this.ChooseModeGroupBox.TabStop = false;
            this.ChooseModeGroupBox.Text = "Choose Mode";
            // 
            // PlayAgainstComputerRadioButton
            // 
            this.PlayAgainstComputerRadioButton.AutoSize = true;
            this.PlayAgainstComputerRadioButton.Location = new System.Drawing.Point(471, 36);
            this.PlayAgainstComputerRadioButton.Name = "PlayAgainstComputerRadioButton";
            this.PlayAgainstComputerRadioButton.Size = new System.Drawing.Size(131, 17);
            this.PlayAgainstComputerRadioButton.TabIndex = 1;
            this.PlayAgainstComputerRadioButton.TabStop = true;
            this.PlayAgainstComputerRadioButton.Text = "Play Against Computer";
            this.PlayAgainstComputerRadioButton.UseVisualStyleBackColor = true;
            // 
            // OneVsOneRadioButton
            // 
            this.OneVsOneRadioButton.AutoSize = true;
            this.OneVsOneRadioButton.Checked = true;
            this.OneVsOneRadioButton.Location = new System.Drawing.Point(145, 36);
            this.OneVsOneRadioButton.Name = "OneVsOneRadioButton";
            this.OneVsOneRadioButton.Size = new System.Drawing.Size(43, 17);
            this.OneVsOneRadioButton.TabIndex = 0;
            this.OneVsOneRadioButton.TabStop = true;
            this.OneVsOneRadioButton.Text = "1v1";
            this.OneVsOneRadioButton.UseVisualStyleBackColor = true;
            // 
            // ConfirmSettingsButton
            // 
            this.ConfirmSettingsButton.Font = new System.Drawing.Font("Verdana", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmSettingsButton.Location = new System.Drawing.Point(246, 529);
            this.ConfirmSettingsButton.Name = "ConfirmSettingsButton";
            this.ConfirmSettingsButton.Size = new System.Drawing.Size(240, 41);
            this.ConfirmSettingsButton.TabIndex = 2;
            this.ConfirmSettingsButton.Text = "Confirm Settings";
            this.ConfirmSettingsButton.UseVisualStyleBackColor = true;
            this.ConfirmSettingsButton.Click += new System.EventHandler(this.ConfirmSettingsClicked);
            // 
            // Settings
            // 
            this.Icon = global::Go.Properties.Images.default_icon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 602);
            this.Controls.Add(this.ConfirmSettingsButton);
            this.Controls.Add(this.ChooseModeGroupBox);
            this.Controls.Add(this.ChooseBoardGroupBox);
            this.Name = "Settings";
            this.Text = "Choose Game Settings";
            this.ChooseBoardGroupBox.ResumeLayout(false);
            this.ChooseBoardGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ChooseModeGroupBox.ResumeLayout(false);
            this.ChooseModeGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ChooseBoardGroupBox;
        private System.Windows.Forms.RadioButton NineteenByNineteenRadioButton;
        private System.Windows.Forms.RadioButton ThirteenByThirteenRadioButton;
        private System.Windows.Forms.RadioButton NineByNineRadioButton;
        private System.Windows.Forms.GroupBox ChooseModeGroupBox;
        private System.Windows.Forms.RadioButton PlayAgainstComputerRadioButton;
        private System.Windows.Forms.RadioButton OneVsOneRadioButton;
        private System.Windows.Forms.Button ConfirmSettingsButton;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}