using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.TileEntities
{
    class TileEntity
    {
        private static TileEntity[] tiles = new TileEntity[500];
        private int x;
        private int y;
        private int id;

        public TileEntity(int id, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public int getID()
        {
            return id;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void update() { }

        public void init() { }

        public static void registerTileEntity(TileEntity t)
        {
            tiles[t.id] = t;
        }
    }
}
