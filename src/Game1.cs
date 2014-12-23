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
using Reality.Content.Item;
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
        //private Color color;
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

        //Texture2D heart;

        private Texture2D hHud;
        public static bool g = false;
        public static bool gravity = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont main;

        float[,] light;

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
            renderDistanceX = screenW / 24 + 2;
            renderDistanceY = screenH / 24 + 1;
            this.graphics.ApplyChanges();
            //End Graphic Int.
            Block.supplyContent(this.Content);
            guiTest.supplyContent(this.Content);
            //gui.supplyDrawingEngine(this.graphics, this.spriteBatch);

            light = new float[screenW/2, screenH/2];

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
            grass = new Block(1, "Grass", Block.sidesAll, "grass", 100);
            dirt = new Block(2, "Dirt", Block.sidesAll, "dirt", 100);
            stone = new Block(3, "Stone", Block.sidesAll, "stone", 100);

            Block.registerBlock(grass);
            Block.registerBlock(dirt);
            Block.registerBlock(stone);

            bkg = Content.Load<Texture2D>("bkg");
            invis = Content.Load<Texture2D>("inv");
            //font = Content.Load<SpriteFont>("segoe");
            main = Content.Load<SpriteFont>("main");
            hHud = Content.Load<Texture2D>("healthhud");
            guiFrame = Content.Load<Texture2D>("assets/GUIframe");
            guiSlot = Content.Load<Texture2D>("assets/GUIslot");

            //heart = Content.Load<Texture2D>("heart");

            player.setItem(0, 0, new ItemStack(Block.getBlockByID(1), 1));
            player.setItem(1, 0, new ItemStack(Block.getBlockByID(2), 1));
            player.setItem(2, 0, new ItemStack(Block.getBlockByID(3), 1));

            //Init GUIs (TEMP)
            gui.init();
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

                int wx = (x / 24 + player.getX() - renderDistanceX / 2) + 1;
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
                        int beginx = Convert.ToInt32(sx) + (screenW / 2) - (guiFrame.Width * 4 / 2) + 16;
                        int beginy = Convert.ToInt32(sy) + (screenH / 2) - (guiFrame.Height * 4 / 2) + 16;

                        if (mousex >= beginx && mousey >= beginy && mousex <= beginx + (17 * 3) && mousey <= beginy + (17 * 3) && lastLMact == false)
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

                int wx = (x / 24 + player.getX() - renderDistanceX / 2) + 1;
                int wy = y / 24 + player.getY() - renderDistanceY / 2;

                if (world.getBlockAt(wx, wy) != 0 && lastRMact == false)
                {
                    drawGUI = true;
                }

                if (world.getBlockAt(wx, wy) == 0 && world.hasSurroundingBlock(wx, wy))
                {
                    if (player.getSelectedStack() != null && player.getSelectedStack().getType())
                    {
                        world.setBlock(wx, wy, player.getSelectedStack().getBlock().getID());
                    }
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
            if (wheelDirection == 1) player.slotDown();
            else if(wheelDirection == -1) player.slotUp();

            //Change Block By Numbers
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                player.setSlot(1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                player.setSlot(2);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                player.setSlot(3);
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

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                for (int y = 0; y < world.getHeight(); y++)
                {
                    for (int x = 0; x < world.getWidth(); x++)
                    {
                        world.setLight(x, y, 0);
                    }
                }
                PropogateLight(1.0f, player.getX(), player.getY(), true);
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

            for (int y = 0; y < world.getHeight(); y++)
            {
                for (int x = 0; x < world.getWidth(); x++)
                {
                    world.setLight(x, y, 0);
                }
            }

            PropogateLight(1.0f, player.getX(), player.getY(), true);

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
            int rx = player.getX() - renderDistanceX / 2;

            int offx = player.getOffX();
            int offy = player.getOffY();

            for (int y = 0; y <= renderDistanceY; y++)
            {
                rx = player.getX() - renderDistanceX / 2;

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
                    int bgID = world.getBgAt(rx, ry);

                    if (bgID != 0)
                    {
                        float level = world.getLightAt(rx, ry);
                        level -= 0.5f;
                        spriteBatch.Draw(Block.getBlockByID(bgID).getTexture(side), new Rectangle((x - 1) * 24 - offx, y * 24 - offy, 24, 24), new Color(level, level, level));
                    }

                    if (blockID != 0)
                    {
                        float level2 = world.getLightAt(rx, ry);
                        spriteBatch.Draw(Block.getBlockByID(blockID).getTexture(side), new Rectangle((x - 1) * 24 - offx, y * 24 - offy, 24, 24), new Color(level2, level2, level2));
                    }
                    rx++;
                }
                ry++;
            }

            //Lighting code
            /*
            for (int y1 = 0; y1 < screenH / 2; y1++)
            {
                for (int x1 = 0; x1 < screenW / 2; x1++)
                {
                    int x2 = (x1 / 24 + player.getX() - renderDistanceX / 2) + 1;
                    int y2 = y1 / 24 + player.getY() - renderDistanceY / 2;

                    x2 -= offx;
                    y2 -= offy;

                    light[x1, y1] = 0;

                    if (world.getBlockAt(x2, y2) == 0 && world.getBgAt(x2, y2) == 0)
                    {
                        light[x1, y1] = 1.0f;
                    }
                }
            }

            for (int y = 0; y < screenH/2; y++)
            {
                for (int x = 0; x < screenW/2; x++)
                {
                    int x2 = (x / 24 + player.getX() - renderDistanceX / 2) + 1;
                    int y2 = y / 24 + player.getY() - renderDistanceY / 2;

                    float level = 0f;

                    if (x > 0 && x < (screenW / 2) - 1 && y > 0 && y < (screenH / 2) - 1)
                    {
                        if (light[x + 1, y] > level) level = light[x + 1, y] - 0.025f;
                        if (light[x - 1, y] > level) level = light[x - 1, y] - 0.025f;
                        if (light[x, y + 1] > level) level = light[x, y + 1] - 0.025f;
                        if (light[x, y - 1] > level) level = light[x, y - 1] - 0.025f;
                    }

                    if(level <= 0)
                    {
                        level = 0f;
                    }

                    if(level >= 1)
                    {
                        level = 1f;
                    }

                    if (world.getBlockAt(x2, y2) == 0 && world.getBgAt(x2, y2) == 0)
                    {
                        level = 1.0f;
                    }

                    light[x, y] = level;
                    
                    spriteBatch.Draw(invis, new Rectangle(x * 2, y * 2, 2, 2), new Color(0f, 0f, 0f, 1 - level));
                }
            }
             * */

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
            spriteBatch.Draw(dirt.getTexture("0000"), new Rectangle(screenW / 24 / 2 * 24, screenH / 24 / 2 * 24, 24, 24), Color.White); // Player

            /* Old Hotbar Code
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
             */

            ItemStack[,] inv = player.getInventory();
            int sel = player.getSelectedSlot() - 1;

            for (int p = 0; p < 10; p++)
            {
                if (inv[p, 0] != null)
                {
                    bool b = inv[p, 0].getType();
                    Texture2D t;

                    if (b) t = inv[p, 0].getBlock().getTexture();
                    else t = inv[p, 0].getItem().getTexture();

                    if (p == sel)
                    {
                        spriteBatch.Draw(t, new Rectangle(p * 24 + (5*(p+1)), 5, 24, 24), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(t, new Rectangle(p * 24 + (5*(p+1)), 5, 24, 24), Color.White * 0.2f);
                    }

                    if (inv[p, 0].getAmount() > 1)
                        if (p == sel)
                            spriteBatch.DrawString(main, inv[p, 0].getAmount() + "", new Vector2(p * 24 + (5 * (p + 1)), 5), Color.White);
                        else
                            spriteBatch.DrawString(main, inv[p, 0].getAmount() + "", new Vector2(p * 24 + (5 * (p + 1)), 5), Color.White * 0.5f);
                }
            }

            //draw GUI if on
            if (drawGUI == true)
            {
                spriteBatch.Draw(guiFrame, new Rectangle((screenW / 2) - (guiFrame.Width * 4 / 2), (screenH / 2) - (guiFrame.Height * 4 / 2), 368, 288), Color.White);
                //draw slots
                for (int si = 0; si < gui.getSlotAmmount(); si++)
                {
                    Vector2 spos = gui.getSlotPos(si);
                    spriteBatch.Draw(guiSlot, new Rectangle(Convert.ToInt32(spos.X) + (screenW / 2) - (guiFrame.Width * 4 / 2) + 16, Convert.ToInt32(spos.Y) + (screenH / 2) - (guiFrame.Height * 4 / 2) + 16, 17 * 3, 17 * 3), Color.White);
                    if (gui.getItemIn(si) != 0)
                    {
                        spriteBatch.Draw(Block.getBlockByID(gui.getItemIn(si)).getTexture("1111"), new Rectangle(Convert.ToInt32(spos.X) + (screenW / 2) - (guiFrame.Width * 4 / 2) + 29, Convert.ToInt32(spos.Y) + (screenH / 2) - (guiFrame.Height * 4 / 2) + 29, 24, 24), Color.White);
                    }
                }

                //draw images
                for (int si = 0; si < gui.getImageAmmount(); si++)
                {
                    Vector2 spos = gui.getImagePos(si);
                    spriteBatch.Draw(gui.getImageIn(si), new Rectangle(Convert.ToInt32(spos.X) + (screenW / 2) - (guiFrame.Width * 4 / 2) + 16, Convert.ToInt32(spos.Y) + (screenH / 2) - (guiFrame.Height * 4 / 2) + 16, gui.getImageIn(si).Width, gui.getImageIn(si).Height), Color.White);
                }

                //Draw holding block.
                spriteBatch.Draw(Block.getBlockByID(clientHolding).getTexture("0000"), new Rectangle(mousex, mousey, 24, 24), Color.White);
            }

            //DRAW TEXT
            int sel2 = player.getSelectedSlot() - 1;

            if (inv[sel2, 0] != null)
            {
                if (inv[sel2, 0].getType())
                {
                    spriteBatch.DrawString(main, inv[sel2, 0].getBlock().getDisplayName(), new Vector2(screenW - 100, 10), Color.White);
                }
            }

            //Draw hearts
            /*
            for (int k = 0; k < (player.getHealth() / player.getMaxHealth()) * 10; k++)
            {
                //spriteBatch.Draw(heart, new Rectangle(screenW - ((k+1)*16), 4, 12, 12), Color.White);
            }
             */

            //spriteBatch.Draw(hHud, new Rectangle(0, 24, 241, 24), Color.White); disabled, work on later.
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void PropogateLight(float sourceLight, int toX, int toY, bool init)
        {
            int ry = player.getY() - renderDistanceY;
            int rx = player.getX() - renderDistanceX;

            int ry2 = player.getY() + renderDistanceY;
            int rx2 = player.getX() + renderDistanceX;

            if ((toX >= rx2 || toY >= ry2 || toX < rx || toY < ry ) && !init) return;

            if (toX >= world.getWidth() || toY >= world.getHeight() || toX < 0 || toY < 0) return;

            if (world.getLightAt(toX, toY) >= sourceLight) return;

            float newLight;

            if(world.getBlockAt(toX, toY) == 0 && world.getBgAt(toX, toY) != 0)
                newLight = sourceLight - 0.05f;
            else
                newLight = sourceLight - 0.25f;

            if (world.getBlockAt(toX, toY) == 0 && world.getBgAt(toX, toY) == 0) { newLight = 1; }
            //if (world.getBlockAt(toX, toY) == 4) { newLight = 2; }

            if (newLight < 0)
            {
                world.setLight(toX, toY, 0);
                return;
            }

            world.setLight(toX, toY, newLight);

            PropogateLight(newLight, toX + 1, toY, false);
            PropogateLight(newLight, toX - 1, toY, false);
            PropogateLight(newLight, toX, toY + 1, false);
            PropogateLight(newLight, toX, toY - 1, false);
        }
    }
}
