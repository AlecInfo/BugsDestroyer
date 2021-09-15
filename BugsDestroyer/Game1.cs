using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugsDestroyer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Song song;

        // Game
        Keys currentDirectionalKey;

        // Perso
        private Texture2D[] playerWalkingSprites = new Texture2D[7];
        private Texture2D[] playerShotSprites = new Texture2D[3];
        private Vector2 playerPos = new Vector2(100,100);
        private int playerWalkingSpeed = 10;
        private float playerRotation = 0f;

        // Decor
        private Texture2D Sol;
        private Texture2D Murs;
        private Texture2D Glass;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Position = new Point(0, 0);
            Window.IsBorderless = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            IsMouseVisible = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // musique
            this.song = Content.Load<Song>("Sounds/Music/Danger Escape");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            // Decor
            Sol = Content.Load<Texture2D>("Img/Decor/Sol");
            Murs = Content.Load<Texture2D>("Img/Decor/mur0");
            Glass = Content.Load<Texture2D>("Img/Decor/Glass");

            // Perso
            for (int x = 0; x < 7; x++) // load walking sprites
            {
                playerWalkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking"+x.ToString());
            }
            playerShotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            playerShotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            playerShotSprites[2] = Content.Load<Texture2D>("Img/Perso/shot/shot2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D0))
                Exit();

            if (Keyboard.GetState().GetPressedKeys().Length != 0)
            {
                currentDirectionalKey = (Keyboard.GetState().GetPressedKeys().Last());
            }
            else
            {
                currentDirectionalKey = Keys.None;
            }

            // Perso
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerPos.X += playerWalkingSpeed / 1.4f;
                playerPos.Y -= playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI*7/4; //315
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPos.X += playerWalkingSpeed / 1.4f;
                playerPos.Y += playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI / 4; // 45
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                playerPos.X -= playerWalkingSpeed / 1.4f;
                playerPos.Y += playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI*3/4; // 135
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPos.X -= playerWalkingSpeed / 1.4f;
                playerPos.Y -= playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI*5/4; //225
            }
            else
            {
                if (currentDirectionalKey == Keys.D)
                {
                    playerPos.X += playerWalkingSpeed;
                    playerRotation = 0; // 0
                }
                if (currentDirectionalKey == Keys.S)
                {
                    playerPos.Y += playerWalkingSpeed;
                    playerRotation = (float)Math.PI/2; // 90
                }
                if (currentDirectionalKey == Keys.A)
                {
                    playerPos.X -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI; // 180
                }
                if (currentDirectionalKey == Keys.W)
                {
                    playerPos.Y -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI*1.5f; // 270
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);



            _spriteBatch.Begin(samplerState:SamplerState.PointClamp);

            _spriteBatch.Draw(Sol, new Vector2(245,124), null, Color.White, 0f, Vector2.Zero, 0.90f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(Glass, new Vector2(245, 124), null, Color.White * 0.25f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(Murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
            
            _spriteBatch.Draw(playerWalkingSprites[0], playerPos, null, Color.White, playerRotation, new Vector2(playerWalkingSprites[0].Width/2, playerWalkingSprites[0].Height/ 2), 1f, SpriteEffects.None, 0f);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
