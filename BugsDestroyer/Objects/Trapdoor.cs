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
        private Texture2D _texture;
        private float _size;

        public Trapdoor(Vector2 position, float size)
        {
            this._position = position;
            this._size = size;
        }

        public void Load(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>("Img/Decor/trapdoor0");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(_texture.Width / 2, _texture.Height / 2), _size, SpriteEffects.None, 0f);
        }
    }
}
