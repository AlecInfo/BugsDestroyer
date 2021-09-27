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
        // Varriables
        private Texture2D _texture;
        private Vector2 _position;
        private float _rotation;
        private Game1.direction _direction;

        private Random rnd = new Random();

        private int _speed;
        
        public Projectiles(Texture2D[] texture, Vector2 position, float rotation, Game1.direction currentDirection, int speed = 14)
        {
            // Récupération des données
            this._texture = texture[rnd.Next(0, 2)];
            this._position = position;
            this._rotation = rotation;
            this._direction = currentDirection;
            this._speed = speed;
        }

        public void projectileUpdate(GameTime gameTime, List<Projectiles> listProjectiles)
        {
            // acctualisation de l'image du projectile dans chaque direction
            if (_direction == Game1.direction.NW)
            {
                _position.X -= _speed / 1.4f;
                _position.Y -= _speed / 1.4f;
                _rotation = (float)Math.PI / 4; 
            }
            else if (_direction == Game1.direction.NE)
            {
                _position.X += _speed / 1.4f;
                _position.Y -= _speed / 1.4f;
            }
            else if (_direction == Game1.direction.SW)
            {
                _position.X -= _speed / 1.4f;
                _position.Y += _speed / 1.4f;
                _rotation = (float)Math.PI * 7 / 4;
            }
            else if (_direction == Game1.direction.SE)
            {
                _position.X += _speed / 1.4f;
                _position.Y += _speed / 1.4f;
            }
            else
            {
                if (_direction == Game1.direction.S)
                {
                    _position.Y += _speed;
                }
                if (_direction == Game1.direction.W)
                {
                    _position.X -= _speed;
                    _rotation = 0; 
                }
                if (_direction == Game1.direction.E)
                {
                    _position.X += _speed;
                }
                if (_direction == Game1.direction.N)
                {
                    _position.Y -= _speed;
                }
            }

            // Collision des murs avec suppretion 
            if (_position.X < 250) // Left
            {
                listProjectiles.Remove(this);
            }
            else if (_position.X > 1660) // Right
            {
                listProjectiles.Remove(this);
            }
            if (_position.Y < 140) // Top
            {
                listProjectiles.Remove(this);
            }
            else if (_position.Y > 940) // Bottom
            {
                listProjectiles.Remove(this);
            }
        }

        public void projectileDraw(SpriteBatch spriteBatch)
        {
            // affichage du projectile
            spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y), null, Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 2f, SpriteEffects.None, 0f);
        }
    }
}
