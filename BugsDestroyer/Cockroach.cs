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
    class Cockroach
    {
        // attributs
        private Vector2 _position;
        private Texture2D[] _cockroachFrames = new Texture2D[2];
        private Texture2D _cockroachCurrentFrame;
        private int _speed;
        private int rotation;

        // ctor
        public Cockroach()
        {

        }

        // methods
        public void Load()
        {
        }

        public void Update()
        {
        }

        private void FollowPlayer()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._cockroachCurrentFrame, _position, null, Color.White, 0f, new Vector2(_cockroachCurrentFrame.Width / 2, _cockroachCurrentFrame.Height / 2), 1f , SpriteEffects.None, 0f);
        }
    }
}