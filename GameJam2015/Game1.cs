﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace GameJam2015
{
    ///TESTESTESTEST
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        readonly int PLAYER_SPEED = 12;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Timer aTime;
        Player player;
        AudioManager audio;
        enum States { MainMenu, Play, PauseMenu, Credits };
        enum Direction { Up, Down, Left, Right, Still };
        enum MenuSelect { Start, Exit };
        States CurrentState;
        MenuSelect menuOption;
        List<Entity> entities = new List<Entity>();
        Entity menuStart;
        Entity menuExit;
        SoundEffect newplay;
        SoundEffectInstance newplayinstance;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
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
            audio = new AudioManager(Content.RootDirectory);
            entities.Add(player);
            CurrentState = States.Play;
            audio = new AudioManager();
            CurrentState = States.MainMenu;
            menuOption = MenuSelect.Start;
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
            newplay = Content.Load<SoundEffect>(@"Audio\\03_Child_Bride.wav");
            newplayinstance = newplay.CreateInstance();
            // TODO: use this.Content to load your game content here
            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Sprites/BunJumpSheet.png");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 128, 128, 3, 80, Color.White, 1f, true);

            // Load audio into the AudioManager
            audio.LoadAudio();
            //audio.PlayBackground();
            // Load the player resources

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, 1, playerPosition);

            // Load the bunny resources
            Animation stareAnimation = new Animation();
            Texture2D stareTexture = Content.Load<Texture2D>("Sprites/BunStareSheet.png");
            stareAnimation.Initialize(stareTexture, Vector2.Zero, 128, 128, 8, 80, Color.White, 1f, true);

            Animation jumpAnimation = new Animation();
            Texture2D jumpTexture = Content.Load<Texture2D>("Sprites/BunJumpSheet.png");
            jumpAnimation.Initialize(jumpTexture, Vector2.Zero, 128, 128, 3, 80, Color.White, 1f, true);
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
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.Velocity = new Vector2(0, -PLAYER_SPEED);
                    audio.Play("world");
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.Velocity = new Vector2(-PLAYER_SPEED, 0);
                    Console.WriteLine("Player goes left");
                    newplayinstance.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.Velocity = new Vector2(PLAYER_SPEED, 0);
                    newplayinstance.Stop();
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.Velocity = new Vector2(0, PLAYER_SPEED);
                    audio.Play("child");
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    CurrentState = States.PauseMenu;
                    Thread.Sleep(100);
                }
                player.Update(entities, gameTime);
                player.Velocity = Vector2.Zero;

                // Make sure that the player does not go out of bounds
                player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width());
                player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height());

                base.Update(gameTime);
            }
            else if (CurrentState == States.MainMenu || CurrentState == States.PauseMenu)
            {
                menuStart = new Entity();
                menuExit = new Entity();

                Vector2 menuPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 3);
                Vector2 exitPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

                // Make sure to not call update methods or game time here
                if(menuOption == MenuSelect.Start)
                {
                    menuStart.Initialize(Content.Load<Texture2D>("Sprites/Sprite2.png"), 0.5f, menuPosition);
                    menuExit.Initialize(Content.Load<Texture2D>("Sprites/Sprite.png"), 0.5f, exitPosition);
                }
                else
                {
                    menuStart.Initialize(Content.Load<Texture2D>("Sprites/Sprite.png"), 0.5f, menuPosition);
                    menuExit.Initialize(Content.Load<Texture2D>("Sprites/Sprite2.png"), 0.5f, exitPosition);
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (menuOption == MenuSelect.Start)
                    {
                        menuOption = MenuSelect.Exit;
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                    if (menuOption == MenuSelect.Exit)
                    {
                        menuOption = MenuSelect.Start;
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
                menuStart.Update(null, null);
                menuExit.Update(null, null);
                base.Update(gameTime);
            }
            else if (CurrentState == States.Credits)
            {
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
            player.Draw(spriteBatch);
            }
            else if (CurrentState == States.MainMenu || CurrentState == States.PauseMenu)
            {
                menuStart.Draw(spriteBatch);
                menuExit.Draw(spriteBatch);
            }

            // Stop drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
