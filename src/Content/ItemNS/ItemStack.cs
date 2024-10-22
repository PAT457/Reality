﻿using Reality.Content.ItemEntityNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Reality.Content.Item;

namespace Reality.Content.ItemNS
{
    class ItemStack
    {
        private Item item;
        private ItemEntity itement;
        private Block.Block block;
        private int total;
        private int maxStack;
        private bool type;

        public ItemStack(Item itemObj, int totalStack)
        {
            maxStack = itemObj.getMaxStack();
            if (totalStack < maxStack)
            {
                totalStack = maxStack;
            }
            item = itemObj;
            total = totalStack;
            type = false;
        }

        public ItemStack(ItemEntity itemObj, int totalStack)
        {
            maxStack = itemObj.getMaxStack();
            if (totalStack < maxStack)
            {
                totalStack = maxStack;
            }
            itement = itemObj;
            total = totalStack;
            type = false;
        }

        public ItemStack(Block.Block blockObj, int totalStack)
        {
            maxStack = blockObj.getMaxStack();
            if (totalStack < maxStack)
            {
                totalStack = maxStack;
            }
            block = blockObj;
            total = totalStack;
            type = true;
        }

        public Item getItem()
        {
            return item;
        }

        public Block.Block getBlock()
        {
            return block;
        }

        public Object getObject()
        {
            if (type) return block;
            else return item;
        }

        public int getID()
        {
            if (type) return block.getID();
            else return item.getID();
        }

        public bool getType()
        {
            return type;
        }

        public int getMaxAmount()
        {
            return maxStack;
        }

        public int getAmount()
        {
            return total;
        }

        public void changeItem(Item newItem)
        {
            item = newItem;
            maxStack = newItem.getMaxStack();
            type = false;
        }

        public void changeBlock(Block.Block newBlock)
        {
            block = newBlock;
            maxStack = newBlock.getMaxStack();
            type = true;
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
