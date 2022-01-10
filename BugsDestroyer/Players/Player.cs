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
     public class Player
     {
        // controls
        private Keys[] _directionalKeys;
        private Keys _shootKey;
        public Keys interactKey;

        // attributs
        private GameTime _gameTime;
        private GraphicsDeviceManager _graphics;
        private int[] walkAnimSteps = { 1, 2, 3, 2, 1, 4, 5, 6, 5, 4 };
        public Texture2D[] walkingSprites = new Texture2D[7];
        public int[] shotAnimSteps = { 0, 0, 1, 0};
        private Texture2D[] _shotSprites = new Texture2D[3];
        public bool isShooting = false;
        public bool hasReleasedShootingkey = true;
        public Texture2D currentSprite;
        public int currentStep = 0;
        private Texture2D _deadSprite;
        public Vector2 position;
        public int walkingSpeed = 8;
        public float rotation = 0f;
        private bool _isHit = false;
        private bool _isCured = false;
        private int _effectTime = 200;
        public Color color = Color.White;
        public bool isOnTrapdoor = false;

        // Point de vie
        public const int HEALTH_POINT_MAX = 100;
        public const int HEALTH_POINT_MIN = 0; 
        private int _healthPoint = HEALTH_POINT_MAX;
        public int healthPoint { get => _healthPoint;
            set {
                if (value < _healthPoint)
                {
                    _isHit = true;
                }

                if (value > _healthPoint)
                {
                    _isCured = true;
                }

                _healthPoint = Math.Max(HEALTH_POINT_MIN, Math.Min(value, HEALTH_POINT_MAX));
            } 
        }

        // Health bar
        Rectangle healthBarRectangle;

        //Anim
        public int currentFrameNb;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;

        // Sfx
        private List<SoundEffect> _listSfx;
        private const int NUMWALLHURTSFX = 0;

        // Projectiles
        private Texture2D[] _projectileSprite = new Texture2D[2];
        private Game1.direction currentDirection = Game1.direction.E;


        // ctor
        public Player(GameTime gametime, GraphicsDeviceManager graphics, Texture2D[] walkingSprites, Texture2D[] shotSprites, Texture2D deadSprite, List<SoundEffect> listSfx, Keys[] directionalKeys, Keys shootkey, Keys interactKey, Texture2D[] projectileSprite, Vector2 position)
        {
            // Récuperation des données
            this._gameTime = gametime;
            this._graphics = graphics;
            this.walkingSprites = walkingSprites;
            this._shotSprites = shotSprites;
            this.currentSprite = this.walkingSprites[0];

            this._deadSprite = deadSprite;

            this._listSfx = listSfx;

            this._directionalKeys = directionalKeys;
            this._shootKey = shootkey;
            this.interactKey = interactKey;

            this._projectileSprite = projectileSprite;

            this.position = position;
        }


        public void playerUpdate(GameTime gameTime, List<Projectiles> listProjectiles, List<Object> listObjects)
        {
            // si les points de vie du player <= 0
            if (this.healthPoint <= 0)
            {
                // changement de l'image en image mort
                this.currentSprite = this._deadSprite;
                
                return;
            }


            KeyboardState kbdState = Keyboard.GetState();

            Mouvement(kbdState, gameTime, listObjects);

            // animation du joueur
            this.timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (this.timeSinceLastFrame > this.millisecondsPerFrame)
            {
                this.timeSinceLastFrame -= this.millisecondsPerFrame;

                Shot(kbdState, listProjectiles);

                // if is walking
                if ((kbdState.IsKeyDown(this._directionalKeys[0]) || kbdState.IsKeyDown(this._directionalKeys[1]) || kbdState.IsKeyDown(this._directionalKeys[2]) || kbdState.IsKeyDown(this._directionalKeys[3])) && !this.isShooting)
                {
                    this.currentStep += 1; // increment current frame
                    if (this.currentStep == this.walkingSprites.Length)
                    {
                        this.currentStep = 0; // reset anim
                    }
                    this.currentSprite = this.walkingSprites[this.walkAnimSteps[this.currentStep]];
                }
                else if (this.isShooting)
                {
                    this.currentStep += 1; // increment current frame
                    if (this.currentStep == this.shotAnimSteps.Length)
                    {
                        this.walkingSpeed = 8;
                        this.currentStep = 0; // reset anim
                        this.isShooting = false;
                    }
                    this.currentSprite = this._shotSprites[this.shotAnimSteps[this.currentStep]];
                }
                else
                {
                    this.currentSprite = this.walkingSprites[0];
                }
            }

            // barre de vie
            this.healthBarRectangle = new Rectangle(
                        Convert.ToInt32(this.position.X - Player.HEALTH_POINT_MAX / 4),
                        Convert.ToInt32(this.position.Y - this.currentSprite.Height / 2 - 20),
                        this.healthPoint / 2,
                        7);



            if (this._isHit) // if the player is hit 
            {
                this.color = new Color(255, 100, 100); // red
                this._effectTime -= gameTime.ElapsedGameTime.Milliseconds;

                if (this._effectTime <= 0)
                {
                    _listSfx[2].Play(volume: 0.3f, 0f, 0f); // play hurt sfx with 30% volume

                    this._effectTime = 100;
                    this._isHit = false;
                }
            }
            else if (this._isCured) // if the player is cured 
            {
                this.color = new Color(55, 255, 55); // red
                this._effectTime -= gameTime.ElapsedGameTime.Milliseconds;

                if (this._effectTime <= 0)
                {
                    this._effectTime = 100;
                    this._isCured = false;
                }
            }
            else
                this.color = Color.White;

            
            if (this.healthPoint == 0) // if the player is dead
            {
                // remove the effects
                this.color = Color.White;
            }

            isCollidingWitObject(listObjects);
        }

        private void Mouvement(KeyboardState kbdState, GameTime gameTime, List<Object> listObjects)
        {
            // Modification de la _position du player dans toutes les directions
            if (kbdState.IsKeyDown(this._directionalKeys[0]) && kbdState.IsKeyDown(this._directionalKeys[3]))
            {
                this.position.X += this.walkingSpeed / 1.4f;
                this.position.Y -= this.walkingSpeed / 1.4f;
                this.rotation = (float)Math.PI * 7 / 4; //NE
                this.currentDirection = Game1.direction.NE;
            }
            else if (kbdState.IsKeyDown(this._directionalKeys[3]) && kbdState.IsKeyDown(this._directionalKeys[2]))
            {
                this.position.X += this.walkingSpeed / 1.4f;
                this.position.Y += this.walkingSpeed / 1.4f;
                this.rotation = (float)Math.PI / 4; // SE
                this.currentDirection = Game1.direction.SE;
            }
            else if (kbdState.IsKeyDown(this._directionalKeys[2]) && kbdState.IsKeyDown(this._directionalKeys[1]))
            {
                this.position.X -= this.walkingSpeed / 1.4f;
                this.position.Y += this.walkingSpeed / 1.4f;
                this.rotation = (float)Math.PI * 3 / 4; // SW
                this.currentDirection = Game1.direction.SW;
            }
            else if (kbdState.IsKeyDown(this._directionalKeys[1]) && kbdState.IsKeyDown(this._directionalKeys[0]))
            {
                this.position.X -= this.walkingSpeed / 1.4f;
                this.position.Y -= this.walkingSpeed / 1.4f;
                this.rotation = (float)Math.PI * 5 / 4; // NW
                this.currentDirection = Game1.direction.NW;
            }
            else
            {
                if (kbdState.IsKeyDown(this._directionalKeys[3]))
                {
                    this.position.X += this.walkingSpeed;
                    this.rotation = 0; // E
                    this.currentDirection = Game1.direction.E;
                }
                if (kbdState.IsKeyDown(this._directionalKeys[2]))
                {
                    this.position.Y += this.walkingSpeed;
                    this.rotation = (float)Math.PI / 2; // S
                    this.currentDirection = Game1.direction.S;
                }
                if (kbdState.IsKeyDown(this._directionalKeys[1]))
                {
                    this.position.X -= this.walkingSpeed;
                    this.rotation = (float)Math.PI; // W
                    this.currentDirection = Game1.direction.W;
                }
                if (kbdState.IsKeyDown(this._directionalKeys[0]))
                {
                    this.position.Y -= this.walkingSpeed;
                    this.rotation = (float)Math.PI * 1.5f; // N
                    this.currentDirection = Game1.direction.N;
                }
            }


            // Collision des murs
            if (this.position.X < _graphics.PreferredBackBufferWidth / 5.7f) // Left
            {
                this.position.X = _graphics.PreferredBackBufferWidth / 5.7f;
            }
            else if (this.position.X > _graphics.PreferredBackBufferWidth / 1.17f) // Right
            {
                this.position.X = _graphics.PreferredBackBufferWidth / 1.17f;
            }

            if (this.position.Y < _graphics.PreferredBackBufferHeight / 5.6f) // Top
            {
                this.position.Y = _graphics.PreferredBackBufferHeight / 5.6f;
            }
            else if (this.position.Y > _graphics.PreferredBackBufferHeight / 1.18f) // Bottom
            {
                this.position.Y = _graphics.PreferredBackBufferHeight / 1.18f;
            }
        }

        private void Shot(KeyboardState kbdState, List<Projectiles> listProjectiles)
        {
            // if has clicked shoot key (F)
            if (kbdState.IsKeyDown(this._shootKey) && this.hasReleasedShootingkey && !this.isShooting)
            {
                this.walkingSpeed = 6;
                this.currentStep = 0; // reset anim
                this.hasReleasedShootingkey = false;
                this.isShooting = true;
                _listSfx[3].Play(volume: 0.3f, 0f, 0f); // play tir sfx with 30% volume

                // Create a projectile
                if (kbdState.IsKeyDown(_directionalKeys[0]) || kbdState.IsKeyDown(_directionalKeys[1]) || kbdState.IsKeyDown(_directionalKeys[2]) || kbdState.IsKeyDown(_directionalKeys[3]))
                {
                    listProjectiles.Add(new Projectiles(_projectileSprite, position, rotation, currentDirection, _listSfx[NUMWALLHURTSFX], 20));
                }
                else
                    listProjectiles.Add(new Projectiles(_projectileSprite, position, rotation, currentDirection, _listSfx[NUMWALLHURTSFX]));
            }

            // if has released shoot key (F)
            if (kbdState.IsKeyUp(Keys.F) && !this.hasReleasedShootingkey)
            {
                this.currentStep = 0; // reset anim
                this.hasReleasedShootingkey = true;
            }
        }

        private void isCollidingWitObject(List<Object> listObjects)
        {            
            foreach (Object decorObject in listObjects)
            {
                if (this.position.X + this.walkingSprites[0].Height / 2 > decorObject.position.X - decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.X - this.walkingSprites[0].Height / 2 < decorObject.position.X - decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.Y + this.walkingSprites[0].Height / 2 - 10 > decorObject.position.Y - decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.Y - this.walkingSprites[0].Height / 2 + 10 < decorObject.position.Y + decorObject.texture.Height * decorObject.size / 2) // Left
                {
                    this.position.X = (decorObject.position.X - decorObject.texture.Width * decorObject.size / 2) - (this.walkingSprites[0].Height / 2);
                }

                if (this.position.X - this.walkingSprites[0].Height / 2 < decorObject.position.X + decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.X + this.walkingSprites[0].Height / 2 > decorObject.position.X + decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.Y + this.walkingSprites[0].Height / 2 - 10 > decorObject.position.Y - decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.Y - this.walkingSprites[0].Height / 2 + 10< decorObject.position.Y + decorObject.texture.Height * decorObject.size / 2) // Right
                {
                    this.position.X = (decorObject.position.X + decorObject.texture.Width * decorObject.size / 2) + (this.walkingSprites[0].Height / 2);
                }

                if (this.position.Y + this.walkingSprites[0].Height / 2 > decorObject.position.Y - decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.Y - this.walkingSprites[0].Height / 2 < decorObject.position.Y - decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.X + this.walkingSprites[0].Height / 2 > decorObject.position.X - decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.X - this.walkingSprites[0].Height / 2 < decorObject.position.X + decorObject.texture.Width * decorObject.size / 2)  // Top
                {
                    this.position.Y = (decorObject.position.Y - decorObject.texture.Height * decorObject.size / 2) - (this.walkingSprites[0].Height / 2);
                }

                if (this.position.Y - this.walkingSprites[0].Height / 2 < decorObject.position.Y + decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.Y + this.walkingSprites[0].Height / 2 > decorObject.position.Y + decorObject.texture.Height * decorObject.size / 2 &&
                    this.position.X + this.walkingSprites[0].Height / 2 > decorObject.position.X - decorObject.texture.Width * decorObject.size / 2 &&
                    this.position.X - this.walkingSprites[0].Height / 2 < decorObject.position.X + decorObject.texture.Width * decorObject.size / 2) // Botton
                {
                    this.position.Y = (decorObject.position.Y + decorObject.texture.Height * decorObject.size / 2) + (this.walkingSprites[0].Height / 2);
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, Texture2D healthBarTexture)
        {
            _spriteBatch.Draw(this.currentSprite, this.position, null, this.color, this.rotation, new Vector2(this.currentSprite.Width / 2, this.currentSprite.Height / 2), 1f, SpriteEffects.None, 0f);
            
            if (this.healthPoint > 0)
            {
                // Draw health bar border
                healthBarTexture.SetData(new Color[] { Color.Black });
                _spriteBatch.Draw(healthBarTexture, new Rectangle(healthBarRectangle.X - 2, healthBarRectangle.Y - 2, Player.HEALTH_POINT_MAX / 2 + 4, 7 + 4), Color.Black * 0.5f);

                // Draw health bar
                healthBarTexture.SetData(new Color[] { Color.FromNonPremultiplied(34, 177, 76, 255) });
                _spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);
            }
        }

    }
} 