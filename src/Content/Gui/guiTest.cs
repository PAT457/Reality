using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class guiTest : Gui
    {
        public guiSlot[] slots = new guiSlot[50];
        public guiImage[] textures = new guiImage[50];
        public guiText[] text = new guiText[50];             //A limit of 50 is set on each object to prevent overuse and RAM issues. Even 50 seems a little crazy - I might lower it to 25 or something.
        private static ContentManager Content;

        public guiTest()
        {
            Console.Out.WriteLine("[Reality][Notice] GUI created. Waiting for content to be suplied.");
        }

        public void init()
        {
            slots[0] = new guiSlot(2, 2, true);
            slots[1] = new guiSlot(2, 73, true);
            slots[2] = new guiSlot(73, 35, true);

            textures[0] = new guiImage(100, 100, Content.Load<Texture2D>("Blocks/grass.1111"));

            text[0] = new guiText("Strange Crafting Table...", 200, 100);
        }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }

        public int getItemIn(int slotID)
        {
            return slots[slotID].getItemInSlot();
        }

        public Texture2D getImageIn(int imageID)
        {
            return textures[imageID].getImage();
        }

        public String getTextIn(int textID)
        {
            return text[textID].getText();
        }

        public void setItemIn(int slotID, int itemID)
        {
            slots[slotID].setItemInSlot(itemID);
        }

        public void setTextureIn(int imageID, Texture2D texture)
        {
            textures[imageID].setImage(texture);
        }

        public void setTextIn(int textID, String newText)
        {
            text[textID].setText(newText);
        }

        public int getSlotAmmount()
        {
            return 3; //Change soon.
        }

        public int getImageAmmount()
        {
            return 1; //Change Soon.
        }

        public int getTextAmmount()
        {
            return 1; //Change Soon.
        }

        public Vector2 getSlotPos(int slotID)
        {
            return slots[slotID].getPos();
        }
        public Vector2 getImagePos(int imageID)
        {
            return textures[imageID].getImagePos();
        }

        public Vector2 getTextPos(int textID)
        {
            return text[textID].getTextPos();
        }

        public void updateGUI()
        {
            if (slots[0].getItemInSlot() == 1)
            {
                if (slots[1].getItemInSlot() == 2)
                {
                    slots[2].setItemInSlot(3);
                }
            }
        }
    }
}
