using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace WPDraughts
{

    public class XNAGameBoardPostion
    {
        Point gameBoardSpace;
        Point phoneLocation;
        Vector2 screenLocation;

        public Point PhoneLocation
        {
            get { return phoneLocation; }
            set { phoneLocation = value; }
        }

        public Point GameBoardSpace
        {
            get { return gameBoardSpace; }
            set { gameBoardSpace = value; }
        }        

        public Vector2 ScreenLocation
        {
            get { return screenLocation; }
            set { screenLocation = value; }
        }

        public XNAGameBoardPostion(Point gameboardSpace)
        {
            GameBoardSpace = gameboardSpace;
        }

        public XNAGameBoardPostion(Point gameboardSpace, Vector2 screenLocation)
        {
            GameBoardSpace = gameboardSpace;
            ScreenLocation = screenLocation;
        }
    }
}
