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
    public class Levels
    {
        Random rnd = new Random();

        public Texture2D _background;
        private int _nbCrockoach;
        private int _nbBeetle;
        private int _nbBlackWidow;
        public Trapdoor _trapdoor;

        public List<Enemy> listEnemies = new List<Enemy>();
        public List<Object> listObjects = new List<Object>();

        public bool ready = false;


        public Levels(Texture2D background, int nbCrockoach, int nbBeetle, int nbBlackWidow, List<Decor> listDecor, Trapdoor trapdoor)
        {
            this._background = background;
            this._nbCrockoach = nbCrockoach;
            this._nbBeetle = nbBeetle;
            this._nbBlackWidow = nbBlackWidow;
            this._trapdoor = trapdoor;

            foreach (var item in listDecor)
            {
                listObjects.Add(item);
            }
        }

        public void Update(GameTime gameTime1)
        {
            // ajout des bugs suivent le niveau
            for (int i = 0; i < _nbCrockoach; i++)
            {
                listEnemies.Add(new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080))));
            }
            for (int i = 0; i < _nbBeetle; i++)
            {
                listEnemies.Add(new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080))));
            }
            for (int i = 0; i < _nbBlackWidow; i++)
            {
                listEnemies.Add(new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080))));
            }
            ready = true;
        }
    }
}
