using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPDraughts
{
    public class GamePieceMove : IEquatable<GamePieceMove>
    {
        private int newHorizontalPostion, newVerticalPostion, originalHorizontalPostion, originalVerticalPostion;
        private MoveDirections moveDirection;
        private bool isJump = false;
        private bool isKingBeforeMove, isKingAfterMove;
        private DraughtPiece movingPiece, jumpedPiece;

        public GamePieceMove(DraughtPiece piece, int newHorizontalPostion, int newVerticalPostion)
        {
            NewHorizontalPostion = newHorizontalPostion;
            NewVerticalPostion = newVerticalPostion;
            MovingPiece = piece;
            OriginalHorizontalPostion = piece.HorizontalPostion;
            OriginalVerticalPostion = piece.VerticalPostion;
            DetermineMoveDirection();
        }

        public bool IsKingAfterMove
        {
            get { return isKingAfterMove; }
            set { isKingAfterMove = value; }
        }

        public bool IsKingBeforeMove
        {
            get { return isKingBeforeMove; }
            set { isKingBeforeMove = value; }
        }

        public int OriginalVerticalPostion
        {
            get { return originalVerticalPostion; }
            set { originalVerticalPostion = value; }
        }

        public int OriginalHorizontalPostion
        {
            get { return originalHorizontalPostion; }
            set { originalHorizontalPostion = value; }
        }

        public DraughtPiece MovingPiece
        {
            get { return movingPiece; }
            set { movingPiece = value; }
        }

        public DraughtPiece JumpedPiece
        {
            get { return jumpedPiece; }
            set { jumpedPiece = value; }
        }

        public MoveDirections MoveDirection
        {
            get { return moveDirection; }
            set { moveDirection = value; }
        }     

        public bool IsJump
        {
            get { return isJump; }
            set { isJump = value; }
        }        
       
        public int NewVerticalPostion
        {
            get { return newVerticalPostion; }
            set { newVerticalPostion = value; }
        }

        public int NewHorizontalPostion
        {
            get { return newHorizontalPostion; }
            set { newHorizontalPostion = value; }
        }
        
        public bool IsNewLocationLegal(DraughtPiece piece)
        {
            if (piece.IsKing)
                return IsNewLocationLegalForKingPiece();
            else
                return IsNewLocationLegalForNormalPiece(piece);
        }

        public bool IsNewLocationLegalForKingPiece()
        {
            if (IsMoveToTopLeft())
                return true;
            else if (IsMoveToBottomLeft())
                return true;
            else if (IsMoveToTopRight())
                return true;
            else if (IsMoveToBottomRight())
                return true;
            else if (IsJumpToTopLeft())
                return true;
            else if (IsJumpToBottomLeft())
                return true;
            else if (IsJumpToTopRight())
                return true;
            else if (IsJumpToBottomRight())
                return true;
            else
                return false;
        }

        public bool IsNewLocationLegalForNormalPiece(DraughtPiece piece)
        {
            switch (piece.PieceColour)
            {
                case PlayerColours.White:
                    if (IsMoveToTopRight())
                        return true;
                    else if (IsMoveToBottomRight())
                        return true;
                    else if (IsJumpToTopRight())
                        return true;
                    else if (IsJumpToBottomRight())
                        return true;
                    break;
                case PlayerColours.Black:
                    if (IsMoveToTopLeft())
                        return true;
                    else if (IsMoveToBottomLeft())
                        return true;
                    else if (IsJumpToTopLeft())
                        return true;
                    else if (IsJumpToBottomLeft())
                        return true;
                    break;                                  
            }
            return false;
        }
        
        private bool IsMoveToTopLeft()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion - 1 &&
                NewVerticalPostion == MovingPiece.VerticalPostion - 1)
                return true;
            else
                return false;
        }

        private bool IsMoveToBottomLeft()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion - 1 &&
                NewVerticalPostion == MovingPiece.VerticalPostion + 1)
                return true;
            else
                return false;
        }

        private bool IsMoveToTopRight()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion + 1 &&
                NewVerticalPostion == MovingPiece.VerticalPostion - 1)
                return true;
            else
                return false;
        }

        private bool IsMoveToBottomRight()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion + 1 &&
                NewVerticalPostion == MovingPiece.VerticalPostion + 1)
                return true;
            else
                return false;
        }

        private bool IsJumpToTopLeft()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion - 2 &&
                NewVerticalPostion == MovingPiece.VerticalPostion - 2)
            {
                IsJump = true;
                return true;
            }
            else
                return false;
        }

        private bool IsJumpToBottomLeft()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion - 2 &&
                NewVerticalPostion == MovingPiece.VerticalPostion + 2)
            {
                IsJump = true;
                return true;
            }
            else
                return false;
        }

        private bool IsJumpToTopRight()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion + 2 &&
                NewVerticalPostion == MovingPiece.VerticalPostion - 2)
            {
                IsJump = true;
                return true;
            }
            else
                return false;
        }

        private bool IsJumpToBottomRight()
        {
            if (NewHorizontalPostion == MovingPiece.HorizontalPostion + 2 &&
                NewVerticalPostion == MovingPiece.VerticalPostion + 2)
            {
                
                IsJump = true;
                return true;
            }
            else
                return false;
        }

        private void DetermineMoveDirection()
        {
            if (IsJumpToTopLeft() || IsMoveToTopLeft())
                MoveDirection = MoveDirections.TopLeft;
            else if (IsMoveToBottomLeft() || IsJumpToBottomLeft())
                MoveDirection = MoveDirections.BottomLeft;
            else if (IsJumpToTopRight() || IsMoveToTopRight())
                MoveDirection = MoveDirections.TopRight;
            else
                MoveDirection = MoveDirections.BottomRight;
        }

        public bool Equals(GamePieceMove other)
        {
            if (this.OriginalHorizontalPostion == other.OriginalHorizontalPostion && this.OriginalVerticalPostion == other.OriginalVerticalPostion)
            {
                if (this.NewHorizontalPostion == other.NewHorizontalPostion && this.NewVerticalPostion == other.NewVerticalPostion)
                    return true;
                else
                    return false;
            }
            else
                return false;                
        }

    }
}
