using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Reality.Content.Block
{
    class Block
    {
        private static Block blankBlock;
        private static Block[] blocks = new Block[500]; 
        private String[] sides;
        private Texture2D[] animatableTextures;
        private int animID = 0;
        private Texture2D[] textures;
        private String basename;
        private int id;
        private String name;
        private static ContentManager Content;
        private static Boolean allowEdit = true;
        private int stackLimit;

        //BLOCK MODIFIERS

        private bool animatable = false;
        private int offsetX = 0;
        private int offsetY = 0;
        private bool alwaysBackground = false;
        private bool bkgLightEffect = true;


        public static String[] sidesAll = new String[16] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        public Block(int BlockId, String InGameName, String[] sides, int MaxStack)
        {
            id = BlockId;
            name = InGameName;
            textures = new Texture2D[sides.Length];
            stackLimit = MaxStack;
            animatable = false;
        }

        /*public static Boolean idRegistered(int id)
        {
            return !blocks[id].Equals(blankBlock);
        }*/

        public static void registerBlock(Block block)
        {
            //if (!idRegistered(block.id))
            //{
                blocks[block.id] = block;
            //}
            //else
            //{
                //System.Console.WriteLine("A block attempted to register an ID slot that is already occupied.");
            //}
        }

        public int getID()
        {
            return id;
        }

        public void setTextures(String[] TextureSides, String TextureBaseName)
        {
            sides = TextureSides;
            basename = TextureBaseName;

            for (int f = 0; f < sides.Length; f++)
            {
                textures[f] = Content.Load<Texture2D>("Blocks/" + basename + "." + "1111");
            }
        }

        public void setTextures(String animBase)
        {
            animatable = true;
            basename = animBase;

            string folder = AppDomain.CurrentDomain.BaseDirectory + "/Content/Blocks/" + animBase;
            animatableTextures = new Texture2D[Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly).Length];
            for (int f = 0; f < Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly).Length; f++)
            {
                Console.Out.WriteLine("it is: " + f + 1);
                int nm = f + 1;
                animatableTextures[f] = Content.Load<Texture2D>("Blocks/" + basename + "/" + basename + "." + nm);
            }
        }

        public void setCurrentTexture(int aid)
        {
            animID = aid;
        }
        
        public void setAnimatable(bool setA)
        {
            animatable = setA;
        }

        public String getDisplayName()
        {
            return name;
        }

        public String getTextureName()
        {
            return basename;
        }

        public int getTextureAmmount()
        {
            return animatableTextures.Length;
        }

        public int getCurrentTexture()
        {
            return animID;
        }

        public void setAlwaysBackground(bool isAlways)
        {
            alwaysBackground = isAlways;
        }

        public bool isAlwaysBackground()
        {
            return alwaysBackground;
        }

        public void setBkgLightEffect(bool setBkg)
        {
            bkgLightEffect = setBkg;
        }

        public bool getBkgLightEffect()
        {
            return bkgLightEffect;
        }

        public void giveGUI()
        {

        }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }

        public Texture2D getTexture(String side)
        {

            if (animatable)
            {
                return animatableTextures[animID];
            }

            for (int i = 0; i <= sides.Length; i++)
            {
                if (sides[i] == side)
                {
                    return textures[i];
                }
            }
            return null;
        }

        public Texture2D getTexture()
        {
            if (animatable)
            {
                return animatableTextures[animID];
            }
            else
            {
                return textures[15];
            }
        }

        public int getMaxStack()
        {
            return stackLimit;
        }

        public bool isAnimatable()
        {
            return animatable;
        }

        public void setOffsetX(int ox)
        {
            offsetX = ox;
        }

        public void setOffsetY(int oy)
        {
            offsetY = oy;
        }

        public int getOffsetX()
        {
            return offsetX;
        }

        public int getOffsetY()
        {
            return offsetY;
        }

        public static Block[] getBlocks()
        {
            return blocks;
        }

        public static void overrideRegistry(Block[] newRegistry)
        {
            if (allowEdit)
            {
                blocks = newRegistry;
            }
            else
            {
                return;
            }
        }

        public static Block getBlockByID(int id)
        {
            if (id < 0) id *= -1;

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
