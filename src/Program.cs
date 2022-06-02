/* Program.cs */

using Go.src.Control;
using Go.src.View;
using System;
using System.Windows.Forms;

namespace Go
{
    internal static class Program
    {

        static Game g;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings settingWindow = new Settings();
            Application.Run(settingWindow);

            if (settingWindow.boardMode != BoardMode._NONE_SELECTED && settingWindow.gameMode != GameMode._NONE_SELECTED)
            {
                g = new Game(settingWindow.gameMode, settingWindow.boardMode);

                MainForm gameWindow = new MainForm(
                    new Action<Object, MouseEventArgs>(g.MouseClick), // Mouse Click Function
                    new Action<Object, PaintEventArgs>(g.rePaintBoard), // Re-paint board panel
                    new Action<Object, PaintEventArgs>(g.rePaintScore)); // Re-paint score board

                g.SetRefresh(gameWindow.Refresh);

                Application.Run(gameWindow);
            }
        }
    }
}
