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
    public abstract class Item
    {
        // Attributs
        public Texture2D sprite;
        public Vector2 position;
        public float minSize = 2f;
        public float maxSize = 3f;

        // Anim
        public float rotation = (float)Math.PI;
        public float size = 2f;
        public bool isGrowing = true;
        public bool isRotatingRight = true;


        // Methods
        public abstract void Update(GameTime gameTime, List<Item> listItems, List<Player> listPlayers);

        public void ItemAnimation()
        {
            // pulsating animation
            if (this.isGrowing)
            {
                this.size += 0.02f;

                if (this.size >= this.maxSize)
                {
                    this.isGrowing = false;
                }
            }
            else
            {
                this.size -= 0.02f;

                if(this.size <= this.minSize)
                {
                    this.isGrowing = true;
                }
            }

            // rotating animation
            if (this.isRotatingRight)
            {
                this.rotation += 0.01f;

                if (this.rotation >= Math.PI * 1.1)
                {
                    this.isRotatingRight = false;
                }
            }
            else
            {
                this.rotation -= 0.01f;

                if (this.rotation <= Math.PI / 1.1)
                {
                    this.isRotatingRight = true;
                }

            }
        }

        public bool hasCollidedWithPlayer(List<Item> listItems, Player Player)
        {
            float radius = Player.walkingSprites[0].Width + this.sprite.Width;

            if (Vector2.DistanceSquared(Player.position, this.position) < Math.Pow(radius, 2)) // if is colliding
            {
                return true;
            }
            
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, this.position, null, Color.White, this.rotation, new Vector2(this.sprite.Width / 2, this.sprite.Width / 2), this.size, SpriteEffects.None, 0f);
        }
    }
}
