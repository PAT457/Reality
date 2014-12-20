using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class guiSlot
    {
        private int inSlot = 0;
        private Vector2 pos;
        private bool isOutput;

        public guiSlot(int posx, int posy, bool io)
        {
            pos.X = posx;
            pos.Y = posy;
            isOutput = io;
        }

        public int getItemInSlot()
        {
            return inSlot;
        }

        public void setItemInSlot(int id)
        {
            inSlot = id;
        }

        public Vector2 getPos()
        {
            return pos;
        }
    }
}
