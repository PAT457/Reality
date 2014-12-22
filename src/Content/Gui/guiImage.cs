using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class guiImage
    {
        private Vector2 pos;
        private Texture2D text;
        
        public guiImage(int posx, int posy, Texture2D t)
        {
            pos.X = posx;
            pos.Y = posy;
            text = t;
        }

        public Vector2 getImagePos()
        {
            return pos;
        }

        public void setImage(Texture2D tex)
        {
            text = tex;
        }

        public Texture2D getImage()
        {
            return text;
        }
    }
}
