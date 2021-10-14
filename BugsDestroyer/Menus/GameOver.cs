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

        private bool isDead = false;
        private bool isOnMenuGameOver = false;
        private bool gameOver = true;
        private float opacity = 0f;
        private Song songGameOver;
        private bool isGOSongPlaying = false;
        private bool isChangeSelect = true;

        // images
        private List<Texture2D> _listGameOver;

        // timer seconds
        private float currentTimeSecond = 0f;
        private float countDurationSecond = 1f;
        private int timerSecond = 0;

        // timer milisecond
        private float currentTimeMili = 0f;
        private float countDurationMili = 1f;
        private int timerMili = 0;

        // question
        private bool isYes = true;
        private Color onYes = Color.Lime;
        private Color onNo = Color.White * 0.4f;


        protected void gameOverLoad()
        {
            // load img gameOver
            _listGameOver = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/gameOver"),
                Content.Load<Texture2D>("Img/Logo/logo_CFPT"),
                Content.Load<Texture2D>("Img/Logo/logo_espaceEntreprise"),
            };

            this.songGameOver = Content.Load<Song>("Sounds/Music/Funeral March");

            MenuSfx = Content.Load<SoundEffect>("Sounds/Sfx/MenuSfx");
            StartSfx = Content.Load<SoundEffect>("Sounds/Sfx/StartSfx");
        }

        protected void gameOverUpdate(GameTime gameTime)
        {
            // si le player est mort
            if ((selectedPlayer1 && player1.healthPoint <= 0) || (!selectedPlayer1 && player1.healthPoint <= 0 && player2.healthPoint <= 0))
            {
                isDead = true;
            }

            if (isDead)
            {
                if (!isOnMenuGameOver)
                {
                    // timer pour faire un effet de plus en plus sombre
                    currentTimeMili += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (currentTimeMili >= countDurationMili)
                    {
                        timerMili++;
                        currentTimeMili -= countDurationMili;
                    }

                    if (timerMili >= 10)
                    {
                        if (opacity >= 1f)
                        {
                            isOnMenuGameOver = true;
                        }

                        opacity = opacity + 0.1f;
                        timerMili = 0;
                    }

                }
                else if (isOnMenuGameOver)
                {
                    if (!isGOSongPlaying)
                    { 
                        MediaPlayer.Stop();
                        MediaPlayer.Play(songGameOver);
                        MediaPlayer.IsRepeating = false;
                        MediaPlayer.Volume = 0.5f;
                        isGOSongPlaying = true;
                    }

                    // timer pour faire un effect sur l'img game over
                    currentTimeSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (currentTimeSecond >= countDurationSecond)
                    {
                        timerSecond++;
                        currentTimeSecond -= countDurationSecond;
                    }

                    if (timerSecond >= 1)
                    {
                        timerSecond = 0;
                        gameOver = !gameOver;
                    }

                    // changement de selection

                    if (!isYes && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)))
                    {
                        MenuSfx.Play();
                        onYes = Color.Lime;
                        onNo = Color.White * 0.4f;
                        isYes = true;
                        isChangeSelect = false;
                    }
                    else if (isYes && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                    {
                        MenuSfx.Play();
                        onYes = Color.White * 0.4f;
                        onNo = Color.Lime;
                        isYes = false;
                        isChangeSelect = false;
                    }



                    // reponse au question si oui 
                    if (isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        level = 0;

                        StartSfx.Play();
                        this.Exit();
                        Game1 game = new Game1();
                        game.Run();
                    }// si non
                    else if (!isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        StartSfx.Play();
                        this.Exit();
                    }
                }
            }
        }

        protected void gameOverDraw(GameTime gameTime)
        {
            if (isDead)
            {
                // background
                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * opacity, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);


                if (isOnMenuGameOver)
                {
                    if (gameOver)
                    {
                        // affichage de l'image game over
                        _spriteBatch.Draw(_listGameOver[0], new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 3), null, Color.White, 0f, new Vector2(_listGameOver[0].Width / 2, _listGameOver[0].Height / 2), 1.5f, SpriteEffects.None, 0f);
                    }

                    // affichage des logos
                    _spriteBatch.Draw(_listGameOver[1], new Vector2(100, 1000), null, Color.White * 0.4f, 0f, new Vector2(_listGameOver[1].Width / 2, _listGameOver[1].Height / 2), 1.5f, SpriteEffects.None, 0f);
                    _spriteBatch.Draw(_listGameOver[2], new Vector2(1830, 1000), null, Color.White * 0.4f, 0f, new Vector2(_listGameOver[2].Width / 2, _listGameOver[2].Height / 2), 1.5f, SpriteEffects.None, 0f);

                    // affichge de la question reponse
                    _spriteBatch.DrawString(font, "do you want to continue ?", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 50), Color.White * 0.8f, 0f, new Vector2(font.MeasureString("DO YOU WANT TO CONTINUE ?").X / 2, font.MeasureString("DO YOU WANT TO CONTINUE ?").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    
                    _spriteBatch.DrawString(font, "yes", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50, _graphics.PreferredBackBufferHeight / 2 + 100), onYes, 0f, new Vector2(font.MeasureString("YES").X / 2, font.MeasureString("YES").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    _spriteBatch.DrawString(font, "no", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 50, _graphics.PreferredBackBufferHeight / 2 + 100), onNo, 0f, new Vector2(font.MeasureString("NO").X / 2, font.MeasureString("NO").Y / 2), 0.5f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
