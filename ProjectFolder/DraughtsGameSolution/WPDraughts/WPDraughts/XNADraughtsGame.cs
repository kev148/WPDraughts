using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace WPDraughts
{
   
    public class XNADraughtsGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Vector2 touchPostion;
        private XNAGameScreenView xnaGameScreenView;
        private XNAGameBoard xnaGameBoard;
        private GameStates gameState;
        private Player player1, player2;
        private AI ai;
        private PlayerTurn gameTurn;
        private PlayerColours winner;
        private GameType gameType;
        private GameBoard gameBoard;
        private List<Rectangle> squaresMovableTo;
        private DraughtPiece selectedPiece;
        private XNAGameBoardPostion selectedPoint;

        public XNADraughtsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            gameBoard = new GameBoard();
            xnaGameBoard = new XNAGameBoard();
            xnaGameScreenView = new XNAGameScreenView(this, gameBoard, xnaGameBoard);            
            //gameTurn = PlayerTurn.WhiteTurn;         
            gameState = GameStates.TitleScreen;
            TouchPanel.EnabledGestures = GestureType.Tap;
            xnaGameScreenView.CreateHowManyPlayersButtons();
            xnaGameScreenView.CreateCheckBox();
            spriteBatch = new SpriteBatch(GraphicsDevice); 
            base.Initialize();
        }
        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (gameType == GameType.OnePlayer && gameTurn == PlayerTurn.BlackTurn)
            {
                System.Threading.Thread.Sleep(1000);
                HandleBlackTurnForAIPlayer();
            }
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gameState == GameStates.TitleScreen)
                    HandleScreenTouch(gesture);
                else if (gameState == GameStates.SelectDifficultyScreen)
                    HandleScreenTouch(gesture);
                else if (gameState != GameStates.GameOver)
                {
                    selectedPoint = GetTouchLocation(gesture);
                    if (gameTurn == PlayerTurn.WhiteTurn)
                        HandleTurnForHumanPlayer(PlayerColours.White, selectedPoint);
                    else if (gameType == GameType.TwoPlayer && gameTurn == PlayerTurn.BlackTurn)
                        HandleTurnForHumanPlayer(PlayerColours.Black, selectedPoint);                    
                }
                else
                    ResetGame();
            }            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    xnaGameScreenView.DrawBackGround(spriteBatch);
                    xnaGameScreenView.DrawTitleScreen(spriteBatch);
                    xnaGameScreenView.DrawForcedJumpOption(spriteBatch);
                    if (gameBoard.ForcedJumpsOn)
                        xnaGameScreenView.DrawCheckedBox(spriteBatch);
                    else
                        xnaGameScreenView.DrawUnCheckBox(spriteBatch);
                    break;
                case GameStates.SelectDifficultyScreen:
                    xnaGameScreenView.DrawBackGround(spriteBatch);
                    xnaGameScreenView.DrawSelectDifficultyScreen(spriteBatch);
                    break;
                case GameStates.Playing:
                    xnaGameScreenView.DrawCommonGameElements(spriteBatch, gameType, gameTurn);
                    break;
                case GameStates.PieceSelected:
                    xnaGameScreenView.DrawCommonGameElements(spriteBatch, gameType, gameTurn);
                    xnaGameScreenView.DrawPossibleMoveSquares(spriteBatch, squaresMovableTo);
                    break;
                case GameStates.NextJump:
                    xnaGameScreenView.DrawCommonGameElements(spriteBatch, gameType, gameTurn);
                    xnaGameScreenView.DrawPossibleMoveSquares(spriteBatch, squaresMovableTo);
                    break;
                case GameStates.GameOver:
                    xnaGameScreenView.DrawWinner(spriteBatch, winner);
                    break;
            }
            base.Draw(gameTime);
        }

        private void HandleBlackTurnForAIPlayer()
        {
            List<GamePieceMove> aiMoves = ai.GetAIMoves(gameBoard.CloneBoard());
            foreach(GamePieceMove move in aiMoves)
                gameBoard.ApplyMove(move);
            gameTurn = PlayerTurn.WhiteTurn;
            if (IsGameOver())
            {
                gameState = GameStates.GameOver;
                winner = PlayerColours.Black;
            }
        }
        
        private void HandleTurnForHumanPlayer(PlayerColours currentPlayerColour, XNAGameBoardPostion selectedPoint)
        { 
                switch (gameState)
                {
                    case GameStates.Playing:
                        GetSelectedPiece(selectedPoint);
                        if(selectedPiece!=null)
                            if (gameBoard.IsMovablePieceSelected(selectedPiece, currentPlayerColour))
                            {
                                HighLightPossibleMoves(selectedPiece, currentPlayerColour);
                                gameState = GameStates.PieceSelected;
                            }
                            break;
                    case GameStates.PieceSelected:
                        if (IsNextClickAMove())
                        {
                            if (MakeMove(selectedPoint))
                                CompleteCurrentTurn(currentPlayerColour);
                            else
                                gameState = GameStates.NextJump;
                        }
                        else
                        {
                            UnSelectPiece();
                            gameState = GameStates.Playing;
                        }
                        break;
                    case GameStates.NextJump:
                        if (IsNextClickAMove())
                        {
                            if (MakeMove(selectedPoint))
                                CompleteCurrentTurn(currentPlayerColour);
                        }
                        break;
                }
            HandleAdditionalJumps(currentPlayerColour);
        }

        private void CompleteCurrentTurn(PlayerColours currentPlayerColour)
        {
            if (IsGameOver())
            {
                gameState = GameStates.GameOver;
                winner = currentPlayerColour;
            }
            else
            {
                gameTurn = GetWhosTurnIsIt(gameTurn);
                UnSelectPiece();
                gameState = GameStates.Playing;
            }
        }

        private void HandleAdditionalJumps(PlayerColours colour)
        {
            if (gameState == GameStates.NextJump)
            {
                Point jumpAbleSquare = new Point(selectedPiece.HorizontalPostion, selectedPiece.VerticalPostion);
                XNAGameBoardPostion selectedSquare = new XNAGameBoardPostion(jumpAbleSquare);
                HighLightPossibleJumps(selectedPiece, colour);
            }
        }

        private void HighLightPossibleJumps(DraughtPiece selectedPiece, PlayerColours colour)
        {
            List<GamePieceMove> jumps = new List<GamePieceMove>();
            squaresMovableTo = new List<Rectangle>();
            jumps.AddRange(gameBoard.GetAllPossibleJumpsForThisPiece(selectedPiece));
            foreach (GamePieceMove move in jumps)
            {
                Rectangle rect = xnaGameBoard.BoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion];
                squaresMovableTo.Add(rect);
            }
        }

        private XNAGameBoardPostion GetTouchLocation(GestureSample gesture)
        {
            const int OUTSIDEOFBOARD = 8;
            touchPostion = gesture.Position;
            Point touchPoint = new Point((int)touchPostion.X, (int)touchPostion.Y);
            for (int i = 0; i < xnaGameBoard.BoardSpaces.GetLength(0); i++)
            {
                for (int j = 0; j < xnaGameBoard.BoardSpaces.GetLength(1); j++)
                    if (xnaGameBoard.BoardSpaces[i, j].Contains((int)touchPostion.X, (int)touchPostion.Y))
                        return new XNAGameBoardPostion(new Point(i, j), touchPostion);
            }
            return new XNAGameBoardPostion(new Point(OUTSIDEOFBOARD, OUTSIDEOFBOARD), touchPostion);
        }

        private void HandleScreenTouch(GestureSample gesture)
        { 
            Point touchPoint = new Point((int)gesture.Position.X,(int)gesture.Position.Y);
            if (xnaGameScreenView.OnePlayerButton.Contains(touchPoint))
                OnePlayerButton_Tapped();
            else if (xnaGameScreenView.TwoPlayerButton.Contains(touchPoint))
                TwoPlayerButton_Tapped();
            else if (xnaGameScreenView.CheckBox.Contains(touchPoint))
                CheckBox_Tapped();
            else if (xnaGameScreenView.EasyButton.Contains(touchPoint))
                EasyButton_Tapped();
            else if (xnaGameScreenView.NormalButton.Contains(touchPoint))
                NormalButton_Tapped();
            else if (xnaGameScreenView.HardButton.Contains(touchPoint))
                HardButton_Tapped();
        }

        private void CheckBox_Tapped()
        {
            if (gameBoard.ForcedJumpsOn)
                gameBoard.ForcedJumpsOn = false;
            else
                gameBoard.ForcedJumpsOn = true;
        }

        private void HardButton_Tapped()
        {
            gameState = GameStates.Playing;
            ai.SetGameDifficulty(Difficulty.Hard);
            xnaGameScreenView.RemoveChooseDifficultyButtons();
        }

        private void NormalButton_Tapped()
        {
            gameState = GameStates.Playing;
            ai.SetGameDifficulty(Difficulty.Normal);
            xnaGameScreenView.RemoveChooseDifficultyButtons();
        }

        private void EasyButton_Tapped()
        {
            gameState = GameStates.Playing;
            ai.SetGameDifficulty(Difficulty.Easy);
            xnaGameScreenView.RemoveChooseDifficultyButtons();
        }

        private void TwoPlayerButton_Tapped()
        {
            gameState = GameStates.Playing;
            gameType = GameType.TwoPlayer;
            SetUpPlayers(gameType);
            xnaGameScreenView.RemoveHowManyPlayersButtons();
            xnaGameScreenView.RemoveCheckBox();
        }

        private void OnePlayerButton_Tapped()
        {
            gameState = GameStates.SelectDifficultyScreen;
            gameType = GameType.OnePlayer;
            SetUpPlayers(gameType);
            xnaGameScreenView.RemoveHowManyPlayersButtons();
            xnaGameScreenView.RemoveCheckBox();
            xnaGameScreenView.CreateChooseDifficultyButtons();
        }   
      
        private Boolean MakeMove(XNAGameBoardPostion selectedSquare)
        {
            GamePieceMove move = new GamePieceMove(selectedPiece,
                selectedSquare.GameBoardSpace.X, selectedSquare.GameBoardSpace.Y);
            gameBoard.ApplyMove(move);
            if(move.IsJump)
                if(gameBoard.IsThereASecondJump(move))
                    return false;
            return true;
        }

        private Boolean IsNextClickAMove()
        {
            foreach (Rectangle boardSquare in squaresMovableTo)
            {
                if (boardSquare.Contains(new Point((int)touchPostion.X, (int)touchPostion.Y)))
                    return true;                
            }
            return false;
        }

        private void GetSelectedPiece(XNAGameBoardPostion selectedSquare)
        {
            if (gameBoard.IsPieceInThisSquare(selectedSquare.GameBoardSpace.X, selectedSquare.GameBoardSpace.Y))
                selectedPiece = gameBoard.GameBoardSpaces[selectedSquare.GameBoardSpace.X, selectedSquare.GameBoardSpace.Y];
        }

        private void HighLightPossibleMoves(DraughtPiece selectedPiece, PlayerColours colour)
        {         
            List<GamePieceMove> moves;
            squaresMovableTo = new List<Rectangle>();
            moves = gameBoard.GetAllPossibleMovesForThisPiece(selectedPiece);
            foreach (GamePieceMove move in moves)
            {
                    Rectangle rect = xnaGameBoard.BoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion];
                    squaresMovableTo.Add(rect);
            }
        }               

        private void UnSelectPiece()
        {
            squaresMovableTo = null;
            selectedPiece = null;   
        }

        private PlayerTurn GetWhosTurnIsIt(PlayerTurn currentPlayer)
        {
            if (currentPlayer == PlayerTurn.BlackTurn)
                return PlayerTurn.WhiteTurn;
            else
                return PlayerTurn.BlackTurn;
        }

        private Boolean IsGameOver()
        {
            return gameBoard.IsGameOver();
        }
     
        private void SetUpPlayers(GameType gameType) 
        {
            if (gameType == GameType.OnePlayer)
            {
                player2 = new Player(PlayerTypes.Computer, PlayerColours.Black);
                ai = new AI();
            }
            else
                player2 = new Player(PlayerTypes.Human, PlayerColours.Black);
            player1 = new Player(PlayerTypes.Human, PlayerColours.White);

        }

        private void ResetGame()
        {
            ai = null;
            Initialize();
        }
    }
}
