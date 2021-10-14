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
        private Vector2 _position;
        public Vector2 direction;
        public bool hasCalculatedDirection = false;
        private Texture2D[] _Frames;
        private Texture2D _CurrentFrame;
        private int _health = 2;
        private float rotation = 0;
        private Color _color = new Color(200, 200, 200);
        private List<Player> mobPlayers = new List<Player>();

        // attack logic
        private float _jumpDelay = 0.5f;
        private bool _isAttacking = false;
        private int _damage = 50;
        private float _speed = 12f;

        // Knockback
        private int _knockbackAmount = 5;
        private int _knockbackCpt = 0;
        private int _knockbackSpeed = 22;
        private int _knockbackJumpTime = 22;
        private bool _hasDealtDamage = false;



        // Ctor
        public Spider(Vector2 initialPos, Texture2D[] cockroachFrames)
        {
            this._position = initialPos;

            this._Frames = cockroachFrames;
            this._CurrentFrame = _Frames[0];
        }



        // Methods
        public override void Update(GameTime gameTime, List<Player> players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            mobPlayers.Clear();
            foreach (Player player in players)
            {
                if (player.healthPoint > 0)
                {
                    mobPlayers.Add(player);
                }
            }


            _jumpDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_isAttacking)
            {
                if (mobPlayers.Count > 0)
                {
                    if (!_hasDealtDamage)
                    {
                        FollowPlayer(mobPlayers);
                        playerCollision(players, enemies);
                    }
                }

                this._CurrentFrame = _Frames[1];
            }
            else
            {
                this.hasCalculatedDirection = false;
                this._CurrentFrame = _Frames[0];
            }

            if (_hasDealtDamage)
            {
                Knockback(gameTime);
            }


            if (_jumpDelay <= 0)
            {
                _isAttacking = !_isAttacking; // invserse attack
                _jumpDelay = 0.5f;
            }


            // collision
            if (_position.X < 250) // Left
            {
                _position.X = 251;

                _isAttacking = false;
                _jumpDelay = 0.5f;
            }
            else if (_position.X > 1660) // Right
            {
                _position.X = 1659;

                _isAttacking = false;
                _jumpDelay = 0.5f;
            }
            if (_position.Y < 140) // Top
            {
                _position.Y = 141;

                _isAttacking = false;
                _jumpDelay = 0.5f;
            }
            else if (_position.Y > 940) // Bottom
            {
                _position.Y = 939;

                _isAttacking = false;
                _jumpDelay = 0.5f;
            }

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
        }

        #region update methods
        public void Knockback(GameTime gameTime)
        {
            _isAttacking = false;
            _jumpDelay = 0.5f;

            _knockbackCpt += gameTime.ElapsedGameTime.Milliseconds;
            if (_knockbackCpt > _knockbackSpeed)
            {
                _knockbackCpt -= _knockbackSpeed;

                direction.Normalize();
                Vector2 velocity = direction * _knockbackAmount;
                _position -= velocity;
                _knockbackJumpTime -= 1;

                if (_knockbackJumpTime > 0)
                {
                    Knockback(gameTime);
                }
                else
                {
                    _hasDealtDamage = false;
                    _knockbackJumpTime = 22;
                }
            }
        }

        private void FollowPlayer(List<Player> players)
        {
            Player playerToFollow = players[0];
            float distancePlayer1 = (float)Math.Sqrt(Math.Pow((players[0].position.X - _position.X), 2) + Math.Pow((players[0].position.Y - _position.Y), 2)); // calculate distance to player 1

            if (!hasCalculatedDirection) {

                if (players.Count > 1) // si il y a deux joueur 
                {
                    float distancePlayer2 = (float)Math.Sqrt(Math.Pow((players[1].position.X - _position.X), 2) + Math.Pow((players[1].position.Y - _position.Y), 2)); // calculate distance to player 2

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


                direction = playerToFollow.position - _position;

                float rotationDegrees = 0;

                // calculate rotation angle to mqake enemy look at player (degrees)
                if ((direction.X > 0 && direction.Y < 0) || (direction.X > 0 && direction.Y > 0)) // NE or SE
                {
                    rotationDegrees = (float)Math.Atan(direction.Y / direction.X);
                }
                else if ((direction.X < 0 && direction.Y < 0) || (direction.X < 0 && direction.Y > 0)) // NW or SW
                {
                    rotationDegrees = (float)Math.Atan(direction.Y / direction.X) - 135;
                }

                rotation = (float)(rotationDegrees + 90 * (Math.PI / 180));

                this.hasCalculatedDirection = true;
            }

            // Move enemy towards player
            direction.Normalize();
            Vector2 velocity = direction * _speed;
            _position += velocity;
        }

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + _CurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, _position) < Math.Pow(radius, 2)) // if is colliding
                {

                    // change color to indicate damage (more red = more damage)
                    _health -= 1;
                    if (_health == 1)
                    {
                        _color = new Color(200, 100, 100);
                        _speed = 17f; 
                    }

                    // if theres no more health remove enemy
                    if (_health == 0)
                    {
                        enemies.Remove(this); // remove enemy
                        explosions.Add(new Explosion(_position, mobExplosion, size: 2));
                    }

                    projectiles.Remove(projectiles[i]); // remove projectile
                }
            }
        }

        public void playerCollision(List<Player> players, List<Enemy> enemies)
        {
            foreach (Player player in players)
            {
                float radius = player.walkingSprites[0].Width + _CurrentFrame.Width;

                if (Vector2.DistanceSquared(player.position, _position) < Math.Pow(radius, 2)) // if is colliding
                {
                    player.healthPoint -= _damage;
                    _hasDealtDamage = true;
                }
            }
        }
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._CurrentFrame, _position, null, _color, rotation, new Vector2(_CurrentFrame.Width / 2, _CurrentFrame.Height / 2), 2f, SpriteEffects.None, 0f);
        }
    }
}
