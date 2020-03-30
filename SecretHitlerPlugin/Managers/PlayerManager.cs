using DarkRift;
using DarkRift.Server;
using SecretHitlerPlugin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerPlugin
{
    public class PlayerManager : Plugin
    {
        public override bool ThreadSafe => false;

        public override Version Version => new Version(1, 0, 0);
        Dictionary<IClient, Player> players = new Dictionary<IClient, Player>();

        public PlayerManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += ClientConnected;
            ClientManager.ClientDisconnected += ClientDisconnected;
        }

        public void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            e.Client.MessageReceived += Client_MessageReceived;

            Random r = new Random();
            Player newPlayer = new Player(
                e.Client.ID,
                (PlayerColor)players.Count(),
                (PlayerRole)PlayerRole.fascist
                ) ;

            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create()) //Sends the data that a new player spawned to all the connected players.
            {
                newPlayerWriter.Write(newPlayer.ID);
                newPlayerWriter.Write((int)newPlayer.Color);
                newPlayerWriter.Write((int)newPlayer.Role);

                using (Message newPlayerMessage = Message.Create(Tags.SpawnPlayerTag, newPlayerWriter))
                {
                    foreach (IClient client in ClientManager.GetAllClients().Where(x => x != e.Client))
                        client.SendMessage(newPlayerMessage, SendMode.Reliable);
                }
            }

            players.Add(e.Client, newPlayer);

            using (DarkRiftWriter playerWriter = DarkRiftWriter.Create()) //Sends the data from all the connected players to the new player.
            {
                foreach (Player player in players.Values)
                {
                    playerWriter.Write(player.ID);
                    playerWriter.Write((int)player.Color);
                    playerWriter.Write((int)player.Role);                   
                }

                using (Message playerMessage = Message.Create(Tags.SpawnPlayerTag, playerWriter))
                    e.Client.SendMessage(playerMessage, SendMode.Reliable);
                players.First(x => x.Value.Color == PlayerColor.green);
            }
        }

        private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage() as Message)
            {
                if (message.Tag == Tags.DrawCardsTag)
                {

                }
                if (message.Tag == Tags.GameStartTag)
                {
                    Console.WriteLine("GameStart Received");
                    GameSetup gameSetup = new GameSetup();
                    players = gameSetup.GiveColors(players);
                    UpdateColors(players);
                    players = gameSetup.GiveRoles(players);
                    UpdateRoles(players);                 
                }
            }
        }

        public void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            players.Remove(e.Client);

            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(e.Client.ID);

                using (Message message = Message.Create(Tags.DisconnectPlayerTag, writer))
                {
                    foreach (IClient client in ClientManager.GetAllClients())
                        client.SendMessage(message, SendMode.Reliable);
                }
            }
        }


        public void UpdateRoles(Dictionary<IClient, Player> players)
        {
            using (DarkRiftWriter updateRoleWriter = DarkRiftWriter.Create()) //Sends the role to the player
            {
                foreach (IClient client in ClientManager.GetAllClients())
                {
                    PlayerRole role = players[client].Role;
                    updateRoleWriter.Write(client.ID);
                    updateRoleWriter.Write((int)role);
                    Console.WriteLine("Client: " + client.ID + " - Color: " + players[client].Color);
                    Console.WriteLine("Client role: " + players[client].Role);
                    using (Message updateRoleMessage = Message.Create(Tags.UpdateRoleTag, updateRoleWriter))
                    {
                        client.SendMessage(updateRoleMessage, SendMode.Reliable);
                    }
                }
            }
        }

        public void UpdateColors(Dictionary<IClient, Player> players)
        {
            using (DarkRiftWriter updateColorWriter = DarkRiftWriter.Create()) //Sends the role to the player
            {
                foreach (IClient client in ClientManager.GetAllClients())
                {
                    PlayerColor color = players[client].Color;
                    updateColorWriter.Write(client.ID);
                    updateColorWriter.Write((int)color);
                    Console.WriteLine("Client: " + client.ID + " - Color: " + players[client].Color);
                    using (Message updateColorMessage = Message.Create(Tags.UpdateColorTag, updateColorWriter))
                    {
                        client.SendMessage(updateColorMessage, SendMode.Reliable);
                    }
                }
            }
        }


        public Dictionary<IClient, Player> GetPlayers()
        {
            return players;
        }
    }
}
