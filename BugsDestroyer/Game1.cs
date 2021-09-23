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
        private SoundEffect keyboardSfx;

        // Game
        Random rnd = new Random();
        private Texture2D[] player1walkingSprites = new Texture2D[7];
        private Texture2D[] player1shotSprites = new Texture2D[3];
        private Texture2D player1DeadSprite;
        private Texture2D[] player2walkingSprites = new Texture2D[7];
        private Texture2D[] player2shotSprites = new Texture2D[3];

        // Decor
        private Texture2D Sol;
        private Texture2D Murs;
        private Texture2D Glass;
        private Texture2D Ombre;

        // Menu
        private SpriteFont font;
        private bool isOnMenu = true;
        private bool isPause = false;

        // Enemies
        private List<Object> enemies;

        // Players
        private Player player1;

        private Texture2D[] projectileSprite = new Texture2D[2];
        public enum direction
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW,
        }
        public direction currentDirection;

        // Health bar
        Texture2D healthBarTexture;
        Rectangle healthBarRectangle;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
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
            MediaPlayer.Volume = 1f;

            // Decor
            Sol = Content.Load<Texture2D>("Img/Decor/Sol0");
            Murs = Content.Load<Texture2D>("Img/Decor/mur0");
            Glass = Content.Load<Texture2D>("Img/Decor/Glass");
            Ombre = Content.Load<Texture2D>("Img/Decor/Ombre");



            // load walking sprites
            for (int x = 0; x < 7; x++)
            {
                player1walkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
                player2walkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
            }

            // load shooting sprites
            player1shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            player1shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            player1shotSprites[2] = Content.Load<Texture2D>("Img/Perso/shot/shot2");
            player2shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            player2shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            player2shotSprites[2] = Content.Load<Texture2D>("Img/Perso/shot/shot2");

            // load dead sprite
            player1DeadSprite = Content.Load<Texture2D>("Img/Perso/mort");

            // load health bar
            healthBarTexture = Content.Load<Texture2D>("Img/Health/healthPixel");

            // load sfx
            keyboardSfx = Content.Load<SoundEffect>("Sounds/Sfx/tir");

            // load projectile sprite
            projectileSprite[0] = Content.Load<Texture2D>("Img/Perso/tir/balle2");
            projectileSprite[1] = Content.Load<Texture2D>("Img/Perso/tir/balle1");

            player1 = new Player(player1walkingSprites, player1shotSprites, player1DeadSprite, keyboardSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D } , Keys.F, projectileSprite);



            menuLoad();
            menuPauseLoad();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D0))
                Exit();

            if (isOnMenu)
            {
                menuUpdate(gameTime);
            }else if (!isOnMenu)
            {
                if (!isPause)
                {
                    player1.playerUpdate(gameTime);

                    if (player1.isShooting)
                    {
                        player1.projectiles.projectileUpdate(gameTime);
                    }

                    healthBarRectangle = new Rectangle(
                        Convert.ToInt32(player1.position.X - Player.HEALTH_POINT_MAX/4),
                        Convert.ToInt32(player1.position.Y - player1.currentSprite.Height/2 -20),
                        player1.healthPoint/2,
                        7);

                }
                menuPauseUpdate(gameTime);
  
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            if (isOnMenu)
            {
                menuDraw(gameTime);
            }
            else if (!isOnMenu)
            {
                #region Player
                for (int y = 0; y < _graphics.PreferredBackBufferHeight; y += Sol.Height / 2)
                {
                    for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += Sol.Width / 2)
                    {
                        _spriteBatch.Draw(Sol, new Vector2(x, y), null, Color.White * 0.75f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    }
                }
                _spriteBatch.Draw(Murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                _spriteBatch.Draw(player1.currentSprite, player1.position, null, Color.White, player1.rotation, new Vector2(player1.currentSprite.Width / 2, player1.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
                _spriteBatch.Draw(Ombre, new Vector2(245, 121), null, Color.White * 0.75f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                #endregion

                if (player1.isShooting)
                {
                    player1.projectiles.projectileDraw(_spriteBatch);
                }
                menuPauseDraw(gameTime);

                // Draw health bar border
                Texture2D borderTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
                borderTexture.SetData(new Color[] { Color.Black });
                _spriteBatch.Draw(borderTexture, new Rectangle(healthBarRectangle.X-2, healthBarRectangle.Y-2, Player.HEALTH_POINT_MAX / 2 + 4, 7 + 4), Color.Black * 0.5f);
                // Draw health bar
                _spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);

                menuPauseDraw(gameTime);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}