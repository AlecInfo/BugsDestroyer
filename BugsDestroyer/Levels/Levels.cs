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
        public Trapdoor _trapdoor;

        public List<Enemy> listEnemies = new List<Enemy>();
        public List<Object> listObjects = new List<Object>();

        public bool ready = false;


        public Levels(Texture2D background, List<Enemy> listEnemy, List<Decor> listDecor, Trapdoor trapdoor)
        {
            this._background = background;
            this._trapdoor = trapdoor;

            foreach (var item in listEnemy)
            {
                listEnemies.Add(item);
            }

            foreach (var item in listDecor)
            {
                listObjects.Add(item);
            }
        }

        public void Update(GameTime gameTime1)
        {
            
        }
    }
}
