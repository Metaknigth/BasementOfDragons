namespace BasementOfDragons
{
    partial class BasementOfDragons
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
            this.rtxtMap = new System.Windows.Forms.RichTextBox();
            this.lblPlayerHP = new System.Windows.Forms.Label();
            this.txtDragonsHP = new System.Windows.Forms.TextBox();
            this.lblKillCount = new System.Windows.Forms.Label();
            this.grpPlayer = new System.Windows.Forms.GroupBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblMoveCount = new System.Windows.Forms.Label();
            this.grpDragons = new System.Windows.Forms.GroupBox();
            this.txtTextLog = new System.Windows.Forms.TextBox();
            this.btnScoreboard = new System.Windows.Forms.Button();
            this.grpPlayer.SuspendLayout();
            this.grpDragons.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtMap
            // 
            this.rtxtMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxtMap.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtxtMap.DetectUrls = false;
            this.rtxtMap.Enabled = false;
            this.rtxtMap.Location = new System.Drawing.Point(12, 12);
            this.rtxtMap.Name = "rtxtMap";
            this.rtxtMap.ReadOnly = true;
            this.rtxtMap.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtxtMap.Size = new System.Drawing.Size(205, 300);
            this.rtxtMap.TabIndex = 0;
            this.rtxtMap.Text = "";
            // 
            // lblPlayerHP
            // 
            this.lblPlayerHP.AutoSize = true;
            this.lblPlayerHP.Location = new System.Drawing.Point(6, 15);
            this.lblPlayerHP.Name = "lblPlayerHP";
            this.lblPlayerHP.Size = new System.Drawing.Size(41, 13);
            this.lblPlayerHP.TabIndex = 1;
            this.lblPlayerHP.Text = "Health:";
            // 
            // txtDragonsHP
            // 
            this.txtDragonsHP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDragonsHP.Enabled = false;
            this.txtDragonsHP.Location = new System.Drawing.Point(6, 15);
            this.txtDragonsHP.Multiline = true;
            this.txtDragonsHP.Name = "txtDragonsHP";
            this.txtDragonsHP.ReadOnly = true;
            this.txtDragonsHP.Size = new System.Drawing.Size(99, 79);
            this.txtDragonsHP.TabIndex = 4;
            this.txtDragonsHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblKillCount
            // 
            this.lblKillCount.AutoSize = true;
            this.lblKillCount.Location = new System.Drawing.Point(6, 37);
            this.lblKillCount.Name = "lblKillCount";
            this.lblKillCount.Size = new System.Drawing.Size(48, 13);
            this.lblKillCount.TabIndex = 5;
            this.lblKillCount.Text = "KillCount";
            // 
            // grpPlayer
            // 
            this.grpPlayer.BackColor = System.Drawing.SystemColors.Control;
            this.grpPlayer.Controls.Add(this.lblScore);
            this.grpPlayer.Controls.Add(this.lblMoveCount);
            this.grpPlayer.Controls.Add(this.lblKillCount);
            this.grpPlayer.Controls.Add(this.lblPlayerHP);
            this.grpPlayer.Location = new System.Drawing.Point(235, 18);
            this.grpPlayer.Name = "grpPlayer";
            this.grpPlayer.Size = new System.Drawing.Size(111, 100);
            this.grpPlayer.TabIndex = 7;
            this.grpPlayer.TabStop = false;
            this.grpPlayer.Text = "Player:";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(6, 81);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 10;
            this.lblScore.Text = "Score:";
            // 
            // lblMoveCount
            // 
            this.lblMoveCount.AutoSize = true;
            this.lblMoveCount.Location = new System.Drawing.Point(6, 59);
            this.lblMoveCount.Name = "lblMoveCount";
            this.lblMoveCount.Size = new System.Drawing.Size(62, 13);
            this.lblMoveCount.TabIndex = 6;
            this.lblMoveCount.Text = "MoveCount";
            // 
            // grpDragons
            // 
            this.grpDragons.BackColor = System.Drawing.SystemColors.Control;
            this.grpDragons.Controls.Add(this.txtDragonsHP);
            this.grpDragons.Location = new System.Drawing.Point(361, 18);
            this.grpDragons.Name = "grpDragons";
            this.grpDragons.Size = new System.Drawing.Size(111, 100);
            this.grpDragons.TabIndex = 8;
            this.grpDragons.TabStop = false;
            this.grpDragons.Text = "Dragons:";
            // 
            // txtTextLog
            // 
            this.txtTextLog.Enabled = false;
            this.txtTextLog.Location = new System.Drawing.Point(235, 133);
            this.txtTextLog.Multiline = true;
            this.txtTextLog.Name = "txtTextLog";
            this.txtTextLog.ReadOnly = true;
            this.txtTextLog.Size = new System.Drawing.Size(231, 50);
            this.txtTextLog.TabIndex = 9;
            // 
            // btnScoreboard
            // 
            this.btnScoreboard.Enabled = false;
            this.btnScoreboard.Location = new System.Drawing.Point(305, 287);
            this.btnScoreboard.Name = "btnScoreboard";
            this.btnScoreboard.Size = new System.Drawing.Size(97, 23);
            this.btnScoreboard.TabIndex = 10;
            this.btnScoreboard.Text = "View Scoreboard";
            this.btnScoreboard.UseVisualStyleBackColor = true;
            this.btnScoreboard.Click += new System.EventHandler(this.btnScoreboard_Click);
            // 
            // BasementOfDragons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(484, 322);
            this.Controls.Add(this.btnScoreboard);
            this.Controls.Add(this.txtTextLog);
            this.Controls.Add(this.grpDragons);
            this.Controls.Add(this.grpPlayer);
            this.Controls.Add(this.rtxtMap);
            this.MinimumSize = new System.Drawing.Size(500, 360);
            this.Name = "BasementOfDragons";
            this.Text = "Basement Of Dragons";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.grpPlayer.ResumeLayout(false);
            this.grpPlayer.PerformLayout();
            this.grpDragons.ResumeLayout(false);
            this.grpDragons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlayerHP;
        private System.Windows.Forms.TextBox txtDragonsHP;
        private System.Windows.Forms.Label lblKillCount;
        private System.Windows.Forms.GroupBox grpPlayer;
        private System.Windows.Forms.GroupBox grpDragons;
        private System.Windows.Forms.TextBox txtTextLog;
        private System.Windows.Forms.Label lblMoveCount;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Button btnScoreboard;
        private System.Windows.Forms.RichTextBox rtxtMap;
    }
}

