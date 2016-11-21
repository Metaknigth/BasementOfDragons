using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasementOfDragons
{
    public partial class ScoreBoard : Form
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }

        public void PopulateScoreboard(DataTable pdTable)
        {
            dgvScoreBoard.DataSource = pdTable;

        }

    }
}
