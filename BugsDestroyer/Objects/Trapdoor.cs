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
        public  Vector2 _position;
        private List<Texture2D> _texture = new List<Texture2D>();
        public Texture2D currentFrame;
        private float _size;
        public bool trapdoorIsOpen = false;

        private SoundEffect _sfx;

        public Trapdoor(List<Texture2D> texture, Vector2 position, float size, SoundEffect sfx)
        {
            this._texture = texture;
            this._position = position;
            this._size = size;

            currentFrame = _texture[0];
            this._sfx = sfx;
        }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentFrame, _position, null, Color.White, 0, new Vector2(currentFrame.Width / 2, currentFrame.Height / 2), _size, SpriteEffects.None, 0f);
        }
    }
}
