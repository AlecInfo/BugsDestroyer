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
    public class Projectiles 
    {

        private Texture2D _texture;
        private Vector2 _position;
        private float _rotation;
        private Game1.direction _direction;

        private Random rnd = new Random();

        private const int PROJECILESPEED = 12;
        
        public Projectiles(Texture2D[] texture, Vector2 position, float rotation, Game1.direction currentDirection)
        {
            this._texture = texture[rnd.Next(0, 2)];
            this._position = position;
            this._rotation = rotation;
            this._direction = currentDirection;
        }

        public void projectileUpdate(GameTime gameTime)
        {
            if (_direction == Game1.direction.NW)
            {
                _position.X -= PROJECILESPEED / 1.4f;
                _position.Y -= PROJECILESPEED / 1.4f;
                _rotation = (float)Math.PI / 4; 
            }
            else if (_direction == Game1.direction.NE)
            {
                _position.X += PROJECILESPEED / 1.4f;
                _position.Y -= PROJECILESPEED / 1.4f;
            }
            else if (_direction == Game1.direction.SW)
            {
                _position.X -= PROJECILESPEED / 1.4f;
                _position.Y += PROJECILESPEED / 1.4f;
                _rotation = (float)Math.PI * 7 / 4;
            }
            else if (_direction == Game1.direction.SE)
            {
                _position.X += PROJECILESPEED / 1.4f;
                _position.Y += PROJECILESPEED / 1.4f;
            }
            else
            {
                if (_direction == Game1.direction.S)
                {
                    _position.Y += PROJECILESPEED;
                }
                if (_direction == Game1.direction.W)
                {
                    _position.X -= PROJECILESPEED;
                    _rotation = 0; 
                }
                if (_direction == Game1.direction.E)
                {
                    _position.X += PROJECILESPEED;
                }
                if (_direction == Game1.direction.N)
                {
                    _position.Y -= PROJECILESPEED;
                }
            }

            // Collision des murs
            if (_position.X < 250) // Left
            {
                _position.X = 250;
            }
            else if (_position.X > 1640) // Right
            {
                _position.X = 1640;
            }

            if (_position.Y < 155) // Top
            {
                _position.Y = 155;
            }
            else if (_position.Y > 915) // Bottom
            {
                _position.Y = 915;
            }
        }

        public void projectileDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y), null, Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 2f, SpriteEffects.None, 0f);
        }
    }
}
