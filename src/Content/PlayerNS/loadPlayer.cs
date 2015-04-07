using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content.PlayerNS;

namespace Reality.Content.PlayerNS
{
    class loadPlayer
    {
        private static Player player;

        public static void createPlayer()
        {
            player = new Player(250, 250, 100);
        }

        /// <summary>
        /// Gets the player, due to problems with accessibility levels the best way to do this is this.
        /// </summary>
        public static Player getPlayer()
        {
            return player;
        }

        /// <summary>
        /// Sets player for same reason as above.
        /// </summary>
        public static void setPlayer(Player newPlayer)
        {
            player = newPlayer;
        }
    }
}
