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
        public Texture2D texture;
        public Vector2 position;
        private float _rotation;
        private Game1.direction _direction;

        private Random rnd = new Random();

        private int _speed;

        public Projectiles(Texture2D[] texture, Vector2 position, float rotation, Game1.direction currentDirection, int speed = 14)
        {
            // Récupération des données
            this.texture = texture[rnd.Next(0, 2)];
            this.position = position;
            this._rotation = rotation;
            this._direction = currentDirection;
            this._speed = speed;
        }

        public void projectileUpdate(GameTime gameTime, List<Projectiles> listProjectiles, List<Explosion> listExplosions, Texture2D shotExplosion)
        {
            // acctualisation de l'image du projectile dans chaque direction
            if (_direction == Game1.direction.NW)
            {
                position.X -= _speed / 1.4f;
                position.Y -= _speed / 1.4f;
                _rotation = (float)Math.PI / 4;
            }
            else if (_direction == Game1.direction.NE)
            {
                position.X += _speed / 1.4f;
                position.Y -= _speed / 1.4f;
            }
            else if (_direction == Game1.direction.SW)
            {
                position.X -= _speed / 1.4f;
                position.Y += _speed / 1.4f;
                _rotation = (float)Math.PI * 7 / 4;
            }
            else if (_direction == Game1.direction.SE)
            {
                position.X += _speed / 1.4f;
                position.Y += _speed / 1.4f;
            }
            else
            {
                if (_direction == Game1.direction.S)
                {
                    position.Y += _speed;
                }
                if (_direction == Game1.direction.W)
                {
                    position.X -= _speed;
                    _rotation = 0;
                }
                if (_direction == Game1.direction.E)
                {
                    position.X += _speed;
                }
                if (_direction == Game1.direction.N)
                {
                    position.Y -= _speed;
                }
            }

            // Collision des murs avec suppretion 
            if (position.X < 250) // Left
            {
                position.X = 250;
                listProjectiles.Remove(this);
                listExplosions.Add(new Explosion(position, new List<Texture2D> { shotExplosion, shotExplosion }));
            }
            else if (position.X > 1670) // Right
            {
                position.X = 1670;
                listProjectiles.Remove(this);
                listExplosions.Add(new Explosion(position, new List<Texture2D> { shotExplosion, shotExplosion }, (float)Math.PI));
            }
            if (position.Y < 125) // Top
            {
                position.Y = 125;
                listProjectiles.Remove(this);
                listExplosions.Add(new Explosion(position, new List<Texture2D> { shotExplosion, shotExplosion }, (float)Math.PI / 2));
            }
            else if (position.Y > 945) // Bottom
            {
                position.Y = 945;
                listProjectiles.Remove(this);
                listExplosions.Add(new Explosion(position, new List<Texture2D> { shotExplosion, shotExplosion }, (float)Math.PI * 3 / 2));
            }
        }

        public void projectileDraw(SpriteBatch spriteBatch)
        {
            // affichage du projectile
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), null, Color.White, _rotation, new Vector2(texture.Width / 2, texture.Height / 2), 2f, SpriteEffects.None, 0f);
        }
    }
}
