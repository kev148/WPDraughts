using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace WPDraughts
{
    public class GameBoard
    {
        private const int GAMEBOARDWIDTH = 8;
        private const int GAMEBOARDHEIGHT = 8;
        private DraughtPiece[,] gameBoardSpaces;
        private bool forcedJumpsOn = true;

        public bool ForcedJumpsOn
        {
            get { return forcedJumpsOn; }
            set { forcedJumpsOn = value; }
        }

        public DraughtPiece[,] GameBoardSpaces
        {
            get { return gameBoardSpaces; }
            set { gameBoardSpaces = value;}            
        }

        public GameBoard()
        {
            SetTheGameBoard();            
        }

        public int EvaluateBoard(PlayerColours playerColour)
        {
            const int MAXIMUMNUMBEROFOPPONENTPIECES = 12;
            int normalPieces = 0, kingPieces = 0, opponentPieces = 0;

            for (int i = 0; i <= GAMEBOARDWIDTH - 1; i++)
                for (int j = 0; j <= GAMEBOARDHEIGHT - 1; j++)
                {
                    if (gameBoardSpaces[i, j] != null)
                        if (gameBoardSpaces[i, j].PieceColour == playerColour)
                        {
                            if (gameBoardSpaces[i, j].IsKing == true)
                                kingPieces++;
                            else
                                normalPieces++;
                        }
                        else
                            opponentPieces++;
                }

            return normalPieces + (kingPieces * 3) + (MAXIMUMNUMBEROFOPPONENTPIECES - opponentPieces);
        }
        
        public void ApplyMove(GamePieceMove move)
        {
            if(IsMoveLegal(move))
            {
                if (move.IsJump)
                    RemoveJumpedPiece(move);
                gameBoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion] = move.MovingPiece;
                gameBoardSpaces[move.MovingPiece.HorizontalPostion, move.MovingPiece.VerticalPostion] = null;
                gameBoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion].HorizontalPostion = move.NewHorizontalPostion;
                gameBoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion].VerticalPostion = move.NewVerticalPostion;
                if (IsPieceToBeCrowned(move.MovingPiece))
                    move.MovingPiece.IsKing = true;                
            }
        }

        public void UnDoMove(GamePieceMove move)
        {
            GameBoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion] = null;
            GameBoardSpaces[move.OriginalHorizontalPostion, move.OriginalVerticalPostion] = move.MovingPiece;
            move.MovingPiece.HorizontalPostion = move.OriginalHorizontalPostion;
            move.MovingPiece.VerticalPostion = move.OriginalVerticalPostion;
            if (move.IsJump)
                ReplaceJumpedPiece(move);
            if (IsPieceToBeUnCrowned(move))
                move.MovingPiece.IsKing = false;
        }

        public Boolean IsThereASecondJump(GamePieceMove move)
        {
            if (GetAllPossibleJumpsForThisPiece(move.MovingPiece).Count > 0)
                return true;
            else
                return false;
        }

        public GameBoard CloneBoard()
        {
            GameBoard newBoard = (GameBoard)this.MemberwiseClone();
            newBoard.GameBoardSpaces = (DraughtPiece[,])this.GameBoardSpaces.Clone();
            for (int i = 0; i <= GAMEBOARDWIDTH - 1; i++)
                for (int j = 0; j <= GAMEBOARDHEIGHT - 1; j++)
                 {
                    if(GameBoardSpaces[i,j] != null)
                        newBoard.GameBoardSpaces[i,j] = newBoard.GameBoardSpaces[i,j].ClonePiece();
                }
            return newBoard;
        }

        public Boolean IsPieceInThisSquare(int col, int row)
        {
            const int OUTSIDEOFBOARD = 8;
            if (col == OUTSIDEOFBOARD || row == OUTSIDEOFBOARD)
                return false;
            else if (GameBoardSpaces[col, row] == null)
                return false;            
            else
                return true;
        }

        public List<GameBoard> GetPossibleBoards(PlayerColours colour)
        {
            List<GameBoard> allPossibleBoards = new List<GameBoard>();
            
            foreach (GamePieceMove move in GetAllPossibleMoves(colour))
            {
                GameBoard newBoard = this.CloneBoard();
                newBoard.ApplyMove(move);
                allPossibleBoards.Add(newBoard);
                
            }
            return allPossibleBoards;
        }

        public List<GamePieceMove> GetAllPossibleMoves(PlayerColours colour)
        {
            List<GamePieceMove> allPossibleMoves = new List<GamePieceMove>();
            allPossibleMoves.AddRange(GetAllPossibleJumps(colour));
            if(forcedJumpsOn && allPossibleMoves.Count == 0)
                allPossibleMoves.AddRange(GetAllPossibleNormalMoves(colour));
            else if(!forcedJumpsOn)
                allPossibleMoves.AddRange(GetAllPossibleNormalMoves(colour));          
            return allPossibleMoves;
        }

        public List<GamePieceMove> GetAllPossibleMovesForThisPiece(DraughtPiece piece)
        {
            List<GamePieceMove> allPossibleMovesForThisPiece = new List<GamePieceMove>();
            allPossibleMovesForThisPiece.AddRange(GetAllPossibleJumpsForThisPiece(piece));
            if (forcedJumpsOn && allPossibleMovesForThisPiece.Count == 0)
                allPossibleMovesForThisPiece.AddRange(GetAllPossibleNormalMovesForThisPiece(piece));
            else if (!forcedJumpsOn)
                allPossibleMovesForThisPiece.AddRange(GetAllPossibleNormalMovesForThisPiece(piece));
            return allPossibleMovesForThisPiece;
        }
        
        public List<GamePieceMove> GetAllPossibleNormalMovesForThisPiece(DraughtPiece piece)
        {
            List<GamePieceMove> moves = new List<GamePieceMove>();
            GamePieceMove move;
            move = new GamePieceMove(piece, piece.HorizontalPostion - 1, piece.VerticalPostion - 1);
            if (IsMoveLegal(move))
                moves.Add(move);
            move = new GamePieceMove(piece, piece.HorizontalPostion - 1, piece.VerticalPostion + 1);
            if (IsMoveLegal(move))
                moves.Add(move);
            move = new GamePieceMove(piece, piece.HorizontalPostion + 1, piece.VerticalPostion - 1);
            if (IsMoveLegal(move))
                moves.Add(move);
            move = new GamePieceMove(piece, piece.HorizontalPostion + 1, piece.VerticalPostion + 1);
            if (IsMoveLegal(move))
                moves.Add(move);
            return moves;
        }

        public List<GamePieceMove> GetAllPossibleJumpsForThisPiece(DraughtPiece piece)
        {
            List<GamePieceMove> jumps = new List<GamePieceMove>();
            GamePieceMove jump;
            jump = new GamePieceMove(piece, piece.HorizontalPostion - 2, piece.VerticalPostion - 2);
            if (IsMoveLegal(jump))
                jumps.Add(jump);
            jump = new GamePieceMove(piece, piece.HorizontalPostion - 2, piece.VerticalPostion + 2);
            if (IsMoveLegal(jump))
                jumps.Add(jump);
            jump = new GamePieceMove(piece, piece.HorizontalPostion + 2, piece.VerticalPostion - 2);
            if (IsMoveLegal(jump))
                jumps.Add(jump);
            jump = new GamePieceMove(piece, piece.HorizontalPostion + 2, piece.VerticalPostion + 2);
            if (IsMoveLegal(jump))
                jumps.Add(jump);
            return jumps;
        }

        public Boolean IsMovablePieceSelected(DraughtPiece selectedPiece, PlayerColours colour)
        {
            bool allMovesFound = true;
            List<GamePieceMove> allPossibleMove = GetAllPossibleMoves(colour);
            List<GamePieceMove> allPossibleMoveForThisPiece = GetAllPossibleMovesForThisPiece(selectedPiece);
            foreach (GamePieceMove move in allPossibleMoveForThisPiece)
            {
                if (!allPossibleMove.Contains(move))
                    allMovesFound = false;
            }
            return allMovesFound;
        }

        public Boolean IsMoveLegal(GamePieceMove move)
        {
            if (IsNewLocationAnEmptySquare(move))
            {
                if (move.IsNewLocationLegal(move.MovingPiece))
                {
                    if (move.IsJump && IsOpponentPieceInNextSquare(move))
                        return true;
                    else if (!move.IsJump)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;                       
        }

        public Boolean IsGameOver()
        {
            int blackCount = 0, whiteCount = 0;
            blackCount = GetAllPossibleMoves(PlayerColours.Black).Count;
            whiteCount = GetAllPossibleMoves(PlayerColours.White).Count;
            if (blackCount == 0 || whiteCount == 0)
                return true;
            else
                return false;
        }

        private List<GamePieceMove> GetAllPossibleJumps(PlayerColours colour)
        {
            List<GamePieceMove> allPossibleJumps = new List<GamePieceMove>();
            for (int i = 0; i <= GAMEBOARDWIDTH - 1; i++)
                for (int j = 0; j <= GAMEBOARDHEIGHT - 1; j++)
                {
                    if (gameBoardSpaces[i, j] != null)
                        if (gameBoardSpaces[i, j].PieceColour == colour)
                            allPossibleJumps.AddRange(GetAllPossibleJumpsForThisPiece(gameBoardSpaces[i, j]));
                }
            return allPossibleJumps;
        }

        private List<GamePieceMove> GetAllPossibleNormalMoves(PlayerColours colour)
        {
            List<GamePieceMove> allPossibleNormalMoves = new List<GamePieceMove>();
            for (int i = 0; i <= GAMEBOARDWIDTH - 1; i++)
                for (int j = 0; j <= GAMEBOARDHEIGHT - 1; j++)
                {
                    if (gameBoardSpaces[i, j] != null)
                        if (gameBoardSpaces[i, j].PieceColour == colour)
                            allPossibleNormalMoves.AddRange(GetAllPossibleNormalMovesForThisPiece(gameBoardSpaces[i, j]));
                }
            return allPossibleNormalMoves;
        }

        private void ReplaceJumpedPiece(GamePieceMove move)
        {
            gameBoardSpaces[move.JumpedPiece.HorizontalPostion, move.JumpedPiece.VerticalPostion] = move.JumpedPiece;
        }

        private void RemoveJumpedPiece(GamePieceMove move)
        {
            switch (move.MoveDirection)
            {
                case MoveDirections.TopLeft:
                    move.JumpedPiece = gameBoardSpaces[move.MovingPiece.HorizontalPostion - 1, move.MovingPiece.VerticalPostion - 1];
                    gameBoardSpaces[move.MovingPiece.HorizontalPostion - 1, move.MovingPiece.VerticalPostion - 1] = null;
                    break;
                case MoveDirections.BottomLeft:
                    move.JumpedPiece = gameBoardSpaces[move.MovingPiece.HorizontalPostion - 1, move.MovingPiece.VerticalPostion + 1];
                    gameBoardSpaces[move.MovingPiece.HorizontalPostion - 1, move.MovingPiece.VerticalPostion + 1] = null;
                    break;
                case MoveDirections.TopRight:
                    move.JumpedPiece = gameBoardSpaces[move.MovingPiece.HorizontalPostion + 1, move.MovingPiece.VerticalPostion - 1];
                    gameBoardSpaces[move.MovingPiece.HorizontalPostion + 1, move.MovingPiece.VerticalPostion - 1] = null;
                    break;
                case MoveDirections.BottomRight:
                    move.JumpedPiece = gameBoardSpaces[move.MovingPiece.HorizontalPostion + 1, move.MovingPiece.VerticalPostion + 1];
                    gameBoardSpaces[move.MovingPiece.HorizontalPostion + 1, move.MovingPiece.VerticalPostion + 1] = null;
                    break;
            }
        }
        
        private bool IsOpponentPieceInNextSquare(GamePieceMove move)
        {
            switch(move.MoveDirection)
            {
                case MoveDirections.TopLeft:
                    if (IsOpponentToTheTopLeft(move.MovingPiece))
                        return true;
                    break;
                case MoveDirections.BottomLeft:
                    if (IsOpponentToTheBottomLeft(move.MovingPiece))
                        return true;
                    break;
                case MoveDirections.TopRight:
                    if (IsOpponentToTheTopRight(move.MovingPiece))
                        return true;
                    break;
                case MoveDirections.BottomRight:
                    if (IsOpponentToTheBottomRight(move.MovingPiece))
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        private Boolean IsOpponentToTheTopLeft(DraughtPiece piece)
        {
            if (gameBoardSpaces[piece.HorizontalPostion - 1, piece.VerticalPostion - 1] != null)
            {
                if (gameBoardSpaces[piece.HorizontalPostion - 1, piece.VerticalPostion - 1].PieceColour
                        == GetOpponentColour(piece.PieceColour))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private Boolean IsOpponentToTheBottomLeft(DraughtPiece piece)
        {
            if (gameBoardSpaces[piece.HorizontalPostion - 1, piece.VerticalPostion + 1] != null)
            {
                if (gameBoardSpaces[piece.HorizontalPostion - 1, piece.VerticalPostion + 1].PieceColour
                        == GetOpponentColour(piece.PieceColour))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private Boolean IsOpponentToTheTopRight(DraughtPiece piece)
        {
            if (gameBoardSpaces[piece.HorizontalPostion + 1, piece.VerticalPostion - 1] != null)
            {
                if (gameBoardSpaces[piece.HorizontalPostion + 1, piece.VerticalPostion - 1].PieceColour
                        == GetOpponentColour(piece.PieceColour))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private Boolean IsOpponentToTheBottomRight(DraughtPiece piece)
        {
            if (gameBoardSpaces[piece.HorizontalPostion + 1, piece.VerticalPostion + 1] != null)
            {
                if (gameBoardSpaces[piece.HorizontalPostion + 1, piece.VerticalPostion + 1].PieceColour
                        == GetOpponentColour(piece.PieceColour))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private PlayerColours GetOpponentColour(PlayerColours playerColour)
        {
            if (playerColour == PlayerColours.White)
                return PlayerColours.Black;
            else
                return PlayerColours.White;
        }

        private Boolean IsPieceToBeCrowned(DraughtPiece piece)
        {
            const int BLACKSIDEOFBOARD = 7;
            const int WHITESIDEOFBOARD = 0;
            if (piece.PieceColour == PlayerColours.White)
                if (piece.HorizontalPostion == BLACKSIDEOFBOARD)
                    return true;
                else
                    return false;
            else
                if (piece.HorizontalPostion == WHITESIDEOFBOARD)
                    return true;
                else                
                    return false;
        }

        private Boolean IsPieceToBeUnCrowned(GamePieceMove move)
        {          
            if (!move.IsKingBeforeMove && move.IsKingAfterMove)
                return true;            
            else
                return false;             
        }
        
        private bool IsNewLocationAnEmptySquare(GamePieceMove move)
        {
            if (IsLocationInsideOfBoard(move))
            {
                if (gameBoardSpaces[move.NewHorizontalPostion, move.NewVerticalPostion] == null)
                    return true;
                else
                    return false;
            }
            else
                return false;            
        }

        private bool IsLocationInsideOfBoard(GamePieceMove move)
        {
            if (move.NewHorizontalPostion >= 0 && move.NewHorizontalPostion <= 7 && move.NewVerticalPostion >= 0 && move.NewVerticalPostion <= 7)
                return true;
            else
                return false;
        }

        private void SetTheGameBoard()
        {
            gameBoardSpaces = new DraughtPiece[GAMEBOARDWIDTH, GAMEBOARDHEIGHT];
            for (int i = 0; i <= GAMEBOARDWIDTH - 1; i++)
                for (int j = 0; j <= GAMEBOARDHEIGHT - 1; j++)
                {
                    if ((i == 0 || i == 2) && j % 2 == 0)
                        gameBoardSpaces[i, j] = new DraughtPiece(PlayerColours.White,i,j);
                    else if (i == 1 && j % 2 != 0)
                        gameBoardSpaces[i, j] = new DraughtPiece(PlayerColours.White, i, j);
                    else if ((i == 5 || i == 7) && j % 2 != 0)
                        gameBoardSpaces[i, j] = new DraughtPiece(PlayerColours.Black, i, j);
                    else if (i == 6 && j % 2 == 0)
                        gameBoardSpaces[i, j] = new DraughtPiece(PlayerColours.Black, i, j);
                    else
                        gameBoardSpaces[i, j] = null;
                }            
        }
       
    }
}
