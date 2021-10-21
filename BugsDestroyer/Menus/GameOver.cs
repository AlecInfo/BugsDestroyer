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
        // varriables
        private bool _isDead = false;
        private bool _isOnMenuGameOver = false;
        private bool _gameOver = true;
        private float _opacity = 0f;
        private Song _songGameOver;
        private bool _isGOSongPlaying = false;

        // images
        private List<Texture2D> _listGameOver;

        // timer seconds
        private float _currentTimeSecond = 0f;
        private float _countDurationSecond = 1f;
        private int _timerSecond = 0;

        // timer miliseconde
        private float _currentTimeMili = 0f;
        private float _countDurationMili = 1f;
        private int _timerMili = 0;

        // question
        private bool _isYes = true;
        private Color _onYes = Color.Lime;
        private Color _onNo = Color.White * 0.4f;

        /// <summary>
        /// Récupération de toutes les images pour le menu GameOver
        /// (Alec Piette)
        /// </summary>
        protected void gameOverLoad()
        {
            // load img gameOver
            _listGameOver = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/gameOver"),
                Content.Load<Texture2D>("Img/Logo/logo_CFPT"),
                Content.Load<Texture2D>("Img/Logo/logo_espaceEntreprise"),
            };

            this._songGameOver = Content.Load<Song>("Sounds/Music/Funeral March");

            MenuSfx = Content.Load<SoundEffect>("Sounds/Sfx/MenuSfx");
            StartSfx = Content.Load<SoundEffect>("Sounds/Sfx/StartSfx");
        }

        /// <summary>
        /// Update du menu GameOver
        /// (Alec Piette)
        /// </summary>
        /// <param name="gameTime"></param>
        protected void gameOverUpdate(GameTime gameTime)
        {
            // si le player est mort
            if (_players.Count == 2)
            {
                if ((_selectedPlayer1 && _players[0].healthPoint <= 0) || (!_selectedPlayer1 && _players[0].healthPoint <= 0 && _players[1].healthPoint <= 0))
                {
                    _isDead = true;
                }
            }
            else
            {
                if ((_selectedPlayer1 && _players[0].healthPoint <= 0) || (!_selectedPlayer1 && _players[0].healthPoint <= 0))
                {
                    _isDead = true;
                }
            }

            if (_isDead)
            {
                if (!_isOnMenuGameOver)
                {
                    // timer pour faire un effet de plus en plus sombre
                    _currentTimeMili += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (_currentTimeMili >= _countDurationMili)
                    {
                        _timerMili++;
                        _currentTimeMili -= _countDurationMili;
                    }

                    if (_timerMili >= 10)
                    {
                        if (_opacity >= 1f)
                        {
                            _isOnMenuGameOver = true;
                        }

                        _opacity = _opacity + 0.1f;
                        _timerMili = 0;
                    }

                }
                else if (_isOnMenuGameOver)
                {
                    if (!_isGOSongPlaying)
                    { 
                        MediaPlayer.Stop();
                        MediaPlayer.Play(_songGameOver);
                        MediaPlayer.IsRepeating = false;
                        MediaPlayer.Volume = 0.5f;
                        _isGOSongPlaying = true;
                    }

                    // timer pour faire un effect sur l'img game over
                    _currentTimeSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (_currentTimeSecond >= _countDurationSecond)
                    {
                        _timerSecond++;
                        _currentTimeSecond -= _countDurationSecond;
                    }

                    if (_timerSecond >= 1)
                    {
                        _timerSecond = 0;
                        _gameOver = !_gameOver;
                    }

                    // changement de selection
                    if (!_isYes && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)))
                    {
                        MenuSfx.Play();
                        _onYes = Color.Lime;
                        _onNo = Color.White * 0.4f;
                        _isYes = true;
                    }
                    else if (_isYes && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                    {
                        MenuSfx.Play();
                        _onYes = Color.White * 0.4f;
                        _onNo = Color.Lime;
                        _isYes = false;
                    }



                    // reponse au question si oui 
                    if (_isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        _level = 0;

                        StartSfx.Play();
                        this.Exit();
                        Game1 game = new Game1();
                        game.Run();
                    }// si non
                    else if (!_isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        StartSfx.Play();
                        this.Exit();
                    }
                }
            }
        }

        /// <summary>
        /// Affichage des éléments du menu GameOver
        /// (Alec Piette)
        /// </summary>
        protected void gameOverDraw()
        {
            if (_isDead)
            {
                // background
                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * _opacity, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);


                if (_isOnMenuGameOver)
                {
                    if (_gameOver)
                    {
                        // affichage de l'image game over
                        _spriteBatch.Draw(_listGameOver[0], new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 3), null, Color.White, 0f, new Vector2(_listGameOver[0].Width / 2, _listGameOver[0].Height / 2), 1.5f, SpriteEffects.None, 0f);
                    }

                    // affichage des logos
                    _spriteBatch.Draw(_listGameOver[1], new Vector2(100, 1000), null, Color.White * 0.4f, 0f, new Vector2(_listGameOver[1].Width / 2, _listGameOver[1].Height / 2), 1.5f, SpriteEffects.None, 0f);
                    _spriteBatch.Draw(_listGameOver[2], new Vector2(1830, 1000), null, Color.White * 0.4f, 0f, new Vector2(_listGameOver[2].Width / 2, _listGameOver[2].Height / 2), 1.5f, SpriteEffects.None, 0f);

                    // affichge de la question reponse
                    _spriteBatch.DrawString(_font, "do you want to continue ?", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 50), Color.White * 0.8f, 0f, new Vector2(_font.MeasureString("DO YOU WANT TO CONTINUE ?").X / 2, _font.MeasureString("DO YOU WANT TO CONTINUE ?").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    
                    _spriteBatch.DrawString(_font, "yes", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50, _graphics.PreferredBackBufferHeight / 2 + 100), _onYes, 0f, new Vector2(_font.MeasureString("YES").X / 2, _font.MeasureString("YES").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    _spriteBatch.DrawString(_font, "no", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 50, _graphics.PreferredBackBufferHeight / 2 + 100), _onNo, 0f, new Vector2(_font.MeasureString("NO").X / 2, _font.MeasureString("NO").Y / 2), 0.5f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
