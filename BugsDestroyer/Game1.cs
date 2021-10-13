﻿using Microsoft.Xna.Framework;
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
        private List<Explosion> listExplosion = new List<Explosion>();
        private Texture2D[] mobExplosion = new Texture2D[3];
        private Texture2D shotExplosion;
        private static int level = 0;
        private Texture2D[] cockroachSprites = new Texture2D[2];
        private Texture2D[] beetleSprites = new Texture2D[2];
        private Texture2D[] spiderSprites = new Texture2D[2];

        // Decor
        public List<Texture2D> Sol = new List<Texture2D>();
        private Texture2D Murs;
        private Texture2D Glass;
        private Texture2D Ombre;
        private Texture2D Processeur;
        private Texture2D MiniPCI;
        private Texture2D PCI;
        private Texture2D PileBios;
        private Texture2D Ram;
        private List<Texture2D> trapdoor = new List<Texture2D>();

        // Menu
        private SpriteFont font;
        private bool isOnMenu = true;
        private bool isPause = false;

        // Sfx
        SoundEffect MenuSfx;
        SoundEffect StartSfx;

        // Players
        private List<Player> players = new List<Player>();
        private Player player1;
        private Player player2;

        // Levels
        private List<Levels> listLevels = new List<Levels>();


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

            #region Music

            // musique
            this.song = Content.Load<Song>("Sounds/Music/Danger Escape");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;

            #endregion

            #region Decor

            // Decor
            Sol.Add(Content.Load<Texture2D>("Img/Decor/Sol0"));
            Sol.Add(Content.Load<Texture2D>("Img/Decor/Sol1"));
            Sol.Add(Content.Load<Texture2D>("Img/Decor/Sol2"));
            Sol.Add(Content.Load<Texture2D>("Img/Decor/Sol3"));

            Murs = Content.Load<Texture2D>("Img/Decor/mur0");
            Glass = Content.Load<Texture2D>("Img/Decor/Glass");
            Ombre = Content.Load<Texture2D>("Img/Decor/Ombre");

            Processeur = Content.Load<Texture2D>("Img/Decor/Processeur");
            MiniPCI = Content.Load<Texture2D>("Img/Decor/MiniPCI");
            PCI = Content.Load<Texture2D>("Img/Decor/PCI");
            PileBios = Content.Load<Texture2D>("Img/Decor/PileBios");
            Ram = Content.Load<Texture2D>("Img/Decor/Ram");

            trapdoor.Add(Content.Load<Texture2D>("Img/Decor/trapdoor0"));
            trapdoor.Add(Content.Load<Texture2D>("Img/Decor/trapdoor1"));

            #endregion

            #region Players Walking 

            // load walking sprites
            for (int x = 0; x < 7; x++)
            {
                player1walkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
                player2walkingSprites[x] = Content.Load<Texture2D>("Img/Perso2/walking/walking" + x.ToString());
            }
            #endregion

            #region Players Shooting

            // load shooting sprites
            player1shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            player1shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            player2shotSprites[0] = Content.Load<Texture2D>("Img/Perso2/shot/shot0");
            player2shotSprites[1] = Content.Load<Texture2D>("Img/Perso2/shot/shot1");
            #endregion

            #region Players Dead

            // load dead sprite
            player1DeadSprite = Content.Load<Texture2D>("Img/Perso/mort");
            player2DeadSprite = Content.Load<Texture2D>("Img/Perso2/mort");
            #endregion

            #region Players Health Bar

            // load health bar
            healthBarTexture = Content.Load<Texture2D>("Img/Health/healthPixel");
            healthBarBorderTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            #endregion

            #region Projectiles

            // load sfx
            keyboardSfx = Content.Load<SoundEffect>("Sounds/Sfx/tir");


            // load projectile sprite
            projectileSprite[0] = Content.Load<Texture2D>("Img/Perso/tir/balle2");
            projectileSprite[1] = Content.Load<Texture2D>("Img/Perso/tir/balle1");
            shotExplosion = Content.Load<Texture2D>("Img/Perso/tir/shotParticle");

            #endregion

            #region Enemys

            #region Cockroach

            cockroachSprites[0] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard0");
            cockroachSprites[1] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard1");

            #endregion
            
            #region Enemys Explosion

            mobExplosion[0] = Content.Load<Texture2D>("Img/Mobs/Mort/mort0");
            mobExplosion[1] = Content.Load<Texture2D>("Img/Mobs/Mort/mort1");
            mobExplosion[2] = Content.Load<Texture2D>("Img/Mobs/Mort/mort2");

            #endregion

            #region Beetle

            beetleSprites[0] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe0");
            beetleSprites[1] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe1");

            #endregion

            #region Spider

            spiderSprites[0] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira0");
            spiderSprites[1] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira1");

            #endregion

            #endregion

            LevelLoad();

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
                    #region Levels

                    // update des niveaux
                    if (listLevels[level].listEnemies.Count <= 0)
                    {
                        listLevels[level].Update(gameTime);

                        listLevels[level]._trapdoor.Update(gameTime, players, listLevels[level].listEnemies);

                        if (!listLevels[level]._trapdoor.trapdoorIsOpen)
                        {
                            if (listLevels.Count - 1 != level)
                            {
                                level++;
                            }
                        }
                        else
                        {
                            float radius = player1.currentSprite.Width + listLevels[level]._trapdoor.currentFrame.Width * 2/3;
                            if (Vector2.DistanceSquared(player1.position, listLevels[level]._trapdoor._position) < Math.Pow(radius, 2))
                            {
                                if (Keyboard.GetState().IsKeyDown(Keys.G) || Keyboard.GetState().IsKeyDown(Keys.NumPad5))
                                {
                                    if (listLevels.Count - 1 != level)
                                    {
                                        level++;
                                    }
                                }
                            }
                        }

                    }

                    #endregion

                    #region Players

                    // si un joueur
                    if (selectedPlayer1)
                    {
                        if (players.Count <= 0)
                        {
                            player1 = new Player(gameTime, player1walkingSprites, player1shotSprites, player1DeadSprite, keyboardSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, projectileSprite, new Vector2(1500, 400));
                            
                            players.Add(player1);
                        }
                        
                        player1.playerUpdate(gameTime, listProjectiles);
                    } // sinon si deux joueur séléctionnées
                    else if (!selectedPlayer1)
                    {
                        if (players.Count <= 0)
                        {
                            player1 = new Player(gameTime, player1walkingSprites, player1shotSprites, player1DeadSprite, keyboardSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, projectileSprite, new Vector2(400, 400));
                            player2 = new Player(gameTime, player2walkingSprites, player2shotSprites, player2DeadSprite, keyboardSfx, new Keys[] { Keys.Up, Keys.Left, Keys.Down, Keys.Right }, Keys.NumPad4, projectileSprite, new Vector2(400, 600));
                            players.Add(player1);
                            players.Add(player2);
                        }

                        player1.playerUpdate(gameTime, listProjectiles);
                        player2.playerUpdate(gameTime, listProjectiles);
                    }
                    #endregion

                    #region Enemys
                    // update des enemies
                    for (int i = listLevels[level].listEnemies.Count - 1; i >= 0; i--)
                    {
                        listLevels[level].listEnemies[i].Update(gameTime, players, listProjectiles, listLevels[level].listEnemies, listExplosion, mobExplosion.ToList());
                    }
                    #endregion

                    #region Projectiles

                    // update des projectiles
                    for (int i = listExplosion.Count - 1; i >= 0; i--)
                    {
                        listExplosion[i].Update(gameTime, listExplosion);
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
                    #endregion

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

                #region Decor
                // affichage du sol ( multiplier )
                for (int y = 0; y < _graphics.PreferredBackBufferHeight; y += listLevels[level]._background.Height / 2)
                {
                    for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += listLevels[level]._background.Width / 2)
                    {
                        _spriteBatch.Draw(listLevels[level]._background, new Vector2(x, y), null, Color.White * 0.75f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    }
                }

                // Décor
                foreach (Object item in listLevels[level].listObjects)
                {
                    item.Draw(_spriteBatch);
                }

                listLevels[level]._trapdoor.Draw(_spriteBatch);

                // affichage des murs
                _spriteBatch.Draw(Murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);

                #endregion

                #region Player 
                // si le un joueur est sélectionnée
                if (players.Count > 0)
                {
                    if (selectedPlayer1)
                    {
                        // affichage des deux joueurs
                        _spriteBatch.Draw(player1.currentSprite, player1.position, null, player1.color, player1.rotation, new Vector2(player1.currentSprite.Width / 2, player1.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);

                        // affichage de la bar de vie
                        players[0].playerDrawHealthBar(_spriteBatch, healthBarBorderTexture, healthBarTexture);
                    } // sinon si deux joueurs
                    else if (!selectedPlayer1)
                    {
                        // affichage des deux joueurs
                        _spriteBatch.Draw(player1.currentSprite, player1.position, null, player1.color, player1.rotation, new Vector2(player1.currentSprite.Width / 2, player1.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
                        _spriteBatch.Draw(player2.currentSprite, player2.position, null, player2.color, player2.rotation, new Vector2(player2.currentSprite.Width / 2, player2.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);

                        // affichage de la bar de vie
                        players[0].playerDrawHealthBar(_spriteBatch, healthBarBorderTexture, healthBarTexture);
                        players[1].playerDrawHealthBar(_spriteBatch, healthBarBorderTexture, healthBarTexture);
                    }
                }
                #endregion

                #region Enemys
                // affichage des enemies
                foreach (Enemy enemy in listLevels[level].listEnemies)
                {
                    enemy.Draw(_spriteBatch);
                }
                #endregion

                #region Ombre
                // affichage d'une ombre à coté des murs
                _spriteBatch.Draw(Ombre, new Vector2(245, 121), null, Color.White * 0.75f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                #endregion

                #region Projectiles
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