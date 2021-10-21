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

        private Random _rnd = new Random();

        private int _speed;

        private SoundEffect _wallHurt;

        /// <summary>
        /// Création d'un projectiles
        /// (Alec Piette)
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="currentDirection"></param>
        /// <param name="wallHurt"></param>
        /// <param name="speed"></param>
        public Projectiles(Texture2D[] texture, Vector2 position, float rotation, Game1.direction currentDirection, SoundEffect wallHurt, int speed = 14)
        {
            // Récupération des données
            this.texture = texture[_rnd.Next(0, 2)];
            this.position = position;
            this._rotation = rotation;
            this._direction = currentDirection;
            this._speed = speed;

            this._wallHurt = wallHurt;
        }

        /// <summary>
        /// Update du projectile
        /// (Alec Piette)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="listProjectiles"></param>
        /// <param name="listExplosions"></param>
        /// <param name="shotExplosion"></param>
        public void Update(GameTime gameTime, List<Projectiles> listProjectiles, List<Explosion> listExplosions, Texture2D shotExplosion)
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
                RemoveAndExplode(listProjectiles, listExplosions, shotExplosion, _wallHurt,0f);
            }
            else if (position.X > 1670) // Right
            {
                position.X = 1670;
                RemoveAndExplode(listProjectiles, listExplosions, shotExplosion, _wallHurt, (float)Math.PI);
            }
            if (position.Y < 125) // Top
            {
                position.Y = 125;

                RemoveAndExplode(listProjectiles, listExplosions, shotExplosion, _wallHurt, (float)Math.PI / 2);
            }
            else if (position.Y > 945) // Bottom
            {
                position.Y = 945;

                RemoveAndExplode(listProjectiles, listExplosions, shotExplosion, _wallHurt, (float)Math.PI * 3/2);
            }

        }

        /// <summary>
        /// Affichage du projectile
        /// (Alec Piette)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // affichage du projectile
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), null, Color.White, _rotation, new Vector2(texture.Width / 2, texture.Height / 2), 2f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Suppretion du projectile et ajout d'une explosion
        /// (Alec Piette)
        /// </summary>
        /// <param name="listProjectiles"></param>
        /// <param name="listExplosions"></param>
        /// <param name="shotExplosion"></param>
        /// <param name="sfx"></param>
        /// <param name="angle"></param>
        private void RemoveAndExplode(List<Projectiles> listProjectiles, List<Explosion> listExplosions, Texture2D shotExplosion, SoundEffect sfx, float angle)
        {
            listProjectiles.Remove(this);
            listExplosions.Add(new Explosion(position, new List<Texture2D> { shotExplosion, shotExplosion }, sfx, angle));
        }
    }
}
