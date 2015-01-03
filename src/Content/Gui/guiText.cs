using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class guiText
    {
        private String text;
        private Vector2 pos;

        public guiText(String txt, int posx, int posy)
        {
            text = txt;
            pos.X = posx;
            pos.Y = posy;
        }

        public void setText(String newText)
        {
            text = newText;
        }

        public String getText()
        {
            return text;
        }

        public void setTextPos(int newx, int newy)
        {
            pos.X = newx;
            pos.Y = newy;
        }

        public Vector2 getTextPos()
        {
            return pos;
        }

    }
}
