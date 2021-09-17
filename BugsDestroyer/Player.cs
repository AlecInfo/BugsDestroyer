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

        protected void playerUpdate(GameTime gameTime)
        {
            KeyboardState playerKbdState = Keyboard.GetState();
            Keys[] directionalKeys = { Keys.W, Keys.A, Keys.S, Keys.D };
            if (playerKbdState.GetPressedKeys().Length != 0)
            {
                if (directionalKeys.Contains(playerKbdState.GetPressedKeys().Last()))
                {
                    currentDirectionalKey = playerKbdState.GetPressedKeys().Last();
                }
            }
            else
            {
                currentDirectionalKey = Keys.None;
            }


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
                if (currentDirectionalKey == Keys.D)
                {
                    playerPos.X += playerWalkingSpeed;
                    playerRotation = 0; // 0
                }
                if (currentDirectionalKey == Keys.S)
                {
                    playerPos.Y += playerWalkingSpeed;
                    playerRotation = (float)Math.PI / 2; // 90
                }
                if (currentDirectionalKey == Keys.A)
                {
                    playerPos.X -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI; // 180
                }
                if (currentDirectionalKey == Keys.W)
                {
                    playerPos.Y -= playerWalkingSpeed;
                    playerRotation = (float)Math.PI * 1.5f; // 270
                }
            }



            if (playerKbdState.IsKeyDown(Keys.W) || playerKbdState.IsKeyDown(Keys.D) || playerKbdState.IsKeyDown(Keys.S) || playerKbdState.IsKeyDown(Keys.A))
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame -= millisecondsPerFrame;

                    // increment current frame
                    currentStep += 1;
                    if (currentStep == 10)
                    {
                        currentStep = 0;
                    }
                }

                playerCurrentSprite = playerWalkingSprites[PlayerAnimSteps[currentStep]];

            }
            else
            {
                playerCurrentSprite = playerWalkingSprites[0];
            }
        }


    }
}