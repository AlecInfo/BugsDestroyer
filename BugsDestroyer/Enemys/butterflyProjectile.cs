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
    class butterflyProjectile : Enemy
    {
        private bool _isFirstTime = true;
        private List<Player> mobPlayers = new List<Player>();
        private int _damage = 10;


        // Ctor
        public butterflyProjectile(Vector2 initialPos, Texture2D projectile)
        {
            this.CurrentFrame = projectile;

            this.position = initialPos;
            this.speed = 2;
            this.speed = 15;
        }

        // Methods
        public override void Update(GameTime gameTime, List<Player> players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion, List<Object> objects)
        {
            this.mobPlayers.Clear();
            foreach (Player player in players)
            {
                if (player.healthPoint > 0)
                {
                    this.mobPlayers.Add(player);
                }
            }

            if (_isFirstTime)
            {
                Player playerToFollow = players[0];
                float distancePlayer1 = (float)Math.Sqrt(Math.Pow((players[0].position.X - this.position.X), 2) + Math.Pow((players[0].position.Y - this.position.Y), 2)); // calculate distance to player 1

                if (players.Count > 1) // si il y a deux joueur 
                {
                    float distancePlayer2 = (float)Math.Sqrt(Math.Pow((players[1].position.X - this.position.X), 2) + Math.Pow((players[1].position.Y - this.position.Y), 2)); // calculate distance to player 2

                    // decide to follow the closest player
                    if (distancePlayer1 < distancePlayer2)
                    {
                        playerToFollow = players[0];
                        float distancePlayerToFollow = distancePlayer1;
                    }
                    else
                    {
                        playerToFollow = players[1];
                        float distancePlayerToFollow = distancePlayer2;
                    }
                }


                this.direction = playerToFollow.position - this.position;

                float rotationDegrees = 0;

                // calculate rotation angle to make enemy look at player (degrees)
                if ((this.direction.X > 0 && this.direction.Y < 0) || (this.direction.X > 0 && this.direction.Y > 0)) // NE or SE
                {
                    rotationDegrees = (float)Math.Atan(this.direction.Y / this.direction.X);
                }
                else if ((this.direction.X < 0 && this.direction.Y < 0) || (this.direction.X < 0 && this.direction.Y > 0)) // NW or SW
                {
                    rotationDegrees = (float)Math.Atan(this.direction.Y / this.direction.X) - 135;
                }

                this.rotation = (float)(rotationDegrees + 90 * (Math.PI / 180));


                _isFirstTime = false;
            }

            // Move enemy towards player
            this.direction.Normalize();
            velocity = this.direction * this.speed;
            this.position += velocity;

            playerCollision(players, enemies);

            if (this.position.X < 250) // Left
            {
                enemies.Remove(this); // remove enemy
            }
            else if (this.position.X > 1660) // Right
            {
                enemies.Remove(this); // remove enemy
            }
            if (this.position.Y < 140) // Top
            {
                enemies.Remove(this); // remove enemy
            }
            else if (this.position.Y > 940) // Bottom
            {
                enemies.Remove(this); // remove enemy
            }
        }

        public void playerCollision(List<Player> players, List<Enemy> enemies)
        {
            foreach (Player player in this.mobPlayers)
            {
                float radius = player.walkingSprites[0].Width + this.CurrentFrame.Width;

                if (Vector2.DistanceSquared(player.position, this.position) < Math.Pow(radius, 2)) // if is colliding
                {
                    player.healthPoint -= this._damage;
                    enemies.Remove(this); // remove enemy
                }
            }
        }
    }
}
