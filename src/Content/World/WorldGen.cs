﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.World
{
    class WorldGen
    {
        private int[,] world;
        private int width;
        private int height;

        public WorldGen(int w, int h)
        {
            //Set Variables
            width = w;
            height = h;
            world = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    world[y, x] = 0;
                }
            }
        }

        public int[,] getWorld()
        {
            return world;
        }

        public int getBlockAt(int x, int y)
        {
            return world[y, x];
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public bool setBlock(int x, int y, int Id)
        {
            if (x >= width || x <= 0 || y >= height || y <= 0)  //Cheks if the block placment is out of bounds.
            {
                Console.WriteLine("[Reality][Notice] Tried To Place A Block Out Of Bounds Of The World, Discarded.");
                return false;
            }
            //Console.WriteLine("Debug: {0}  {1}", x, y);
            world[y, x] = Id;
            return true;
        }

        /// <summary>
        /// Gets if there is a block other then air around it.
        /// </summary>
        /// <param name="x">X of the block</param>
        /// <param name="y">Y of the block</param>
        /// <returns>A boolean if there is a block or not.</returns>
        public bool hasSurroundingBlock(int x, int y)
        {
            if (getBlockAt(x-1, y) != 0)
            {
                return true;
            }
            if (getBlockAt(x + 1, y) != 0)
            {
                return true;
            }
            if (getBlockAt(x, y - 1) != 0)
            {
                return true;
            }
            if (getBlockAt(x, y + 1) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
