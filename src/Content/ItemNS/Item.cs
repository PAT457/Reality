using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content.PlayerNS;
using Reality.Content;
using Reality.Content.Block;
using Microsoft.Xna.Framework.Content;

namespace Reality.Content.ItemNS
{
    class Item
    {
        private static Item blankItem;
        private static Item[] items = new Item[500];
        public static Func<bool>[] itemFuncs = new Func<bool>[500]; //For doing stuff like adding updates on Item Entitys, onuses, etc.
        //private String[] sides;
        private Texture2D[] textures;
        private Texture2D texture;
        private String basename;
        private int id;
        private String name;
        private static ContentManager Content;
        private int stackLimit;
        private Boolean isBlockItem = false;
        private Block.Block itemBlock;
        private static Boolean allowEdit = true;

        //public static String[] sidesAll = new String[16] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        public Item(int ItemId, String InGameName, String TextureBaseName, int MaxStack)
        {
            isBlockItem = false;
            id = ItemId;
            stackLimit = MaxStack;
            name = InGameName;
            basename = TextureBaseName;
            Console.WriteLine(TextureBaseName);
            textures = new Texture2D[1];
            textures[0] = Content.Load<Texture2D>("Items/" + TextureBaseName);
            texture = textures[0];
        }

        public Item(Block.Block block, int ItemId, String InGameName, int MaxStack)
        {
            itemBlock = block;
            isBlockItem = true;
            id = ItemId;
            stackLimit = MaxStack;
            name = InGameName;
            //basename = TextureBaseName;
            texture = Content.Load<Texture2D>("Items/" + basename);
        }

        public void addUpdate(int id, Func<bool> newMethod)
        {
            itemFuncs[id] = newMethod;
        }

        public static Boolean idRegistered(int id)
        {
            //return !items[id].Equals(blankItem); buggy will check out later
            return true; //if its made by use for now we know that there will be no mistakes.
        }


        public static void registerItem(Item item)
        {
                items[item.id] = item;
        }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }

        public Boolean isBlock()
        {
            return isBlockItem;
        }

        public Block.Block getBlock()
        {
            return itemBlock;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public String getDisplayName()
        {
            return name;
        }

        public String getTextureName()
        {
            return basename;
        }

        public int getMaxStack()
        {
            return stackLimit;
        }

        public int getID()
        {
            return id;
        }

        public static Item[] getItems()
        {
            return items;
        }

        public static void overrideRegistry(Item[] newRegistry)
        {
            if (allowEdit)
            {
                items = newRegistry;
            }
            else
            {
                return;
            }
        }

        public static Item getItemByID(int id)
        {
            for (int i = 1; i < 500; i++)
            {
                Console.WriteLine("T: " + i);
                if (items[i] != null)
                {
                    if (items[i].id == id)
                    {
                        Console.WriteLine("Returning: " + i + " as it is available.");
                        return items[i];
                    }
                }
            }
            return null;
        }

        public static int getIdByItem(Item item)
        {
            return item.id;
        }

        public int getMaxStack(Item item)
        {
            return stackLimit;
        }

        public static void update()
        {
            for (int i = 0; i < 500; i++)
            {
                if (itemFuncs[i] != null)
                {
                    bool diditSucceed = itemFuncs[i]();
                    if (!diditSucceed)
                    {
                        Console.Out.WriteLine("[Reality] An ItemEntity ( ID " + items[i].getID() + " ) has failed to update. Disregarding, but please be aware of this.");
                    }
                }
            }
        }
    }
}
