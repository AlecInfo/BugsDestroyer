using System;
using System.Collections.Generic;
using System.Text;

namespace BugsDestroyer.Menus
{
    [Serializable]
    public class HighScore
    {
        // varriables
        private string _name;
        private double _score;

        // geter seter
        public string Name { get => _name; set => _name = value; }
        public double Score { get => _score; set => _score = value; }

        /// <summary>
        /// Création d'un score
        /// (Alec Piette)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public HighScore(string name, double score) : this()
        {
            this.Name = name;
            this.Score = score;
        }

        /// <summary>
        /// Constructeur pour la serialization
        /// (Alec Piette)
        /// </summary>
        public HighScore()
        {
            // constructor pour serializer
        }
    }
}
