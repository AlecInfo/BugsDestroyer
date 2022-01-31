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
        private List<Player> _players = new List<Player>();
        private List<Levels> _listLevels = new List<Levels>();
        public List<Projectiles> listProjectiles = new List<Projectiles>();
        private List<Explosion> _listExplosion = new List<Explosion>();

        // Textures Autres
        private Texture2D[] _player1walkingSprites = new Texture2D[7];
        private Texture2D[] _player1shotSprites = new Texture2D[3];
        private Texture2D _player1DeadSprite;
        private Texture2D[] _player2walkingSprites = new Texture2D[7];
        private Texture2D[] _player2shotSprites = new Texture2D[3];
        private Texture2D _player2DeadSprite;
        private Texture2D[] _mobExplosion = new Texture2D[3];
        private Texture2D _shotExplosion;
        private Texture2D[] _cockroachSprites = new Texture2D[2];
        private Texture2D[] _beetleSprites = new Texture2D[2];
        private Texture2D[] _spiderSprites = new Texture2D[2];
        private Texture2D[] _butterflySprites = new Texture2D[4];
        private Texture2D _butterflyProjectile;
        private Texture2D[] _projectileSprite = new Texture2D[2];
        private Texture2D _healthBarTexture;
        private Texture2D _healthItem;

        // Textures Decor
        public List<Texture2D> Sol = new List<Texture2D>();
        private Texture2D _murs;
        private Texture2D _glass;
        private Texture2D _ombre;
        private Texture2D _processeur;
        private Texture2D _miniPci;
        private Texture2D _pci;
        private Texture2D _pileBios;
        private Texture2D _ram;
        private List<Texture2D> _trapdoor = new List<Texture2D>();

        // Menu
        private SpriteFont _font;
        private bool _isOnMenu = true;
        private bool _isPause = false;

        // Sound
        Song song;
        private SoundEffect _keyboardSfx;
        public int music = 0;

        // Sfx
        private List<SoundEffect> _listSfx = new List<SoundEffect>();
        SoundEffect MenuSfx;
        SoundEffect StartSfx;

        //Timer
        public float timerPrincipal = 0f;

        // Levels
        private static int _level = 0;
        private bool _isOpacityDark = true;
        private bool _startTransitionDark = false;
        private bool _startTransitionLight = false;
        private float _opacityTrasition = 1f;
        private float _currentTimeMiliTrasition = 0f;
        private float _countDurationMiliTrasition = 1f;
        private int _timerMiliTrasition = 0;

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

            _murs = Content.Load<Texture2D>("Img/Decor/mur1");
            _glass = Content.Load<Texture2D>("Img/Decor/Glass");
            _ombre = Content.Load<Texture2D>("Img/Decor/Ombre");

            _processeur = Content.Load<Texture2D>("Img/Decor/Processeur");
            _miniPci = Content.Load<Texture2D>("Img/Decor/MiniPCI");
            _pci = Content.Load<Texture2D>("Img/Decor/PCI");
            _pileBios = Content.Load<Texture2D>("Img/Decor/PileBios");
            _ram = Content.Load<Texture2D>("Img/Decor/Ram");

            _trapdoor.Add(Content.Load<Texture2D>("Img/Decor/trapdoor0"));
            _trapdoor.Add(Content.Load<Texture2D>("Img/Decor/trapdoor1"));

            #endregion

            #region Players Walking 

            // recupération des images des joueur en marche
            for (int x = 0; x < 7; x++)
            {
                _player1walkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
                _player2walkingSprites[x] = Content.Load<Texture2D>("Img/Perso2/walking/walking" + x.ToString());
            }
            #endregion

            #region Players Shooting

            // récupération des images en tire
            _player1shotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            _player1shotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            _player2shotSprites[0] = Content.Load<Texture2D>("Img/Perso2/shot/shot0");
            _player2shotSprites[1] = Content.Load<Texture2D>("Img/Perso2/shot/shot1");
            #endregion

            #region Players Dead

            // récupération des images (mort)
            _player1DeadSprite = Content.Load<Texture2D>("Img/Perso/mort");
            _player2DeadSprite = Content.Load<Texture2D>("Img/Perso2/mort");
            #endregion

            #region Players Health Bar

            // création de la texture de la barre de vie
            _healthBarTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            #endregion

            #region Projectiles

            // récupération des projectiles
            _projectileSprite[0] = Content.Load<Texture2D>("Img/Perso/tir/balle2");
            _projectileSprite[1] = Content.Load<Texture2D>("Img/Perso/tir/balle1");
            _shotExplosion = Content.Load<Texture2D>("Img/Perso/tir/shotParticle");

            #endregion

            #region Enemys
            // images des enemys

            #region Enemys Explosion

            // récupération des images de  la destruction des enemys
            _mobExplosion[0] = Content.Load<Texture2D>("Img/Mobs/Mort/mort0");
            _mobExplosion[1] = Content.Load<Texture2D>("Img/Mobs/Mort/mort1");
            _mobExplosion[2] = Content.Load<Texture2D>("Img/Mobs/Mort/mort2");

            #endregion

            #region Cockroach

            // récupération des images du caffard
            _cockroachSprites[0] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard0");
            _cockroachSprites[1] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard1");

            #endregion           

            #region Beetle
            // récupération des images du scarabe
            _beetleSprites[0] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe0");
            _beetleSprites[1] = Content.Load<Texture2D>("Img/Mobs/Scarabe/scarabe1");

            #endregion

            #region Spider
            // récupération des images de l'araignée
            _spiderSprites[0] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira0");
            _spiderSprites[1] = Content.Load<Texture2D>("Img/Mobs/Armadeira/armadeira1");

            #endregion

            #region Butterfly

            _butterflySprites[0] = Content.Load<Texture2D>("Img/Mobs/Papillon/cocon");
            _butterflySprites[1] = Content.Load<Texture2D>("Img/Mobs/Papillon/papillon0");
            _butterflySprites[2] = Content.Load<Texture2D>("Img/Mobs/Papillon/papillon1");
            _butterflySprites[3] = Content.Load<Texture2D>("Img/Mobs/Papillon/papillon2");

            _butterflyProjectile = Content.Load<Texture2D>("Img/Mobs/butterflyProjectile");

            #endregion

            #endregion

            #region Items

            _healthItem = Content.Load<Texture2D>("Img/Items/healthItem");

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
            if (_isOnMenu)
            {
                menuUpdate(gameTime);
            } // sinon si il est pas dans le menu
            else if (!_isOnMenu)
            {

                // si le joueur est pas dans le menu pause
                if (!_isPause)
                {
                    #region Timer
                    if (!_isDead && !_isWin) // si les joueurs ne sont pas mort
                    {
                        // le timer fonctionne
                        timerPrincipal += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }


                    #endregion

                    #region Levels

                    // update des niveaux
                    if (_listLevels[_level].listEnemies.Count <= 0)
                    {
                        // update de la liste de niveau et de la trap
                        _listLevels[_level].trapdoor.Update(gameTime, _players, _listLevels[_level].listEnemies);
                    }
                    else
                    {
                        // debut de l'animation plus claire automatiquement
                        _startTransitionLight = true;
                    }

                    // appel de la fonction dark
                    if (_startTransitionDark)
                    {
                        opacityDark(gameTime);

                    }

                    // appel de la fonction light
                    if (_startTransitionLight)
                    {
                        opacityLight(gameTime);
                    }

                    #endregion

                    #region Players

                    #region Update

                    // Mettre a jour les players
                    for (int i = _players.Count - 1; i >= 0; i--)
                    {
                        if (!_isOpacityDark)
                        {
                            _players[i].playerUpdate(gameTime, listProjectiles, _listLevels[_level].listObjects);

                            // recuperation de la taille du joueur avec un plus grande HIT BOX
                            float radius = _players[i].currentSprite.Width + _listLevels[_level].trapdoor.currentFrame.Width * 2 / 3;


                            if (_players[i].healthPoint > 0)
                            {
                                // si la HIT BOX est dans la trapdoor alors
                                if (Vector2.DistanceSquared(_players[i].position, _listLevels[_level].trapdoor.position) < Math.Pow(radius, 2) && _listLevels[_level].trapdoor.trapdoorIsOpen)
                                {
                                    _players[i].isOnTrapdoor = true;
                                }
                                else 
                                {
                                    _players[i].isOnTrapdoor = false;
                                }
                            }
                            else
                            {
                                _players[i].isOnTrapdoor = true;
                            }
                        }
                    }


                    for (int i = _players.Count - 1; i >= 0; i--)
                    {
                        // si l un des joueurs presse le bouton pour intéragire et que l un des deux est en vie
                        if (Keyboard.GetState().IsKeyDown(_players[i].interactKey) && _players[i].healthPoint > 0) {
                            // si les joueurs sont sur la trapdoor
                            if (_players.Count >= 2 && _players[0].isOnTrapdoor && _players[1].isOnTrapdoor)
                            {
                                // changement du niveau
                                changeLevel();
                            } 
                            // si le joueur est sur la trapdoor
                            else if (_players.Count == 1 && _players[0].isOnTrapdoor)
                            {
                                // changement de niveau
                                changeLevel();
                            }
                        }
                        // si le joueur est mort 
                        else if (_players[i].healthPoint <= 0)
                        {
                            // enlever l'interaction
                            _players[i].interactKey = Keys.None;
                        }
                    }

                    void changeLevel()
                    {
                        // fonction pour changer de niveau
                        if (_listLevels.Count - 1 != _level)
                        {
                            for (int y = _players.Count - 1; y >= 0; y--)
                            {
                                if (_players[y].healthPoint <= 0)
                                {
                                    _players.Remove(_players[y]);
                                }
                            }
                            _startTransitionDark = true;
                        }
                        else
                        {
                            _isWin = true;
                        }


                    }

                    #endregion

                    #region Create 

                    // creer les players, et les ajouter dans la liste players
                    if (_selectedPlayer1) // si un joueur est séléctonné
                    {
                        if (_players.Count <= 0)
                        {
                            _players.Add(
                                new Player(gameTime, _graphics, _player1walkingSprites, _player1shotSprites, _player1DeadSprite, _listSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, Keys.G, _projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 2))
                            );
                        }
                    } // sinon si deux joueurs séléctionnés
                    else if (!_selectedPlayer1)
                    {
                        if (_players.Count <= 0)
                        {
                            // Player 1
                            _players.Add(
                                new Player(gameTime, _graphics, _player1walkingSprites, _player1shotSprites, _player1DeadSprite, _listSfx, new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }, Keys.F, Keys.G, _projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 2.25f))
                            );

                            // Player 2
                            _players.Add(
                                new Player(gameTime, _graphics, _player2walkingSprites, _player2shotSprites, _player2DeadSprite, _listSfx, new Keys[] { Keys.Up, Keys.Left, Keys.Down, Keys.Right }, Keys.NumPad4, Keys.NumPad5, _projectileSprite, new Vector2(_graphics.PreferredBackBufferWidth / 5, _graphics.PreferredBackBufferHeight / 1.75f))
                            );
                        }
                    }
                    #endregion

                    #endregion

                    #region Enemys

                    // update des enemies
                    if (!_isOpacityDark)
                    {
                        for (int i = _listLevels[_level].listEnemies.Count - 1; i >= 0; i--)
                        {
                            _listLevels[_level].listEnemies[i].Update(gameTime, _players, listProjectiles, _listLevels[_level].listEnemies, _listExplosion, _mobExplosion.ToList(), _listLevels[_level].listObjects);
                        }
                    }

                    #endregion

                    #region Items
                    //  update des items (bonnus de vie)
                    if (!_isOpacityDark)
                    {
                        for (int i = _listLevels[_level].listItems.Count - 1; i >= 0; i--)
                        {
                            _listLevels[_level].listItems[i].Update(gameTime, _listLevels[_level].listItems, _players); 
                        }
                    }
                    #endregion

                    #region Projectiles

                    // update des projectiles
                    for (int i = _listExplosion.Count - 1; i >= 0; i--)
                    {
                        _listExplosion[i].Update(gameTime, _listExplosion);
                    }

                    // si la liste du nombre de projectile n'est pas à zero
                    if (listProjectiles.Count != 0)
                    {
                        // parcourir la liste en partant du plus grand index
                        for (int i = listProjectiles.Count -1; i >= 0; i--)
                        {
                            // acctualisation du projectile
                            listProjectiles[i].Update(gameTime, listProjectiles, _listExplosion, _shotExplosion);
                        }
                    }
                    #endregion

                    gameOverUpdate(gameTime);

                }

                winUpdate(gameTime);

                // acctualisation du menu pause

                if (!_isDead) // if all player aren't dead
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
            if (_isOnMenu)
            {
                // affichage du menu
                menuDraw();
            } // sinon si il n'est pas dans le menu
            else if (!_isOnMenu)
            {
                #region Decor
                // affichage du sol ( multiplier )
                for (int y = 0; y < _graphics.PreferredBackBufferHeight; y += _listLevels[_level].background.Height / 2)
                {
                    for (int x = 0; x < _graphics.PreferredBackBufferWidth; x += _listLevels[_level].background.Width / 2)
                    {
                        _spriteBatch.Draw(_listLevels[_level].background, new Vector2(x, y), null, Color.White * 0.75f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    }
                }


                // Décor
                foreach (Object item in _listLevels[_level].listObjects)
                {
                    item.Draw(_spriteBatch);
                }


                _listLevels[_level].trapdoor.Draw(_spriteBatch);
                #endregion

                #region Player 
                // si le un joueur est sélectionnée
                if (_players.Count > 0)
                {
                    List<Player> playerDrawOrder = new List<Player>();
                    foreach (Player player in _players)
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
                        player.Draw(_spriteBatch, _healthBarTexture);
                    }
                }
                #endregion
                
                #region Items
                // affichage des items
                foreach (Item item in _listLevels[_level].listItems){
                    item.Draw(_spriteBatch);
                }
                #endregion

                #region Murs / Ombre
                // affichage d'une ombre à coté des murs
                //_spriteBatch.Draw(_ombre, new Vector2(_graphics.PreferredBackBufferWidth / 7, _graphics.PreferredBackBufferHeight / 8), null, Color.White * 0.75f, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                
                // affichage des murs
                //_spriteBatch.Draw(_murs, new Vector2(-70, -42), null, Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                _spriteBatch.Draw(_murs,
                   new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height),
                   new Rectangle(0, 0, _murs.Width, _murs.Height),
                   Color.White);
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

                foreach (Explosion explosion in _listExplosion)
                {
                    explosion.Draw(_spriteBatch);
                }
                #endregion

                #region timer

                _spriteBatch.DrawString(_font, Math.Round(timerPrincipal, 2).ToString(), new Vector2(25, 1020), Color.White, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
                #endregion


                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * _opacityTrasition, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                #region Enemys
                // affichage des enemies
                foreach (Enemy enemy in _listLevels[_level].listEnemies)
                {
                    enemy.Draw(_spriteBatch, _graphics.GraphicsDevice);
                }
                #endregion

                // affichage du menu pause
                menuPauseDraw();

                // affichage du gameOver
                gameOverDraw();

                // affichage de la page de win
                winDraw();

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }


        /// <summary>
        /// Confection de l'animation qui va de plus en plus sombre et puis change de niveau
        /// (Alec Piette)
        /// </summary>
        /// <param name="game"></param>
        void opacityDark(GameTime game)
        {
            // timer pour faire un effet de plus en plus sombre
            _currentTimeMiliTrasition += (float)game.ElapsedGameTime.TotalMilliseconds;

            if (_currentTimeMiliTrasition >= _countDurationMiliTrasition)
            {
                _timerMiliTrasition++;
                _currentTimeMiliTrasition -= _countDurationMiliTrasition;
            }

            if (_timerMiliTrasition >= 1.5)
            {
                _isOpacityDark = true;

                if (_opacityTrasition >= 1f)
                {   
                    _startTransitionDark = false;
                    _opacityTrasition = 1f;

                    if (_listLevels.Count - 1 != _level)
                    {
                        _level++;
                    }
                    else
                    {
                        _startTransitionLight = true;
                    }
                }

                _opacityTrasition = _opacityTrasition + 0.1f;
                _timerMiliTrasition = 0;
            }
        }

        /// <summary>
        /// Confection de l'animation qui va de plus en plus claire
        /// (Alec Piette)
        /// </summary>
        /// <param name="game"></param>
        void opacityLight(GameTime game)
        {
            // timer pour faire un effet de plus en plus clair
            _currentTimeMiliTrasition += (float)game.ElapsedGameTime.TotalMilliseconds;

            if (_currentTimeMiliTrasition >= _countDurationMiliTrasition)
            {
                _timerMiliTrasition++;
                _currentTimeMiliTrasition -= _countDurationMiliTrasition;
            }

            if (_timerMiliTrasition >= 1.5)
            {
                if (_opacityTrasition <= 0f)
                {
                    _isOpacityDark = false;
                    _startTransitionLight = false;
                    _opacityTrasition = 0f;
                }

                _opacityTrasition = _opacityTrasition - 0.1f;
                _timerMiliTrasition = 0;
            }
        }
    }
}