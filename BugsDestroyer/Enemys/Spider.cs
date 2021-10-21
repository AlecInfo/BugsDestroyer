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
    class Spider : Enemy
    {
        // Attributs
        public bool hasCalculatedDirection = false;
        private Texture2D[] _Frames;
        private int _health = 2;
        private List<Player> mobPlayers = new List<Player>();

        // attack logic
        private float _jumpDelay = 0.5f;
        private bool _isAttacking = false;
        private int _damage = 50;

        // Knockback
        private int _knockbackAmount = 5;
        private int _knockbackCpt = 0;
        private int _knockbackSpeed = 22;
        private int _knockbackJumpTime = 22;
        private bool _hasDealtDamage = false;

        // Sfx
        private List<SoundEffect> _listSfx = new List<SoundEffect>();
        private const int NUMWALLHURTSFX = 0;
        private const int NUMENEMYSHURTSFX = 1;
        private const int NUMPLAYERHURTSFX = 2;

        // Ctor
        public Spider(Vector2 initialPos, Texture2D[] cockroachFrames, List<SoundEffect> listSfx)
        {
            this.position = initialPos;
            this.color = new Color(200, 200, 200);

            this._Frames = cockroachFrames;

            this._listSfx = listSfx;

            this.CurrentFrame = _Frames[0];
            this.speed = 12;
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


            this._jumpDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this._isAttacking)
            {
                if (this.mobPlayers.Count > 0)
                {
                    if (!this._hasDealtDamage)
                    {
                        FollowPlayer(this.mobPlayers, objects);
                        playerCollision(mobPlayers, enemies);
                    }
                }

                this.CurrentFrame = this._Frames[1];
            }
            else
            {
                this.hasCalculatedDirection = false;
                this.CurrentFrame = this._Frames[0];
            }

            if (this._hasDealtDamage)
            {
                Knockback(gameTime);
            }


            if (this._jumpDelay <= 0)
            {
                this._isAttacking = !this._isAttacking; // invserse attack
                this._jumpDelay = 0.5f;
            }


            // collision
            if (this.position.X < 250) // Left
            {
                this.position.X = 251;

                this._isAttacking = false;
                this._jumpDelay = 0.5f;
            }
            else if (this.position.X > 1660) // Right
            {
                this.position.X = 1659;

                this._isAttacking = false;
                this._jumpDelay = 0.5f;
            }
            if (this.position.Y < 140) // Top
            {
                this.position.Y = 141;

                this._isAttacking = false;
                this._jumpDelay = 0.5f;
            }
            else if (this.position.Y > 940) // Bottom
            {
                this.position.Y = 939;

                this._isAttacking = false;
                this._jumpDelay = 0.5f;
            }

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
            objectCollision(objects);
        }

        #region update methods
        public void Knockback(GameTime gameTime)
        {
            this._isAttacking = false;
            this._jumpDelay = 0.5f;

            this._knockbackCpt += gameTime.ElapsedGameTime.Milliseconds;
            if (this._knockbackCpt > this._knockbackSpeed)
            {
                this._knockbackCpt -= this._knockbackSpeed;

                this.direction.Normalize();
                velocity = this.direction * this._knockbackAmount;
                this.position -= velocity;
                this._knockbackJumpTime -= 1;

                if (this._knockbackJumpTime > 0)
                {
                    Knockback(gameTime);
                }
                else
                {
                    this._hasDealtDamage = false;
                    this._knockbackJumpTime = 22;
                }
            }
        }

        private void FollowPlayer(List<Player> players, List<Object> objects)
        {
            Player playerToFollow = players[0];
            float distancePlayer1 = (float)Math.Sqrt(Math.Pow((players[0].position.X - this.position.X), 2) + Math.Pow((players[0].position.Y - this.position.Y), 2)); // calculate distance to player 1

            if (!this.hasCalculatedDirection) {

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
                this.rotation = (float)(rotationDegrees + 90 * (Math.PI / 180));

                if (playerToFollow.position.X == this.position.X) // if the enemy is aligned with the player on x or y (parallel bug)
                {
                    if (playerToFollow.position.Y < this.position.Y)
                    {
                        this.rotation = 0;
                    }
                    else
                        this.rotation = (float)Math.PI;

                }
                else if (playerToFollow.position.Y == this.position.Y)
                {
                    if (playerToFollow.position.X < this.position.X)
                    {
                        this.rotation = (float)Math.PI * 3 / 2;
                    }
                    else
                        this.rotation = (float)Math.PI / 2;
                }

                this.hasCalculatedDirection = true;
            }

            // Move enemy towards player
            this.direction.Normalize();
            velocity = this.direction * this.speed;
            this.position += velocity;
        }

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + this.CurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, position) < Math.Pow(radius, 2)) // if is colliding
                {

                    // change color to indicate damage (more red = more damage)
                    this._health -= 1;
                    if (this._health == 1)
                    {
                        this.color = new Color(200, 100, 100);
                        this.speed = 17f; 
                    }

                    // if theres no more health remove enemy
                    if (this._health == 0)
                    {
                        enemies.Remove(this); // remove enemy
                        explosions.Add(new Explosion(this.position, mobExplosion, _listSfx[NUMENEMYSHURTSFX], size: 2));
                    }
                    else
                    {
                        _listSfx[NUMWALLHURTSFX].Play();
                        explosions.Add(new Explosion(this.position, mobExplosion, _listSfx[NUMWALLHURTSFX], size: 2));
                    }

                    projectiles.Remove(projectiles[i]); // remove projectile
                }
            }
        }

        public void playerCollision(List<Player> players, List<Enemy> enemies)
        {
            foreach (Player player in players)
            {
                float radius = player.walkingSprites[0].Width + this.CurrentFrame.Width;

                if (Vector2.DistanceSquared(player.position, this.position) < Math.Pow(radius, 2)) // if is colliding
                {
                    player.healthPoint -= this._damage;
                    this._hasDealtDamage = true;
                }
            }
        }
        #endregion
    }
}
