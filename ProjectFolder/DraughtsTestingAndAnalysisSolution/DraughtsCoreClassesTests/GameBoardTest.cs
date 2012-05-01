using WPDraughts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DraughtsCoreClassesTests
{
    
    
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameBoardTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Evaluate Board Tests

        [TestMethod()]
        public void EvaluateBoardForWhitePlayerUsingInitialBoard()
        {
            GameBoard target = new GameBoard();
            PlayerColours playerColour = PlayerColours.White;
            int expected = 12;
            int actual;
            actual = target.EvaluateBoard(playerColour);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void EvaluateBoardForBlackPlayerUsingInitialBoard()
        {
            GameBoard target = new GameBoard();
            PlayerColours playerColour = PlayerColours.Black;
            int expected = 12;
            int actual;
            actual = target.EvaluateBoard(playerColour);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EvaluateBoardForWhitePlayerUsingTestBoard()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours playerColour = PlayerColours.White;
            int expected = 16;
            int actual;
            actual = target.EvaluateBoard(playerColour);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EvaluateBoardForBlackPlayerUsingTestBoard()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours playerColour = PlayerColours.Black;
            int expected = 18;
            int actual;
            actual = target.EvaluateBoard(playerColour);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Tests For Possible Jumps

        [TestMethod()]
        public void GetAllPossibleJumpsForNormalWhitePieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[3, 3];
            int expected = 2;
            int actual = target.GetAllPossibleJumpsForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetAllPossibleJumpsForNormalBlackPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[4, 2];
            int expected = 2;
            int actual = target.GetAllPossibleJumpsForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleJumpsForKingPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[0, 2];
            int expected = 2;
            int actual = target.GetAllPossibleJumpsForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Tests For Possible Moves

        [TestMethod()]
        public void GetAllPossibleMovesForWhitePieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[1, 1];
            int expected = 2;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetAllPossibleMovesForBlackPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[4, 4];
            int expected = 2;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForKingPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[2, 6];
            int expected = 4;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForWhitePlayerTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            int expected = 12;
            int actual = target.GetAllPossibleMoves(colour).Count;
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetAllPossibleMovesForBlackPlayerTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.Black;
            int expected = 14;
            int actual = target.GetAllPossibleMoves(colour).Count;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Test For PossibleMoves With Forced Jumps Enabled

        [TestMethod()]
        public void GetAllPossibleMovesForWhitePieceWithForcedJumpsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[3, 1];
            target.ForcedJumpsOn = true;
            int expected = 1;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForBlackPieceWithForcedJumpsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[4, 4];
            target.ForcedJumpsOn = true;
            int expected = 1;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForKingPieceWithForcedJumpsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[0, 4];
            target.ForcedJumpsOn = true;
            int expected = 1;
            int actual = target.GetAllPossibleMovesForThisPiece(piece).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForWhitePlayerWithForcedJumpsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            target.ForcedJumpsOn = true;
            int expected = 3;
            int actual = target.GetAllPossibleMoves(colour).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllPossibleMovesForBlackPlayerWithForcedJumpsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.Black;
            target.ForcedJumpsOn = true;
            int expected = 6;
            int actual = target.GetAllPossibleMoves(colour).Count;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Get Opponent Colour Tests

        [TestMethod()]
        public void GetOpponentColourForWhitePieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[1, 1];
            PlayerColours expected = PlayerColours.Black;
            PlayerColours actual = target.GetOpponentColour(piece.PieceColour);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetOpponentColourForBlackPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[4, 2];
            PlayerColours expected = PlayerColours.White;
            PlayerColours actual = target.GetOpponentColour(piece.PieceColour);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Get Possible Boards Tests
        
        [TestMethod()]
        public void GetPossibleBoardsForWhitePlayerTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            int expected = 12;
            int actual = target.GetPossibleBoards(colour).Count;
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void GetPossibleBoardsForBlackPlayerTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.Black;
            int expected = 14;
            int actual = target.GetPossibleBoards(colour).Count;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check that movable piece is selected tests

        [TestMethod()]
        public void CheckThatMovablePieceIsSelectedWhenItShouldBe()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            DraughtPiece piece = target.GameBoardSpaces[3,3];
            target.ForcedJumpsOn = true;
            bool expected = true;
            bool actual = target.IsMovablePieceSelected(piece,colour);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckThatMovablePieceIsSelectedWhenItShouldNotBe()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            DraughtPiece piece = target.GameBoardSpaces[1, 1];
            target.ForcedJumpsOn = true;
            bool expected = false;           
            bool actual = target.IsMovablePieceSelected(piece, colour);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckThatMovablePieceIsSelectedWhenItShouldBeNoForcedJumps()
        {
            GameBoard_Accessor target = CreateTestBoard();
            PlayerColours colour = PlayerColours.White;
            DraughtPiece piece = target.GameBoardSpaces[1, 1];
            bool expected = true;
            bool actual = target.IsMovablePieceSelected(piece, colour);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Gameover Tests

        [TestMethod()]
        public void IsGameOverWhenWhitePlayerShouldHaveWonTest()
        {
            GameBoard_Accessor target = CreateWhitePlayerHasWonBoard();
            bool expected = true;
            bool actual = target.IsGameOver();
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsGameOverWhenBlackPlayerShouldHaveWonTest()
        {
            GameBoard_Accessor target = CreateBlackPlayerHasWonBoard();
            bool expected = true;
            bool actual = target.IsGameOver();
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Legal Moves Tests

        [TestMethod()]
        public void IsMoveLegalForWhitePieceMovingToTheTopRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 0);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsMoveLegalForWhitePieceMovingToTheBottomRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 2);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForBlackPieceMovingToTheTopLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 6], 3, 5);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForBlackPieceMovingToTheBottomLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 4], 3, 5);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForWhitePieceJumpingToTheTopRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[3, 3], 5, 1);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForWhitePieceJumpingToTheBottomRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[3, 3], 5, 5);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForBlackPieceJumpingToTheTopLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 2, 0);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveLegalForBlackPieceJumpingToTheBottomLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 2, 4);
            bool expected = true;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Illegal Moves Test

        [TestMethod()]
        public void IsMoveIllegalForWhitePieceMovingToTheTopLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[3, 1], 2, 0);
            bool expected = false;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveIllegalForWhitePieceMovingToTheBottomLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[3, 1], 2, 2);
            bool expected = false;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveIllegalForBlackPieceMovingToTheTopRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 5, 1);
            bool expected = false;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMoveIllegalForBlackPieceMovingToTheBottomRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 5, 3);
            bool expected = false;
            bool actual = target.IsMoveLegal(move);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region New Location Tests

        [TestMethod()]
        public void IsNewLocationAnEmptySquareWhenAnEmptySquareIsSelectedTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 0);
            bool expected = true;
            bool actual = target.IsNewLocationAnEmptySquare(move);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsNewLocationAnEmptySquareWhenAnEmptySquareIsNotSelectedTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[3, 1], 4, 2);
            bool expected = false;
            bool actual = target.IsNewLocationAnEmptySquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForNormalWhitePieceWhenItShouldBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[1, 1], 2, 0);
            bool expected = true;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForNormalBlackPieceWhenItShouldBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[4, 4], 3, 5);
            bool expected = true;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForKingPieceWhenItShouldBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[7, 3], 6, 2);
            bool expected = true;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForNormalWhitePieceWhenItShouldNotBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[1, 1], 0, 0);
            bool expected = false;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForNormalBlackPieceWhenItShouldNotBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[4, 2], 5, 1);
            bool expected = false;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNewLocationLegalForKingPieceWhenItShouldNotBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove_Accessor move = new GamePieceMove_Accessor(target.GameBoardSpaces[0, 2], 1, 2);
            bool expected = false;
            bool actual = move.IsNewLocationLegal(move.MovingPiece);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check For Adjacent Pieces Tests

        [TestMethod()]
        public void IsOpponentPieceInSquareToTheTopRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 2], 1, 1);
            bool expected = true;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsOpponentPieceInSquareToTheBottomRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 2], 1, 3);
            bool expected = true;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceInSquareToTheTopLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 3, 1);
            bool expected = true;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceInSquareToTheBottomLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 2], 3, 3);
            bool expected = true;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceNotInSquareToTheTopRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 0);
            bool expected = false;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceNotInSquareToTheBottomRightTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 2);
            bool expected = false;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceNotInSquareToTheTopLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[7, 3], 6, 2);
            bool expected = false;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsOpponentPieceNotInSquareToTheBottomLeftTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[7, 3], 6, 4);
            bool expected = false;
            bool actual = target.IsOpponentPieceInNextSquare(move);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check If Piece Is In Selected Square Tests

        [TestMethod()]
        public void IsPieceInThisSquareWhenThereIsAPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            int col = 1;
            int row = 1;
            bool expected = true;
            bool actual = target.IsPieceInThisSquare(col, row);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPieceInThisSquareWhenThereIsNoPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            int col = 0;
            int row = 0;
            bool expected = false;
            bool actual = target.IsPieceInThisSquare(col, row);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check If Draught Piece Should be Crowned Tests

        [TestMethod()]
        public void IsBlackPieceToBeCrownedWhenItShouldBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[0, 2];
            bool expected = true;
            bool actual;
            actual = target.IsPieceToBeCrowned(piece);
            Assert.AreEqual(expected, actual);           
        }
                
        [TestMethod()]
        public void IsWhitekPieceToBeCrownedWhenItShouldBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[7, 3];
            bool expected = true;
            bool actual;
            actual = target.IsPieceToBeCrowned(piece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsWhitePieceToBeCrownedWhenItShouldNotBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[1, 1];
            bool expected = false;
            bool actual;
            actual = target.IsPieceToBeCrowned(piece);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsBlackPieceToBeCrownedWhenItShouldNotBeTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            DraughtPiece piece = target.GameBoardSpaces[4, 2];
            bool expected = false;
            bool actual;
            actual = target.IsPieceToBeCrowned(piece);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check If Draught Piece Should be Uncrowned Tests

        [TestMethod()]
        public void IsPieceToBeUnCrownedIfPieceWasNotKingBeforeMoveButIsAfterTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 0, 0);
            move.IsKingBeforeMove = false;
            move.IsKingAfterMove = true;
            bool expected = true;
            bool actual = target.IsPieceToBeUnCrowned(move);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsPieceToBeUnCrownedIfPieceWasKingBeforeMoveTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 0, 0);
            move.IsKingBeforeMove = true;
            move.IsKingAfterMove = true;
            bool expected = false;
            bool actual = target.IsPieceToBeUnCrowned(move);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Check For Second Jumps Tests

        [TestMethod()]
        public void IsThereASecondJumpWhenThereIsTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 4], 2, 2);
            target.ApplyMove(move);
            bool expected = true;
            bool actual;
            actual = target.IsThereASecondJump(move);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsThereASecondJumpWhenThereIsNotTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 2], 2, 0);
            target.ApplyMove(move);
            bool expected = false;
            bool actual;
            actual = target.IsThereASecondJump(move);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Remove and Replace Jumped Pieces Test

        [TestMethod()]
        public void RemoveJumpedPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 2], 2, 0);
            target.RemoveJumpedPiece(move);
            DraughtPiece expected = null;
            DraughtPiece actual = target.GameBoardSpaces[1, 1];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReplaceJumpedPieceTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 2], 2, 0);
            DraughtPiece expected = target.GameBoardSpaces[1,1];
            target.RemoveJumpedPiece(move);
            target.ReplaceJumpedPiece(move);
            DraughtPiece actual = target.GameBoardSpaces[1, 1];
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Set Board Test

        [TestMethod()]
        public void SetTheGameBoardTest()
        {
            GameBoard_Accessor target = new GameBoard_Accessor();
            string expected = "\nw_w_w_w_\n_w_w_w_w\nw_w_w_w_\n________\n________\n_b_b_b_b\nb_b_b_b_\n_b_b_b_b";
            target.SetTheGameBoard();
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ApplyMove and UndoMove Tests

        [TestMethod()]
        public void ApplyNormalWhitePieceMoveTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 0);
            string expected = "\n__B_B___\n___w____\nw_____B_\n_w_w____\n__b_b_b_\n________\n________\n___W_W__";
            target.ApplyMove(move);
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ApplyNormalBlackPieceMoveTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[4, 4], 3, 5);
            string expected = "\n__B_B___\n_w_w____\n______B_\n_w_w_b__\n__b___b_\n________\n________\n___W_W__";
            target.ApplyMove(move);
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ApplyKingPieceMoveTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[0, 4], 1, 5);
            string expected = "\n__B_____\n_w_w_B__\n______B_\n_w_w____\n__b_b_b_\n________\n________\n___W_W__";
            target.ApplyMove(move);
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UnDoMoveTest()
        {
            GameBoard_Accessor target = CreateTestBoard();
            GamePieceMove move = new GamePieceMove(target.GameBoardSpaces[1, 1], 2, 0);
            string expected = "\n__B_B___\n_w_w____\n______B_\n_w_w____\n__b_b_b_\n________\n________\n___W_W__";
            target.ApplyMove(move);
            target.UnDoMove(move);
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Game Tree DepthReached Tests

        [TestMethod()]
        public void IsDepthReachedWhenItHasBeenTest()
        {
            AI_Accessor target = new AI_Accessor();
            target.numberOfMovesAhead = 4;
            int depth = 4;
            bool expected = true;
            bool actual = target.IsDepthReached(depth);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void IsDepthReachedWhenItHasNotBeenTest()
        {
            AI_Accessor target = new AI_Accessor();
            target.numberOfMovesAhead = 4;
            int depth = 2;
            bool expected = false;
            bool actual = target.IsDepthReached(depth);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region MinMax Test

        [TestMethod()]
        public void MinMaxTest()
        {
            AI_Accessor target = new AI_Accessor();
            GameBoard_Accessor board =  CreateTestBoard();
            GameBoard testBoard = new GameBoard();
            testBoard = board.CloneBoard();
            target.numberOfMovesAhead = 2;
            List<GamePieceMove> aiMoves = target.GetAIMoves(testBoard);
            foreach(GamePieceMove move in aiMoves)
                testBoard.ApplyMove(move);
            string expected = "\nB_B_B___\n___w____\n______B_\n_w______\n__b___b_\n________\n________\n___W_W__";
            string actual = testBoard.ToString();
            Assert.AreEqual(expected,actual);
        }

        #endregion

        #region Set Game Difficulty Tests

        [TestMethod()]
        public void SetGameDifficultyToEasyTest()
        {
            AI_Accessor target = new AI_Accessor();
            Difficulty difficulty = Difficulty.Easy;
            target.SetGameDifficulty(difficulty);
            int expected = 2;
            int actual = target.numberOfMovesAhead;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void SetGameDifficultyToNormalTest()
        {
            AI_Accessor target = new AI_Accessor();
            Difficulty difficulty = Difficulty.Normal;
            target.SetGameDifficulty(difficulty);
            int expected = 4;
            int actual = target.numberOfMovesAhead;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SetGameDifficultyToHardTest()
        {
            AI_Accessor target = new AI_Accessor();
            Difficulty difficulty = Difficulty.Hard;
            target.SetGameDifficulty(difficulty);
            int expected = 8;
            int actual = target.numberOfMovesAhead;
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Methods To Create Boards for Testing

        private GameBoard_Accessor CreateTestBoard()
        {
            GameBoard_Accessor gameboard = new GameBoard_Accessor();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    gameboard.GameBoardSpaces[i, j] = null;            
            gameboard.GameBoardSpaces[1, 1] = new DraughtPiece(PlayerColours.White, 1, 1);
            gameboard.GameBoardSpaces[3, 1] = new DraughtPiece(PlayerColours.White, 3, 1);
            gameboard.GameBoardSpaces[0, 2] = new DraughtPiece(PlayerColours.Black, 0, 2);
            gameboard.GameBoardSpaces[0, 2].IsKing = true;
            gameboard.GameBoardSpaces[4, 2] = new DraughtPiece(PlayerColours.Black, 4, 2);
            gameboard.GameBoardSpaces[1, 3] = new DraughtPiece(PlayerColours.White, 1, 3);
            gameboard.GameBoardSpaces[3, 3] = new DraughtPiece(PlayerColours.White, 3, 3);
            gameboard.GameBoardSpaces[4, 6] = new DraughtPiece(PlayerColours.Black, 4, 6);
            gameboard.GameBoardSpaces[7, 3] = new DraughtPiece(PlayerColours.White, 7, 3);
            gameboard.GameBoardSpaces[7, 3].IsKing = true;
            gameboard.GameBoardSpaces[0, 4] = new DraughtPiece(PlayerColours.Black, 0, 4);
            gameboard.GameBoardSpaces[0, 4].IsKing = true;
            gameboard.GameBoardSpaces[4, 4] = new DraughtPiece(PlayerColours.Black, 4, 4);
            gameboard.GameBoardSpaces[7, 5] = new DraughtPiece(PlayerColours.White, 7, 5);
            gameboard.GameBoardSpaces[7, 5].IsKing = true;
            gameboard.GameBoardSpaces[2, 6] = new DraughtPiece(PlayerColours.Black, 2, 6);
            gameboard.GameBoardSpaces[2, 6].IsKing = true;
            return gameboard;
        }

        private GameBoard_Accessor CreateWhitePlayerHasWonBoard()
        {
            GameBoard_Accessor gameboard = new GameBoard_Accessor();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    gameboard.GameBoardSpaces[i, j] = null;
            gameboard.GameBoardSpaces[0, 0] = new DraughtPiece(PlayerColours.White, 0, 0);
            return gameboard;
        }

        private GameBoard_Accessor CreateBlackPlayerHasWonBoard()
        {
            GameBoard_Accessor gameboard = new GameBoard_Accessor();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    gameboard.GameBoardSpaces[i, j] = null;
            gameboard.GameBoardSpaces[0, 0] = new DraughtPiece(PlayerColours.Black, 0, 0);
            return gameboard;
        }

        #endregion

    }
}
