#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Reality.Content.World;
using Reality.Content.Player;
using Reality.Content.Block;
using Reality.Content.Generation;
using Reality.Content.Utils;
#endregion

namespace Reality
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        //[[-- THE GREAT NOTE NOTE THING --]]\\
        //Mouse Wheel directions:
        //-1 is down, 0 is still, +1 is up.

        private WorldGen world = Generation.genPlain();
        private Player player = new Player(250, 250, 100);
        private static int screenW = 800;
        private static int screenH = 450;
        public static int renderDistanceX;
        public static int renderDistanceY;
        public static int wheelDirection = 0;
        private int lastWheelP = 0;
        private Color color;
        private Block grass;
        private Block dirt;
        private Block stone;
        private int f = 0;
        private Texture2D bkg;
        private Texture2D invis;
        private int score = 0;
        private int speed = 6;
        private Texture2D hHud;
        public static bool g = false;
        public static bool gravity = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            //this.graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Graphic Int.
            Console.WriteLine("{0}    {1}", GraphicsDevice.Viewport.Width, GraphicsDevice.DisplayMode.Height);
            this.graphics.PreferredBackBufferWidth = screenW;
            this.graphics.PreferredBackBufferHeight = screenH;
            this.IsMouseVisible = true;
            renderDistanceX = screenW / 28 + 1;
            renderDistanceY = screenH / 28 + 1;
            this.graphics.ApplyChanges();
            //End Graphic Int.
            Block.supplyContent(this.Content);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grass = new Block(1, "Grass", Block.sidesAll, "grass");
            dirt = new Block(2, "Dirt", Block.sidesAll, "dirt");
            stone = new Block(3, "Stone", Block.sidesAll, "stone");

            Block.registerBlock(grass);
            Block.registerBlock(dirt);
            Block.registerBlock(stone);

            bkg = Content.Load<Texture2D>("bkg");
            invis = Content.Load<Texture2D>("inv");
            font = Content.Load<SpriteFont>("tempFont");
            hHud = Content.Load<Texture2D>("healthhud");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Controlls

            //Left Click
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                int wx = x / 28 + player.getX() - renderDistanceX / 2;
                int wy = y / 28 + player.getY() - renderDistanceY / 2;

                world.setBlock(wx, wy, 0);
            }

            //Right Click
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                int wx = x / 28 + player.getX() - renderDistanceX / 2;
                int wy = y / 28 + player.getY() - renderDistanceY / 2;

                if (world.getBlockAt(wx, wy) == 0 && world.hasSurroundingBlock(wx, wy))
                {
                    world.setBlock(wx, wy, player.getBlockHolding());
                }
            }

            //Mouse Scroll
            if (Mouse.GetState().ScrollWheelValue > lastWheelP)
            {
                wheelDirection = 1;
            }

            if (Mouse.GetState().ScrollWheelValue < lastWheelP)
            {
                wheelDirection = -1;
            }

            if (Mouse.GetState().ScrollWheelValue == lastWheelP)
            {
                wheelDirection = 0;
            }

            Console.WriteLine("{0}", Mouse.GetState().ScrollWheelValue);
            lastWheelP = Mouse.GetState().ScrollWheelValue;

            //Change block
            if (player.getBlockHolding() != 499 && wheelDirection == 1)
            {
                Block[] blocks = Block.getBlocks();
                if (blocks[player.getBlockHolding() + 1] != null)
                {
                    player.setBlockHolding(player.getBlockHolding() + 1);
                }
            }

            if (player.getBlockHolding() == 499 && wheelDirection == 1)
            {
                player.setBlockHolding(0);
            }

            if (player.getBlockHolding() != 1 && wheelDirection == -1)
            {
                player.setBlockHolding(player.getBlockHolding() - 1);
            }

            //Up
            if (Keyboard.GetState().IsKeyDown(Keys.W) && g)
            {
                player.moveUp(world);
            }
            //Down
            if (Keyboard.GetState().IsKeyDown(Keys.S) && g)
            {
                player.moveDown(world);
            }
            //Left
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.moveLeft(world);
            }

            //Right
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.moveRight(world);
            }

            //Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && g)
            {
                if (f == 0)
                {
                    f = f + 1;
                    gravity = false;
                    g = false;
                }
            }

            //Keep Jumping
            if (f != 0 && f < 20)
            {
                if (f < 15)
                {
                    player.moveUp(world);
                }
                f++;
            }

            else if (f >= 10)
            {
                f = 0;
                gravity = true;

            }

            //Gravity
            if (gravity)
            {
                player.moveDown(world);
            }

            // TODO: Add your update logic here
            score++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(bkg, new Rectangle(0, 0, screenW, screenH), Color.White); //Change it to fit the screen res, not just mine.
            int ry = player.getY() - renderDistanceY/2;
            int offx = player.getOffX();
            int offy = player.getOffY();
            for (int y = 0; y <= renderDistanceY; y++)
            {
                int rx = player.getX() - renderDistanceX/2;
                for (int x = 0; x <= renderDistanceX; x++)
                {
                    String side = "";
                    if (world.getBlockAt(rx - 1, ry) != 0)
                    {
                        side = side + "1";
                    }
                    else if (world.getBlockAt(rx - 1, ry) == 0)
                    {
                        side = side + "0";
                    }
                    if (world.getBlockAt(rx, ry - 1) != 0)
                    {
                        side = side + "1";
                    }
                    else if (world.getBlockAt(rx, ry - 1) == 0)
                    {
                        side = side + "0";
                    }
                    if (world.getBlockAt(rx + 1, ry) != 0)
                    {
                        side = side + "1";
                    }
                    else if (world.getBlockAt(rx + 1, ry) == 0)
                    {
                        side = side + "0";
                    }
                    if (world.getBlockAt(rx, ry + 1) != 0)
                    {
                        side = side + "1";
                    }
                    else if (world.getBlockAt(rx, ry + 1) == 0)
                    {
                        side = side + "0";
                    }

                    int blockID = world.getBlockAt(rx, ry);
                    if (blockID != 0)
                    {
                        spriteBatch.Draw(Block.getBlockByID(blockID).getTexture(side), new Rectangle(x * 28 - offx, y * 28 - offy, 28, 28), Color.White);
                    }
                    rx++;
                }
                ry++;
            }

            //Draw Light
            /*float col = 0f + (player.getY() * 4) * 0.0008f;
            Console.WriteLine("{0}", col);
            for (int y = 0; y <= renderDistanceY; y++)
            {
                float colx = 0f + ((player.getY() + y) * 4) * 0.0008f;
                for (int x = 0; x <= renderDistanceX; x++)
                {
                    col = colx;
                    for (int iy = 0; iy < 4; iy++)
                    {
                        col = col + 0.0008f;
                        for (int ix = 0; ix < 4; ix++)
                        {
                            spriteBatch.Draw(invis, new Rectangle(x * 28 - offx + (ix * 7), y * 28 - offy + (iy * 7), 7, 7), Color.Black * col);
                        }
                    }
                }
            }*/
          

            //spriteBatch.DrawString(font, "Player Pos: X: "+player.getX()+"."+player.getOffX()+"     Y: "+player.getY()+"."+player.getOffY(), new Vector2(5, 5), Color.White);
            //Get Light Level
            /*int charlightLevel = 250;
            for (int i = 260; i <= player.getY(); i++)
            {
                if (charlightLevel >= 30)
                {
                    charlightLevel = charlightLevel - 8;
                }
            }*/
            //color = new Color(charlightLevel, charlightLevel, charlightLevel);
            spriteBatch.Draw(dirt.getTexture("0000"), new Rectangle(screenW / 28 / 2 * 28, screenH / 28 / 2 * 28, 28, 28), Color.White);
            //spriteBatch.Draw(hHud, new Rectangle(0, 24, 241, 28), Color.White); disabled, work on later.
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
