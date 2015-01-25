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
        SoundEffect bunnyMelt;
        SoundEffectInstance bunnyMeltInstance;
        Entity menuStart, menuInstructions1, menuInstructions2, menuPause, cursor;
        AnimatedEntity stareBunny;
        Room room;
        Obstacles fullShelf1, fullShelf2, fullShelf3, fullShelf4, table;
        Texture2D instructions1Texture, instructions2Texture, pauseTexture, endTexture;
        Texture2D stareTexture;

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
            table = new Obstacles();
            fullShelf1 = new Obstacles();
            fullShelf2 = new Obstacles();
            fullShelf3 = new Obstacles();
            fullShelf4 = new Obstacles();
            entities.Add(goalBunny);
            entities.Add(player);
            entities.Add(table);
            entities.Add(fullShelf1);
            entities.Add(fullShelf2);
            menuInstructions1 = new Entity();
            menuInstructions2 = new Entity();
            /*entities.Add(fullShelf3);
            entities.Add(fullShelf4);*/
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
            Texture2D roomTexture = Content.Load<Texture2D>("Sprites/Test Background.png");
            //Texture2D roomTexture = Content.Load<Texture2D>("Sprites/Title Screen.png");
            room.Initialize(roomTexture, .25f, Vector2.Zero);

            // TODO: use this.Content to load your game content here
            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Sprites/HeroIdleSheet.png");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 128, 256, 5, 80, Color.White, .25f, true);
            //bunnyMelt = Content.Load<SoundEffect>(@"Audio\\03_Child_Bride.wav");
            //bunnyMeltInstance = bunnyMelt.CreateInstance();

            Texture2D playerIdleTexture = Content.Load<Texture2D>("Sprites/HeroIdleSheet.png");
            playerIdleAnimation.Initialize(playerIdleTexture, Vector2.Zero, 128, 256, 5, 80, Color.White, .25f, true);

            Texture2D playerUpTexture = Content.Load<Texture2D>("Sprites/HeroWalkUPSHEET.png");
            playerUpAnimation.Initialize(playerUpTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .25f, true);

            Texture2D playerDownTexture = Content.Load<Texture2D>("Sprites/HeroWalkDownSheet.png");
            playerDownAnimation.Initialize(playerDownTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .25f, true);

            Texture2D playerRightTexture = Content.Load<Texture2D>("Sprites/HeroWalkSideSheet.png");
            playerRightAnimation.Initialize(playerRightTexture, Vector2.Zero, 128, 256, 4, 80, Color.White, .25f, true);

            // Load audio into the AudioManager. Plays the background music upon loading.
            audio.LoadAudio();
            audio.Play("fuq");

            // Load the player resources
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerIdleAnimation, 1, playerPosition);

            // Load the bunny resources
            Animation jumpAnimation = new Animation();
            Texture2D jumpTexture = Content.Load<Texture2D>("Sprites/BunJumpSheet.png");
            jumpAnimation.Initialize(jumpTexture, Vector2.Zero, 128, 128, 4, 80, Color.White, .25f, true);

            Vector2 bunnyPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height * (8 / 10));
            goalBunny.Initialize(jumpAnimation, 1f, bunnyPosition);

            Texture2D tableTexture = Content.Load<Texture2D>("Sprites/Table.png");
            table.Initialize(tableTexture, .25f, new Vector2(room.Height() / 2, room.Width() / 2));

            Texture2D shelfTexture = Content.Load<Texture2D>("Sprites/Shelf.png");
            fullShelf1.Initialize(shelfTexture, .25f, Vector2.Zero);

            fullShelf2.Initialize(shelfTexture, .25f, new Vector2(3 * room.Width() / 4, 3 * room.Height() / 4));

            room.addItem(entities);
            instructions1Texture = Content.Load<Texture2D>("Sprites/Instruction Screen.png");
            instructions2Texture = Content.Load<Texture2D>("Sprites/Instructions 2.png");
            pauseTexture = Content.Load<Texture2D>("Sprites/Pause Screen.png");
            endTexture = Content.Load<Texture2D>("Sprites/End Screen.png");

            stareTexture = Content.Load<Texture2D>("Sprites/BunStareSheet.png");
            stareAnimation.Initialize(stareTexture, Vector2.Zero, 128, 128, 8, 90, Color.White, 1.5f, true);
            endTexture = Content.Load<Texture2D>("Sprites/End Screen.png");
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
            if (CurrentState == States.Play)
            {
                InputPlusMovement();
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    audio.Play("child");
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    audio.Play("fuq");
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                {
                    audio.Play("world");
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                {
                    bunnyMeltInstance.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    bunnyMeltInstance.Stop();
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

                if (player.EndGame)
                {
                    CurrentState = States.Credits;
                }
                if (player.Reset)
                {
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

                foreach (Entity e in room.roomItems)
                {
                    e.Draw(spriteBatch);
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

            // Player 1 Input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P1Vote = true;
                Vup++;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P1Vote = true;
                Vleft++;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P1Vote = true;
                Vright++;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed && !P1Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P1Vote = true;
                Vdown++;
            }
            //Player 2 Input
            if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P2Vote = true;
                Vup++;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Left == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P2Vote = true;
                Vleft++;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Right == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P2Vote = true;
                Vright++;
            }
            if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed && !P2Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P2Vote = true;
                Vdown++;
            }

            //Player 3 Input
            if (GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Three).DPad.Up == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P3Vote = true;
                Vup++;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Left == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P3Vote = true;
                Vleft++;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Right == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P3Vote = true;
                Vright++;
            }
            if (GamePad.GetState(PlayerIndex.Three).DPad.Down == ButtonState.Pressed && !P3Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P3Vote = true;
                Vdown++;
            }

            //Player 4 
            if (GamePad.GetState(PlayerIndex.Four).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.Four).DPad.Up == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                P4Vote = true;
                Vup++;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Left == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                P4Vote = true;
                Vleft++;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Right == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                P4Vote = true;
                Vright++;
            }
            if (GamePad.GetState(PlayerIndex.Four).DPad.Down == ButtonState.Pressed && !P4Vote || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                P4Vote = true;
                Vdown++;
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
            }
        }


    }
}
