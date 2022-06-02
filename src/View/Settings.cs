/* User Designed
 Settingcs.cs
 */

using Go.src.Control;
using System;
using System.Windows.Forms;

namespace Go.src.View
{

    public partial class Settings : Form
    {
        public BoardMode boardMode { get; set; }
        public GameMode gameMode { get; set; }
        public Settings()
        {
            InitializeComponent();
            boardMode = BoardMode._NONE_SELECTED;
            gameMode = GameMode._NONE_SELECTED;
        }

        private void ConfirmSettingsClicked(object sender, EventArgs e)
        {
            if (NineByNineRadioButton.Checked)
                boardMode = BoardMode._NINE_BY_NINE;
            else if(ThirteenByThirteenRadioButton.Checked)
                boardMode = BoardMode._THIRTEEN_BY_THIRTEEN;
            else if (NineteenByNineteenRadioButton.Checked)
                boardMode = BoardMode._NINETEEN_BY_NINETEEN;

            if (OneVsOneRadioButton.Checked)
                gameMode = GameMode._ONE_VS_ONE;
            else if(PlayAgainstComputerRadioButton.Checked)
                 gameMode = GameMode._PLAY_AGAINST_COMPUTER;

            Dispose();
        }
    }
}
