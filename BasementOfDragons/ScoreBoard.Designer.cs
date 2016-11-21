namespace BasementOfDragons
{
    partial class ScoreBoard
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
            this.dgvScoreBoard = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoreBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvScoreBoard
            // 
            this.dgvScoreBoard.AllowUserToAddRows = false;
            this.dgvScoreBoard.AllowUserToDeleteRows = false;
            this.dgvScoreBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScoreBoard.Location = new System.Drawing.Point(12, 12);
            this.dgvScoreBoard.Name = "dgvScoreBoard";
            this.dgvScoreBoard.ReadOnly = true;
            this.dgvScoreBoard.Size = new System.Drawing.Size(337, 240);
            this.dgvScoreBoard.TabIndex = 0;
            // 
            // ScoreBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(361, 264);
            this.Controls.Add(this.dgvScoreBoard);
            this.Name = "ScoreBoard";
            this.Text = "ScoreBoard";
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoreBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvScoreBoard;
    }
}