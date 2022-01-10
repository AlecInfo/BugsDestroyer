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
        private List<Texture2D> _menuImages;

        private Color _colorSectionPlayer = Color.White;
        private Color _colorSectionGame = Color.White * 0.4f;
        private bool _isSectionPlayer = true;

        private string _selectedPlayerText = "1 player";
        private bool _selectedPlayer1 = true;


        /// <summary>
        /// Récupération de touts les éléments don le menu a besoin
        /// (Alec Piette)
        /// </summary>
        protected void menuLoad()
        {
            // liste de sprite 
            _menuImages = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp"),
                Content.Load<Texture2D>("Img/Menu/flecheDroite"),
                Content.Load<Texture2D>("Img/Menu/flecheGauche"),
                Content.Load<Texture2D>("Img/Menu/boutonCentre"),
                Content.Load<Texture2D>("Img/Menu/panelRouge"),
                Content.Load<Texture2D>("Img/Menu/panelBleu"),
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp1"),
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp2"),
            };

            _font = Content.Load<SpriteFont>("Fonts/GameBoy30");

            MenuSfx = Content.Load<SoundEffect>("Sounds/Sfx/MenuSfx");
            StartSfx = Content.Load<SoundEffect>("Sounds/Sfx/StartSfx");
        }

        /// <summary>
        /// Update du menu Principal
        /// (Alec Piette)
        /// </summary>
        /// <param name="gameTime"></param>
        protected void menuUpdate(GameTime gameTime)
        {
            // modification de couleur et d'opacitiée entre la sélection de player et play
            if (_isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)))
            {
                MenuSfx.Play();
                _isSectionPlayer = false;
                _colorSectionPlayer = Color.LightGray * 0.5f;
                _colorSectionGame = Color.White;
                
            }
            else if (!_isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)))
            {
                MenuSfx.Play();
                _isSectionPlayer = true;
                _colorSectionGame = Color.LightGray * 0.5f;
                _colorSectionPlayer = Color.White;
            }

            
            if (_isSectionPlayer)
            {
                // Les sélections 1 player ou 2 player
                if (_selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D9)))
                {
                    MenuSfx.Play();
                    _selectedPlayer1 = false;
                    _selectedPlayerText = "2 players";
                }
                else if (!_selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.D7)))
                {
                    MenuSfx.Play();
                    _selectedPlayer1 = true;
                    _selectedPlayerText = "1 player";
                }
            }
            else if (!_isSectionPlayer)
            {
                // lencement du jeu
                if (_isOnMenu && (Keyboard.GetState().IsKeyDown(Keys.D8)))
                {
                    StartSfx.Play();
                    _isOnMenu = false;
                }
            }
        }

        /// <summary>
        /// Affichage du menu Principal
        /// (Alec Piette)
        /// </summary>
        protected void menuDraw()
        {
            // background
            _spriteBatch.Draw(_menuImages[6],
               new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height),
               new Rectangle(0, 0, _menuImages[6].Width, _menuImages[6].Height),
               Color.White);

            // Titre
            _spriteBatch.DrawString(_font, "bugs destroyer", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(_font.MeasureString("bugs destroyer").X / 2, _font.MeasureString("bugs destroyer").Y / 2), 2f, SpriteEffects.None, 0f);

            // Choix du joueur
            if (!_selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[2], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 175, 430), null, _colorSectionPlayer, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
            }
            _spriteBatch.DrawString(_font, _selectedPlayerText, new Vector2(_graphics.PreferredBackBufferWidth / 2, 450), _colorSectionPlayer, 0f, new Vector2(_font.MeasureString(_selectedPlayerText).X / 2, _font.MeasureString(_selectedPlayerText).Y / 2), 0.75f, SpriteEffects.None, 0f);
            if (_selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[1], new Vector2(_graphics.PreferredBackBufferWidth / 2 + 130, 430), null, _colorSectionPlayer, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
            }

            // Jeux
            _spriteBatch.DrawString(_font, "Play", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50, 520), _colorSectionGame, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);

            //Controle
            _spriteBatch.Draw(_menuImages[3], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 130, 720), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_font, "play-pause", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 43, 700), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_font, "quit", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 90, 700), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            if (_selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 120, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 -145, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 48, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 10, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            }
            else
            {
                _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 325, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 225, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 190, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);

                _spriteBatch.Draw(_menuImages[5], new Vector2(_graphics.PreferredBackBufferWidth / 2 + 70, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 40, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 145, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(_font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 180, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            }
        }

        
}
}