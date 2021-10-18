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
    class Cockroach : Enemy
    {
        // attributs
        private Texture2D[] _Frames;
        private int _damage = 10;
        private List<Player> mobPlayers = new List<Player>();

        // Anim
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;

        // Knockback
        private int _knockbackAmount = 5;
        private int _knockbackCpt = 0;
        private int _knockbackSpeed = 22;
        private int _knockbackJumpTime = 22;
        private bool _hasDealtDamage = false;


        // ctor
        public Cockroach(Vector2 initialPos, Texture2D[] cockroachFrames)
        {
            this._Frames = cockroachFrames;
            this.CurrentFrame = _Frames[0];

            this.position = initialPos;
            this.speed = 3;
        }

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

            // Animation
            this.timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (this.timeSinceLastFrame > this.millisecondsPerFrame)
            {
                this.timeSinceLastFrame -= this.millisecondsPerFrame;


                if (this.CurrentFrame == this._Frames[0])
                {
                    this.CurrentFrame = this._Frames[1];
                }else
                    this.CurrentFrame = this._Frames[0];
            }

            if (this.mobPlayers.Count > 0)
            {
                if (!this._hasDealtDamage)
                {
                    FollowPlayer(this.mobPlayers, enemies, objects);
                    playerCollision(players, enemies);
                }
                else
                    Knockback(gameTime);
            }

            if (this.position.X < 250) // Left
            {
                this.position.X = 250;
            }
            else if (this.position.X > 1660) // Right
            {
                this.position.X = 1660;
            }
            if (this.position.Y < 140) // Top
            {
                this.position.Y = 140;
            }
            else if (this.position.Y > 940) // Bottom
            {
                this.position.Y = 940;
            }

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
        }

        private void FollowPlayer(List<Player> players, List<Enemy> enemies, List<Object> objects)
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
                }
                else
                {
                    playerToFollow = players[1];
                }
            }

            this.direction = playerToFollow.position - position;


            float rotationDegrees = 0;

            // calculate rotation angle to mqake enemy look at player (degrees)
            if ((this.direction.X > 0 && this.direction.Y < 0) || (this.direction.X > 0 && this.direction.Y > 0)) // NE or SE
            {
                rotationDegrees = (float)Math.Atan(this.direction.Y / this.direction.X);
            }
            else if ((this.direction.X < 0 && this.direction.Y < 0) || (this.direction.X < 0 && this.direction.Y > 0)) // NW or SW
            {
                rotationDegrees = (float)Math.Atan(this.direction.Y / this.direction.X) - 135;
            }

            // Convert degrees to radians 
            this.rotation = (float)(rotationDegrees+90 * (Math.PI / 180));


            // Move enemy towards player
            this.direction.Normalize();
            this.velocity = this.direction * this.speed;
            this.position += velocity;

            // Collisions
            enemyCollision(enemies);
            objectCollision(objects);
        }

        public void Knockback(GameTime gameTime)
        {
            this._knockbackCpt += gameTime.ElapsedGameTime.Milliseconds;
            if (this._knockbackCpt > this._knockbackSpeed)
            {
                this._knockbackCpt -= this._knockbackSpeed;

                this.direction.Normalize();
                this.velocity = this.direction * this._knockbackAmount;
                this.position -= velocity;
                this._knockbackJumpTime -= 1;

                if (this._knockbackJumpTime > 0)
                {
                    Knockback(gameTime);
                }else
                {
                    this._hasDealtDamage = false;
                    this._knockbackJumpTime = 22;
                }
            }
        }

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + this.CurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, this.position) < Math.Pow(radius, 2)) // if is colliding
                {
                    enemies.Remove(this); // remove enemy
                    projectiles.Remove(projectiles[i]); // remove projectile
                    explosions.Add(new Explosion(this.position, mobExplosion, size:2));
                }
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
                    this._hasDealtDamage = true;
                }
            }
        }
    }
}