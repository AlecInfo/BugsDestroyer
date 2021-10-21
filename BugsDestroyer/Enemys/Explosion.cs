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
    public class Explosion
    {
        // Attributs
        private List<Texture2D> _animationSprites;
        private int _currentSpriteNb;
        private Vector2 _position;
        private float _rotation;
        private float _size;
        private SoundEffect _sfx;
        // sfx var

        // Ctor
        public Explosion(Vector2 position, List<Texture2D> animationSprites, SoundEffect sfx, float rotation = 0, float size = 1.5f)
        {
            _position = position;
            _rotation = rotation;
            _size = size;
            this._sfx = sfx;

            _animationSprites = animationSprites;
            _currentSpriteNb = 0;
        }


        // Methods
        public void Update(GameTime gametime, List<Explosion> listExplosions)
        {
            _currentSpriteNb += 1;

            if (_currentSpriteNb == 1)
            {
                _sfx.Play(0.5f, 0f, 0f);
            }

            if (_currentSpriteNb == _animationSprites.Count)
            {
                listExplosions.Remove(this);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_animationSprites[_currentSpriteNb], _position, null, Color.White, _rotation, new Vector2(_animationSprites[_currentSpriteNb].Width / 2, _animationSprites[_currentSpriteNb].Height / 2), _size, SpriteEffects.None, 0f);
        }
    }
}
