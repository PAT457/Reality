using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class Gui
    {
        public guiSlot[] slots;
        public guiImage[] textures;
        public guiText[] text;
        private static ContentManager Content;

        public void init() { }

        public int getItemIn(int slotID)
        {
            return slots[slotID].getItemInSlot();
        }

        public Texture2D getImageIn(int imageID)
        {
            return textures[imageID].getImage();
        }

        public void setItemIn(int slotID, int itemID)
        {
            slots[slotID].setItemInSlot(itemID);
        }

        public void setTextureIn(int imageID, Texture2D texture)
        {
            textures[imageID].setImage(texture);
        }

        public int getSlotAmmount()
        {
            return 3; //Change soon.
        }

        public int getImageAmmount()
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

        public int getStuff()
        {
            return 5034252;
        }

        public void updateGUI() { }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }
    }
}
