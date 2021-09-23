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


        private int[] walkAnimSteps = { 1, 2, 3, 2, 1, 4, 5, 6, 5, 4 };
        private Texture2D[] _walkingSprites = new Texture2D[7];
        public int[] shotAnimSteps = { 0, 0, 1, 0};
        private Texture2D[] _shotSprites = new Texture2D[3];
        public bool isShooting = false;
        public bool hasReleasedShootingkey = true;
        public Texture2D currentSprite;
        public int currentStep = 0;
        private Texture2D _deadSprite;

        public Vector2 position = new Vector2(100, 100);
        public int walkingSpeed = 8;
        public float rotation = 0f;

        // Point de vie
        public const int HEALTH_POINT_MAX = 100;
        public const int HEALTH_POINT_MIN = 0;
        private int _healthPoint = HEALTH_POINT_MAX;
        public int healthPoint { get => _healthPoint; set => _healthPoint = Math.Max(HEALTH_POINT_MIN, Math.Min(value, HEALTH_POINT_MAX)); }

        // Health bar
        Rectangle healthBarRectangle;

        //Anim
        public int currentFrameNb;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;

        // Sfx
        private SoundEffect _keyboardSfx;

        // Projectiles
        public Projectiles projectiles;
        private Texture2D[] _projectileSprite = new Texture2D[2];
        private Game1.direction currentDirection;

        // ctor
        public Player(Texture2D[] walkingSprites, Texture2D[] shotSprites, Texture2D deadSprite, SoundEffect keyboardSfx, Keys[] directionalKeys, Keys shootkey, Texture2D[] projectileSprite)
        {
            _walkingSprites = walkingSprites;
            _shotSprites = shotSprites;
            currentSprite = _walkingSprites[0];

            _deadSprite = deadSprite;

            _keyboardSfx = keyboardSfx;

            _directionalKeys = directionalKeys;
            _shootKey = shootkey;

            this._projectileSprite = projectileSprite;
        }


        public void playerUpdate(GameTime gameTime)
        {
            if (healthPoint <= 0)
            {
                currentSprite = _deadSprite;
                return;
            }

            KeyboardState playerKbdState = Keyboard.GetState();

            // Just for test the health system
            if (playerKbdState.IsKeyDown(Keys.M))
            {
                healthPoint -= 5;
            }

            if (playerKbdState.IsKeyDown(_directionalKeys[0]) && playerKbdState.IsKeyDown(_directionalKeys[3]))
            {
                position.X += walkingSpeed / 1.4f;
                position.Y -= walkingSpeed / 1.4f;
                rotation = (float)Math.PI * 7 / 4; //NE
                currentDirection = Game1.direction.NE;
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[3]) && playerKbdState.IsKeyDown(_directionalKeys[2]))
            {
                position.X += walkingSpeed / 1.4f;
                position.Y += walkingSpeed / 1.4f;
                rotation = (float)Math.PI / 4; // SE
                currentDirection = Game1.direction.SE;
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[2]) && playerKbdState.IsKeyDown(_directionalKeys[1]))
            {
                position.X -= walkingSpeed / 1.4f;
                position.Y += walkingSpeed / 1.4f;
                rotation = (float)Math.PI * 3 / 4; // SW
                currentDirection = Game1.direction.SW;
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[1]) && playerKbdState.IsKeyDown(_directionalKeys[0]))
            {
                position.X -= walkingSpeed / 1.4f;
                position.Y -= walkingSpeed / 1.4f;
                rotation = (float)Math.PI * 5 / 4; // NW
                currentDirection = Game1.direction.NW;
            }
            else
            {
                if (playerKbdState.IsKeyDown(_directionalKeys[3]))
                {
                    position.X += walkingSpeed;
                    rotation = 0; // E
                    currentDirection = Game1.direction.E;
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[2]))
                {
                    position.Y += walkingSpeed;
                    rotation = (float)Math.PI / 2; // S
                    currentDirection = Game1.direction.S;
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[1]))
                {
                    position.X -= walkingSpeed;
                    rotation = (float)Math.PI; // W
                    currentDirection = Game1.direction.W;
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[0]))
                {
                    position.Y -= walkingSpeed;
                    rotation = (float)Math.PI * 1.5f; // N
                    currentDirection = Game1.direction.N;
                }
            }


            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;



                // if has clicked shoot key (F)
                if (playerKbdState.IsKeyDown(_shootKey) && hasReleasedShootingkey && !isShooting)
                {
                    currentStep = 0; // reset anim
                    hasReleasedShootingkey = false;
                    isShooting = true;
                    _keyboardSfx.Play(volume: 0.3f, 0f, 0f); // play keybord sfx with 30% volume

                    // Create a projectile
                    projectiles = new Projectiles(_projectileSprite, position, rotation, currentDirection);
                }
                // if has released shoot key (F)
                if (playerKbdState.IsKeyUp(Keys.F) && !hasReleasedShootingkey)
                {
                    currentStep = 0; // reset anim
                    hasReleasedShootingkey = true;
                }

                // if is walking
                if ((playerKbdState.IsKeyDown(_directionalKeys[0]) || playerKbdState.IsKeyDown(_directionalKeys[1]) || playerKbdState.IsKeyDown(_directionalKeys[2]) || playerKbdState.IsKeyDown(_directionalKeys[3])) && !isShooting)
                {
                    currentStep += 1; // increment current frame
                    if (currentStep == _walkingSprites.Length)
                    {
                        currentStep = 0; // reset anim
                    }
                    currentSprite = _walkingSprites[walkAnimSteps[currentStep]];
                }
                else if (isShooting)
                {
                    currentStep += 1; // increment current frame
                    if (currentStep == _shotSprites.Length)
                    {
                        currentStep = 0; // reset anim
                        isShooting = false;
                    }
                    currentSprite = _shotSprites[shotAnimSteps[currentStep]];
                }
                else
                {
                    currentSprite = _walkingSprites[0];
                }
            }


            // Collision des murs
            if (position.X < 280) // Left
            {
                position.X = 280;
            }
            else if (position.X > 1640) // Right
            {
                position.X = 1640;
            }

            if (position.Y < 155) // Top
            {
                position.Y = 155;
            }
            else if (position.Y > 915) // Bottom
            {
                position.Y = 915;
            }

            healthBarRectangle = new Rectangle(
                        Convert.ToInt32(position.X - HEALTH_POINT_MAX / 4),
                        Convert.ToInt32(position.Y - currentSprite.Height / 2 - 20),
                        healthPoint / 2,
                        7);
        }

        public void playerDrawHealthBar(GameTime gameTime, SpriteBatch _spriteBatch, Texture2D healthBarBorderTexture, Texture2D healthBarTexture)
        {
            if (healthPoint > 0)
            {
                // Draw health bar border
                healthBarBorderTexture.SetData(new Color[] { Color.Black });
                _spriteBatch.Draw(healthBarBorderTexture, new Rectangle(healthBarRectangle.X - 2, healthBarRectangle.Y - 2, Player.HEALTH_POINT_MAX / 2 + 4, 7 + 4), Color.Black * 0.5f);
                // Draw health bar
                _spriteBatch.Draw(healthBarTexture, healthBarRectangle, Color.White);
            }
        }
    }
}