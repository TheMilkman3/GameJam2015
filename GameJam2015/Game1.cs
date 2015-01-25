#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace GameJam2015
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        readonly int PLAYER_SPEED = 5;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Timer aTime;
        Player player;
        Animation playerIdleAnimation, playerUpAnimation, playerDownAnimation, playerRightAnimation, stareAnimation;
        GoalBunny goalBunny;
        AudioManager audio;
        enum States { MainMenu, Instructions1, Instructions2, Play, PauseMenu, Credits };
        enum Direction { Up, Down, Left, Right, Still };
        enum MenuSelect { Start, Instructions, Exit };
        States CurrentState;
        Direction playerDirection;
        MenuSelect menuOption;
        List<Entity> entities = new List<Entity>();
        Entity menuStart, menuInstructions1, menuInstructions2, menuPause, cursor;
        AnimatedEntity stareBunny;
        Room room;
        Obstacles fullShelf1, fullShelf2, fullShelf3, fullShelf4, fullShelf5, fullShelf6, fullShelf7,
            fullShelf8, fullShelf9, fullShelf10, halfShelf1, halfShelf2, halfShelf3, halfShelf4, halfShelf5, halfShelf6,
            halfShelf7, halfShelf8, table1, table2, table3, bed1, bed2, bed3, bed4;
        Texture2D instructions1Texture, instructions2Texture, pauseTexture, endTexture;
        Texture2D stareTexture;
        int Invincibility = 2000;

        Entity ArrowU, ArrowD, ArrowL, ArrowR;
        Texture2D arrowU, arrowD, arrowL, arrowR;
        Boolean ShowUp, ShowDown, ShowLeft, ShowRight, Staying; //Booleans used with the Arrows
        float Px, Py; // simple place to know players positiong for use with the Arrows

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 1024;
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
            aTime = new Timer(1000);
            //aTime.Start();
            player = new Player();
            playerIdleAnimation = new Animation();
            playerUpAnimation = new Animation();
            playerDownAnimation = new Animation();
            playerRightAnimation = new Animation();
            stareAnimation = new Animation();
            stareBunny = new AnimatedEntity();

            goalBunny = new GoalBunny();
            audio = new AudioManager(Content.RootDirectory);
            CurrentState = States.MainMenu;
            playerDirection = Direction.Still;
            menuOption = MenuSelect.Start;

            room = new Room();

            table1 = new Obstacles();
            table2 = new Obstacles();
            table3 = new Obstacles();

            fullShelf1 = new Obstacles();
            fullShelf2 = new Obstacles();
            fullShelf3 = new Obstacles();
            fullShelf4 = new Obstacles();
            fullShelf5 = new Obstacles();
            fullShelf6 = new Obstacles();
            fullShelf7 = new Obstacles();
            fullShelf8 = new Obstacles();
            fullShelf9 = new Obstacles();
            fullShelf10 = new Obstacles();

            halfShelf1 = new Obstacles();
            halfShelf2 = new Obstacles();
            halfShelf3 = new Obstacles();
            halfShelf4 = new Obstacles();
            halfShelf5 = new Obstacles();
            halfShelf6 = new Obstacles();
            halfShelf7 = new Obstacles();
            halfShelf8 = new Obstacles();

            bed1 = new Obstacles();
            bed2 = new Obstacles();
            bed3 = new Obstacles();
            bed4 = new Obstacles();

            entities.Add(goalBunny);

            entities.Add(player);

            entities.Add(table1);
            entities.Add(table2);
            entities.Add(table3);

            entities.Add(fullShelf1);
            entities.Add(fullShelf2);
            entities.Add(fullShelf3);
            entities.Add(fullShelf4);
            entities.Add(fullShelf5);
            entities.Add(fullShelf6);
            entities.Add(fullShelf7);
            entities.Add(fullShelf8);
            /*entities.Add(fullShelf9);
            entities.Add(fullShelf10);*/

            entities.Add(halfShelf1);
            entities.Add(halfShelf2);
            entities.Add(halfShelf3);
            entities.Add(halfShelf4);
            entities.Add(halfShelf5);
            entities.Add(halfShelf6);
            entities.Add(halfShelf7);
            entities.Add(halfShelf8);

            entities.Add(bed1);
            entities.Add(bed2);
            entities.Add(bed3);
            entities.Add(bed4);

            menuInstructions1 = new Entity();
            menuInstructions2 = new Entity();



            ArrowD = new Entity();
            ArrowU = new Entity();
            ArrowL = new Entity();
            ArrowR = new Entity();
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

            //Sound effect loading. Plays while background music is playing.
            //bunnyMelt = Content.Load<SoundEffect>(@"Audio\\03_Child_Bride.wav");
            //bunnyMeltInstance = bunnyMelt.CreateInstance();



            //Initialize the room where all entities reside
            Texture2D roomTexture = Content.Load<Texture2D>("Sprites/Test Background 2.png");
            //Texture2D roomTexture = Content.Load<Texture2D>("Sprites/Title Screen.png");
            room.Initialize(roomTexture, .5f, Vector2.Zero);

            // TODO: use this.Content to load your game content here
            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Sprites/HeroIdleSheet.png");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 128, 256, 5, 80, Color.White, .2f, true);
            //bunnyMelt = Content.Load<SoundEffect>(@"Audio\\03_Child_Bride.wav");
            //bunnyMeltInstance = bunnyMelt.CreateInstance();

            Texture2D playerIdleTexture = Content.Load<Texture2D>("Sprites/HeroIdleSheet.png");
            playerIdleAnimation.Initialize(playerIdleTexture, Vector2.Zero, 128, 256, 5, 80, Color.White, .2f, true);

            Texture2D playerUpTexture = Content.Load<Texture2D>("Sprites/HeroWalkUPSHEET.png");
            playerUpAnimation.Initialize(playerUpTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .2f, true);

            Texture2D playerDownTexture = Content.Load<Texture2D>("Sprites/HeroWalkDownSheet.png");
            playerDownAnimation.Initialize(playerDownTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .2f, true);

            Texture2D playerRightTexture = Content.Load<Texture2D>("Sprites/HeroWalkSideSheet.png");
            playerRightAnimation.Initialize(playerRightTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .25f, true);

            // Load audio into the AudioManager. Plays the background music upon loading.
            audio.LoadAudio();
            audio.Play("fuq");

            // Load the player resources
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerIdleAnimation, 1, playerPosition, this);

            // Load the bunny resources
            Animation jumpAnimation = new Animation();
            Texture2D jumpTexture = Content.Load<Texture2D>("Sprites/BunJumpSheet.png");
            jumpAnimation.Initialize(jumpTexture, Vector2.Zero, 128, 128, 4, 80, Color.White, .25f, true);

            Vector2 bunnyPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height * (8 / 10));
            goalBunny.Initialize(jumpAnimation, 1f, bunnyPosition);

            Texture2D tableTexture = Content.Load<Texture2D>("Sprites/Table.png");
            table1.Initialize(tableTexture, .25f, new Vector2(1 * room.Width() / 8, 9 * room.Height() / 16));

            table2.Initialize(tableTexture, .25f, new Vector2(9 * room.Width() / 16, 9 * room.Height() / 16));

            table3.Initialize(tableTexture, .25f, new Vector2(3 * room.Width() / 4, 13 * room.Height() / 16));

            Texture2D shelfTexture = Content.Load<Texture2D>("Sprites/Shelf.png");
            fullShelf1.Initialize(shelfTexture, .25f, new Vector2(3 * room.Width() / 8, 3 * room.Height() / 4));

            fullShelf2.Initialize(shelfTexture, .25f, new Vector2(7 * room.Width() / 16, 3 * room.Height() / 4));

            fullShelf3.Initialize(shelfTexture, .25f, new Vector2(1 * room.Width() / 2, 3 * room.Height() / 4));

            fullShelf4.Initialize(shelfTexture, .25f, new Vector2(5 * room.Width() / 8, 1 * room.Height() / 4));

            fullShelf5.Initialize(shelfTexture, .25f, new Vector2(11 * room.Width() / 16, 1 * room.Height() / 4));

            fullShelf6.Initialize(shelfTexture, .25f, new Vector2(3 * room.Width() / 4, 1 * room.Height() / 4));

            fullShelf7.Initialize(shelfTexture, .25f, new Vector2(5 * room.Width() / 16, 1 * room.Height() / 8));

            fullShelf8.Initialize(shelfTexture, .25f, new Vector2(3 * room.Width() / 8, 1 * room.Height() / 8));

            //fullShelf10.Initialize(shelfTexture, .25f, new Vector2(1 * room.Width() / 4, 3 * room.Height() / 4));

            Texture2D sideShelfTexture = Content.Load<Texture2D>("Sprites/Shelf 2.png");
            halfShelf1.Initialize(sideShelfTexture, .25f, new Vector2(9 * room.Width() / 32, 1 * room.Height() / 8));

            halfShelf2.Initialize(sideShelfTexture, .25f, new Vector2(9 * room.Width() / 32, 1 * room.Height() / 4));

            halfShelf3.Initialize(sideShelfTexture, .25f, new Vector2(13 * room.Width() / 16, 3 * room.Height() / 8));

            halfShelf4.Initialize(sideShelfTexture, .25f, new Vector2(13 * room.Width() / 16, 1 * room.Height() / 2));

            halfShelf5.Initialize(sideShelfTexture, .25f, new Vector2(3 * room.Width() / 8, 1 * room.Height() / 2));

            halfShelf6.Initialize(sideShelfTexture, .25f, new Vector2(3 * room.Width() / 8, 5 * room.Height() / 8));

            halfShelf7.Initialize(sideShelfTexture, .25f, new Vector2(29 * room.Width() / 32, 5 * room.Height() / 8));

            halfShelf8.Initialize(sideShelfTexture, .25f, new Vector2(29 * room.Width() / 32, 3 * room.Height() / 4));

            Texture2D bedTexture = Content.Load<Texture2D>("Sprites/Bed.png");
            bed1.Initialize(bedTexture, .25f, new Vector2(1 * room.Width() / 16, 3 * room.Height() / 4));

            bed2.Initialize(bedTexture, .25f, new Vector2(3 * room.Width() / 16, 3 * room.Height() / 4));

            bed3.Initialize(bedTexture, .25f, new Vector2(1 * room.Width() / 16, 1 * room.Height() / 4));

            bed4.Initialize(bedTexture, .25f, new Vector2(3 * room.Width() / 16, 1 * room.Height() / 4));
           
            room.addItem(entities);
            instructions1Texture = Content.Load<Texture2D>("Sprites/Instruction Screen.png");
            instructions2Texture = Content.Load<Texture2D>("Sprites/Instructions 2.png");
            pauseTexture = Content.Load<Texture2D>("Sprites/Pause Screen.png");
            endTexture = Content.Load<Texture2D>("Sprites/End Screen.png");

            stareTexture = Content.Load<Texture2D>("Sprites/BunStareSheet.png");
            stareAnimation.Initialize(stareTexture, Vector2.Zero, 128, 128, 7, 90, Color.White, 1.5f, true);
            endTexture = Content.Load<Texture2D>("Sprites/End Screen.png");


            //Loading in the arrows
            arrowD = Content.Load<Texture2D>("Sprites/CursorD.png");
            arrowU = Content.Load<Texture2D>("Sprites/CursorU.png");
            arrowL = Content.Load<Texture2D>("Sprites/CursorL.png");
            arrowR = Content.Load<Texture2D>("Sprites/Cursor.png");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void SingleButtonPress(Buttons button)
        {
            if (CurrentState == States.Play)
            {
                switch (button)
                {
                    case Buttons.A:
                        Console.WriteLine("A button");
                        //audio.Play("child");
                        break;
                    case Buttons.B:
                        Console.WriteLine("B button");
                        //audio.Play("fuq");
                        break;
                    case Buttons.X:
                        Console.WriteLine("X button");
                        //audio.Play("world");
                        break;
                    case Buttons.Y:
                        Console.WriteLine("Y button");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (CurrentState == States.Play)
            {
                if (Invincibility >= 0)
                {
                    Invincibility -= gameTime.ElapsedGameTime.Milliseconds;
                }
                InputPlusMovement();
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                {
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    CurrentState = States.PauseMenu;
                    Thread.Sleep(100);
                }

                

                if (playerDirection == Direction.Still)
                {
                    player.SpriteAnimation = playerIdleAnimation;
                }
                else if (playerDirection == Direction.Up)
                {
                    player.SpriteAnimation = playerUpAnimation;
                }
                else if (playerDirection == Direction.Down)
                {
                    player.SpriteAnimation = playerDownAnimation;
                }
                else if (playerDirection == Direction.Left)
                {
                    playerRightAnimation.FlipHorizontally = true;
                    player.SpriteAnimation = playerRightAnimation;
                }
                else if (playerDirection == Direction.Right)
                {
                    playerRightAnimation.FlipHorizontally = false;
                    player.SpriteAnimation = playerRightAnimation;
                }

                foreach (Entity e in room.roomItems)
                {
                    e.Position.X = MathHelper.Clamp(e.Position.X, 0, room.Width() - e.Width());
                    e.Position.Y = MathHelper.Clamp(e.Position.Y, 0, room.Height() - e.Height());
                    e.Update(room.roomItems, gameTime);
                }

                player.Velocity = Vector2.Zero;

                if (player.EndGame && Invincibility > 0)
                {
                    player.EndGame = false;
                }
                else if (player.EndGame)
                {
                    CurrentState = States.Credits;
                }

                else if (player.Reset)
                {
                    Invincibility = 2000;
                    player.Position = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 50, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
                    playerDirection = Direction.Still;
                    goalBunny.Position = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height * (8 / 10));
                    player.Reset = false;

                    Texture2D doodad_texture = Content.Load<Texture2D>("Sprites/Orb_IdleSheet");
                    Animation doodad_animation = new Animation();
                    doodad_animation.Initialize(doodad_texture, Vector2.Zero, 64, 64, 9, 160, Color.White, 1f, true);
                    DeadlyDoodad deadlyDoodad = new DeadlyDoodad();
                    deadlyDoodad.Initialize(doodad_animation, 0.25f, new Vector2(room.Width() /2,
                    room.Height()/2));
                    room.roomItems.Add(deadlyDoodad);
                }
                base.Update(gameTime);
            }
            else if (CurrentState == States.MainMenu)
            {
                menuStart = new Entity();
                cursor = new Entity();

                Vector2 pos = new Vector2(0, 0);
                Vector2 startPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 700, GraphicsDevice.Viewport.TitleSafeArea.Y + 85);
                Vector2 instructionsPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 500, GraphicsDevice.Viewport.TitleSafeArea.Y + 340);
                Vector2 exitPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 815, GraphicsDevice.Viewport.TitleSafeArea.Y + 405);
                menuStart.Initialize(Content.Load<Texture2D>("Sprites/Title Screen.png"), 1f, pos);

                Vector2 starePosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 90, GraphicsDevice.Viewport.TitleSafeArea.Y + 250);
                stareBunny.Initialize(stareAnimation, 1f, starePosition);

                if (menuOption == MenuSelect.Start)
                {
                    cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, startPosition);
                }
                else if (menuOption == MenuSelect.Instructions)
                {
                    cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, instructionsPosition);
                }
                else
                {
                    cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, exitPosition);
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (menuOption == MenuSelect.Start)
                    {
                        menuOption = MenuSelect.Instructions;
                        Thread.Sleep(200);
                    }
                    else if (menuOption == MenuSelect.Instructions)
                    {
                        menuOption = MenuSelect.Exit;
                        Thread.Sleep(200);
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (menuOption == MenuSelect.Instructions)
                    {
                        menuOption = MenuSelect.Start;
                        Thread.Sleep(200);
                    }
                    else if (menuOption == MenuSelect.Exit)
                    {
                        menuOption = MenuSelect.Instructions;
                        Thread.Sleep(200);
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                    || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (menuOption == MenuSelect.Start)
                    {
                        Thread.Sleep(100);
                        CurrentState = States.Play;
                    }
                    else if (menuOption == MenuSelect.Instructions)
                    {
                        Thread.Sleep(100);
                        CurrentState = States.Instructions1;
                    }
                    else
                    {
                        Exit();
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                stareAnimation.Update(gameTime);
                //stareBunny.Update(null, gameTime);
                base.Update(gameTime);
            }
            else if (CurrentState == States.Instructions1)
            {
                cursor = new Entity();

                Vector2 pos = new Vector2(0, 0);
                Vector2 nextPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 870, GraphicsDevice.Viewport.TitleSafeArea.Y + 460);
                menuInstructions1.Initialize(instructions1Texture, 1f, pos);
                cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, nextPosition);

                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                    || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Thread.Sleep(100);
                    CurrentState = States.Instructions2;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                base.Update(gameTime);
            }
            else if (CurrentState == States.Instructions2)
            {
                cursor = new Entity();

                Vector2 pos = new Vector2(0, 0);
                Vector2 mainMenuPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 870, GraphicsDevice.Viewport.TitleSafeArea.Y + 460);
                menuInstructions2.Initialize(instructions2Texture, 1f, pos);
                cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, mainMenuPosition);

                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                    || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Thread.Sleep(100);
                    CurrentState = States.MainMenu;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                base.Update(gameTime);
            }
            else if (CurrentState == States.PauseMenu)
            {
                menuPause = new Entity();
                cursor = new Entity();

                Vector2 pos = new Vector2(0, 0);
                Vector2 resumePosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 630, GraphicsDevice.Viewport.TitleSafeArea.Y + 260);
                Vector2 exitPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 780, GraphicsDevice.Viewport.TitleSafeArea.Y + 337);
                menuPause.Initialize(Content.Load<Texture2D>("Sprites/Pause Screen.png"), 1f, pos);

                if (menuOption == MenuSelect.Start) // Start means Resume here
                {
                    cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, resumePosition);
                }
                else
                {
                    cursor.Initialize(Content.Load<Texture2D>("Sprites/Cursor.png"), 1f, exitPosition);
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (menuOption == MenuSelect.Start)
                    {
                        menuOption = MenuSelect.Exit;
                        Thread.Sleep(200);
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (menuOption == MenuSelect.Exit)
                    {
                        menuOption = MenuSelect.Start;
                        Thread.Sleep(200);
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                    || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (menuOption == MenuSelect.Start)
                    {
                        Thread.Sleep(100);
                        CurrentState = States.Play;
                    }
                    else
                    {
                        Exit();
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                base.Update(gameTime);
            }
            else if (CurrentState == States.Credits)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing
            spriteBatch.Begin();

            // Draw the Player
            if (CurrentState == States.Play)
            {
                room.Draw(spriteBatch);
                if (Invincibility > 0)
                {
                    player.SpriteAnimation.color = Color.White * 0.5f;
                }
                else
                {
                    player.SpriteAnimation.color = Color.White;
                }
                foreach (Entity e in room.roomItems)
                {
                    e.Draw(spriteBatch);
                }
                //Booleans set up to see if any player is pushin a directiong, but it only displays if the player is in that stay state.
                if (Staying)
                {
                    if (ShowDown)
                    {
                        ArrowD.Initialize(arrowD, 0.5f, new Vector2(Px + 5, Py + 60));
                        ArrowD.Draw(spriteBatch);
                    }
                    if (ShowUp)
                    {
                        ArrowU.Initialize(arrowU, 0.5f, new Vector2(Px + 5, Py - 25));
                        ArrowU.Draw(spriteBatch);
                    }
                    if (ShowLeft)
                    {
                        ArrowL.Initialize(arrowL, 0.5f, new Vector2(Px - 35, Py + 20));
                        ArrowL.Draw(spriteBatch);
                    }
                    if (ShowRight)
                    {
                        ArrowR.Initialize(arrowR, 0.5f, new Vector2(Px + 45, Py + 20));
                        ArrowR.Draw(spriteBatch);
                    }
                }

            }
            else if (CurrentState == States.MainMenu)
            {
                menuStart.Draw(spriteBatch);
                cursor.Draw(spriteBatch);
                stareBunny.SpriteAnimation = stareAnimation;
                stareBunny.Draw(spriteBatch);
            }
            else if (CurrentState == States.Instructions1)
            {
                spriteBatch.Draw(instructions1Texture, endTexture.Bounds, Color.White);
                cursor.Draw(spriteBatch);
            }
            else if (CurrentState == States.Instructions2)
            {
                spriteBatch.Draw(instructions2Texture, endTexture.Bounds, Color.White);
                cursor.Draw(spriteBatch);
            }
            else if (CurrentState == States.PauseMenu)
            {
                spriteBatch.Draw(pauseTexture, endTexture.Bounds, Color.White);
                //menuPause.Draw(spriteBatch);
                cursor.Draw(spriteBatch);
            }
            if (CurrentState == States.Credits)
            {
                spriteBatch.Draw(endTexture, endTexture.Bounds, Color.White);
            }

            // Stop drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Each button press sets that players voting boolean to true, preventing them from getting multiple votes, as well as raises the 'global' vote for 
        /// the direction chosen. Absense of a vote results in a vote to not move. Then it checks to see what direction had the most votes and moves in that 
        /// direction, ties result in staying. 
        /// </summary>
        public void InputPlusMovement()
        {
            Boolean P1Vote, P2Vote, P3Vote, P4Vote; //has the player voted this update cycle?
            int Vup, Vdown, Vleft, Vright, Vstay;   // tally up the votes
            P1Vote = false;
            P2Vote = false;
            P3Vote = false;
            P4Vote = false;
            Vstay = 0;
            Vup = 0;
            Vdown = 0;
            Vright = 0;
            Vleft = 0;
            Staying = false;
            ShowUp = false;
            ShowRight = false;
            ShowLeft = false;
            ShowDown = false;


            Px = player.SpriteAnimation.Position.X;
            Py = player.SpriteAnimation.Position.Y;

            // Player 1 Input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P1Vote = true;
                Vup++;
                ShowUp = true;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P1Vote = true;
                Vleft++;
                ShowLeft = true;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P1Vote = true;
                Vright++;
                ShowRight = true;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P1Vote = true;
                Vdown++;
                ShowDown = true;
            }
            //Player 2 Input
            if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P2Vote = true;
                Vup++;
                ShowUp = true;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Left == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P2Vote = true;
                Vleft++;
                ShowLeft = true;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Right == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P2Vote = true;
                Vright++;
                ShowRight = true;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P2Vote = true;
                Vdown++;
                ShowDown = true;
            }

            //Player 3 Input
            if (GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Three).DPad.Up == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P3Vote = true;
                Vup++;
                ShowUp = true;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Left == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P3Vote = true;
                Vleft++;
                ShowLeft = true;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Right == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P3Vote = true;
                Vright++;
                ShowRight = true;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Down == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P3Vote = true;
                Vdown++;
                ShowDown = true;
            }

            //Player 4 
            if (GamePad.GetState(PlayerIndex.Four).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Four).DPad.Up == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P4Vote = true;
                Vup++;
                ShowUp = true;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Left == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P4Vote = true;
                Vleft++;
                ShowLeft = true;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Right == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P4Vote = true;
                Vright++;
                ShowRight = true;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Down == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P4Vote = true;
                Vdown++;
                ShowDown = true;
            }

            //Checks to see who has not voted and gives a vote for staying for each non-voting player.
            if (!P1Vote)
            {
                Vstay++;
            }
            if (!P2Vote)
            {
                Vstay++;
            }
            if (!P3Vote)
            {
                Vstay++;
            }
            if (!P4Vote)
            {
                Vstay++;
            }

            //In the MOST fancy and streamlined of ways, Tallys the votes through several greather than statements (might be worried about how this looks but it
            //works and its game jam soooooo......)
            if (Vup > Vdown && Vup > Vleft && Vup > Vright && Vup > Vstay) // Vote to move UP
            {
                player.Velocity = new Vector2(0, -PLAYER_SPEED);
                playerDirection = Direction.Up;
            }
            else if (Vleft > Vdown && Vleft > Vright && Vleft > Vdown && Vleft > Vstay) // Vote to move LEFT
            {
                player.Velocity = new Vector2(-PLAYER_SPEED, 0);
                playerDirection = Direction.Left;
            }
            else if (Vright > Vdown && Vright > Vleft && Vright > Vup && Vright > Vstay) // Vote to move RIGHT
            {
                player.Velocity = new Vector2(PLAYER_SPEED, 0);
                playerDirection = Direction.Right;
            }
            else if (Vdown > Vleft && Vdown > Vright && Vdown > Vup && Vdown > Vstay) // Vote to move DOWN
            {
                player.Velocity = new Vector2(0, PLAYER_SPEED);
                playerDirection = Direction.Down;
            }
            else if (Vstay > Vleft && Vstay > Vright && Vstay > Vdown && Vstay > Vup) // Vote to STAY
            {
                player.Velocity = Vector2.Zero;
                playerDirection = Direction.Still;
                Staying = true;  
            }
            else
            {
                Staying = true;
            }
        }


    }
}
