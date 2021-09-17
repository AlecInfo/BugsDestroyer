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
        private List<Texture2D> _menuImages;

        private Color colorSectionPlayer = Color.White;
        private Color colorSectionGame = Color.LightGray * 0.7f;
        private bool isSectionPlayer = true;

        private string selectedPlayerText = "1 player";
        private bool selectedPlayer1 = true;

        private bool hasReleasedKey = true;

        protected void menuLoad()
        {
            _menuImages = new List<Texture2D>()
            {
                Content.Load<Texture2D>("Img/Menu/backgroundMenuTemp"),
                Content.Load<Texture2D>("Img/Menu/flecheDroite"),
                Content.Load<Texture2D>("Img/Menu/flecheGauche"),
            };

            font = Content.Load<SpriteFont>("Fonts/GameBoy30");

        }

        protected void menuUpdate(GameTime gameTime)
        {
            if (isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)))
            {
                isSectionPlayer = false;
                colorSectionPlayer = Color.LightGray * 0.7f;
                colorSectionGame = Color.White;
                
            }
            else if (!isSectionPlayer && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)))
            {
                isSectionPlayer = true;
                colorSectionGame = Color.LightGray * 0.7f;
                colorSectionPlayer = Color.White;
            }


            if (isSectionPlayer)
            {
                if (selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                {
                    selectedPlayer1 = false;
                    selectedPlayerText = "2 players";
                }
                else if (!selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)))
                {
                    selectedPlayer1 = true;
                    selectedPlayerText = "1 player";
                }

                //if (selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                //{
                //    selectedPlayer1 = false;
                //    selectedPlayerText = "2 players";
                //}
                //else if (!selectedPlayer1 && (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                //{
                //    selectedPlayer1 = true;
                //    selectedPlayerText = "1 player";
                //}
            }
            else if (!isSectionPlayer)
            {
                if (isOnMenu && (Keyboard.GetState().IsKeyDown(Keys.D8)))
                {
                    isOnMenu = false;
                }
            }
        }

        protected void menuDraw(GameTime gameTime)
        {
            // background
            _spriteBatch.Draw(_menuImages[0], new Vector2(0, -100), null, Color.White * 0.7f, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

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

        }

        
}
}