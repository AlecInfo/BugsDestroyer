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
        private int _hitEffectTime = 200;
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
        private SoundEffect _keyboardSfx;

        // Projectiles
        private Texture2D[] _projectileSprite = new Texture2D[2];
        private Game1.direction currentDirection = Game1.direction.E;


        // ctor
        public Player(GameTime gametime,Texture2D[] walkingSprites, Texture2D[] shotSprites, Texture2D deadSprite, SoundEffect keyboardSfx, Keys[] directionalKeys, Keys shootkey, Keys interactKey, Texture2D[] projectileSprite, Vector2 position)
        {
            // R�cuperation des donn�es
            this._gameTime = gametime;
            this.walkingSprites = walkingSprites;
            this._shotSprites = shotSprites;
            this.currentSprite = this.walkingSprites[0];

            this._deadSprite = deadSprite;

            this._keyboardSfx = keyboardSfx;

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

            // Just for test the health system
            if (kbdState.IsKeyDown(Keys.M))
            {
                this.healthPoint -= 5;
            }

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


            // hit effect
            if (this._isHit && this._healthPoint != 0) // if the player is hit and he's not dead
            {
                this.color = new Color(255, 100, 100); // red
                this._hitEffectTime -= gameTime.ElapsedGameTime.Milliseconds;

                if (this._hitEffectTime <= 0)
                {
                    this._hitEffectTime = 100;
                    this._isHit = false;
                }
            }else
                this.color = Color.White;

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
            if (this.position.X < 280) // Left
            {
                this.position.X = 280;
            }
            else if (this.position.X > 1640) // Right
            {
                this.position.X = 1640;
            }

            if (this.position.Y < 155) // Top
            {
                this.position.Y = 155;
            }
            else if (this.position.Y > 915) // Bottom
            {
                this.position.Y = 915;
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
                this._keyboardSfx.Play(volume: 0.3f, 0f, 0f); // play keybord sfx with 30% volume

                // Create a projectile
                if (kbdState.IsKeyDown(this._directionalKeys[0]) || kbdState.IsKeyDown(this._directionalKeys[1]) || kbdState.IsKeyDown(this._directionalKeys[2]) || kbdState.IsKeyDown(this._directionalKeys[3]))
                {
                    listProjectiles.Add(new Projectiles(this._projectileSprite, this.position, this.rotation, this.currentDirection, 20));
                }
                else
                    listProjectiles.Add(new Projectiles(this._projectileSprite, this.position, this.rotation, this.currentDirection));

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

                // continuer
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
<<<<<<< HEAD
                healthBarBorderTexture.SetData(new Color[] { Color.Black });
                _spriteBatch.Draw(healthBarBorderTexture, new Rectangle(this.healthBarRectangle.X - 2, this.healthBarRectangle.Y - 2, Player.HEALTH_POINT_MAX / 2 + 4, 7 + 4), Color.Black * 0.5f);
                // Draw health bar
                _spriteBatch.Draw(healthBarTexture, this.healthBarRectangle, Color.White);
=======
                healthBarTexture.SetData(new Color[] { Color.Black });
                _spriteBatch.Draw(healthBarTexture, new Rectangle(healthBarRectangle.X - 2, healthBarRectangle.Y - 2, Player.HEALTH_POINT_MAX / 2 + 4, 7 + 4), Color.Black * 0.5f);
                // Draw health bar
                healthBarTexture.SetData(new Color[] { Color.FromNonPremultiplied(34, 177, 76, 255) });
                _spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);
>>>>>>> af4b1bbc7ac12af5cd9a560498c99a496a839c59
            }
        }

    }
} 