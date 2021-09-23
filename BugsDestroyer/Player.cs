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
     class Player
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

        //Anim
        public int currentFrameNb;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;

        // Sfx
        private SoundEffect _keyboardSfx;

        // ctor
        public Player(Texture2D[] walkingSprites, Texture2D[] shotSprites, Texture2D deadSprite, SoundEffect keyboardSfx, Keys[] directionalKeys, Keys shootkey)
        {
            _walkingSprites = walkingSprites;
            _shotSprites = shotSprites;
            currentSprite = _walkingSprites[0];

            _deadSprite = deadSprite;

            _keyboardSfx = keyboardSfx;

            _directionalKeys = directionalKeys;
            _shootKey = shootkey;
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
                rotation = (float)Math.PI * 7 / 4; //315
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[3]) && playerKbdState.IsKeyDown(_directionalKeys[2]))
            {
                position.X += walkingSpeed / 1.4f;
                position.Y += walkingSpeed / 1.4f;
                rotation = (float)Math.PI / 4; // 45
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[2]) && playerKbdState.IsKeyDown(_directionalKeys[1]))
            {
                position.X -= walkingSpeed / 1.4f;
                position.Y += walkingSpeed / 1.4f;
                rotation = (float)Math.PI * 3 / 4; // 135
            }
            else if (playerKbdState.IsKeyDown(_directionalKeys[1]) && playerKbdState.IsKeyDown(_directionalKeys[0]))
            {
                position.X -= walkingSpeed / 1.4f;
                position.Y -= walkingSpeed / 1.4f;
                rotation = (float)Math.PI * 5 / 4; //225
            }
            else
            {
                if (playerKbdState.IsKeyDown(_directionalKeys[3]))
                {
                    position.X += walkingSpeed;
                    rotation = 0; // 0
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[2]))
                {
                    position.Y += walkingSpeed;
                    rotation = (float)Math.PI / 2; // 90
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[1]))
                {
                    position.X -= walkingSpeed;
                    rotation = (float)Math.PI; // 180
                }
                if (playerKbdState.IsKeyDown(_directionalKeys[0]))
                {
                    position.Y -= walkingSpeed;
                    rotation = (float)Math.PI * 1.5f; // 270
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
        }
    }
}