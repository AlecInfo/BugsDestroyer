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
        private Vector2 _position;
        public Vector2 direction;
        private Texture2D[] _cockroachFrames = new Texture2D[2];
        private Texture2D _cockroachCurrentFrame;
        private int _speed = 3;
        private float rotation = 0;
        private int _damage = 10;
        private int _knockbackAmount = 1;
        private int knockbackTime = 100;
        private bool _hasDealtDamage = false;

        //Anim
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;


        // ctor
        public Cockroach(Vector2 initialPos)
        {
            _position = initialPos;
        }

        // methods
        public override void Load(ContentManager Content)
        {
            _cockroachFrames[0] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard0");
            _cockroachFrames[1] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard1");

            _cockroachCurrentFrame = _cockroachFrames[0];
        }

        public override void Update(GameTime gameTime, Player[] players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            // Animation
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;


                if (_cockroachCurrentFrame == _cockroachFrames[0])
                {
                    _cockroachCurrentFrame = _cockroachFrames[1];
                }else
                    _cockroachCurrentFrame = _cockroachFrames[0];
            }

            if (!_hasDealtDamage)
            {
                FollowPlayer(players);
            }
            else
                Knockback();

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
            playerCollision(players, enemies);
        }


        private void FollowPlayer(Player[] players)
        {
            Player playerToFollow;
            float distancePlayer1 = (float)Math.Sqrt(Math.Pow((players[0].position.X - _position.X), 2) + Math.Pow((players[0].position.Y - _position.Y), 2)); // calculate distance to player 1
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
            rotation = (float)(rotationDegrees+90 * (Math.PI / 180));
            
            
            // Move enemy towards player
            direction.Normalize();
            Vector2 velocity = direction * _speed;
            _position += velocity;
        }

        public void Knockback()
        {
            direction.Normalize();
            Vector2 velocity = direction * _knockbackAmount;
            _position -= velocity;
            knockbackTime -= 1;
            if (knockbackTime > 0)
            {
                Knockback();
            }else
            {
                _hasDealtDamage = false;
                knockbackTime = 100;
            }
        }

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + _cockroachCurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, _position) < Math.Pow(radius, 2)) // if is colliding
                {
                    enemies.Remove(this); // remove enemy
                    projectiles.Remove(projectiles[i]); // remove projectile
                    explosions.Add(new Explosion(_position, mobExplosion, size:2));
                }
            }
        }

        public void playerCollision(Player[] players, List<Enemy> enemies)
        {
            foreach (Player player in players)
            {
                float radius = player.currentSprite.Width + _cockroachCurrentFrame.Height;

                if (Vector2.DistanceSquared(player.position, _position) < Math.Pow(radius, 2)) // if is colliding
                {
                    player.healthPoint -= _damage;
                    _hasDealtDamage = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._cockroachCurrentFrame, _position, null, Color.White, rotation, new Vector2(_cockroachCurrentFrame.Width / 2, _cockroachCurrentFrame.Height / 2), 2f , SpriteEffects.None, 0f);
        }
    }
}