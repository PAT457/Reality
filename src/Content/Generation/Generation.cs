using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reality.Content.World;

namespace Reality.Content.Generation
{
    class Generation
    {
        public static WorldGen genPlain()
        {
            Random random = new Random();
            WorldGen world = new WorldGen(500, 500);
            int y = 250;
            for (int i = 0; i < 500; i++)
            {
                //Levetates Land.
                if (random.Next(10) >= 6 && random.Next(10) <= 8)
                {
                    y = y + 1;
                }
                if (random.Next(10) >= 8 && random.Next(10) <= 9)
                {
                   y = y - 1;
                }
                
                //Check to make sure that the land doesnt get too high (or low.)
                if (y < 240)
                {
                    y++;
                }
                if (y > 260)
                {
                    y--;
                }

                //Set Down Blocks
                world.setBlock(i, y, 1);
                i++;
                world.setBlock(i, y, 1);
                i++;
                world.setBlock(i, y, 1);
            }

            //Place Down Dirt And Stone
            for (int oy = 1; oy < world.getHeight()-230; oy++ )
            {
                for (int ox = 1; ox < world.getWidth(); ox++)
                {
                    if (world.getBlockAt(ox, oy-1) == 1 || world.getBlockAt(ox, oy-1) == 2)
                    {
                        world.setBlock(ox, oy, 2);
                    }
                }
            }

            for (int oy = world.getHeight()-230; oy < world.getHeight(); oy++)
            {
                for (int ox = 1; ox < world.getWidth(); ox++)
                {
                    if (world.getBlockAt(ox, oy - 1) == 2 || world.getBlockAt(ox, oy - 1) == 3)
                    {
                        world.setBlock(ox, oy, 3);
                    }
                }
            }

            int[,] bg = new int[500,500];

            Array.Copy(world.getWorld(), bg, world.getWorld().Length);

            world.setBg(bg);

            return world;
        }
    }
}
