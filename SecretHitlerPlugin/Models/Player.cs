using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerPlugin
{
    public class Player
    {
        public ushort ID { get; set; }
        public PlayerColor Color { get; set; }
        public PlayerRole Role { get; set; }
        
        public Player(ushort id, PlayerColor color, PlayerRole role)
        {
            this.ID = id;
            this.Color = color;
            this.Role = role;
        }
    }
}
