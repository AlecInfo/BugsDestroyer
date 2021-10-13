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

        private Color colorSectionPlayer = Color.White;
        private Color colorSectionGame = Color.LightGray * 0.7f;
        private bool isSectionPlayer = true;

        private string selectedPlayerText = "1 player";
        private bool selectedPlayer1 = true;



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

            font = Content.Load<SpriteFont>("Fonts/GameBoy30");

            MenuSfx = Content.Load<SoundEffect>("Sounds/Sfx/MenuSfx");
            StartSfx = Content.Load<SoundEffect>("Sounds/Sfx/StartSfx");
        }

        protected void menuUpdate(GameTime gameTime)
        {
            // modification de couleur et d'opacitiée entre la sélection de player et play
            if (isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)))
            {
                MenuSfx.Play();
                isSectionPlayer = false;
                colorSectionPlayer = Color.LightGray * 0.5f;
                colorSectionGame = Color.White;
                
            }
            else if (!isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)))
            {
                MenuSfx.Play();
                isSectionPlayer = true;
                colorSectionGame = Color.LightGray * 0.5f;
                colorSectionPlayer = Color.White;
            }

            
            if (isSectionPlayer)
            {
                // Les sélections 1 player ou 2 player
                if (selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D9)))
                {
                    MenuSfx.Play();
                    selectedPlayer1 = false;
                    selectedPlayerText = "2 players";
                }
                else if (!selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.D7)))
                {
                    MenuSfx.Play();
                    selectedPlayer1 = true;
                    selectedPlayerText = "1 player";
                }
            }
            else if (!isSectionPlayer)
            {
                // lencement du jeu
                if (isOnMenu && (Keyboard.GetState().IsKeyDown(Keys.D8)))
                {
                    StartSfx.Play();
                    isOnMenu = false;
                }
            }
        }

        protected void menuDraw(GameTime gameTime)
        {
            // background
            _spriteBatch.Draw(_menuImages[6], new Vector2(0, -100), null, Color.White * 0.7f, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);

            // Titre
            _spriteBatch.DrawString(font, "bugs destroyer", new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), Color.White, 0f, new Vector2(font.MeasureString("bugs destroyer").X / 2, font.MeasureString("bugs destroyer").Y / 2), 2f, SpriteEffects.None, 0f);

            // Choix du joueur
            if (!selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[2], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 175, 430), null, colorSectionPlayer, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
            }
            _spriteBatch.DrawString(font, selectedPlayerText, new Vector2(_graphics.PreferredBackBufferWidth / 2, 450), colorSectionPlayer, 0f, new Vector2(font.MeasureString(selectedPlayerText).X / 2, font.MeasureString(selectedPlayerText).Y / 2), 0.75f, SpriteEffects.None, 0f);
            if (selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[1], new Vector2(_graphics.PreferredBackBufferWidth / 2 + 130, 430), null, colorSectionPlayer, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
            }

            // Jeux
            _spriteBatch.DrawString(font, "Play", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50, 520), colorSectionGame, 0f, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);

            //Controle
            _spriteBatch.Draw(_menuImages[3], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 130, 720), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, "play-pause", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 43, 700), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(font, "quit", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 90, 700), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            if (selectedPlayer1)
            {
                _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 120, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 -145, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 48, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 10, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            }
            else
            {
                _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 325, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 225, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 190, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);

                _spriteBatch.Draw(_menuImages[5], new Vector2(_graphics.PreferredBackBufferWidth / 2 + 70, 810), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "mouvement", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 40, 905), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "shot", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 145, 810), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 180, 795), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);
            }
        }

        
}
}