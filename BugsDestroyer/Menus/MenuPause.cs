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
        // Varriables
        private List<Texture2D> _menuPauseImages;
        private bool _dollarKeyIsUp = false;

        /// <summary>
        /// Récupération de tous les éléments (images) du menu Pause
        /// (Alec Piette)
        /// </summary>
        protected void menuPauseLoad()
        {
            // Listes de Sprites
            _menuPauseImages = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp"),
                Content.Load<Texture2D>("Img/Menu/boutonCentre"),
                Content.Load<Texture2D>("Img/Menu/panelRouge"),
                Content.Load<Texture2D>("Img/Menu/panelBleu"),
            };

            _font = Content.Load<SpriteFont>("Fonts/GameBoy30");

        }

        /// <summary>
        /// Update du menu Pause
        /// (Alec Piette)
        /// </summary>
        /// <param name="gameTime"></param>
        protected void menuPauseUpdate(GameTime gameTime)
        {
            // si le joueur a appuié sur pause
            if (_isPause)
            {
                // quand le joueur est dans le menu pause
                MediaPlayer.Volume = 0.5f;
                

                if (Keyboard.GetState().IsKeyDown(Keys.D8) && _dollarKeyIsUp)
                {
                    _dollarKeyIsUp = false;
                    _isPause = false;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D8) && _dollarKeyIsUp == false)
                {
                    _isPause = true;
                    _dollarKeyIsUp = true;
                }
            }
            else
            {
                // quand il ne l est plus
                MediaPlayer.Volume = 1f;

                if (Keyboard.GetState().IsKeyDown(Keys.D8) && _dollarKeyIsUp)
                {
                    _dollarKeyIsUp = false;
                    _isPause = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.D8) && _dollarKeyIsUp == false)
                {
                    _isPause = false;
                    _dollarKeyIsUp = true;
                }
            }
        }

        /// <summary>
        /// Affichages des éléments pour le menu pause
        /// (Alec Piette)
        /// </summary>
        protected void menuPauseDraw()
        {
            if (_isPause)
            {
                // background
                _spriteBatch.Draw(_menuPauseImages[0], new Vector2(0, -100), null, Color.Black * 0.5f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                // Titre
                _spriteBatch.DrawString(_font, "pause", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(_font.MeasureString("PAUSE").X / 2, _font.MeasureString("PAUSE").Y / 2), 2f, SpriteEffects.None, 0f);

                //Controle
                _spriteBatch.Draw(_menuPauseImages[1], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, 720), null, Color.White, 0f, Vector2.Zero, 1.7f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "play-pause", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 53, 700), Color.White, 0f, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "quit", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 95, 700), Color.White, 0f, new Vector2(0, 0), 0.30f, SpriteEffects.None, 0f);
            }
        }  
    }
}