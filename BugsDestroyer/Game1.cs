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
        public int music = 0;

        // Game
        Random rnd = new Random();
        private Texture2D[] player1walkingSprites = new Texture2D[7];
        private Texture2D[] player1shotSprites = new Texture2D[3];
        private Texture2D player1DeadSprite;
        private Texture2D player2DeadSprite;
        private Texture2D[] player2walkingSprites = new Texture2D[7];
        private Texture2D[] player2shotSprites = new Texture2D[3];
        public List<Projectiles> listProjectiles = new List<Projectiles>();
        private List<Enemy> listEnemies = new List<Enemy>();
        private List<Explosion> listExplosion = new List<Explosion>();
        private Texture2D[] mobExplosion = new Texture2D[3];
        private Texture2D shotExplosion;


        // Decor
        private Texture2D Sol;
        private Texture2D Murs;
        private Texture2D Glass;
        private Texture2D Ombre;

        // Menu
        private SpriteFont font;
        private bool isOnMenu = true;
        private bool isPause = false;

        // Sfx
        SoundEffect MenuSfx;
        SoundEffect StartSfx;

        // Players
        private Player[] players = new Player[2];
        private Player player1;
        private Player player2;


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

        // Health bar
        private Texture2D healthBarTexture;
        private Texture2D healthBarBorderTexture;


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
                player2walkingSprites[x] = Content.Load<Texture2D>("Img/Perso2/walking/walking" + x.ToString());
            }

            // load shooting sprites
            player1shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            player1shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            player2shotSprites[0] = Content.Load<Texture2D>("Img/Perso2/shot/shot0");
            player2shotSprites[1] = Content.Load<Texture2D>("Img/Perso2/shot/shot1");

            // load dead sprite
            player1DeadSprite = Content.Load<Texture2D>("Img/Perso/mort");
            player2DeadSprite = Content.Load<Texture2D>("Img/Perso2/mort");

            // load health bar
            healthBarTexture = Content.Load<Texture2D>("Img/Health/healthPixel");
            healthBarBorderTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);

            // load sfx
            keyboardSfx = Content.Load<SoundEffect>("Sounds/Sfx/tir");

            // load projectile sprite
            projectileSprite[0] = Content.Load<Texture2D>("Img/Perso/tir/balle2");
            projectileSprite[1] = Content.Load<Texture2D>("Img/Perso/tir/balle1");

            // load explosion
            mobExplosion[0] = Content.Load<Texture2D>("Img/Mobs/Mort/mort0");
            mobExplosion[1] = Content.Load<Texture2D>("Img/Mobs/Mort/mort1");
            mobExplosion[2] = Content.Load<Texture2D>("Img/Mobs/Mort/mort2");
            shotExplosion = Content.Load<Texture2D>("Img/Perso/tir/shotParticle");


            player1 = new Player(player1walkingSprites, player1shotSprites, player1DeadSprite, keyboardSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D } , Keys.F, projectileSprite, new Vector2(400, 400));
            player2 = new Player(player2walkingSprites, player2shotSprites, player2DeadSprite, keyboardSfx, new Keys[] { Keys.Up, Keys.Left, Keys.Down, Keys.Right }, Keys.NumPad4, projectileSprite, new Vector2(400, 600));
            players[0] = player1;
            players[1] = player2;


            listEnemies.Add(new Cockroach(new Vector2(500, 500)));
            listEnemies.Add(new Cockroach(new Vector2(500, 100)));
            listEnemies.Add(new Cockroach(new Vector2(1250, 500)));
            listEnemies.Add(new Cockroach(new Vector2(100, 750)));
            foreach (Enemy enemy in listEnemies)
            {
                enemy.Load(Content);
            }



            menuLoad();
            menuPauseLoad();
            gameOverLoad();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D0))
                Exit();

            // si le joueur est dans le menu de bienvenu
            if (isOnMenu)
            {
                menuUpdate(gameTime);
            } // sinon si il est pas dans le menu
            else if (!isOnMenu)
            {
                // si le joueur est pas dans le menu pause
                if (!isPause)
                {
                    // mettre a jour tout les ennemis
                    for (int i = listEnemies.Count - 1; i >= 0; i--)
                    {
                        listEnemies[i].Update(gameTime, players, listProjectiles, listEnemies, listExplosion, mobExplosion.ToList());
                    }

                    for (int i = listExplosion.Count - 1; i >= 0; i--)
                    {
                        listExplosion[i].Update(gameTime, listExplosion);
                    }

                    if (selectedPlayer1)
                    {
                        player1.playerUpdate(gameTime, listProjectiles);
                    } // sinon si deux joueur séléctionnées
                    else if (!selectedPlayer1)
                    {
                        player1.playerUpdate(gameTime, listProjectiles);
                        player2.playerUpdate(gameTime, listProjectiles);
                    }

                    // si la liste du nombre de projectile n'est pas à zero
                    if (listProjectiles.Count != 0)
                    {
                        // parcourir la liste en partant du plus grand index
                        for (int i = listProjectiles.Count -1; i >= 0; i--)
                        {
                            // acctualisation du projectile
                            listProjectiles[i].projectileUpdate(gameTime, listProjectiles, listExplosion, shotExplosion);
                        }
                    }

                    gameOverUpdate(gameTime);
                }

                // acctualisation du menu pause
                menuPauseUpdate(gameTime);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            // si le joueur est dans le menu de bienvenu
            if (isOnMenu)
            {
                // affichage du menu
                menuDraw(gameTime);
            } // sinon si il n'est pas dans le menu
            else if (!isOnMenu)
            {
                #region Player 
                // affichage du sol ( multiplier )
                for (int y = 0; y < _graphics.PreferredBackBufferHeight; y += Sol.Height / 2)
                {
                    for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += Sol.Width / 2)
                    {
                        _spriteBatch.Draw(Sol, new Vector2(x, y), null, Color.White * 0.75f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    }
                }
                // affichage des murs
                _spriteBatch.Draw(Murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);


                // si le un joueur est sélectionnée
                if (selectedPlayer1)
                { 
                    // affichage du joueur
                    _spriteBatch.Draw(player1.currentSprite, player1.position, null, Color.White, player1.rotation, new Vector2(player1.currentSprite.Width / 2, player1.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
                } // sinon si deux joueurs
                else if (!selectedPlayer1)
                {
                    // affichage des deux joueurs
                    _spriteBatch.Draw(player1.currentSprite, player1.position, null, Color.White, player1.rotation, new Vector2(player1.currentSprite.Width / 2, player1.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
                    _spriteBatch.Draw(player2.currentSprite, player2.position, null, Color.White, player2.rotation, new Vector2(player2.currentSprite.Width / 2, player2.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
                }

                // Afficher tout les ennemis
                foreach (Enemy enemy in listEnemies)
                {
                    enemy.Draw(_spriteBatch);
                }

                // affichage d'une ombre à coté des murs
                _spriteBatch.Draw(Ombre, new Vector2(245, 121), null, Color.White * 0.75f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);

                // si la liste du nombre de projectile n'est pas à zero
                if (listProjectiles.Count != 0)
                {
                    // affichage de tous les éléments de la liste 
                    foreach (Projectiles projectile in listProjectiles)
                    {
                        projectile.projectileDraw(_spriteBatch);
                    }
                }

                foreach (Explosion explosion in listExplosion)
                {
                    explosion.Draw(_spriteBatch);
                }

                // affichage de la bar de vie
                player1.playerDrawHealthBar(_spriteBatch, healthBarBorderTexture, healthBarTexture);
                player2.playerDrawHealthBar(_spriteBatch, healthBarBorderTexture, healthBarTexture);

                #endregion

                // affichage du menu pause
                menuPauseDraw(gameTime);

                // affichage du gameOver
                gameOverDraw(gameTime);

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}