using DarkRift.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerPlugin.Helpers
{
    public class GameSetup
    {
        public GameSetup()
        {

        }

        public void StartGame()
        {

        }

        public Dictionary<IClient, Player> GiveRoles(Dictionary<IClient, Player> players)
        {
            int playerCount = players.Count();
            List<PlayerRole> roles = ShuffleRoles(playerCount);
            int  i = 0;

            foreach (Player player in players.Values)
            {
                player.Role = roles[i];
                Console.WriteLine("player: " + player.Color);
                Console.WriteLine("role: " + player.Role);
                Console.WriteLine(roles.Count);
                Console.WriteLine(playerCount);
                i++;
            }

            return players;
        }

        public Dictionary<IClient, Player> GiveColors(Dictionary<IClient, Player> players)
        {
            int playerCount = players.Count();
            List<int> colors = new List<int>();
            Random r = new Random();

            for(int i = 0; i< playerCount; i++)
            {
                Console.WriteLine("Added to the list: " + (PlayerColor) i);
                colors.Add(i);
            }

            foreach (Player player in players.Values)
            {
                var ran = r.Next(0, colors.Count());
                Console.WriteLine(player.ID + " : " + player.Color);
                player.Color = (PlayerColor) colors[ran];
                colors.RemoveAt(ran);
            }

            return players;
        }

        private List<PlayerRole> ShuffleRoles(int playerCount)
        {
            List<PlayerRole> roles = new List<PlayerRole>();
            int fascistCount = 1;
            PlayerRole role = 0;
            Random r = new Random();

            if (playerCount > 6) { fascistCount = 2; }
            if (playerCount > 8) { fascistCount = 3; }

            for(int i = 0;i < playerCount; i++)
            {
                if(i >= fascistCount) { role = PlayerRole.liberal; }
                else if (i < fascistCount) { role = PlayerRole.fascist; }
                if (i == 0) { role = PlayerRole.hitler; }
                roles.Insert(r.Next(0, i + 1), role);
                Console.WriteLine("role: " + roles[i] + " - position: " + i);
            }

            
            return roles; 
        }
    }
}
