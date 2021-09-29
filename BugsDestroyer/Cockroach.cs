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
    class Cockroach
    {
        // attributs
        private Vector2 _position = new Vector2(500,500);
        private Texture2D[] _cockroachFrames = new Texture2D[2];
        private Texture2D _cockroachCurrentFrame;
        private int _speed = 3;
        private float rotation = 0;

        //Anim
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 100;


        // ctor
        public Cockroach()
        {

        }

        // methods
        public void Load(ContentManager Content)
        {
            _cockroachFrames[0] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard0");
            _cockroachFrames[1] = Content.Load<Texture2D>("Img/Mobs/Cafard/cafard1");

            _cockroachCurrentFrame = _cockroachFrames[0];
        }

        public void Update(GameTime gameTime, Player player)
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

            FollowPlayer(player);
        }

        private void FollowPlayer(Player player)
        {
            //float distanceX = player.position.X - _position.X;
            //float distanceY = player.position.Y - _position.Y;
            Vector2 direction = player._position - _position;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._cockroachCurrentFrame, _position, null, Color.White, rotation, new Vector2(_cockroachCurrentFrame.Width / 2, _cockroachCurrentFrame.Height / 2), 2f , SpriteEffects.None, 0f);
        }
    }
}