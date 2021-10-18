using BugsDestroyer.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BugsDestroyer
{
    public partial class Game1 : Game
    {
        // varriable

        private bool isWin = false;
        private bool winTransition = false;
        private bool makeSound = true;
        private bool selectionChanged = false;
        private bool keyDown = false;
        private bool keyUp = false;
        private bool keyLeft = false;
        private bool keyRight = true;
        private bool isOnChooseName = false;
        private bool validate = false;


        private float currentTimeMiliWin = 0f;
        private float countDurationMiliWin = 1f;
        private int timerMiliWin = 0;
        private float sizeCercle = 0f;
        
        private float currentTimeSecondText = 0f;
        private float countDurationSecondText = 1f;
        private int timerSecondText = 0;
        private bool textDraw = true;

        private List<Buttons> listButton = new List<Buttons>();
        private int _selectedButton = 0;

        private Color notChoose = Color.White * 0.4f;
        private Color choose = Color.White;

        private Texture2D _cercle;

        private char letter1 = 'A';
        private char letter2 = 'A';
        private char letter3 = 'A';
        private int currentLetter = 1;
        private string name = "";

        private int arrowX;

        private List<Score> tabScore = new List<Score>();
        private Dictionary<double, string> tabScoreTemp = new Dictionary<double, string>();
        HighScore highScore;
        private int cpt = 1;
        private int posY;
        private int posX;

        private const string FILE = "highScore.xml";


        protected void winLoad()
        {
            _cercle = Content.Load<Texture2D>("Img/Menu/transitionWin");

            // création des boutons
            listButton.Add(new Buttons("save your time", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 530), notChoose));
            listButton.Add(new Buttons("play again", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 600), notChoose));
            listButton.Add(new Buttons("quit", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 670), notChoose));

            arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;



            // ajout des scores dans le dico
            posY = _graphics.PreferredBackBufferHeight / 3;
            posX = _graphics.PreferredBackBufferWidth / 8;

            //tabScoreTemp.Add(30.22, "ptt");
            //tabScoreTemp.Add(38.37, "mar");
            //tabScoreTemp.Add(39.21, "jrm");
            //tabScoreTemp.Add(41.43, "and");
            //tabScoreTemp.Add(48.19, "cau");
            //tabScoreTemp.Add(49.03, "zip");
            //tabScoreTemp.Add(49.59, "bit");
            //tabScoreTemp.Add(59.98, "css");
            //tabScoreTemp.Add(60.42, "php");
            //tabScoreTemp.Add(67.74, "xml");


            if (File.Exists(FILE))
            {
                List<HighScore> listTemp = new List<HighScore>();
                XmlSerializer restore = new XmlSerializer(typeof(HighScore));

                using (StreamReader scorePlayer = new StreamReader(FILE))
                {
                    while ((HighScore)restore.Deserialize(scorePlayer) = !null)
                    {

                    }
                }
            }

            
        }

        protected void winUpdate(GameTime game)
        {
            if (isWin)
            {
                // si n'est pas dans le menu pour choisir son nom
                if (!isOnChooseName)
                {
                    #region menu Win

                    // timer pour faire un effet avec un cercle qui est de plus en plus grand
                    currentTimeMiliWin += (float)game.ElapsedGameTime.TotalMilliseconds;

                    // quand temps courent est égale à une milisec
                    if (currentTimeMiliWin >= countDurationMiliWin)
                    {
                        // incrémentation du timer
                        timerMiliWin++;
                        countDurationMiliWin -= countDurationMiliWin;
                    }

                    // quand le timer est égal à 1 milisec et demi
                    if (timerMiliWin >= 1.5)
                    {
                        // si la taille du cercle et deux fois supérieur que la photo
                        if (sizeCercle >= 2f)
                        {
                            // lancement du menu "Win"
                            winTransition = true;
                        }
                        
                        // grandi la taille du cercle
                        sizeCercle = sizeCercle + 0.1f;
                        timerMiliWin = 0;
                    }


                    // si la transition est finie
                    if (winTransition)
                    {
                        // insertion des scores dans la liste de score
                        foreach (var item in tabScoreTemp)
                        {
                            if (cpt <= 10)
                            {
                                tabScore.Add(new Score(item.Value, item.Key, new Vector2(posX, posY), cpt));
                            }

                            posY += 60;

                            cpt++;
                            if (cpt >= 10)
                            {
                                posX -= 30;
                            }
                        }

                        // si la touche S ou la flèche du bas est pressée
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            // lansement du sound effect
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyDown = true;
                            selectionChanged = true;
                        }
                        // si la touche S ou la flèche du bas est pressée
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            // lansement du sound effect
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyUp = true;
                            selectionChanged = true;
                        }
                        else if (selectionChanged)
                        {
                            // si la touche S ou la flèche du bas est relachée apres l'avoir pressée
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && keyDown)
                            {
                                // changement de la séléction du bouton
                                _selectedButton = _selectedButton + 1;
                                if (_selectedButton >= listButton.Count - 1)
                                    _selectedButton = listButton.Count - 1;
                            }
                            // si la touche W ou la flèche du haut est relachée apres l'avoir pressée
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && keyUp)
                            {
                                // changemnet de la séléction du bouton
                                _selectedButton = _selectedButton - 1;
                                if (_selectedButton <= 0)
                                    _selectedButton = 0;
                            }

                            makeSound = true;

                            selectionChanged = false;
                            keyDown = false;
                            keyUp = false;

                        }

                        // changement la couleur du text de tous les boutons en plus claire
                        foreach (var item in listButton)
                        {
                            item._color = notChoose;
                        }
                        // changement de la couleur du bouton courrent en plus foncée
                        listButton[_selectedButton]._color = choose;

                        // quand la touch 8 (dollar sur la borne)
                        if (Keyboard.GetState().IsKeyDown(Keys.D8))
                        {
                            // action différente suivent le bouton séléctionner
                            switch (_selectedButton)
                            {
                                case 0:
                                    // affichage de la page pour choisir son nom
                                    isOnChooseName = true;
                                    break;
                                case 1:
                                    // restart du programme
                                    level = 0;

                                    StartSfx.Play();
                                    this.Exit();
                                    Game1 game1 = new Game1();
                                    game1.Run();
                                    break;
                                case 2:
                                    // quitter le programme
                                    StartSfx.Play();
                                    this.Exit();
                                    break;
                            }
                        }
                    }

                    #endregion
                }
                else // si oui
                {
                    #region choose name

                    // timer pour faire un effect sur l'écriture verte

                    currentTimeSecondText += (float)game.ElapsedGameTime.TotalSeconds;

                    if (currentTimeSecondText >= countDurationSecondText)
                    {
                        timerSecondText++;
                        currentTimeSecondText -= countDurationSecondText;
                    }

                    if (timerSecondText >= 1)
                    {
                        timerSecondText = 0;
                        textDraw = !textDraw;
                    }

                    // quand D est presser
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        keyRight = true;
                    }
                    // si la touche est relacher apres avoir été presser
                    if (Keyboard.GetState().IsKeyUp(Keys.D) && keyRight)
                    {
                        // changement de séléction à droite de la letter
                        currentLetter++;
                        if (currentLetter > 2)
                        {
                            currentLetter = 3;
                        }

                        keyRight = false;
                    }
                        
                    // quand A est presser
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        keyLeft = true;
                    }
                    // si la touche A est relacher apres l'avoir pressée
                    if (Keyboard.GetState().IsKeyUp(Keys.A) && keyLeft)
                    {
                        // cahngement de la séléction à gauche de la lettre
                        currentLetter--;
                        if (currentLetter < 2)
                        {
                            currentLetter = 1;
                        }

                        keyLeft = false;
                    }

                    // suivent la séléction de la lettre 
                    switch (currentLetter)
                    {
                        case 1:
                            // changement de la position des flèches
                            arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;
                            // appel de la fonction changeLetter avec letter1 en paramettre
                            letter1 = changeLetter(letter1);

                            break;
                        case 2:
                            // changement de la position des flèches
                            arrowX = _graphics.PreferredBackBufferWidth / 2;
                            // appel de la fonction changeLetter avec letter2 en paramettre
                            letter2 = changeLetter(letter2);
                            break;
                        case 3:
                            // changement de la position des flèches
                            arrowX = _graphics.PreferredBackBufferWidth / 2 + 200;
                            // appel de la fonction changeLetter avec letter3 en paramettre
                            letter3 = changeLetter(letter3);
                            break;
                    }

                    // si touche G est presser 
                    if (Keyboard.GetState().IsKeyDown(Keys.G))
                    {
                        validate = true;
                    }

                    // si la touche G est relacher apres avoir été presser
                    if (Keyboard.GetState().IsKeyUp(Keys.G) && validate)
                    {
                        // stockage du pseudo
                        name = Char.ToString(letter1) + Char.ToString(letter2) + Char.ToString(letter3);
                        validate = false;

                        // enregistrement des scores
                        List<HighScore> listTemp = new List<HighScore>();
                        bool breakAdd = true;

                        if (tabScore.Count == 10)
                        {
                            cpt = 1;
                            foreach (var item in tabScore)
                            {
                                if (cpt <= 10)
                                {
                                    if (_timerPrincipale < item.TimeScore && breakAdd)
                                    {
                                        listTemp.Add(new HighScore(name, Math.Round(_timerPrincipale, 2)));
                                        breakAdd = false;
                                    }
                                    else
                                    {
                                        listTemp.Add(new HighScore(item.Name, item.TimeScore));
                                    }
                                    cpt++;
                                }
                            }
                        }
                        else if (tabScore.Count < 10)
                        {
                            cpt = 1;
                            foreach (var item in tabScore)
                            {
                                if (cpt <= 10)
                                {
                                    if (_timerPrincipale < item.TimeScore && breakAdd)
                                    {
                                        listTemp.Add(new HighScore(name, Math.Round(_timerPrincipale, 2)));
                                        breakAdd = false;
                                    }
                                    else
                                    {
                                        listTemp.Add(new HighScore(item.Name, item.TimeScore));
                                    }
                                    cpt++;
                                }
                            }
                        }


                        XmlSerializer sauver = new XmlSerializer(typeof(HighScore));
                        using (StreamWriter f = new StreamWriter(FILE))
                        {
                            for (int i = 0; i < listTemp.Count; i++)
                            {
                                sauver.Serialize(f, listTemp[i]);
                            }
                        }

                        // retour dans le menu Win
                        isOnChooseName = false;
                    }

                    Char changeLetter(char letter)
                    { 
                        // si la touche S ou la flèche du bas est pressée
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            // lancement du sound effect
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyDown = true;
                            selectionChanged = true;
                        }
                        // sinon si la touche W ou flèche du haut est pressée
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            // lancement du sound effect
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyUp = true;
                            selectionChanged = true;
                        }
                        else if (selectionChanged)
                        {
                            // si la touche S ou flèche du haut est relachée
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && keyDown)
                            {
                                // incrémentation de la lettre 
                                int letterInt = Convert.ToInt32(letter);
                                letter = Convert.ToChar(letterInt += 1);

                                
                                if (letter > 'Z')
                                {
                                    letter = 'A';
                                }
                            }
                            // si la touche W ou flèche du bas est relachée
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && keyUp)
                            {
                                // décrémentation de la lettre 
                                int letterInt = Convert.ToInt32(letter);
                                letter = Convert.ToChar(letterInt -= 1);

                                if (letter < 'A')
                                {
                                    letter = 'Z';
                                }
                            }

                            makeSound = true;

                            selectionChanged = false;
                            keyDown = false;
                            keyUp = false;

                        }

                        // retoune la lettre 
                        return letter;
                    }

                    #endregion
                }
            }
        }

        protected void winDraw()
        {
            if (isWin)
            {
                // background + background animation
                _spriteBatch.Draw(_cercle, listLevels[level]._trapdoor._position, null, Color.Black, 0f, new Vector2(_cercle.Width / 2, _cercle.Height / 2), sizeCercle, SpriteEffects.None, 0f);

                if (winTransition)
                {
                    if (!isOnChooseName)
                    {
                        #region Menu Win
                        // Titre
                        _spriteBatch.DrawString(font, "you win", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(font.MeasureString("you win").X / 2, font.MeasureString("you win").Y / 2), 2f, SpriteEffects.None, 0f);

                        // score
                        _spriteBatch.DrawString(font, "your score : " + Convert.ToString(Math.Round(_timerPrincipale, 2).ToString()), new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 1.16f), Color.DarkOrange, 0f, new Vector2(font.MeasureString("your score : ").X / 2, font.MeasureString("your score : ").Y / 2), 0.5f, SpriteEffects.None, 0f);

                        // tableau des scores
                        foreach (var item in tabScore)
                        {
                            item.Draw(_spriteBatch, font);
                        }

                        // affichage des boutons
                        foreach (var item in listButton)
                        {
                            _spriteBatch.DrawString(font, item._text, item._position, item._color, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        #endregion
                    }
                    else
                    {
                        #region Menu choix du nom
                        // Titre
                        _spriteBatch.DrawString(font, "enter your name", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(font.MeasureString("enter your name").X / 2, font.MeasureString("enter your name").Y / 2), 2f, SpriteEffects.None, 0f);

                        // les flèches
                        _spriteBatch.Draw(_menuImages[1], new Vector2(arrowX, 480), null, Color.White * 0.7f, (float)Math.PI * 3 / 2, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                        _spriteBatch.Draw(_menuImages[2], new Vector2(arrowX, 680), null, Color.White * 0.7f, (float)Math.PI * 3 / 2, Vector2.Zero, 4f, SpriteEffects.None, 0f);

                        // pseudo
                        _spriteBatch.DrawString(font, Convert.ToString(letter1), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(font, Convert.ToString(letter2), new Vector2(_graphics.PreferredBackBufferWidth / 2, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(font, Convert.ToString(letter3), new Vector2(_graphics.PreferredBackBufferWidth / 2 + 200, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

                        // info pour valider
                        if (textDraw)
                        {
                            _spriteBatch.DrawString(font, "validate your name with button interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 20 , 770), Color.Lime * 0.8f, 0f, new Vector2(font.MeasureString("validate your name with buttpn top left").X / 2, font.MeasureString("validate your name with buttpn top left").Y / 2), 0.4f, SpriteEffects.None, 0f);
                        }
                        
                        _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 1.15f - 120, 860), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 1.15f - 10, 845), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);

                        #endregion
                    }
                }
            }
        }
    }
}
