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
        // varriables
        private string _name;
        private double _timeScore;
        private Vector2 _position;
        private int _placement;

        // geter seter
        public string Name { get => _name; set => _name = value; }
        public double TimeScore { get => _timeScore; set => _timeScore = value; }

        /// <summary>
        /// Création du score pour l'afficher dans le tableau des scores 
        /// (Alec Piette)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        /// <param name="position"></param>
        /// <param name="placement"></param>
        public Score(string name, double score, Vector2 position, int placement)
        {
            this.Name = name;
            this.TimeScore = score;
            this._position = position;
            this._placement = placement;
        }


        /// <summary>
        /// Affichage du score pour le tableau
        /// (Alec Piette)
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, Convert.ToString(_placement) + "-    " + Name + "    " + TimeScore, _position, Color.Lime, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }
    }
}
