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
        // Draw Attr
        public Vector2 position;
        public Texture2D CurrentFrame;
        public Color color = Color.White;
        public float rotation = 0f;
        public float size = 2f;

        // Update Attr
        public float speed;
        public Vector2 velocity = Vector2.Zero;
        public Vector2 direction;


        public abstract void Update(GameTime gameTime, List<Player> players, List<Projectiles> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<Texture2D> mobExplosion, List<Object> objects);

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            spriteBatch.Draw(this.CurrentFrame, this.position, null, this.color, this.rotation, new Vector2(this.CurrentFrame.Width / 2, this.CurrentFrame.Height / 2), this.size, SpriteEffects.None, 0f);
        }

        public void enemyCollision(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.GetType().ToString() != "BugsDestroyer.Spider") // if the enemy is not a spider
                {
                    Collision(enemy.position, enemy.CurrentFrame);
                }
            }
        }


        public void objectCollision(List<Object> listObjects)
        {

            foreach (Object decorObject in listObjects)
            {

                Collision(decorObject.position, decorObject.texture, decorObject.size);
            }
        }

        public void Collision(Vector2 position, Texture2D texture, float size = 1f)
        {
            Vector2 scaledTexture = new Vector2(texture.Width * size, texture.Height * size);

            if (this.position.X + this.CurrentFrame.Width / 2 > position.X - scaledTexture.X / 2 &&
                this.position.X - this.CurrentFrame.Width / 2 < position.X - scaledTexture.X / 2 &&
                this.position.Y + this.CurrentFrame.Height / 2 - 10 > position.Y - scaledTexture.Y / 2 &&
                this.position.Y - this.CurrentFrame.Height / 2 + 10 < position.Y + scaledTexture.Y / 2) // Left
            {
                this.position.X = (position.X - scaledTexture.X / 2) - (this.CurrentFrame.Width / 2);
            }

            if (this.position.X - this.CurrentFrame.Width / 2 < position.X + scaledTexture.X / 2 &&
                this.position.X + this.CurrentFrame.Width / 2 > position.X + scaledTexture.X / 2 &&
                this.position.Y + this.CurrentFrame.Height / 2 - 10 > position.Y - scaledTexture.Y / 2 &&
                this.position.Y - this.CurrentFrame.Height / 2 + 10 < position.Y + scaledTexture.Y / 2) // Right
            {
                this.position.X = (position.X + scaledTexture.X / 2) + (this.CurrentFrame.Width / 2);
            }

            if (this.position.Y + this.CurrentFrame.Height / 2 > position.Y - scaledTexture.Y / 2 &&
                this.position.Y - this.CurrentFrame.Height / 2 < position.Y - scaledTexture.Y / 2 &&
                this.position.X + this.CurrentFrame.Width / 2 > position.X - scaledTexture.X / 2 &&
                this.position.X - this.CurrentFrame.Width / 2 < position.X + scaledTexture.X / 2)  // Top
            {
                this.position.Y = (position.Y - scaledTexture.Y / 2) - (this.CurrentFrame.Height / 2);
            }

            if (this.position.Y - this.CurrentFrame.Height / 2 < position.Y + scaledTexture.Y / 2 &&
                this.position.Y + this.CurrentFrame.Height / 2 > position.Y + scaledTexture.Y / 2 &&
                this.position.X + this.CurrentFrame.Width / 2 > position.X - scaledTexture.X / 2 &&
                this.position.X - this.CurrentFrame.Width / 2 < position.X + scaledTexture.X / 2) // Bottom
            {
                this.position.Y = (position.Y + scaledTexture.Y / 2) + (this.CurrentFrame.Height / 2);
            }
        }
    }
}
