using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content;
using Microsoft.Xna.Framework.Content;

namespace Reality.Content.Block
{
    class Block
    {
        private static Block[] blocks = new Block[500]; 
        private String[] sides;
        private Texture2D[] textures;
        private String basename;
        private int id;
        private String name;
        private static ContentManager Content;

        public static String[] sidesAll = new String[16] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        public Block(int BlockId, String InGameName, String[] TextureSides, String TextureBaseName)
        {
            id = BlockId;
            name = InGameName;
            sides = TextureSides;
            basename = TextureBaseName;
            textures = new Texture2D[sides.Length];
            for (int f = 0;  f < sides.Length; f++)
            {
                textures[f] = Content.Load<Texture2D>("Blocks/"+basename+"."+sides[f]);
            }
        }

        public static void registerBlock(Block block)
        {
            blocks[block.id] = block;
        }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }

        public Texture2D getTexture(String side)
        {
            for (int i = 0; i <= sides.Length; i++)
            {
                if (sides[i] == side)
                {
                    return textures[i];
                }
            }
            return null;
        }

        public static Block[] getBlocks()
        {
            return blocks;
        }

        public static Block getBlockByID(int id)
        {
            for (int i = 1; i <= 500; i++)
            {
                if (blocks[i] != null)
                {
                    if (blocks[i].id == id)
                    {
                        return blocks[i];
                    }
                }
            }
            return null;
        }

        public static int getIdByBlock(Block block)
        {
            return block.id;
        }

    }
}
