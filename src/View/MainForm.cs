/* User Designed
 MainForm.cs
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Go
{
    public partial class MainForm : Form
    {
        private Action<Object, MouseEventArgs> clickFunction;
        private Action<object, PaintEventArgs> paintBoardFunction;
        private Action<object, PaintEventArgs> paintScoreFunction;


        public MainForm(Action<Object, MouseEventArgs> clickFunction,
            Action<object, PaintEventArgs> paintBoardFunction,
            Action<object, PaintEventArgs> paintScoreFunction)
        {
            InitializeComponent();
            this.clickFunction = clickFunction;
            this.paintBoardFunction = paintBoardFunction;
            this.paintScoreFunction = paintScoreFunction;
        }


        /*
         Event Functions
         */
        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            paintBoardFunction.Invoke(this, e);
        }
        private void scorePanel_Paint(object sender, PaintEventArgs e)
        {
            paintScoreFunction.Invoke(this, e);
        }

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            clickFunction.Invoke(sender, e);
        }
        /* end */


        /*
         Interfaces For GUI Control
         */
        public void updateScore(int scoreA, int scoreB)
        {
            this.PlayerAScoreLabel.Text = scoreA.ToString();
            this.PlayerBScoreLabel.Text = scoreB.ToString();
        }

        public void SetTurn(int id)
        {
            if (id == Go.Properties.PlayerInfo.Default.PLAYER1ID)
            {
                this.PlayerALabel.ForeColor = Color.Red;
                this.PlayerBLabel.ForeColor = Color.Green;
            }
            else if (id == Go.Properties.PlayerInfo.Default.PLAYER2ID)
            {
                this.PlayerALabel.ForeColor = Color.Green;
                this.PlayerBLabel.ForeColor = Color.Red;
            }
                
        }

        /* end */
    }
}
