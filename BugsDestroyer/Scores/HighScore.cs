using System;
using System.Collections.Generic;
using System.Text;

namespace BugsDestroyer.Menus
{
    [Serializable]
    public class HighScore
    {
        private string _name;
        private double _score;

        public string Name { get => _name; set => _name = value; }
        public double Score { get => _score; set => _score = value; }

        public HighScore(string name, double score) : this()
        {
            this.Name = name;
            this.Score = score;
        }

        public HighScore()
        {
            // constructor pour serializer
        }
    }
}
