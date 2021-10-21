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
    public class Decor : Object
    {
        /// <summary>
        /// Création du décor
        /// (Alec Piette)
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        public Decor(Texture2D texture, Vector2 position, float size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        /// <summary>
        /// Affichge du décor
        /// (Alec Piette)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, position, null, Color.White, 0, new Vector2(this.texture.Width / 2, this.texture.Height / 2), this.size, SpriteEffects.None, 0f);
        }
    }
}
