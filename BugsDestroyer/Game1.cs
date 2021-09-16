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
    public partial class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Sound
        Song song;

        // Game
        Keys currentDirectionalKey;
        Random rnd = new Random();

        // Perso
        private Texture2D[] playerWalkingSprites = new Texture2D[7];
        private Texture2D[] playerShotSprites = new Texture2D[3];
        private Texture2D playerCurrentSprite;
        private int[] PlayerAnimSteps = { 1, 2, 3, 2, 1, 4, 5, 6, 5, 4};
        private int currentStep = 0;
        private Vector2 playerPos = new Vector2(100,100);
        private int playerWalkingSpeed = 8;
        private float playerRotation = 0f;

        //Anim
        public int currentFrameNb;
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 100;

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
            Sol = Content.Load<Texture2D>("Img/Decor/Sol0");
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

            playerCurrentSprite = playerWalkingSprites[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D0))
                Exit();

            playerUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);



            _spriteBatch.Begin(samplerState:SamplerState.PointClamp);

            for (int y = 0; y < _graphics.PreferredBackBufferHeight; y+=Sol.Height/2) {
                for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += Sol.Width / 2)
                {
                    _spriteBatch.Draw(Sol, new Vector2(x, y), null, Color.White * 0.75f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
            }
            //_spriteBatch.Draw(Glass, new Vector2(245, 124), null, Color.White * 0.15f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(Murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
            
            _spriteBatch.Draw(playerCurrentSprite, playerPos, null, Color.White, playerRotation, new Vector2(playerWalkingSprites[0].Width/2, playerWalkingSprites[0].Height/ 2), 1f, SpriteEffects.None, 0f);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
