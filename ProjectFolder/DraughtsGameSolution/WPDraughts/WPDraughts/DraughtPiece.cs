using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPDraughts
{
    public class DraughtPiece
    {
        PlayerColours pieceColour;
        int horizontalPostion, verticalPostion;
        Boolean isKing;

        public int HorizontalPostion
        {
            get { return horizontalPostion; }
            set { horizontalPostion = value; }
        }

        public int VerticalPostion
        {
            get { return verticalPostion; }
            set { verticalPostion = value; }
        }

        public Boolean IsKing
        {
            get { return isKing; }
            set { isKing = value; }
        }

        public PlayerColours PieceColour
        {
            get { return pieceColour; }
            set { pieceColour = value; }
        }        

        public DraughtPiece(PlayerColours pieceColour, int horizontalPostion, int verticalPostion)
        {
            isKing = false;
            PieceColour = pieceColour;
            HorizontalPostion = horizontalPostion;
            VerticalPostion = verticalPostion;
        }

        public DraughtPiece ClonePiece()
        {
            return (DraughtPiece)this.MemberwiseClone();
        }
    }
}
