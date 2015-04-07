using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reality.Content.ItemNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Reality.Content.ItemEntityNS
{
    class ItemEntity
    {

        //                                                      !!!!
        //THIS FILE IS NO LONGER USED                           !!!!
        //IT IS NOW INTEGRATED WITH THE ITEM CLASS ITSELF       !!!!
        //IT WILL BE KEPT HERE FOR A FEW MORE BUILDS            !!!!
        //                                                      !!!!

        public static ItemEntity[] itemEntitys = new ItemEntity[500];
        public static Func<bool>[] itemEntityFuncs = new Func<bool>[500];
        private int strength = 100;
        private int maxStrength = 100;
        private Item item;
        private Reality.Content.ItemNS.Condition condition;

        
        public ItemEntity(int ItemId, String InGameName, String TextureBaseName, int MaxStack)
        {
            item = new Item(ItemId, InGameName, TextureBaseName, MaxStack);
        }

        public static void registerItemEntity(ItemEntity itement)
        {
            itemEntitys[itement.getID()] = itement;
        }

        public Boolean isBlock()
        {
            return item.isBlock();
        }

        public Block.Block getBlock()
        {
            return item.getBlock();
        }

        public Texture2D getTexture()
        {
            return item.getTexture();
        }

        public String getDisplayName()
        {
            return item.getDisplayName();
        }

        public String getTextureName()
        {
            return item.getTextureName();
        }

        public int getMaxStack()
        {
            return item.getMaxStack();
        }

        public int getID()
        {
            return item.getID();
        }

        public static ItemEntity getItemByID(int id)
        {
            for (int i = 1; i <= 500; i++)
            {
                if (itemEntitys[i] != null)
                {
                    if (itemEntitys[i].getID() == id)
                    {
                        return itemEntitys[i];
                    }
                }
            }
            return null;
        }

        public void addUpdate(int id, Func<bool> newMethod)
        {
            itemEntityFuncs[id] = newMethod;
        }

        public static void update()
        {
            for (int i = 0; i < 500; i++)
            {
                if (itemEntityFuncs[i] != null)
                {
                    bool diditSucceed = itemEntityFuncs[i]();
                    if (!diditSucceed)
                    {
                        Console.Out.WriteLine("[Reality] An ItemEntity ( ID " + itemEntitys[i].getID() + " ) has failed to update. Disregarding, but please be aware of this.");
                    }
                }
            }
        }
    }
}
