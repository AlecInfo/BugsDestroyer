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
    public abstract class Enemy
    {
        public Vector2 position;
        public Texture2D CurrentFrame;
        public Color color = Color.White;
        public float rotation = 0f;

        public abstract void Update(GameTime gameTime, List<Player> players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.CurrentFrame, this.position, null, this.color, this.rotation, new Vector2(this.CurrentFrame.Width / 2, this.CurrentFrame.Height / 2), 2f, SpriteEffects.None, 0f);
        }

    }
}
