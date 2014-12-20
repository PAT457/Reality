using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Reality.Content.Gui
{

    class gui
    {
        //private static GraphicDeviceManager graphics;
        //private static SpriteBatch spriteBatch;

        public static void supplyDrawingEngine(GraphicsDeviceManager graph, SpriteBatch sb)
        {
            //graphics = graph;
            //spriteBatch = sb;
        }

        public static void drawBeginGUI()
        {
            //Texture2D rect = new Texture2D(graphics.GraphicsDevice, 80, 30);

            Color[] data = new Color[80 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            //rect.SetData(data);

            Vector2 coor = new Vector2(10, 20);
            //spriteBatch.Draw(rect, coor, Color.White);
        }
    }
}
