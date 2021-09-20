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
        private int[] PlayerWalkAnimSteps = { 1, 2, 3, 2, 1, 4, 5, 6, 5, 4 };
        private Texture2D[] playerWalkingSprites = new Texture2D[7];
        private int[] PlayerShotAnimSteps = { 1, 2};
        private Texture2D[] playerShotSprites = new Texture2D[3];
        private Texture2D playerCurrentSprite;
        private int currentStep = 0;

        private Vector2 playerPos = new Vector2(100, 100);
        private int playerWalkingSpeed = 8;
        private float playerRotation = 0f;


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

                // increment current frame
                currentStep += 1;
                if (currentStep == 10)
                {
                    currentStep = 0;
                    
                }
            }

            if (playerKbdState.IsKeyDown(Keys.W) || playerKbdState.IsKeyDown(Keys.D) || playerKbdState.IsKeyDown(Keys.S) || playerKbdState.IsKeyDown(Keys.A))
            {

                playerCurrentSprite = playerWalkingSprites[PlayerWalkAnimSteps[currentStep]];

            }
            else
            {
                playerCurrentSprite = playerWalkingSprites[0];
            }



            // collision des murs
            if (playerPos.X < 280)
            {
                playerPos.X = 280;
            }
            else if (playerPos.X > 1640)
            {
                playerPos.X = 1640;
            }

            if (playerPos.Y < 160)
            {
                playerPos.Y = 160;
            }
            else if (playerPos.Y > 915)
            {
                playerPos.Y = 915;
            }
        }
    }
}