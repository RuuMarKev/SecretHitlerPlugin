using DarkRift.Server;
using DarkRift;
using System;
using SecretHitlerPlugin.Helpers;

namespace SecretHitlerPlugin
{
    public class GameManager : Plugin
    {
        public override bool ThreadSafe => false;

        public override Version Version => new Version(1, 0, 0);

        public GameManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += ClientConnected;
        }

        public void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            e.Client.MessageReceived += ClientMessageReceived;
        }

        public void ClientMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage() as Message)
            {
                if (message.Tag == Tags.GameStartTag)
                {
                    GameSetup gameSetup = new GameSetup();
                    
                }
            }
        }
    }
}
