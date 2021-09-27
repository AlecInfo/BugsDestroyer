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

        public bool isDead = false;
        public bool isOnMenuGameOver = false;
        public bool gameOver = true;
        public float opacity = 0f;

        // images
        public Texture2D _imgGameOver;

        // timer seconds
        public float currentTimeSecond = 0f;
        public float countDurationSecond = 1f;
        public int timerSecond = 0;

        // timer milisecond
        public float currentTimeMili = 0f;
        public float countDurationMili = 1f;
        public int timerMili = 0;

        // question
        public bool isYes = true;
        public Color onYes = Color.Lime;
        public Color onNo = Color.White * 0.6f;

        protected void gameOverLoad()
        {
            // load img gameOver
            _imgGameOver = Content.Load<Texture2D>("Img/Menu/gameOver");
        }

        protected void gameOverUpdate(GameTime gameTime)
        {
            // si le player est mort
            if (player1.healthPoint <= 0)
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
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        onYes = Color.Lime;
                        onNo = Color.White * 0.6f;
                        isYes = true;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        onYes = Color.White * 0.6f;
                        onNo = Color.Lime;
                        isYes = false;
                    }

                    // reponse au question si oui 
                    if (isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        this.Exit();
                        Game1 game = new Game1();
                        game.Run();
                    }// si non
                    else if (!isYes && Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
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
                        _spriteBatch.Draw(_imgGameOver, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 3), null, Color.White, 0f, new Vector2(_imgGameOver.Width / 2, _imgGameOver.Height / 2), 1.5f, SpriteEffects.None, 0f);
                    }
                    // affichge de la question reponse
                    _spriteBatch.DrawString(font, "do you want to continue ?", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), Color.White * 0.8f, 0f, new Vector2(font.MeasureString("DO YOU WANT TO CONTINUE ?").X / 2, font.MeasureString("DO YOU WANT TO CONTINUE ?").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    
                    _spriteBatch.DrawString(font, "yes", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50, _graphics.PreferredBackBufferHeight / 2 + 50), onYes, 0f, new Vector2(font.MeasureString("YES").X / 2, font.MeasureString("YES").Y / 2), 0.5f, SpriteEffects.None, 0f);
                    _spriteBatch.DrawString(font, "no", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 50, _graphics.PreferredBackBufferHeight / 2 + 50), onNo, 0f, new Vector2(font.MeasureString("NO").X / 2, font.MeasureString("NO").Y / 2), 0.5f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
