using Reality.Content.World;
using Reality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Reality.Content.Player
{
    class Player
    {
        private int x;
        private int y;
        private int offx;
        private int offy;
        private int speed;
        private int health;
        private int maxHealth;
        private int block;

        public Player(int px, int py, int mHealth)
        {
            x = px;
            y = py;
            offx = 0;
            offy = 0;
            maxHealth = mHealth;
            health = 0;
            speed = 6;
            block = 1;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getOffX()
        {
            return offx;
        }

        public int getOffY()
        {
            return offy;
        }

        public void setPos(int px, int py)
        {
            x = px;
            y = py;
        }

        public void setPos(int px, int py, int ox, int oy)
        {
            x = px;
            y = py;
            offx = ox;
            offy = oy;
        }

        public void setSpeed(int nspeed)
        {
            speed = nspeed;
        }

        public int getSpeed()
        {
            return speed;
        }

        public int getHealth()
        {
            return health;
        }

        public int getMaxHealth()
        {
            return maxHealth;
        }

        public void setHealth(int h)
        {
            health = h;
        }

        public void setMaxHealth(int h)
        {
            maxHealth = h;
        }

        public int getBlockHolding()
        {
            return block;
        }

        public void setBlockHolding(int id)
        {
            block = id;
        }

        //Movement

        public void moveDown(WorldGen world)
        {
            if (getY() >= world.getHeight() - Game1.renderDistanceY / 2)
            {
                //Just do nothing. Why Do Anything?
            }
            if (world.getBlockAt(getX(), (getY() + 1)) == 0)
            {
                if (getOffY() > 27 - speed)
                {
                    int fixedpos = getOffY() + speed - 27;
                    setPos(getX(), getY() + 1, getOffX(), fixedpos);
                    if (world.getBlockAt(getX(), getY() + 2) != 0 && getOffY() > 0)
                    {
                        setPos(getX(), getY(), getOffX(), 0);
                    }
                }
                else
                {
                    setPos(getX(), getY(), getOffX(), getOffY() + speed);
                }
            }

            else if (world.getBlockAt(getX(), getY() + 1) != 0 && getOffY() >= 0)
            {
                setPos(getX(), getY(), getOffX(), 0);
                Game1.g = true;
            }

            else if (getOffY() < speed)
            {
                setPos(getX(), getY(), getOffX(), getOffY() + speed);
            }
        }

        public void moveUp(WorldGen world)
        {
            if (getY() <= Game1.renderDistanceY / 2)
            {
                //Just do nothing. Why Do Anything?
            }
            if (world.getBlockAt(getX(), (getY() - 1)) == 0)
            {
                if (getOffY() < speed)
                {
                    int fixedpos = 27 - speed + getOffY();
                    setPos(getX(), getY() - 1, getOffX(), fixedpos);
                }
                else
                {
                    setPos(getX(), getY(), getOffX(), getOffY() - speed);
                }
            }

            else if (world.getBlockAt(getX(), getY() + 1) != 0 && getOffY() <= speed)
            {
                setPos(getX(), getY(), getOffX(), 0);
            }

            else if (getOffY() > speed)
            {
                setPos(getX(), getY(), getOffX(), getOffY() - speed);
            }
        }

        public void moveLeft(WorldGen world)
        {
            if (getX() <= Game1.renderDistanceX / 2)
            {
                //Just do nothing. Why Do Anything?
            }
            if (world.getBlockAt((getX() - 1), getY()) == 0)
            {
                if (getOffX() < speed)
                {
                    int fixedpos = 27 - speed + getOffX();
                    setPos(getX() - 1, getY(), fixedpos, getOffY());
                }

                else
                {
                    setPos(getX(), getY(), getOffX() - speed, getOffY());
                }
            }

            else if (world.getBlockAt((getX() - 1), getY()) != 0 && getOffX() <= speed)
            {
                setPos(getX(), getY(), 0, getOffY());
            }

            else if (getOffX() > speed)
            {
                setPos(getX(), getY(), getOffX() - speed, getOffY());
            }
        }

        public void moveRight(WorldGen world)
        {
            if (getX() >= world.getWidth() - Game1.renderDistanceX / 2)
            {
                //Just do nothing. Why Do Anything?
            }

            else if (world.getBlockAt((getX() + 1), getY()) == 0)
            {
                if (getOffX() > 27 - speed)
                {
                    int fixedpos = getOffX() + speed - 27;
                    setPos(getX() + 1, getY(), fixedpos, getOffY());
                    if (world.getBlockAt(getX() + 2, getY()) != 0 && getOffX() > 0)
                    {
                        setPos(getX(), getY(), 0, getOffY());
                    }
                }
                else
                {
                    setPos(getX(), getY(), getOffX() + speed, getOffY());
                }
            }

            else if (world.getBlockAt((getX() + 1), getY()) != 0 && getOffX() >= 0)
            {
                setPos(getX(), getY(), 0, getOffY());
            }

            else if (getOffX() < speed)
            {
                setPos(getX(), getY(), getOffX() + speed, getOffY());
            }
        }
    }
}
