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
    class Butterfly : Enemy
    {
        private Texture2D[] _Frames = new Texture2D[2];
        private Texture2D _butterflyProjectile;
        private Vector2 _centralPos;
        private List<Player> mobPlayers = new List<Player>();

        // Cocoon Anim
        private float _cocoonTimer = 5;
        public bool isCocoon = true;
        private int _currentCocoonAnimStep;
        private float _cocoonAnimTimer;
        private int _damage = 100;


        // Butterfly Anim
        private float _butterflyFlapTimer = 0.75f;
        private bool _butterflyHasFlapped = false;
        private int _bigFlapCounter = 0;

        private float _smallCircleAnimTimer;
        private int _smallCircleSpeed = 5;
        private int _smallCircleSize = 100;

        private float _bigCircleAnimTimer = -8f; //  -8f pour que le papillon spawn sur le cocoon
        private float _bigCircleSpeed = 0.15f;
        private int _bigCircleSizeX = 500;
        private int _bigCircleSizeY = 250;

        // Health
        public const int HEALTH_POINT_MAX = 15;
        public const int HEALTH_POINT_MIN = 0;
        private int _healthPoint = HEALTH_POINT_MAX;
        private bool _isHit;
        private int _effectTime = 200;

        public int healthPoint
        {
            get => _healthPoint;
            set
            {
                if (value < _healthPoint)
                {
                    _isHit = true;
                }

                _healthPoint = Math.Max(HEALTH_POINT_MIN, Math.Min(value, HEALTH_POINT_MAX));
            }
        }

        // Sfx
        private List<SoundEffect> _listSfx = new List<SoundEffect>();
        private const int NUMWALLHURTSFX = 0;
        private const int NUMENEMYSHURTSFX = 1;
        private const int NUMPLAYERHURTSFX = 2;


        // Ctor
        public Butterfly(Vector2 initialPos, Texture2D[] butterflyFrames, Texture2D butterflyProjectile, List<SoundEffect> listSfx)
        {
            this._Frames = butterflyFrames;
            this._butterflyProjectile = butterflyProjectile;
            this.CurrentFrame = _Frames[0];
            this._listSfx = listSfx;

            this.position = initialPos;
            this._centralPos = initialPos;
            this.size = 3.2f;

            this._currentCocoonAnimStep = 1;
        }

        // Methods
        public override void Update(GameTime gameTime, List<Player> players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion, List<Object> objects)
        {
            if (this.isCocoon) // cocoon state
            {
                this._cocoonTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                this._cocoonAnimTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this._cocoonAnimTimer >= 1) // every  other second
                {
                    cocconVibrate(); // vibrate the cocoon
                }

                if (this._cocoonTimer <= 0)
                {
                    this.isCocoon = false;
                }
            }
            else // butterfly state
            {
                this.mobPlayers.Clear();
                foreach (Player player in players)
                {
                    if (player.healthPoint > 0)
                    {
                        this.mobPlayers.Add(player);
                    }
                }

                // make harder when two players, and easier when one
                if (mobPlayers.Count == 2)
                {
                    this._damage = 15;
                }
                else
                    this._damage = 10;


                    this._butterflyFlapTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this._butterflyHasFlapped)
                {
                    if (this._butterflyFlapTimer >= 0.25)
                    {
                        this._butterflyFlapTimer = 0;
                        this._butterflyHasFlapped = false;

                        if (_bigFlapCounter != 2) // small flap
                        {
                            this.CurrentFrame = this._Frames[2];

                            this._bigFlapCounter += 1;
                        }
                        else // big flap
                        {
                            this.CurrentFrame = this._Frames[3];
                            enemies.Add(new butterflyProjectile(this.position, _butterflyProjectile));

                            this._bigFlapCounter = 0;
                        }
                    }
                }
                else
                {
                    if (this._butterflyFlapTimer >= 0.15)
                    {
                        this._butterflyFlapTimer = 0;
                        this._butterflyHasFlapped = true;

                        this.CurrentFrame = this._Frames[1];
                    }
                }

                // Hit effect
                if (this._isHit) // if the enemy is hit
                {
                    this.color = new Color(255, 100, 100); // red
                    this._effectTime -= gameTime.ElapsedGameTime.Milliseconds;

                    if (this._effectTime <= 0)
                    {
                        this._effectTime = 100;
                        this._isHit = false;
                    }
                }
                else
                    this.color = Color.White;

                butterflyAnim(gameTime);
                playerCollision(this.mobPlayers);
            }

            projectileCollision(projectiles, enemies, explosions, mobExplosion);
        }

        private void cocconVibrate()
        {
            switch (this._currentCocoonAnimStep)
            {
                case 1:
                    this.position.X -= 4;
                    this.rotation = 0.2f;
                    break;

                case 2:
                    this.position.Y += 4;
                    this.rotation = 0;
                    break;

                case 3:
                    this.position.X += 4;
                    this.rotation = -0.2f;
                    break;

                case 4:
                    this.position.Y -= 4;
                    this.rotation = 0;
                    break;
            }


            this._currentCocoonAnimStep += 1;
            if (this._currentCocoonAnimStep == 5) // end anim
            {
                this._currentCocoonAnimStep = 1;
                this._cocoonAnimTimer = 0;
            }
        }

        private void butterflyAnim(GameTime gameTime)
        {
            // small fast circle
            this._smallCircleAnimTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * this._smallCircleSpeed;

            this.position.X = this._centralPos.X + (float)Math.Cos(_smallCircleAnimTimer) * this._smallCircleSize;
            this.position.Y = this._centralPos.Y + (float)Math.Sin(_smallCircleAnimTimer) * this._smallCircleSize;

            // big slow circle
            this._bigCircleAnimTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * this._bigCircleSpeed;

            this._centralPos.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 + (float)Math.Cos(_bigCircleAnimTimer) * this._bigCircleSizeX;
            this._centralPos.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2 + (float)Math.Sin(_bigCircleAnimTimer) * this._bigCircleSizeY;
        }

        public void projectileCollision(List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                float radius = projectiles[i].texture.Width + this.CurrentFrame.Height;

                if (Vector2.DistanceSquared(projectiles[i].position, this.position) < Math.Pow(radius, 2)) // if is colliding
                {
                    if (!this.isCocoon)
                    {
                        this._healthPoint -= 1;
                        this._isHit = true;

                        // if theres no more health remove enemy
                        if (this._healthPoint == 0)
                        {
                            enemies.Remove(this); // remove enemy
                            explosions.Add(new Explosion(this.position, mobExplosion, _listSfx[NUMENEMYSHURTSFX], size: 3.2f));
                        } else
                            _listSfx[NUMWALLHURTSFX].Play();
                    }


                    projectiles.Remove(projectiles[i]); // remove projectile
                }
            }
        }
        public void playerCollision(List<Player> players)
        {
            foreach (Player player in this.mobPlayers)
            {
                float radius = player.walkingSprites[0].Width + this.CurrentFrame.Width;

                if (Vector2.DistanceSquared(player.position, this.position) < Math.Pow(radius, 2)) // if is colliding
                {
                    player.healthPoint -= this._damage; // damage the player
                    _listSfx[NUMPLAYERHURTSFX].Play();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            // Draw butterfly
            spriteBatch.Draw(this.CurrentFrame, this.position, null, this.color, this.rotation, new Vector2(this.CurrentFrame.Width / 2, this.CurrentFrame.Height / 2), this.size, SpriteEffects.None, 0f);


            if (!this.isCocoon) // if isn't on cocoon state
            {
                Texture2D healthBarTexture = new Texture2D(graphics, 1, 1);

                // barre de vie
                Rectangle healthBarRectangle = new Rectangle(
                    Convert.ToInt32(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 - this.healthPoint * 15),
                    Convert.ToInt32(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 50),
                    this.healthPoint * 30,
                    50
                );


                // Draw health bar
                healthBarTexture.SetData(new Color[] { Color.FromNonPremultiplied(255, 25, 0, 255) });
                spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);
            }
        }
    }
}
