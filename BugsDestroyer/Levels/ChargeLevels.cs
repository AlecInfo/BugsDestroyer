using Microsoft.Xna.Framework;
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
        private const int NUMTRAPDOORSFX = 4;
        protected void LevelLoad(List<SoundEffect> listSfx)
        {
            // Level 1
            listLevels.Add(
            new Levels(
                Sol[0],
                new List<Enemy>
                {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.5f, _graphics.PreferredBackBufferHeight / 2), cockroachSprites, listSfx),
                },
                new List<Decor> {
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.25f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.2f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.45f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.15f, _graphics.PreferredBackBufferHeight / 3.5f), 1.2f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.83f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.495f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.33f, _graphics.PreferredBackBufferHeight / 3.2f), 1.2f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.23f, _graphics.PreferredBackBufferHeight / 3.5f), 3f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.25f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 3.68f, _graphics.PreferredBackBufferHeight / 1.45f), 1.2f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.05f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.35f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.83f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.495f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.26f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                },
                new List<Item>
                {

                },
                new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 2), 2.3f, listSfx[NUMTRAPDOORSFX])
                )
                );

            // level 2
            listLevels.Add(
                new Levels(
                    Sol[0],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 5), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 3, _graphics.PreferredBackBufferHeight / 2), cockroachSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 2.9f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 2.9f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.63f, _graphics.PreferredBackBufferHeight / 2.9f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.48f, _graphics.PreferredBackBufferHeight / 2.9f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.3f, _graphics.PreferredBackBufferHeight / 4.3f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.5f, _graphics.PreferredBackBufferHeight / 4.3f), 3f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.54409f, _graphics.PreferredBackBufferHeight / 5.8f), 1.2f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.8f, _graphics.PreferredBackBufferHeight / 1.3f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 4.2f, _graphics.PreferredBackBufferHeight / 1.24f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 4.2f, _graphics.PreferredBackBufferHeight / 1.19f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.65f, _graphics.PreferredBackBufferHeight / 1.17f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.63f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.48f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                    },
                    new List<Item>
                    {
                        new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 1.2f))
                    }
                    ,
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 4.5f, _graphics.PreferredBackBufferHeight / 3), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );

            // level 3
            listLevels.Add(
                new Levels(
                    Sol[0],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 1.3f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.3f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.3f, _graphics.PreferredBackBufferHeight / 1.4f), cockroachSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 1.33f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.62f, _graphics.PreferredBackBufferHeight / 1.33f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.55f, _graphics.PreferredBackBufferHeight / 1.33f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.62f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.55f, _graphics.PreferredBackBufferHeight / 2f), 3f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.3f, _graphics.PreferredBackBufferHeight / 2.2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.5f, _graphics.PreferredBackBufferHeight / 2.2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.3f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.5f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 5.3f, _graphics.PreferredBackBufferHeight / 1.8f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3.5f, _graphics.PreferredBackBufferHeight /1.8f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 2.2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 1.8f), 3f),
                    },
                    new List<Item>
                    {

                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 4.5f, _graphics.PreferredBackBufferHeight / 1.4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );

            // level 4
            listLevels.Add(
                new Levels(
                    Sol[0],
                    new List<Enemy>
                    {
                                    new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.3f, _graphics.PreferredBackBufferHeight / 5f), beetleSprites, listSfx),
                    },
                    new List<Decor> {
                                    new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.6f, _graphics.PreferredBackBufferHeight / 1.33f), 3f),
                                    new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.55f, _graphics.PreferredBackBufferHeight / 1.33f), 3f),

                                    new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 3f), 3f),
                                    new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 3f), 3f),
                                    new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 1.75f), 3f),
                                    new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 1.75f), 3f),
                    },
                    new List<Item>
                    {

                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 1.5f, _graphics.PreferredBackBufferHeight / 4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );

            // level 5
            listLevels.Add(
                new Levels(
                    Sol[1],
                    new List<Enemy>
                    {
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.3f, _graphics.PreferredBackBufferHeight / 1.4f), beetleSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.75f, _graphics.PreferredBackBufferHeight / 1.5f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 3, _graphics.PreferredBackBufferHeight / 2f), cockroachSprites, listSfx),

                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 5.8f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 3.8f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 2.85f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.2f, _graphics.PreferredBackBufferHeight / 2.25f), 1.2f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.6f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.85f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.6f, _graphics.PreferredBackBufferHeight / 1.85f), 3f),

                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.42f, _graphics.PreferredBackBufferHeight / 1.65f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.37f, _graphics.PreferredBackBufferHeight / 1.65f), 3f),
                    },
                    new List<Item>
                    {

                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 3.3f, _graphics.PreferredBackBufferHeight / 4), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 6
            listLevels.Add(
                new Levels(
                    Sol[1],
                    new List<Enemy>
                    {
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 3.3f, _graphics.PreferredBackBufferHeight / 1.3f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f), beetleSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.75f, _graphics.PreferredBackBufferHeight / 1.3f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 1.4f), cockroachSprites, listSfx),

                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.6f, _graphics.PreferredBackBufferHeight / 1.63f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.6f, _graphics.PreferredBackBufferHeight / 1.42f), 1.2f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.5f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.25f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.5f, _graphics.PreferredBackBufferHeight / 1.85f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.25f, _graphics.PreferredBackBufferHeight / 1.85f), 3f),

                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.6f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.42f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.26f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                    },
                    new List<Item>
                    {
                        new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f))
                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 1.35f, _graphics.PreferredBackBufferHeight / 1.45f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 7
            listLevels.Add(
                new Levels(
                    Sol[1],
                    new List<Enemy>
                    {
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 4f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.3f), beetleSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 3.5f, _graphics.PreferredBackBufferHeight / 1.4f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 4f), cockroachSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 2f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.8f, _graphics.PreferredBackBufferHeight / 1.4f), 1.2f),

                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2.1f, _graphics.PreferredBackBufferHeight / 4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.915f, _graphics.PreferredBackBufferHeight / 4f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.83f, _graphics.PreferredBackBufferHeight / 4f), 3f),
                    },
                    new List<Item>
                    {

                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 8
            listLevels.Add(
                new Levels(
                    Sol[1],
                    new List<Enemy>
                    {
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 2.35f, _graphics.PreferredBackBufferHeight / 4.5f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 1.3f), beetleSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 4.3f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 3f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 2.5f, _graphics.PreferredBackBufferHeight / 1.4f), cockroachSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 1.4f), 1.2f),
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2.35f, _graphics.PreferredBackBufferHeight / 2.5f), 1.2f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.15f, _graphics.PreferredBackBufferHeight / 2.2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.77f, _graphics.PreferredBackBufferHeight / 2.2f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 2.15f, _graphics.PreferredBackBufferHeight / 2f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.77f, _graphics.PreferredBackBufferHeight / 2f), 3f),

                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 1.5f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 1.4f), 3f),
                    },
                    new List<Item>
                    {
                        new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 1.3f))
                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 9
            listLevels.Add(
                new Levels(
                    Sol[2],
                    new List<Enemy>
                    {
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 4.5f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 1.3f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 1.4f), beetleSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 4.3f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 3.3f), 1.2f),

                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 3f), 3f),
                        new Decor(PCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 2.6f), 3f),

                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.8f, _graphics.PreferredBackBufferHeight / 1.2f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 1.2f), 3f),
                    },
                    new List<Item>
                    {
                         new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 5f))
                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 1.4f, _graphics.PreferredBackBufferHeight / 1.4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 10
            listLevels.Add(
                new Levels(
                    Sol[2],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                        new Spider(new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 4.3f), spiderSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 4f), 1.2f),

                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 1.9f), 3f),
                    },
                    new List<Item>
                    {

                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 4f, _graphics.PreferredBackBufferHeight / 1.3f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );


            // level 11
            listLevels.Add(
                new Levels(
                    Sol[2],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 5f), beetleSprites, listSfx),
                        new Spider(new Vector2(_graphics.PreferredBackBufferWidth / 1.25f, _graphics.PreferredBackBufferHeight / 1.3f), spiderSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 1.17f, _graphics.PreferredBackBufferHeight / 3f), 1.2f),

                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 1.6f), 3f),
                        new Decor(MiniPCI, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 1.5f), 3f),
                    },
                    new List<Item>
                    {
                         new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 6f))
                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 4f, _graphics.PreferredBackBufferHeight / 1.3f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );



            // level 12
            listLevels.Add(
                new Levels(
                    Sol[2],
                    new List<Enemy>
                    {
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 5f), cockroachSprites, listSfx),
                        new Cockroach(new Vector2(_graphics.PreferredBackBufferWidth / 1.7f, _graphics.PreferredBackBufferHeight / 1.3f), cockroachSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 2.8f, _graphics.PreferredBackBufferHeight / 4.2f), beetleSprites, listSfx),
                        new Beetle(new Vector2(_graphics.PreferredBackBufferWidth / 1.3f, _graphics.PreferredBackBufferHeight / 1.4f), beetleSprites, listSfx),
                        new Spider(new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 4.3f), spiderSprites, listSfx),
                    },
                    new List<Decor> {
                        new Decor(Processeur, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.19f), 1.2f),

                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 3.2f, _graphics.PreferredBackBufferHeight / 6f), 3f),
                        new Decor(Ram, new Vector2(_graphics.PreferredBackBufferWidth / 3f, _graphics.PreferredBackBufferHeight / 6f), 3f),
                    },
                    new List<Item>
                    {
                         new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 5f, _graphics.PreferredBackBufferHeight / 6f)),
                          new Health(healthItem, new Vector2(_graphics.PreferredBackBufferWidth / 1.2f, _graphics.PreferredBackBufferHeight / 1.2f))
                    },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 1.4f), 2.3f, listSfx[NUMTRAPDOORSFX])
                    )
                );
            // Boss Level
            listLevels.Add(
                new Levels(
                    Sol[3],
                    new List<Enemy>
                    {
                        new Butterfly(new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), butterflySprites, butterflyProjectile, _listSfx)
                    },
                    new List<Decor>
                    {

                    },
                    new List<Item> { },
                    new Trapdoor(trapdoor, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 2.3f, listSfx[NUMTRAPDOORSFX])
                )       
            );
        }
    }
}
