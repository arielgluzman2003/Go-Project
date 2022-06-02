/* Game.cs */

using Go.src.Model;
using Go.src.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Go.src.Control
{
    public enum GameMode
    {
        _PLAY_AGAINST_COMPUTER, _ONE_VS_ONE, _NONE_SELECTED
    }

    public enum BoardMode
    {
        _NINE_BY_NINE, _THIRTEEN_BY_THIRTEEN, _NINETEEN_BY_NINETEEN, _NONE_SELECTED
    }

    public delegate void RefreshCallback();

    public class Game
    {
        #region Private Variables
        private GameMode gameMode;
        private Board board;
        private BoardFunctions boardFunctions = new BoardFunctions();
        private Image boardImage;
        private Image whiteStone;
        private Image blackStone;
        private GraphicBoard coordiantion;
        static private int playerTurnId = Go.Properties.PlayerInfo.Default.PLAYER1ID;
        private RefreshCallback RefreshWindow;

        // CapturedLastMove:
        //      A Set, Of Sets Of Points
        //      Which Were Captured In The Last Move, Each With Their ID
        HashSet<(HashSet<Point>, int)> CapturedLastMove;

        StaticEvaluation staticEvaluation = new StaticEvaluation(1, 3);

        #endregion

        int scoreA = 0;
        int scoreB = 0;

        public Game(GameMode gameMode, BoardMode boardMode)
        {
            /* For Ko Rule */
            CapturedLastMove = new HashSet<(HashSet<Point>, int)>();


            /* Deserialization of Graphic Coordinates of Boards */
            List<GraphicBoard> boards = JsonConvert.DeserializeObject<List<GraphicBoard>>(
                System.Text.Encoding.UTF8.GetString(Go.Properties.Resources.BoardPositions));

            List<Shape> goodShapes = JsonConvert.DeserializeObject<List<Shape>>(
                System.Text.Encoding.UTF8.GetString(Go.Properties.Resources.GoodShapes));

            List<Shape> badShapes = JsonConvert.DeserializeObject<List<Shape>>(
                System.Text.Encoding.UTF8.GetString(Go.Properties.Resources.BadShapes));


            // Check For Largest Shape in All Shapes

            /* Getting Resources For Board Settings */
            this.gameMode = gameMode;

            switch (boardMode)
            {
                case BoardMode._NINE_BY_NINE:
                    board = new Board(9, 9);
                    boardImage = Go.Properties.Resources._9X9;
                    whiteStone = Go.Properties.Resources._9X9w;
                    blackStone = Go.Properties.Resources._9X9b;
                    foreach (GraphicBoard b in boards)
                    {
                        if (b.id.Equals("9X9"))
                            coordiantion = b;
                    }
                    break;
                case BoardMode._THIRTEEN_BY_THIRTEEN:
                    board = new Board(13, 13);
                    boardImage = Go.Properties.Resources._13X13;
                    whiteStone = Go.Properties.Resources._13X13w;
                    blackStone = Go.Properties.Resources._13X13b;
                    foreach (GraphicBoard b in boards)
                    {
                        if (b.id.Equals("13X13"))
                            coordiantion = b;
                    }
                    break;
                case BoardMode._NINETEEN_BY_NINETEEN:
                    board = new Board(19, 19);
                    boardImage = Go.Properties.Resources._19X19;
                    whiteStone = Go.Properties.Resources._19X19w;
                    blackStone = Go.Properties.Resources._19X19b;
                    foreach (GraphicBoard b in boards)
                    {
                        if (b.id.Equals("19X19"))
                            coordiantion = b;
                    }
                    break;
                default:
                    throw new ArgumentException("Argument is Bad. No Match for Board.");
            }
            switch (gameMode)
            {
                case GameMode._PLAY_AGAINST_COMPUTER:
                    break;
                case GameMode._ONE_VS_ONE:
                    break;
                default:
                    throw new ArgumentException("Argument is Bad. No Match for Mode.");
            }
        }

        #region Events
        
        public void SetRefresh(RefreshCallback r)
        {
            RefreshWindow = r;
        }

        public void rePaintBoard(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardImage, new Rectangle(0, 0, boardImage.Width, boardImage.Height));
            for (int x = 0; x < board.columns; x++)
            {
                for (int y = 0; y < board.rows; y++)
                {
                    int id = board.Get(x, y);
                    Point boardCell = coordiantion.GetCoordinates(x, y);
                    if (id == Go.Properties.PlayerInfo.Default.NULLID)
                        continue;
                    else if (id == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                        e.Graphics.DrawImage(whiteStone, new Rectangle(boardCell.X, boardCell.Y, whiteStone.Width, whiteStone.Height));
                    else if (id == Go.Properties.PlayerInfo.Default.PLAYER2ID)
                        e.Graphics.DrawImage(blackStone, new Rectangle(boardCell.X, boardCell.Y, blackStone.Width, blackStone.Height));
                }
            }
        }

        public void rePaintScore(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            MainForm f = (sender as MainForm);
            f.updateScore(scoreA, scoreB);
        }



        public void MouseClick(Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // TODO: Seprate Click From Move Making

            /* Converting Raw Pixel Coordinate to Coordinates of Cell on Board */
            Point cellClicked = coordiantion.GetBoardPosition(e.Location);

            /* Attempts to Make Move */
            MoveAttempt(cellClicked);
        }
        #endregion

        bool validMove(Point move, int id, Action<Point> cellNotEmpty = null, Action<Point> Ko = null, Action<Point> Suicide = null)
        {
            /* Cell Not Empty */
            if (boardFunctions.isCellEmpty(board, move))
            {
                // If There is a Message to the User
                if (cellNotEmpty != null)
                    cellNotEmpty(move);

                return false;
            }

            /* Ko Rule - Cannot Place Stone In Place Where Stone Was Captured Last Move */

            foreach ((HashSet<Point> captures, int capturesId) in CapturedLastMove)
            {
                if (capturesId == id)
                {
                    if (boardFunctions.cellIn(captures, move))
                    {
                        // If There is a Message to the User
                        if (Ko != null)
                            Ko(move);

                        return false;
                    }
                }
            }

            /* Check For Suicide */
            if (boardFunctions.isSuicideMove(board.CopyWith(move, id), move))
            {
                // If There is a Message to the User
                if (Suicide != null)
                    Suicide(move);

                return false;
            }

            
            return true;
        }

        bool validMove(int x, int y, int id, Action<Point> cellNotEmpty = null, Action<Point> Ko = null, Action<Point> Suicide = null)
        {
            return validMove(new Point(x,y), id, cellNotEmpty, Ko, Suicide);
        }

        private void MoveAttempt(Point cellClicked)
        {

            bool moveAccepted = validMove(cellClicked,playerTurnId,
                (move) => System.Windows.Forms.MessageBox.Show(String.Format("Cell ({0:d}, {1:d}) is not empty!", move.X, move.Y)),
                (move) => System.Windows.Forms.MessageBox.Show(String.Format("Cell ({0:d}, {1:d}) Has Been Captured Last Move!", move.X, move.Y)),
                (move) => System.Windows.Forms.MessageBox.Show(String.Format("Move to Cell ({0:d}, {1:d}) Constitutes As Suicidal Move!", move.X, move.Y)));

            if (moveAccepted)
            {
                CapturedLastMove.Clear();

                board.Set(cellClicked, playerTurnId);

                foreach((HashSet<Point> Captures, int capturesId) in boardFunctions.CheckCaptures(board))
                {
                    CapturedLastMove.Add((Captures, capturesId));
                    if (capturesId == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                        scoreB += Captures.Count;
                    else if (capturesId == Go.Properties.PlayerInfo.Default.PLAYER2ID)
                        scoreA += Captures.Count;
                }

                RefreshWindow();
                if (gameMode == GameMode._PLAY_AGAINST_COMPUTER)
                {
                    Point move = GetNextMove(board, boardFunctions.GetOpponentId(playerTurnId));
                    board.Set(move, boardFunctions.GetOpponentId(playerTurnId));
                    RefreshWindow();
                }
                else
                    playerTurnId = boardFunctions.GetOpponentId(playerTurnId);

            }

        }

        private Point GetNextMove(Board board, int id)
        {
            // TODO: Add New Captures As Variable For Move Evaluation


            Dictionary<Point, Board> moves = new Dictionary<Point, Board>();

            for (int x = 0; x < board.columns; x++)
            {
                for (int y = 0; y < board.rows; y++)
                {
                    if(validMove(x,y,id))
                        moves.Add(new Point(x, y), board.CopyWith(x, y, id));
                }
            }

            int maxStrategicScore = int.MinValue;
            Point chosenMove = new Point();
            foreach (Point move in moves.Keys)
            {
                int strategicScore = staticEvaluation.Heuristic(moves[move], id);
                if(strategicScore > maxStrategicScore)
                {
                    maxStrategicScore=strategicScore;
                    chosenMove = move;
                }    
            }

            return chosenMove;
        }
    }
}
