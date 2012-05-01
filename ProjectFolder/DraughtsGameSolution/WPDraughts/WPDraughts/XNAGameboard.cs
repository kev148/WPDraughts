using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;


namespace WPDraughts
{

    public class XNAGameBoard
    {
        Rectangle[,] boardSpaces;

        const int GAMEBOARDWIDTH = 8;
        const int GAMEBOARDHEIGHT = 8;

        public XNAGameBoard()         
        {
            boardSpaces = new Rectangle[GAMEBOARDHEIGHT, GAMEBOARDWIDTH];
        }

        public Rectangle[,] BoardSpaces
        {
            get { return boardSpaces; }
            set { boardSpaces = value; }
        }

    }
}
