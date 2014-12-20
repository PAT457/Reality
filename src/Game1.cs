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
using Reality.Content.Gui;
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
        private bool drawGUI = true;
        private Texture2D guiFrame;
        private Texture2D guiSlot;
        private bool lastRMact;
        private bool lastLMact;
        private guiTest gui = new guiTest();
        private int clientHolding;
        private int mousex;
        private int mousey;

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
            renderDistanceX = screenW / 24 + 1;
            renderDistanceY = screenH / 24 + 1;
            this.graphics.ApplyChanges();
            //End Graphic Int.
            Block.supplyContent(this.Content);
            //gui.supplyDrawingEngine(this.graphics, this.spriteBatch);
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
            guiFrame = Content.Load<Texture2D>("assets/GUIframe");
            guiSlot = Content.Load<Texture2D>("assets/GUIslot");
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
            //Exit out of GUI
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                drawGUI = false;
            }

            //Controlls

            //set the cursor pos's.
            mousex = Mouse.GetState().X;
            mousey = Mouse.GetState().Y;

            //Left Click
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                int wx = x / 24 + player.getX() - renderDistanceX / 2;
                int wy = y / 24 + player.getY() - renderDistanceY / 2;

                world.setBlock(wx, wy, 0);

                //Place down block if GUI open.
                if (drawGUI)
                {
                    for (int sp = 0; sp < gui.getSlotAmmount(); sp++)
                    {
                        Vector2 sposs = gui.getSlotPos(sp);
                        int sx = (int)sposs.X;
                        int sy = (int)sposs.Y;
                        int beginx = Convert.ToInt32(sx) + (screenW / 2) - (guiFrame.Width * 4 / 2)+16;
                        int beginy = Convert.ToInt32(sy) + (screenH / 2) - (guiFrame.Height * 4 / 2)+16;

                        if (mousex >= beginx && mousey >= beginy && mousex <= beginx+(17*3) && mousey <= beginy+(17*3) && lastLMact == false)
                        {
                            if (gui.getItemIn(sp) == 0)
                            {
                                gui.setItemIn(sp, clientHolding);
                            }

                            else
                            {
                                gui.setItemIn(sp, 0);
                            }
                        }
                    }
                }

                lastLMact = true;
            }

            //Right Click
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                int wx = x / 24 + player.getX() - renderDistanceX / 2;
                int wy = y / 24 + player.getY() - renderDistanceY / 2;

                if (world.getBlockAt(wx, wy) != 0 && lastRMact == false)
                {
                    drawGUI = true;
                }

                if (world.getBlockAt(wx, wy) == 0 && world.hasSurroundingBlock(wx, wy))
                {
                    world.setBlock(wx, wy, player.getBlockHolding());
                }

                lastRMact = true;
            }

            if (lastRMact)
            {
                if (Mouse.GetState().RightButton != ButtonState.Pressed)
                {
                    lastRMact = false;
                }
            }

            if (lastLMact)
            {
                if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                {
                    lastLMact = false;
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
            if (player.getBlockHolding() != 499 && wheelDirection == -1)
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

            if (player.getBlockHolding() != 1 && wheelDirection == 1)
            {
                player.setBlockHolding(player.getBlockHolding() - 1);
            }

            //Change Block By Numbers
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                player.setBlockHolding(1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                player.setBlockHolding(2);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                player.setBlockHolding(3);
            }

            //Up
            if (Keyboard.GetState().IsKeyDown(Keys.W) && g && player.getY() !=  renderDistanceY)
            {
                player.moveUp(world);
            }
            //Down
            if (Keyboard.GetState().IsKeyDown(Keys.S) && g && player.getY() != 500 - renderDistanceY)
            {
                player.moveDown(world);
            }
            //Left
            if (Keyboard.GetState().IsKeyDown(Keys.A) && player.getX() != renderDistanceX)
            {
                player.moveLeft(world);
            }

            //Right
            if (Keyboard.GetState().IsKeyDown(Keys.D) && player.getX() != 500-renderDistanceX)
            {
                player.moveRight(world);
            }

            //Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && g && player.getY() > renderDistanceY+5)
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
                    //--player.setSpeed(player.getSpeed()-1);
                }
                f++;
            }

            else if (f >= 10)
            {
                f = 0;
                gravity = true;
                player.setSpeed(6);
            }

            //Gravity
            if (gravity)
            {
                player.moveDown(world);
            }

            //Set the client holding to the play holding. Change later to more custon thingys mijigs things.
            clientHolding = player.getBlockHolding();

            //update current GUI if any
            if (drawGUI)
            {
                gui.updateGUI();
            }

            // TODO: Add your update logic here
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
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
                        spriteBatch.Draw(Block.getBlockByID(blockID).getTexture(side), new Rectangle(x * 24 - offx, y * 24 - offy, 24, 24), Color.White);
                    }
                    rx++;
                }
                ry++;
            }

            //Draw Light
           /* float col = 0f;
            Console.WriteLine("{0}", col);
            int rxl = player.getX() - renderDistanceX / 2;
            int ryl = player.getY() - renderDistanceY / 2;
            for (int x = 0; x <= renderDistanceX; x++)
            {
                col = 0f;
                for (int y = 0; y <= renderDistanceY; y++)
                {
                    for (int iy = 0; iy < 3; iy++)
                    {
                        if (col < 0.4f && world.getBlockAt(rxl, ryl) != 0)
                        {
                            col = col + 0.1f;
                        }
                        spriteBatch.Draw(invis, new Rectangle(x * 24 - offx, y * 24 - offy + (iy * 8), 24, 8), Color.Black*col);
                    }
                    ryl++;
                }
                rxl++;
            }
          

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
            spriteBatch.Draw(dirt.getTexture("0000"), new Rectangle(screenW / 24 / 2 * 24, screenH / 24 / 2 * 24, 24, 24), Color.White);
            for (int i = 0; i < 3; i++)
            {
                if (player.getBlockHolding() == i + 1)
                {
                    spriteBatch.Draw(Block.getBlockByID(i + 1).getTexture("0000"), new Rectangle(i * 24 + 10, 10, 24, 24), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Block.getBlockByID(i + 1).getTexture("0000"), new Rectangle(i * 24 + 10, 10, 24, 24), Color.White*0.2f);
                }
            }

            //draw GUI if on
            if (drawGUI == true)
            {
                spriteBatch.Draw(guiFrame, new Rectangle((screenW/2)-(guiFrame.Width*4/2), (screenH/2)-(guiFrame.Height*4/2), 368, 288), Color.White);
                for (int si = 0; si < gui.getSlotAmmount(); si++)
                {
                    Vector2 spos = gui.getSlotPos(si);
                    spriteBatch.Draw(guiSlot, new Rectangle(Convert.ToInt32(spos.X) + (screenW / 2) - (guiFrame.Width * 4 / 2)+16, Convert.ToInt32(spos.Y)+(screenH/2)-(guiFrame.Height*4/2)+16, 17 * 3, 17 * 3), Color.White);
                    if (gui.getItemIn(si) != 0)
                    {
                        spriteBatch.Draw(Block.getBlockByID(gui.getItemIn(si)).getTexture("1111"), new Rectangle(Convert.ToInt32(spos.X) + (screenW / 2) - (guiFrame.Width * 4 / 2) + 29, Convert.ToInt32(spos.Y) + (screenH / 2) - (guiFrame.Height * 4 / 2) + 29, 24, 24), Color.White);
                    }
                }

                //Draw holding block.
                spriteBatch.Draw(Block.getBlockByID(clientHolding).getTexture("0000"), new Rectangle(mousex, mousey, 24, 24), Color.White);
            }
            //spriteBatch.Draw(hHud, new Rectangle(0, 24, 241, 24), Color.White); disabled, work on later.
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
