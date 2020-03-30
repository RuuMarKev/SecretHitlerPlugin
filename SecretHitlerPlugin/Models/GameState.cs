using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerPlugin.Helpers
{
    public class GameState
    {
        public int FascistCardsPlayed { get; set; }
        public int LiberalCardsPlayed { get; set; }
        public PlayerColor CurrenPresident { get; set; }
        public PlayerColor LastPresident { get; set; }
        public PlayerColor CurrentChancellor { get; set; }
        public PlayerColor LastChancellor { get; set; }

        public GameState()
        {

        }
    }
}
