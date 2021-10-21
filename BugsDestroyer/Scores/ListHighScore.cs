using BugsDestroyer.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugsDestroyer
{
    [Serializable]
    public class ListHighScore
    {
        // varriable
        public List<HighScore> listHighScore = new List<HighScore>();

        /// <summary>
        /// Création de la liste des meilleurs score
        /// (Alec Piette)
        /// </summary>
        /// <param name="listHighScore"></param>
        public ListHighScore(List<HighScore> listHighScore)
        {
            this.listHighScore = listHighScore;
        }
        
        /// <summary>
        /// Constructeur pour la serialisation
        /// (Alec Piette)
        /// </summary>
        public ListHighScore()
        {
            // constructor pour serializer
        }
    }
}
