using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Gui
{
    class guiTest
    {
        public guiSlot[] slots = new guiSlot[50];

        public guiTest()
        {
            slots[0] = new guiSlot(2, 2, true);
            slots[1] = new guiSlot(2, 73, true);
            slots[2] = new guiSlot(73, 35, true);
        }

        public int getItemIn(int slotID)
        {
            return slots[slotID].getItemInSlot();
        }

        public void setItemIn(int slotID, int itemID)
        {
            slots[slotID].setItemInSlot(itemID);
        }

        public int getSlotAmmount()
        {
            return 3; //Change soon.
        }

        public Vector2 getSlotPos(int slotID)
        {
            return slots[slotID].getPos();
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
