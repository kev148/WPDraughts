using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace WPDraughts
{
    
    public class XNAGameScreenView : Microsoft.Xna.Framework.GameComponent
    {
        private const int GAMEBOARDWIDTH = 8;
        private const int GAMEBOARDHEIGHT = 8;
        private const int SQUAREWIDTH = 54;
        private const int SQUAREHEIGHT = 54;
        private const int INITIALSQUAREX = 25;
        private const int INITIALSQUAREY = 60;
        private Texture2D title, onePlayer, twoPlayer, crown, blackWins, whiteWins, darkSquare, lightSquare, backGround,
            yourTurn, phoneTurn, playerOneTurn, playerTwoTurn, selectDifficulty, easy, normal, hard, whitePiece, blackPiece,
            blackKingPiece, whiteKingPiece, possibleMove, forceJumps, checkedBox, unCheckedBox, whiteTurn, blackTurn;
        private Rectangle onePlayerButton, twoPlayerButton, easyButton, normalButton, hardButton, checkBox;
        private XNAGameBoard xnaGameBoard;
        private GameBoard gameBoard;
        private ContentManager contentManager;        

        public XNAGameScreenView(Game game, GameBoard gameBoard, XNAGameBoard xnaGameBoard) : base(game)
        {
            contentManager = game.Content;
            this.gameBoard = gameBoard;
            darkSquare = contentManager.Load<Texture2D>(@"Textures\darksquare");
            lightSquare = contentManager.Load<Texture2D>(@"Textures\lightsquare");
            backGround = contentManager.Load<Texture2D>(@"Textures\background");
            crown = contentManager.Load<Texture2D>(@"Textures\crown");
            blackWins = contentManager.Load<Texture2D>(@"Textures\blackWins");
            whiteWins = contentManager.Load<Texture2D>(@"Textures\whiteWins");
            title = contentManager.Load<Texture2D>(@"Textures\Title");
            onePlayer = contentManager.Load<Texture2D>(@"Textures\1Player");
            twoPlayer = contentManager.Load<Texture2D>(@"Textures\2Player");
            yourTurn = contentManager.Load<Texture2D>(@"Textures\YourTurn");
            phoneTurn = contentManager.Load<Texture2D>(@"Textures\Thinking");
            playerOneTurn = contentManager.Load<Texture2D>(@"Textures\Player1Turn");
            playerTwoTurn = contentManager.Load<Texture2D>(@"Textures\Player2Turn");
            selectDifficulty = contentManager.Load<Texture2D>(@"Textures\SelectDifficulty");
            easy = contentManager.Load<Texture2D>(@"Textures\easy");
            normal = contentManager.Load<Texture2D>(@"Textures\normal");
            hard = contentManager.Load<Texture2D>(@"Textures\hard");
            whitePiece = contentManager.Load<Texture2D>(@"Textures\whitepiece");
            blackPiece = contentManager.Load<Texture2D>(@"Textures\blackPiece");
            whiteKingPiece = contentManager.Load<Texture2D>(@"Textures\whiteking");
            blackKingPiece = contentManager.Load<Texture2D>(@"Textures\blackking");
            possibleMove = contentManager.Load<Texture2D>(@"Textures\possibleMove");
            forceJumps = contentManager.Load<Texture2D>(@"Textures\ForceJumps");
            checkedBox = contentManager.Load<Texture2D>(@"Textures\checked");
            unCheckedBox = contentManager.Load<Texture2D>(@"Textures\unchecked");
            whiteTurn = contentManager.Load<Texture2D>(@"Textures\whiteTurn");
            blackTurn = contentManager.Load<Texture2D>(@"Textures\blackTurn");
            this.xnaGameBoard = xnaGameBoard;
        }

        public Rectangle CheckBox
        {
            get { return checkBox; }
        }

        public Rectangle TwoPlayerButton
        {
            get { return twoPlayerButton; }
        }

        public Rectangle OnePlayerButton
        {
            get { return onePlayerButton; }
        }

        public Rectangle HardButton
        {
            get { return hardButton; }
        }

        public Rectangle NormalButton
        {
            get { return normalButton; }
        }

        public Rectangle EasyButton
        {
            get { return easyButton; }
        } 

        public void DrawBackGround(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround,
                new Rectangle(0, 0, 480, 800), Color.White);
            spriteBatch.End();
        }

        public void DrawCommonGameElements(SpriteBatch spriteBatch,GameType gameType, PlayerTurn gameTurn)
        {
            DrawBackGround(spriteBatch);
            DrawBoard(spriteBatch);
            DrawDraughtPieces(spriteBatch);
            DrawWhosTurnLabel(spriteBatch, gameType, gameTurn);
            DrawWhichColoursTurnItIsLabel(spriteBatch, gameTurn);
        }

        public void DrawBoard(SpriteBatch spriteBatch)
        {            
            int pixelX = INITIALSQUAREX, pixelY = INITIALSQUAREY;
            Texture2D currentSquare = darkSquare;
            for (int row = 0; row < GAMEBOARDHEIGHT; row++)
            {
                for (int col = 0; col < GAMEBOARDWIDTH; col++)
                {
                    DrawBoardSquare(spriteBatch, pixelX, pixelY, currentSquare, row, col);
                    currentSquare = ChangeSquareColour(currentSquare);
                    pixelY += SQUAREHEIGHT;
                }
                currentSquare = ChangeSquareColour(currentSquare);
                pixelX += SQUAREWIDTH;
                pixelY = INITIALSQUAREY;
            }
        }

        public void DrawDraughtPieces(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < GAMEBOARDHEIGHT; row++)
                for (int col = 0; col < GAMEBOARDWIDTH; col++)
                {
                    if (gameBoard.GameBoardSpaces[row, col] != null)
                    {
                        if (gameBoard.GameBoardSpaces[row, col].PieceColour == PlayerColours.White)
                        {
                            if (gameBoard.GameBoardSpaces[row, col].IsKing)
                                DrawDraughtsPiece(spriteBatch, whiteKingPiece, row, col);
                            else
                                DrawDraughtsPiece(spriteBatch, whitePiece, row, col);
                        }
                        else
                        {
                            if (gameBoard.GameBoardSpaces[row, col].IsKing)
                                DrawDraughtsPiece(spriteBatch, blackKingPiece, row, col);
                            else
                                DrawDraughtsPiece(spriteBatch, blackPiece, row, col);
                        }
                    }
                }
        }

        public void DrawPossibleMoveSquares(SpriteBatch spriteBatch, List<Rectangle> squaresMovableTo)
        {
            foreach (Rectangle rect in squaresMovableTo)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(possibleMove, rect, Color.White);
                spriteBatch.End();
            }
        }

        public void DrawWinner(SpriteBatch spriteBatch, PlayerColours winningColour)
        {
            DrawWinningCrown(spriteBatch);
            DrawWinningLabel(spriteBatch, winningColour);
        }

        public void DrawTitleScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(title, new Rectangle(0, 100, 480, 150), Color.White);
            spriteBatch.Draw(onePlayer, onePlayerButton, Color.White);
            spriteBatch.Draw(twoPlayer, twoPlayerButton, Color.White);
            spriteBatch.End();
        }

        public void DrawWhosTurnLabel(SpriteBatch spriteBatch, GameType gameType, PlayerTurn playerTurn)
        {
            if (gameType == GameType.OnePlayer)
                DrawWhosTurnLabelForOnePlayerGame(spriteBatch, playerTurn);
            else
                DrawWhosTurnLabelForTwoPlayerGame(spriteBatch, playerTurn);
        }

        public void DrawWhichColoursTurnItIsLabel(SpriteBatch spriteBatch, PlayerTurn playerTurn)
        {
            spriteBatch.Begin();
            if(playerTurn == PlayerTurn.WhiteTurn)                
                spriteBatch.Draw(whiteTurn, new Rectangle(90, 620, 320, 50), Color.White);
            else
                spriteBatch.Draw(blackTurn, new Rectangle(90, 620, 320, 50), Color.White);
            spriteBatch.End();
        }

        public void DrawSelectDifficultyScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(selectDifficulty, new Rectangle(0, 100, 460, 150), Color.White);
            spriteBatch.Draw(easy, easyButton, Color.White);
            spriteBatch.Draw(normal, normalButton, Color.White);
            spriteBatch.Draw(hard, hardButton, Color.White);
            spriteBatch.End();
        }

        public void DrawForcedJumpOption(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(forceJumps, new Rectangle(120, 670, 200, 120), Color.White);
            spriteBatch.End();
        }

        public void DrawCheckedBox(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(checkedBox, checkBox, Color.White);
            spriteBatch.End();
        }

        public void DrawUnCheckBox(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(unCheckedBox, checkBox, Color.White);
            spriteBatch.End();
        }
        
        public void CreateCheckBox()
        {
            checkBox = new Rectangle(330,700,50,50);
        }

        public void RemoveCheckBox()
        {
            checkBox = new Rectangle(0, 0, 0, 0);
        }

        public void CreateHowManyPlayersButtons()
        {
            onePlayerButton = new Rectangle(90, 400, 300, 150);
            twoPlayerButton = new Rectangle(90, 550, 300, 150);
        }

        public void CreateChooseDifficultyButtons()
        {
            easyButton = new Rectangle(120, 350, 233, 66);
            normalButton = new Rectangle(120, 450, 233, 66);
            hardButton = new Rectangle(120, 550, 233, 66);
        }

        public void RemoveHowManyPlayersButtons()
        {
            onePlayerButton = new Rectangle(0, 0, 0, 0);
            twoPlayerButton = new Rectangle(0, 0, 0, 0);
        }

        public void RemoveChooseDifficultyButtons()
        {
            easyButton = new Rectangle(0, 0, 0, 0);
            normalButton = new Rectangle(0, 0, 0, 0);
            hardButton = new Rectangle(0, 0, 0, 0);
        }

        private void DrawWhosTurnLabelForTwoPlayerGame(SpriteBatch spriteBatch, PlayerTurn playerTurn)
        {
            spriteBatch.Begin();
            if (playerTurn == PlayerTurn.WhiteTurn)
                spriteBatch.Draw(playerOneTurn, new Rectangle(90, 580, 320, 50), Color.White);
            else
                spriteBatch.Draw(playerTwoTurn, new Rectangle(90, 580, 320, 50), Color.White);
            spriteBatch.End();
        }

        private void DrawWhosTurnLabelForOnePlayerGame(SpriteBatch spriteBatch, PlayerTurn playerTurn)
        {
            spriteBatch.Begin();
            if (playerTurn == PlayerTurn.WhiteTurn)
                spriteBatch.Draw(yourTurn, new Rectangle(90, 580, 320, 50), Color.White);
            else
            {
                spriteBatch.Draw(phoneTurn, new Rectangle(90, 580, 320, 50), Color.White);                
            }
            spriteBatch.End();
        }

        private Texture2D ChangeSquareColour(Texture2D currentSquare)
        {
            if (currentSquare == lightSquare)
                currentSquare = darkSquare;
            else
                currentSquare = lightSquare;
            return currentSquare;
        }

        private void DrawWinningLabel(SpriteBatch spriteBatch, PlayerColours winningColour)
        {
            spriteBatch.Begin();
            if (winningColour == PlayerColours.Black)
                spriteBatch.Draw(blackWins, new Rectangle(0, 0, 480, 100), Color.White);
            else
                spriteBatch.Draw(whiteWins, new Rectangle(0, 0, 480, 100), Color.White);
            spriteBatch.End();
        }

        private void DrawWinningCrown(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(crown,
                new Rectangle(0, 0, 480, 800), Color.White);
            spriteBatch.End();
        }

        private void DrawBoardSquare(SpriteBatch spriteBatch, int pixelX, int pixelY, Texture2D currentSquare, int row, int col)
        {
            xnaGameBoard.BoardSpaces[row, col] = new Rectangle(pixelX, pixelY, SQUAREWIDTH, SQUAREHEIGHT);
            spriteBatch.Begin();
            spriteBatch.Draw(currentSquare, xnaGameBoard.BoardSpaces[row, col], Color.White);
            spriteBatch.End();
        }

        private void DrawDraughtsPiece(SpriteBatch spriteBatch, Texture2D draughtPiece, int row, int col)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(draughtPiece,
                new Rectangle(xnaGameBoard.BoardSpaces[row, col].Left,
                    xnaGameBoard.BoardSpaces[row, col].Top,
                    SQUAREWIDTH,
                    SQUAREHEIGHT), Color.White);
            spriteBatch.End();
        }
    }
}
