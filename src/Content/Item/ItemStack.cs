using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Reality.Content.Item;

namespace Reality.Content.Item
{
    class ItemStack
    {
        private Item item;
        private int total;
        private int maxStack;

        public ItemStack(Item itemObj, int totalStack)
        {
            maxStack = itemObj.getMaxStack();
            if (totalStack < maxStack)
            {
                totalStack = maxStack;
            }
            item = itemObj;
            total = totalStack;
        }

        public Item getItem()
        {
            return item;
        }

        public int getMaxAmount() {
            return maxStack;
        }

        public int getTotalItems()
        {
            return total;
        }

        public void changeItem(Item newItem)
        {
            item = newItem;
            maxStack = newItem.getMaxStack();
        }

        public void changeTotal(int newTotal)
        {   
            if ( maxStack > newTotal ) 
            {
                total = newTotal;
            }
            else
            {
                total = maxStack;
            }
        }

    }
}
