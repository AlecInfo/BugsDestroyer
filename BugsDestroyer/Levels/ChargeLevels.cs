﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BugsDestroyer
{
    public partial class Game1 : Game
    {
        protected void LevelLoad()
        {
            // Level 1g
            listLevels.Add(
                new Levels(
                    Sol[0],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        /*new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),*/
                        new Spider(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), spiderSprites)
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(800, 360), 1.2f)
                    },
                    new Trapdoor(trapdoor, new Vector2(1200, 800), 2.3f)
                    )
                );

            // Level 2
            listLevels.Add(
                new Levels(
                    Sol[1],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(800, 360), 1.2f),
                        new Decor(PCI, new Vector2(650, 550), 3f),
                        new Decor(PCI, new Vector2(650, 650), 3f) 
                    },
                    new Trapdoor(trapdoor, new Vector2(1200, 800), 2.3f)
                    )
                );

            // Level 3
            listLevels.Add(
                new Levels(
                    Sol[2],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(800, 360), 1.2f),
                        new Decor(PCI, new Vector2(650, 550), 3f),
                        new Decor(PCI, new Vector2(650, 650), 3f),
                        new Decor(MiniPCI, new Vector2(600, 750), 3f)
                    },
                    new Trapdoor(trapdoor, new Vector2(1200, 800), 2.3f)
                    )
                );

            // Level 4
            listLevels.Add(
                new Levels(
                    Sol[3],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Cockroach(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), cockroachSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                        new Beetle(new Vector2(rnd.Next(0, 1950), rnd.Next(0, 1080)), beetleSprites),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(800, 360), 1.2f),
                        new Decor(PCI, new Vector2(650, 550), 3f),
                        new Decor(PCI, new Vector2(650, 650), 3f),
                        new Decor(MiniPCI, new Vector2(600, 750), 3f),
                        new Decor(Ram, new Vector2(1000, 400), 3f),
                        new Decor(Ram, new Vector2(1050, 400), 3f),
                        new Decor(Ram, new Vector2(1100, 400), 3f),
                        new Decor(Ram, new Vector2(1150, 400), 3f)
                    },
                    new Trapdoor(trapdoor, new Vector2(1200, 800), 2.3f)
                    )
                );
        }
    }
}
