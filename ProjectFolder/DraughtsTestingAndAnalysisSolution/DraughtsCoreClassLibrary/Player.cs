using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPDraughts
{
    public class Player
    {
        private const int maximumNoOfPieces = 12;
        PlayerColours playerColour;
        private int currentNoOfPieces;
        private PlayerTypes playerType;
                
        public Player(PlayerTypes playerType, PlayerColours colour)
        {
            CurrentNoOfPieces = maximumNoOfPieces;
            PlayerColour = colour;
            this.PlayerType = playerType;
        }

        public PlayerTypes PlayerType
        {
            get { return playerType; }
            set { playerType = value; }
        }

        public int CurrentNoOfPieces
        {
            get { return currentNoOfPieces; }
            set { currentNoOfPieces = value; }
        }     
        
        public PlayerColours PlayerColour
        {
            get { return playerColour; }
            set { playerColour = value; }
        }
    }
}
