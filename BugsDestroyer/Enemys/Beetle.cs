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
    class Beetle : Enemy
    {
        // attributs
        private Vector2 _position;
        public Vector2 direction;
        private Texture2D[] _Frames;
        private Texture2D _CurrentFrame;
        private int _speed = 2;
        private float rotation = 0;
        private int _damage = 25;
        private int _health = 3;
        private List<Player> mobPlayers = new List<Player>();
        public Color color = Color.White;

        // Anim
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;

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

        // ctor
        public Beetle(Vector2 initialPos, Texture2D[] cockroachFrames, List<SoundEffect> listSfx)
        {
            this._Frames = cockroachFrames;
            _CurrentFrame = _Frames[0];

            _position = initialPos;
            this._listSfx = listSfx;
        }



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

            // Animation
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;


                if (_CurrentFrame == _Frames[0])
                {
                    _CurrentFrame = _Frames[1];
                }
                else
                    _CurrentFrame = _Frames[0];
            }

            if (mobPlayers.Count > 0)
            {
                if (!_hasDealtDamage)
                {
                    FollowPlayer(mobPlayers);
                    playerCollision(players, enemies);
                }
                else
                    Knockback(gameTime);
            }

            if (_position.X < 250) // Left
            {
                _position.X = 250;
            }
            else if (_position.X > 1660) // Right
            {
                _position.X = 1660;
            }
            if (_position.Y < 140) // Top
            {
                _position.Y = 140;
            }
            else if (_position.Y > 940) // Bottom
            {
                _position.Y = 940;
            }

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
        }

        private void FollowPlayer(List<Player> players)
        {
            Player playerToFollow = players[0];
            float distancePlayer1 = (float)Math.Sqrt(Math.Pow((players[0].position.X - _position.X), 2) + Math.Pow((players[0].position.Y - _position.Y), 2)); // calculate distance to player 1


            if (players.Count > 1) // si il y a deux joueur 
            {
                float distancePlayer2 = (float)Math.Sqrt(Math.Pow((players[1].position.X - _position.X), 2) + Math.Pow((players[1].position.Y - _position.Y), 2)); // calculate distance to player 2

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

            // Convert degrees to radians 
            rotation = (float)(rotationDegrees + 90 * (Math.PI / 180));


            // Move enemy towards player
            direction.Normalize();
            Vector2 velocity = direction * _speed;
            _position += velocity;
        }

        public void Knockback(GameTime gameTime)
        {
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

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + _CurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, _position) < Math.Pow(radius, 2)) // if is colliding
                {
                    _health -= 1;
                    switch (_health)
                    {
                        case 2:
                            this.color = new Color(255, 170, 170);
                            break;

                        case 1:
                            this.color = new Color(255, 85, 85);
                            break;
                    }

                    if(_health == 0)
                    {
                        enemies.Remove(this); // remove enemy
                        explosions.Add(new Explosion(_position, mobExplosion, _listSfx[NUMENEMYSHURTSFX], size: 2));
                    }
                    else
                    {
                        _listSfx[NUMWALLHURTSFX].Play();
                    }

                    projectiles.Remove(projectiles[i]); // remove projectile
                }
            }
        }

        public void playerCollision(List<Player> players, List<Enemy> enemies)
        {
            foreach (Player player in mobPlayers)
            {
                float radius = player.walkingSprites[0].Width + _CurrentFrame.Width;

                if (Vector2.DistanceSquared(player.position, _position) < Math.Pow(radius, 2)) // if is colliding
                {
                    _listSfx[NUMPLAYERHURTSFX].Play();

                    player.healthPoint -= _damage;
                    _hasDealtDamage = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._CurrentFrame, _position, null, this.color, rotation, new Vector2(_CurrentFrame.Width / 2, _CurrentFrame.Height / 2), 2f, SpriteEffects.None, 0f);
        }
    }
}
