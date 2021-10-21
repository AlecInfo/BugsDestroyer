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
        // Game
        Random rnd = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Game objects (Lists)
        private List<Player> players = new List<Player>();
        private List<Levels> listLevels = new List<Levels>();
        public List<Projectiles> listProjectiles = new List<Projectiles>();
        private List<Explosion> listExplosion = new List<Explosion>();

        // Textures Autres
        private Texture2D[] player1walkingSprites = new Texture2D[7];
        private Texture2D[] player1shotSprites = new Texture2D[3];
        private Texture2D player1DeadSprite;
        private Texture2D[] player2walkingSprites = new Texture2D[7];
        private Texture2D[] player2shotSprites = new Texture2D[3];
        private Texture2D player2DeadSprite;
        private Texture2D[] mobExplosion = new Texture2D[3];
        private Texture2D shotExplosion;
        private Texture2D[] cockroachSprites = new Texture2D[2];
        private Texture2D[] beetleSprites = new Texture2D[2];
        private Texture2D[] spiderSprites = new Texture2D[2];
        private Texture2D[] projectileSprite = new Texture2D[2];
        private Texture2D healthBarTexture;
        private Texture2D healthItem;

        // Textures Decor
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

        // Sound
        Song song;
        private SoundEffect keyboardSfx;
        public int music = 0;

        // Sfx
        private List<SoundEffect> _listSfx = new List<SoundEffect>();
        SoundEffect MenuSfx;
        SoundEffect StartSfx;

        //Timer
        public float _timerPrincipal = 0f;

        // Levels
        private static int level = 0;
        private bool isOpacityDark = true;
        private bool startTransitionDark = false;
        private bool startTransitionLight = false;
        private float opacityTrasition = 1f;
        private float currentTimeMiliTrasition = 0f;
        private float countDurationMiliTrasition = 1f;
        private int timerMiliTrasition = 0;


        public enum direction
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }



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

            #region Music / SoundEffect

            // musique
            this.song = Content.Load<Song>("Sounds/Music/Danger Escape");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;

            // sound effect
            _listSfx.Add(Content.Load<SoundEffect>("Sounds/Sfx/wallHurt"));
            _listSfx.Add(Content.Load<SoundEffect>("Sounds/Sfx/enemysHurt"));
            _listSfx.Add(Content.Load<SoundEffect>("Sounds/Sfx/playersHurt"));
            _listSfx.Add(Content.Load<SoundEffect>("Sounds/Sfx/tir"));
            _listSfx.Add(Content.Load<SoundEffect>("Sounds/Sfx/trapdoor"));


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

            // recupération des images des joueur en marche
            for (int x = 0; x < 7; x++)
            {
                player1walkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
                player2walkingSprites[x] = Content.Load<Texture2D>("Img/Perso2/walking/walking" + x.ToString());
            }
            #endregion

            #region Players Shooting

            // récupération des images en tire
            player1shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            player1shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            player2shotSprites[0] = Content.Load<Texture2D>("Img/Perso2/shot/shot0");
            player2shotSprites[1] = Content.Load<Texture2D>("Img/Perso2/shot/shot1");
            #endregion

            #region Players Dead

            // récupération des images (mort)
            player1DeadSprite = Content.Load<Texture2D>("Img/Perso/mort");
            player2DeadSprite = Content.Load<Texture2D>("Img/Perso2/mort");
            #endregion

            #region Players Health Bar

            // création de la texture de la barre de vie
            healthBarTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            #endregion

            #region Projectiles

            // récupération des projectiles
            projectileSprite[0] = Content.Load<Texture2D>("Img/Perso/tir/balle2");
            projectileSprite[1] = Content.Load<Texture2D>("Img/Perso/tir/balle1");
            shotExplosion = Content.Load<Texture2D>("Img/Perso/tir/shotParticle");

            #endregion

            #region Enemys
            // images des enemys

            #region Enemys Explosion

            // récupération des images de  la destruction des enemys
            mobExplosion[0] = Content.Load<Texture2D>("Img/Mobs/Mort/mort0");
            mobExplosion[1] = Content.Load<Texture2D>("Img/Mobs/Mort/mort1");
            mobExplosion[2] = Content.Load<Texture2D>("Img/Mobs/Mort/mort2");

            #endregion

            #region Cockroach

            // récupération des images du caffard
            cockroachSprites[0] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard0");
            cockroachSprites[1] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard1");

            #endregion           

            #region Beetle
            // récupération des images du scarabe
            beetleSprites[0] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe0");
            beetleSprites[1] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe1");

            #endregion

            #region Spider
            // récupération des images de l'araignée
            spiderSprites[0] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira0");
            spiderSprites[1] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira1");

            #endregion

            #endregion

            #region Items

            healthItem = Content.Load<Texture2D>("Img/Items/healthItem");

            #endregion

            // chargement des méthodes load des classes
            LevelLoad(_listSfx);

            menuLoad();

            menuPauseLoad();

            gameOverLoad();

            winLoad();
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
                    #region Timer
                    if (!isDead && !isWin) // si les joueurs ne sont pas mort
                    {
                        // le timer fonctionne
                        _timerPrincipal += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }


                    #endregion

                    #region Levels

                    // update des niveaux
                    if (listLevels[level].listEnemies.Count <= 0)
                    {
                        // update de la liste de niveau et de la trap
                        listLevels[level]._trapdoor.Update(gameTime, players, listLevels[level].listEnemies);
                    }
                    else
                    {
                        // debut de l'animation plus claire automatiquement
                        startTransitionLight = true;
                    }

                    // appel de la fonction dark
                    if (startTransitionDark)
                    {
                        opacityDark(gameTime);

                    }

                    // appel de la fonction light
                    if (startTransitionLight)
                    {
                        opacityLight(gameTime);
                    }

                    #endregion

                    #region Players

                    #region Update

                    // Mettre a jour les players
                    for (int i = players.Count - 1; i >= 0; i--)
                    {
                        if (!isOpacityDark)
                        {
                            players[i].playerUpdate(gameTime, listProjectiles, listLevels[level].listObjects);

                            // recuperation de la taille du joueur avec un plus grande HIT BOX
                            float radius = players[i].currentSprite.Width + listLevels[level]._trapdoor.currentFrame.Width * 2 / 3;


                            if (players[i].healthPoint > 0)
                            {
                                // si la HIT BOX est dans la trapdoor alors
                                if (Vector2.DistanceSquared(players[i].position, listLevels[level]._trapdoor._position) < Math.Pow(radius, 2) && listLevels[level]._trapdoor.trapdoorIsOpen)
                                {
                                    players[i].isOnTrapdoor = true;
                                }
                                else 
                                {
                                    players[i].isOnTrapdoor = false;
                                }
                            }
                            else
                            {
                                players[i].isOnTrapdoor = true;
                            }
                        }
                    }


                    for (int i = players.Count - 1; i >= 0; i--)
                    {
                        // si l un des joueurs presse le bouton pour intéragire et que l un des deux est en vie
                        if (Keyboard.GetState().IsKeyDown(players[i].interactKey) && players[i].healthPoint > 0) {
                            // si les joueurs sont sur la trapdoor
                            if (players.Count >= 2 && players[0].isOnTrapdoor && players[1].isOnTrapdoor)
                            {
                                // changement du niveau
                                changeLevel();
                            } 
                            // si le joueur est sur la trapdoor
                            else if (players.Count == 1 && players[0].isOnTrapdoor)
                            {
                                // changement de niveau
                                changeLevel();
                            }
                        }
                        // si le joueur est mort 
                        else if (players[i].healthPoint <= 0)
                        {
                            // enlever l'interaction
                            players[i].interactKey = Keys.None;
                        }
                    }

                    void changeLevel()
                    {
                        // fonction pour changer de niveau
                        if (listLevels.Count - 1 != level)
                        {
                            for (int y = players.Count - 1; y >= 0; y--)
                            {
                                if (players[y].healthPoint <= 0)
                                {
                                    players.Remove(players[y]);
                                }
                            }
                            startTransitionDark = true;
                        }
                        else
                        {
                            isWin = true;
                        }


                    }

                    #endregion

                    #region Create 

                    // creer les players, et les ajouter dans la liste players
                    if (selectedPlayer1) // si un joueur est séléctonné
                    {
                        if (players.Count <= 0)
                        {
                            players.Add(
                                new Player(gameTime, player1walkingSprites, player1shotSprites, player1DeadSprite, _listSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, Keys.G, projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 2))
                            );
                        }
                    } // sinon si deux joueurs séléctionnés
                    else if (!selectedPlayer1)
                    {
                        if (players.Count <= 0)
                        {
                            // Player 1
                            players.Add(
                                new Player(gameTime, player1walkingSprites, player1shotSprites, player1DeadSprite, _listSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, Keys.G, projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 2.25f))
                            );

                            // Player 2
                            players.Add(
                                new Player(gameTime, player2walkingSprites, player2shotSprites, player2DeadSprite, _listSfx, new Keys[] { Keys.Up, Keys.Left, Keys.Down, Keys.Right }, Keys.NumPad4, Keys.NumPad5, projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 1.75f))
                            );
                        }
                    }
                    #endregion

                    #endregion

                    #region Enemys

                    // update des enemies
                    if (!isOpacityDark)
                    {
                        for (int i = listLevels[level].listEnemies.Count - 1; i >= 0; i--)
                        {
                            listLevels[level].listEnemies[i].Update(gameTime, players, listProjectiles, listLevels[level].listEnemies, listExplosion, mobExplosion.ToList(), listLevels[level].listObjects);
                        }
                    }

                    #endregion

                    #region Items
                    //  update des items (bonnus de vie)
                    if (!isOpacityDark)
                    {
                        for (int i = listLevels[level].listItems.Count - 1; i >= 0; i--)
                        {
                            listLevels[level].listItems[i].Update(gameTime, listLevels[level].listItems, players); 
                        }
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
                            listProjectiles[i].Update(gameTime, listProjectiles, listExplosion, shotExplosion);
                        }
                    }
                    #endregion

                    gameOverUpdate(gameTime);

                }

                winUpdate(gameTime);

                // acctualisation du menu pause

                if (!isDead) // if all player aren't dead
                {
                    // acctualisation du menu pause
                    menuPauseUpdate(gameTime);
                }
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
                    List<Player> playerDrawOrder = new List<Player>();
                    foreach (Player player in players)
                    {
                        playerDrawOrder.Add(player);

                        if (player.healthPoint <= 0 && playerDrawOrder.Count == 2) // if a dead player ...
                        {
                            if (player == playerDrawOrder[1]) // ... is beeing drawn infront
                            {
                                // inverse the draw order
                                playerDrawOrder[1] = playerDrawOrder[0];
                                playerDrawOrder[0] = player;
                            }
                        }
                    }

                    foreach (Player player in playerDrawOrder)
                    {
                        player.Draw(_spriteBatch, healthBarTexture);
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

                #region Items
                // affichage des items
                foreach (Item item in listLevels[level].listItems){
                    item.Draw(_spriteBatch);
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
                        projectile.Draw(_spriteBatch);
                    }
                }

                foreach (Explosion explosion in listExplosion)
                {
                    explosion.Draw(_spriteBatch);
                }
                #endregion

                #region timer

                _spriteBatch.DrawString(font, Math.Round(_timerPrincipal, 2).ToString(), new Vector2(25, 1020), Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
                #endregion

                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * opacityTrasition, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);


                // affichage du menu pause
                menuPauseDraw(gameTime);

                // affichage du gameOver
                gameOverDraw(gameTime);

                // affichage de la page de win
                winDraw();

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void opacityDark(GameTime game)
        {
            // timer pour faire un effet de plus en plus sombre
            currentTimeMiliTrasition += (float)game.ElapsedGameTime.TotalMilliseconds;

            if (currentTimeMiliTrasition >= countDurationMiliTrasition)
            {
                timerMiliTrasition++;
                currentTimeMiliTrasition -= countDurationMiliTrasition;
            }

            if (timerMiliTrasition >= 1.5)
            {
                isOpacityDark = true;

                if (opacityTrasition >= 1f)
                {   
                    startTransitionDark = false;
                    opacityTrasition = 1f;

                    if (listLevels.Count - 1 != level)
                    {
                        level++;
                    }
                    else
                    {
                        startTransitionLight = true;
                    }
                }

                opacityTrasition = opacityTrasition + 0.1f;
                timerMiliTrasition = 0;
            }
        }

        void opacityLight(GameTime game)
        {
            // timer pour faire un effet de plus en plus claire
            currentTimeMiliTrasition += (float)game.ElapsedGameTime.TotalMilliseconds;

            if (currentTimeMiliTrasition >= countDurationMiliTrasition)
            {
                timerMiliTrasition++;
                currentTimeMiliTrasition -= countDurationMiliTrasition;
            }

            if (timerMiliTrasition >= 1.5)
            {
                if (opacityTrasition <= 0f)
                {
                    isOpacityDark = false;
                    startTransitionLight = false;
                    opacityTrasition = 0f;
                }

                opacityTrasition = opacityTrasition - 0.1f;
                timerMiliTrasition = 0;
            }
        }
    }
}