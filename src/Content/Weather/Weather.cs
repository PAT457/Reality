using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Weather
{
    class Weather
    {

        private static Weather[] weathers = new Weather[50];
        private int fallingSpeed;
        private Texture2D texture;
        private String baseName;
        private int id;
        private static ContentManager Content;

        public Weather(int id, int fallingSpeed, String baseName)
        {
            this.fallingSpeed = fallingSpeed;
            this.baseName = baseName;
            this.id = id;

            //load texture
            texture = Content.Load<Texture2D>("assets/" + baseName);
        }

        public static void supplyContent(ContentManager Con)
        {
            Content = Con;
        }

        public static void registerWeather(Weather weather)
        {
            weathers[weather.getID()] = weather;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public int getFallingSpeed()
        {
            return fallingSpeed;
        }

        public int getID()
        {
            return id;
        }

        public void setFallingSpeed(int newSpeed)
        {
            fallingSpeed = newSpeed;
        }
    }
}
