using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reality.Content.ItemNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content.ItemEntityNS;
using Reality.Content.Utils;
using Reality.Content.PlayerNS;
using Reality;

namespace Reality.Content.ItemEntityNS
{
    class itementityApple
    {

        private static FrameSleep fs = new FrameSleep();
        private static Player player;

        public static bool update()
        {
            //just for now, make it sleep for like 60 frames (about 1 second for a modern computer) and just add 1 object of whatever the player is holding, I guess.
            if (fs.wait(60))
            {
                player = loadPlayer.getPlayer();
                if (player.getSelectedStack() != null)
                {
                    player.getSelectedStack().changeTotal(player.getSelectedStack().getAmount() + 1);
                }
                loadPlayer.setPlayer(player);
            }

            return true;
        }

        public static void onUse()
        {
            //Nothing is done here.
        }
    }
}
