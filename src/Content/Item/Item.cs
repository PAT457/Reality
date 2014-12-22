using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content.Player;
using Reality.Content;
using Reality.Content.Block;
using Microsoft.Xna.Framework.Content;

namespace Reality.Content.Item
{
    class Item
    {
        private static Item blankItem;
        private static Item[] items = new Item[500];
        //private String[] sides;
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
            texture = Content.Load<Texture2D>("Items/" + basename);
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

        public static Boolean idRegistered(int id)
        {
            return !items[id].Equals(blankItem);
        }


        public static void registerItem(Item item)
        {
            if ( !idRegistered(item.getID()) )
            {
                items[item.id] = item;
            }
            else
            {
                System.Console.WriteLine("An item attempted to register an ID slot that is already occupied.");
            }
        }

        public void supplyContent(ContentManager Con)
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

        public void onUse(Player.Player user, int mouseButton)
        {

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
            for (int i = 1; i <= 500; i++)
            {
                if (items[i] != null)
                {
                    if (items[i].id == id)
                    {
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
    }
}
