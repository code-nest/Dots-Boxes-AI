namespace AI
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gameSpeed = new System.Windows.Forms.ProgressBar();
            this.turnStatus = new System.Windows.Forms.Label();
            this.rbPlayerAndRandom = new System.Windows.Forms.RadioButton();
            this.scoreCom = new System.Windows.Forms.Label();
            this.scorePlayer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbPlayerAndAi1 = new System.Windows.Forms.RadioButton();
            this.rbAi2AndAi1 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.gameSpeed);
            this.groupBox1.Controls.Add(this.turnStatus);
            this.groupBox1.Controls.Add(this.rbPlayerAndRandom);
            this.groupBox1.Controls.Add(this.scoreCom);
            this.groupBox1.Controls.Add(this.scorePlayer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbPlayerAndAi1);
            this.groupBox1.Controls.Add(this.rbAi2AndAi1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(398, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 403);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(8, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Game Speed:";
            // 
            // gameSpeed
            // 
            this.gameSpeed.Location = new System.Drawing.Point(90, 89);
            this.gameSpeed.Maximum = 700;
            this.gameSpeed.Minimum = 50;
            this.gameSpeed.Name = "gameSpeed";
            this.gameSpeed.Size = new System.Drawing.Size(86, 16);
            this.gameSpeed.TabIndex = 17;
            this.gameSpeed.Value = 50;
            this.gameSpeed.Click += new System.EventHandler(this.gameSpeed_Click);
            // 
            // turnStatus
            // 
            this.turnStatus.AutoSize = true;
            this.turnStatus.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnStatus.ForeColor = System.Drawing.Color.Red;
            this.turnStatus.Location = new System.Drawing.Point(73, 140);
            this.turnStatus.Name = "turnStatus";
            this.turnStatus.Size = new System.Drawing.Size(49, 22);
            this.turnStatus.TabIndex = 16;
            this.turnStatus.Text = "Turn";
            // 
            // rbPlayerAndRandom
            // 
            this.rbPlayerAndRandom.AutoSize = true;
            this.rbPlayerAndRandom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.rbPlayerAndRandom.Location = new System.Drawing.Point(80, 56);
            this.rbPlayerAndRandom.Name = "rbPlayerAndRandom";
            this.rbPlayerAndRandom.Size = new System.Drawing.Size(115, 19);
            this.rbPlayerAndRandom.TabIndex = 15;
            this.rbPlayerAndRandom.Text = "Player / Random";
            this.rbPlayerAndRandom.UseVisualStyleBackColor = true;
            // 
            // scoreCom
            // 
            this.scoreCom.AutoSize = true;
            this.scoreCom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.scoreCom.ForeColor = System.Drawing.Color.Blue;
            this.scoreCom.Location = new System.Drawing.Point(136, 120);
            this.scoreCom.Name = "scoreCom";
            this.scoreCom.Size = new System.Drawing.Size(14, 15);
            this.scoreCom.TabIndex = 14;
            this.scoreCom.Text = "0";
            // 
            // scorePlayer
            // 
            this.scorePlayer.AutoSize = true;
            this.scorePlayer.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.scorePlayer.ForeColor = System.Drawing.Color.DarkRed;
            this.scorePlayer.Location = new System.Drawing.Point(95, 120);
            this.scorePlayer.Name = "scorePlayer";
            this.scorePlayer.Size = new System.Drawing.Size(14, 15);
            this.scorePlayer.TabIndex = 13;
            this.scorePlayer.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(28, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Scores:";
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(3, 198);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(194, 202);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Game Mode:";
            // 
            // rbPlayerAndAi1
            // 
            this.rbPlayerAndAi1.AutoSize = true;
            this.rbPlayerAndAi1.Checked = true;
            this.rbPlayerAndAi1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.rbPlayerAndAi1.Location = new System.Drawing.Point(80, 32);
            this.rbPlayerAndAi1.Name = "rbPlayerAndAi1";
            this.rbPlayerAndAi1.Size = new System.Drawing.Size(90, 19);
            this.rbPlayerAndAi1.TabIndex = 4;
            this.rbPlayerAndAi1.TabStop = true;
            this.rbPlayerAndAi1.Text = "Player / Ai1";
            this.rbPlayerAndAi1.UseVisualStyleBackColor = true;
            // 
            // rbAi2AndAi1
            // 
            this.rbAi2AndAi1.AutoSize = true;
            this.rbAi2AndAi1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAi2AndAi1.Location = new System.Drawing.Point(80, 9);
            this.rbAi2AndAi1.Name = "rbAi2AndAi1";
            this.rbAi2AndAi1.Size = new System.Drawing.Size(74, 19);
            this.rbAi2AndAi1.TabIndex = 3;
            this.rbAi2AndAi1.Text = "Ai2 / Ai1";
            this.rbAi2AndAi1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(37, 167);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 25);
            this.button2.TabIndex = 0;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(98, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 25);
            this.button1.TabIndex = 10;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dots & Boxs";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton rbAi2AndAi1;
        private System.Windows.Forms.RadioButton rbPlayerAndAi1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label scoreCom;
        private System.Windows.Forms.Label scorePlayer;
        private System.Windows.Forms.RadioButton rbPlayerAndRandom;
        private System.Windows.Forms.Label turnStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar gameSpeed;
    }
}

