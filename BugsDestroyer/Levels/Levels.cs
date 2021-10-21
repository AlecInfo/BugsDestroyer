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
        // varriables
        private Random _rnd = new Random();

        public Texture2D background;
        public Trapdoor trapdoor;

        public List<Enemy> listEnemies = new List<Enemy>();
        public List<Object> listObjects = new List<Object>();
        public List<Item> listItems = new List<Item>();

        public bool ready = false;

        /// <summary>
        /// Création d'un niveau
        /// (Alec Piette)
        /// </summary>
        /// <param name="background"></param>
        /// <param name="listEnemy"></param>
        /// <param name="listDecor"></param>
        /// <param name="listItem"></param>
        /// <param name="trapdoor"></param>
        public Levels(Texture2D background, List<Enemy> listEnemy, List<Decor> listDecor, List<Item> listItem,Trapdoor trapdoor)
        {
            this.background = background;
            this.trapdoor = trapdoor;

            foreach (var item in listEnemy)
            {
                listEnemies.Add(item);
            }

            foreach (var item in listDecor)
            {
                listObjects.Add(item);
            }

            foreach (var item in listItem)
            {
                listItems.Add(item);
            }
        }
    }
}
