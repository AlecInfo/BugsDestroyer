using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BugsDestroyer
{
    public class Trapdoor
    { 
        // varriables
        public  Vector2 position;
        private List<Texture2D> _texture = new List<Texture2D>();
        public Texture2D currentFrame;
        private float _size;
        public bool trapdoorIsOpen = false;

        private SoundEffect _sfx;

        /// <summary>
        /// Création de la Trapdoor
        /// (Alec Piette)
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="sfx"></param>
        public Trapdoor(List<Texture2D> texture, Vector2 position, float size, SoundEffect sfx)
        {
            this._texture = texture;
            this.position = position;
            this._size = size;

            currentFrame = _texture[0];
            this._sfx = sfx;
        }


        /// <summary>
        /// Update de la Trapdoor
        /// (Alec Piette)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="players"></param>
        /// <param name="enemy"></param>
        public void Update(GameTime gameTime, List<Player> players, List<Enemy> enemy)
        {
            if (enemy.Count <= 0)
            {
                if (currentFrame != _texture[1]) // if trapdoor hasn't opened yet
                {
                    _sfx.Play();
                }


                currentFrame = _texture[1];
                trapdoorIsOpen = true;
            }
        }

        /// <summary>
        /// Affichage de la Trapdoor
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentFrame, position, null, Color.White, 0, new Vector2(currentFrame.Width / 2, currentFrame.Height / 2), _size, SpriteEffects.None, 0f);
        }
    }
}
