using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BugsDestroyer.Menus
{
    public class Score
    {
        private string _name;
        private double _timeScore;
        private Vector2 _position;
        private int _placement;

        public string Name { get => _name; set => _name = value; }
        public double TimeScore { get => _timeScore; set => _timeScore = value; }

        public Score(string name, double score, Vector2 position, int placement)
        {
            this.Name = name;
            this.TimeScore = score;
            this._position = position;
            this._placement = placement;
        }



        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, Convert.ToString(_placement) + "-    " + Name + "    " + TimeScore, _position, Color.Lime, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }
    }
}
