using BugsDestroyer.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugsDestroyer
{
    [Serializable]
    public class ListHighScore
    {
        public List<HighScore> _listHighScore = new List<HighScore>();

        public ListHighScore(List<HighScore> listHighScore)
        {
            this._listHighScore = listHighScore;
        }
        
        public ListHighScore()
        {

        }
    }
}
