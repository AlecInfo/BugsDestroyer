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
        private List<Texture2D> _menuPauseImages;
        private bool DollarKeyIsUp = false;

        protected void menuPauseLoad()
        {
            _menuPauseImages = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp"),
                Content.Load<Texture2D>("Img/Menu/boutonCentre"),
                Content.Load<Texture2D>("Img/Menu/panelRouge"),
                Content.Load<Texture2D>("Img/Menu/panelBleu"),
            };

            font = Content.Load<SpriteFont>("Fonts/GameBoy30");

        }

        protected void menuPauseUpdate(GameTime gameTime)
        {
            if (isPause)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D8) && DollarKeyIsUp)
                {
                    DollarKeyIsUp = false;
                    isPause = false;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D8) && DollarKeyIsUp == false)
                {
                    isPause = true;
                    DollarKeyIsUp = true;
                }
            }
            else
            {

                if (Keyboard.GetState().IsKeyDown(Keys.D8) && DollarKeyIsUp)
                {
                    DollarKeyIsUp = false;
                    isPause = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D8) && DollarKeyIsUp == false)
                {
                    isPause = false;
                    DollarKeyIsUp = true;
                }
            }
        }

        protected void menuPauseDraw(GameTime gameTime)
        {
            if (isPause)
            {
                // background
                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * 0.5f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                // Titre
                _spriteBatch.DrawString(font, "pause", new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), Color.White, 0f, new Vector2(font.MeasureString("PAUSE").X / 2, font.MeasureString("PAUSE").Y / 2), 2f, SpriteEffects.None, 0f);

                //Controle
                _spriteBatch.Draw(_menuPauseImages[1], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, 720), null, Color.White, 0f, Vector2.Zero, 1.7f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "play-pause", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 53, 700), Color.White, 0f, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "quit", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 95, 700), Color.White, 0f, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0f);
            }
        }  
    }
}