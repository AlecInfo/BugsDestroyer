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
        private int _speed = 5;
        private int rotation = 0;

        //Anim
        public int currentFrameNb;
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

        public void Update(GameTime gameTime)
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

            FollowPlayer();
        }

        private void FollowPlayer()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._cockroachCurrentFrame, _position, null, Color.White, 0f, new Vector2(_cockroachCurrentFrame.Width / 2, _cockroachCurrentFrame.Height / 2), 2f , SpriteEffects.None, 0f);
        }
    }
}