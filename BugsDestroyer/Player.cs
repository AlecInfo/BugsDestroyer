using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugsDestroyer
{
    public partial class Game1 : Game
    {
        // Variables
        private int[] playerWalkAnimSteps = { 1, 2, 3, 2, 1, 4, 5, 6, 5, 4 };
        private Texture2D[] playerWalkingSprites = new Texture2D[7];
        private int[] playerShotAnimSteps = { 0, 0, 1, 0};
        private Texture2D[] playerShotSprites = new Texture2D[3];
        private bool isShooting = false;
        private bool hasReleasedShootingkey = true;
        private Texture2D playerCurrentSprite;
        private int currentStep = 0;

        private Vector2 playerPos = new Vector2(100, 100);
        private int playerWalkingSpeed = 8;
        private float playerRotation = 0f;

        // Sfx
        private SoundEffect keyboardSfx;



        protected void playerLoad()
        {
            // load walking sprites
            for (int x = 0; x < 7; x++)
            {
                playerWalkingSprites[x] = Content.Load<Texture2D>("Img/Perso/walking/walking" + x.ToString());
            }

            // load shooting sprites
            playerShotSprites[0] = Content.Load<Texture2D>("Img/Perso/shot/shot0");
            playerShotSprites[1] = Content.Load<Texture2D>("Img/Perso/shot/shot1");
            playerShotSprites[2] = Content.Load<Texture2D>("Img/Perso/shot/shot2");

            // load sfx
            keyboardSfx = Content.Load<SoundEffect>("Sounds/Sfx/tir");


            playerCurrentSprite = playerWalkingSprites[0];
        }


        protected void playerUpdate(GameTime gameTime)
        {
            KeyboardState playerKbdState = Keyboard.GetState();

            if (playerKbdState.IsKeyDown(Keys.W) && playerKbdState.IsKeyDown(Keys.D))
            {
                playerPos.X += playerWalkingSpeed / 1.4f;
                playerPos.Y -= playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI * 7 / 4; //315
            }
            else if (playerKbdState.IsKeyDown(Keys.D) && playerKbdState.IsKeyDown(Keys.S))
            {
                playerPos.X += playerWalkingSpeed / 1.4f;
                playerPos.Y += playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI / 4; // 45
            }
            else if (playerKbdState.IsKeyDown(Keys.S) && playerKbdState.IsKeyDown(Keys.A))
            {
                playerPos.X -= playerWalkingSpeed / 1.4f;
                playerPos.Y += playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI * 3 / 4; // 135
            }
            else if (playerKbdState.IsKeyDown(Keys.A) && playerKbdState.IsKeyDown(Keys.W))
            {
                playerPos.X -= playerWalkingSpeed / 1.4f;
                playerPos.Y -= playerWalkingSpeed / 1.4f;
                playerRotation = (float)Math.PI * 5 / 4; //225
            }
            else
            {
                if (playerKbdState.IsKeyDown(Keys.D))
                {
                    playerPos.X += playerWalkingSpeed;
                    playerRotation = 0; // 0
                }
                if (playerKbdState.IsKeyDown(Keys.S))
                {
                    playerPos.Y += playerWalkingSpeed;
                    playerRotation = (float)Math.PI / 2; // 90
                }
                if (playerKbdState.IsKeyDown(Keys.A))
                {
                    playerPos.X -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI; // 180
                }
                if (playerKbdState.IsKeyDown(Keys.W))
                {
                    playerPos.Y -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI * 1.5f; // 270
                }
            }


            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;



                // if has clicked shoot key (F)
                if (playerKbdState.IsKeyDown(Keys.F) && hasReleasedShootingkey && !isShooting)
                {
                    currentStep = 0; // reset anim
                    hasReleasedShootingkey = false;
                    isShooting = true;
                    keyboardSfx.Play(volume: 0.3f, 0f, 0f); // play keybord sfx with 30% volume
                }
                // if has released shoot key (F)
                if (playerKbdState.IsKeyUp(Keys.F) && !hasReleasedShootingkey)
                {
                    currentStep = 0; // reset anim
                    hasReleasedShootingkey = true;
                }

                // if is walking
                if ((playerKbdState.IsKeyDown(Keys.W) || playerKbdState.IsKeyDown(Keys.D) || playerKbdState.IsKeyDown(Keys.S) || playerKbdState.IsKeyDown(Keys.A)) && !isShooting)
                {
                    currentStep += 1; // increment current frame
                    if (currentStep == playerWalkingSprites.Length)
                    {
                        currentStep = 0; // reset anim
                    }
                    playerCurrentSprite = playerWalkingSprites[playerWalkAnimSteps[currentStep]];
                }
                else if (isShooting)
                {
                    currentStep += 1; // increment current frame
                    if (currentStep == playerShotSprites.Length)
                    {
                        currentStep = 0; // reset anim
                        isShooting = false;
                    }
                    playerCurrentSprite = playerShotSprites[playerShotAnimSteps[currentStep]];
                }
                else
                {
                    playerCurrentSprite = playerWalkingSprites[0];
                }
            }


            // Collision des murs
            if (playerPos.X < 280) // Left
            {
                playerPos.X = 280;
            }
            else if (playerPos.X > 1640) // Right
            {
                playerPos.X = 1640;
            }

            if (playerPos.Y < 155) // Top
            {
                playerPos.Y = 155;
            }
            else if (playerPos.Y > 915) // Bottom
            {
                playerPos.Y = 915;
            }
        }
    }
}