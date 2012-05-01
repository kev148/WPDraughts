using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPDraughts
{
    public class Player
    {
        private PlayerColours playerColour;
        private PlayerTypes playerType;
                
        public Player(PlayerTypes playerType, PlayerColours colour)
        {
            PlayerColour = colour;
            this.PlayerType = playerType;
        }

        public PlayerTypes PlayerType
        {
            get { return playerType; }
            set { playerType = value; }
        }  
        
        public PlayerColours PlayerColour
        {
            get { return playerColour; }
            set { playerColour = value; }
        }
    }
}
