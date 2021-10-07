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
        private Vector2 _position;
        private List<Texture2D> _texture = new List<Texture2D>();
        private float _size;

        public Trapdoor(List<Texture2D> texture, Vector2 position, float size)
        {
            this._texture = texture;
            this._position = position;
            this._size = size;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture[0], _position, null, Color.White, 0, new Vector2(_texture[0].Width / 2, _texture[0].Height / 2), _size, SpriteEffects.None, 0f);
        }
    }
}
