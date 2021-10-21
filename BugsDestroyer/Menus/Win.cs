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

        private bool _isWin = false;
        private bool _winTransition = false;
        private bool _makeSound = true;
        private bool _selectionChanged = false;
        private bool _keyDown = false;
        private bool _keyUp = false;
        private bool _keyLeft = false;
        private bool _keyRight = true;
        private bool _isOnChooseName = false;
        private bool _validate = false;
        private bool _timeInTop = false;
        private bool _alreadyValidateName = false;


        private float _currentTimeMiliWin = 0f;
        private float _countDurationMiliWin = 1f;
        private int _timerMiliWin = 0;
        private float _sizeCercle = 0f;
        
        private float _currentTimeSecondText = 0f;
        private float _countDurationSecondText = 1f;
        private int _timerSecondText = 0;
        private bool _textDraw = true;

        private List<Buttons> _listButton = new List<Buttons>();
        private int _selectedButton = 0;

        private Color _notChoose = Color.White * 0.4f;
        private Color _choose = Color.White;

        private Texture2D _cercle;

        private char _letter1 = 'A';
        private char _letter2 = 'A';
        private char _letter3 = 'A';
        private int _currentLetter = 1;
        private string _name = "";

        private int _arrowX;

        private List<Score> _tabScore = new List<Score>();
        private List<HighScore> _listhighScore = new List<HighScore>();
        private bool _listScoreCreate = false;
        private int _cpt = 1;
        private int _posY;
        private int _posX;

        private const string _FILE = "highScore.xml";

        /// <summary>
        /// Récupération de tous les images don menu Win a besoin
        /// (Alec Piette)
        /// </summary>
        protected void winLoad()
        {
            _cercle = Content.Load<Texture2D>("Img/Menu/transitionWin");

            // création des boutons
            _listButton.Add(new Buttons("save your time", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 530), _notChoose));
            _listButton.Add(new Buttons("play again", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 600), _notChoose));
            _listButton.Add(new Buttons("quit", new Vector2(_graphics.PreferredBackBufferWidth / 2.25f, 670), _notChoose));

            _arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;



            // Recupération des données du fichier xml
            _posY = _graphics.PreferredBackBufferHeight / 3;
            _posX = _graphics.PreferredBackBufferWidth / 8;


            if (File.Exists(_FILE))
            {
                ListHighScore listTemp;
                XmlSerializer restore = new XmlSerializer(typeof(ListHighScore));

                using (StreamReader scorePlayer = new StreamReader(_FILE))
                {
                    // recupération de la liste highscore
                    listTemp = (ListHighScore)restore.Deserialize(scorePlayer);
                }

                // parcourir la liste recupérer pour créer les objets
                for (int i = 0; i < listTemp.listHighScore.Count; i++)
                {
                    _listhighScore.Add(new HighScore(listTemp.listHighScore[i].Name, listTemp.listHighScore[i].Score));
                }
            }

            
        }

        /// <summary>
        /// Update du menu Win
        /// (Alec Piette)
        /// </summary>
        /// <param name="game"></param>
        protected void winUpdate(GameTime game)
        {
            if (_isWin)
            {
                // si n'est pas dans le menu pour choisir son nom
                if (!_isOnChooseName)
                {
                    #region menu Win

                    #region animation 
                    // timer pour faire un effet avec un cercle qui est de plus en plus grand
                    _currentTimeMiliWin += (float)game.ElapsedGameTime.TotalMilliseconds;

                    // quand temps courent est égale à une milisec
                    if (_currentTimeMiliWin >= _countDurationMiliWin)
                    {
                        // incrémentation du timer
                        _timerMiliWin++;
                        _countDurationMiliWin -= _countDurationMiliWin;
                    }

                    // quand le timer est égal à 1 milisec et demi
                    if (_timerMiliWin >= 1.5)
                    {
                        // si la taille du cercle et deux fois supérieur que la photo
                        if (_sizeCercle >= 2f)
                        {
                            // lancement du menu "Win"
                            _winTransition = true;
                        }
                        
                        // grandi la taille du cercle
                        _sizeCercle = _sizeCercle + 0.1f;
                        _timerMiliWin = 0;
                    }

                    #endregion

                    // si la transition est finie
                    if (_winTransition)
                    {
                        #region creation du tableau des scores
                        // insertion des scores dans la liste de score
                        if (!_listScoreCreate)
                        {
                            for (int i = 0; i < _listhighScore.Count; i++)
                            {
                                if (i <= 10)
                                {
                                    _tabScore.Add(new Score(_listhighScore[i].Name, _listhighScore[i].Score, new Vector2(_posX, _posY), i + 1));
                                }

                                _posY += 60;

                                if (i >= 10)
                                {
                                    _posX -= 30;
                                }
                            }
                            _listScoreCreate = true; ;
                        }
                        #endregion

                        #region changement de séléction des boutons

                        // si la touche S ou la flèche du bas est pressée
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            // lansement du sound effect
                            if (_makeSound)
                            {
                                MenuSfx.Play();
                                _makeSound = false;
                            }
                            _keyDown = true;
                            _selectionChanged = true;
                        }
                        // si la touche S ou la flèche du bas est pressée
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            // lansement du sound effect
                            if (_makeSound)
                            {
                                MenuSfx.Play();
                                _makeSound = false;
                            }
                            _keyUp = true;
                            _selectionChanged = true;
                        }
                        else if (_selectionChanged)
                        {
                            // si la touche S ou la flèche du bas est relachée apres l'avoir pressée
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && _keyDown)
                            {
                                // changement de la séléction du bouton
                                _selectedButton = _selectedButton + 1;
                                if (_selectedButton >= _listButton.Count - 1)
                                    _selectedButton = _listButton.Count - 1;
                            }
                            // si la touche W ou la flèche du haut est relachée apres l'avoir pressée
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && _keyUp)
                            {
                                // changemnet de la séléction du bouton
                                _selectedButton = _selectedButton - 1;
                                if (_selectedButton <= 0)
                                    _selectedButton = 0;
                            }

                            _makeSound = true;

                            _selectionChanged = false;
                            _keyDown = false;
                            _keyUp = false;

                        }

                        // changement la couleur du text de tous les boutons en plus claire
                        foreach (var item in _listButton)
                        {
                            item._color = _notChoose;
                        }
                        // changement de la couleur du bouton courrent en plus foncée
                        _listButton[_selectedButton]._color = _choose;

                        #endregion

                        #region séléction du bouton

                        // quand la touch 8 (dollar sur la borne)
                        if (Keyboard.GetState().IsKeyDown(Keys.D8))
                        {
                            // action différente suivent le bouton séléctionner
                            switch (_selectedButton)
                            {
                                case 0:
                                    // verrification si le score est dans le top 
                                    if (_tabScore.Count == 10)
                                    {
                                        foreach (var item in _tabScore)
                                        {
                                            if (timerPrincipal < item.TimeScore)
                                            {
                                                _timeInTop = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _timeInTop = true;
                                    }


                                    if (_timeInTop && !_alreadyValidateName)
                                    {
                                        // affichage de la page pour choisir son nom
                                        _isOnChooseName = true;
                                        _timeInTop = false;
                                    }
                                    else
                                    {
                                        // pas la posibilite de mettre son nom
                                        _listButton[0]._color = Color.Red;
                                    }
                                    break;
                                case 1:
                                    // restart du programme
                                    _level = 0;

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

                        #endregion
                    }

                    #endregion
                }
                else // si oui
                {
                    #region choose name

                    #region animation 
                    // timer pour faire un effect sur l'écriture verte

                    _currentTimeSecondText += (float)game.ElapsedGameTime.TotalSeconds;

                    if (_currentTimeSecondText >= _countDurationSecondText)
                    {
                        _timerSecondText++;
                        _currentTimeSecondText -= _countDurationSecondText;
                    }

                    if (_timerSecondText >= 1)
                    {
                        _timerSecondText = 0;
                        _textDraw = !_textDraw;
                    }
                    #endregion

                    #region changement de séléction de lettre
                    // quand D est presser
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        _keyRight = true;
                    }
                    // si la touche est relacher apres avoir été presser
                    if (Keyboard.GetState().IsKeyUp(Keys.D) && _keyRight)
                    {
                        // changement de séléction à droite de la letter
                        _currentLetter++;
                        if (_currentLetter > 2)
                        {
                            _currentLetter = 3;
                        }

                        _keyRight = false;
                    }
                        
                    // quand A est presser
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        _keyLeft = true;
                    }
                    // si la touche A est relacher apres l'avoir pressée
                    if (Keyboard.GetState().IsKeyUp(Keys.A) && _keyLeft)
                    {
                        // cahngement de la séléction à gauche de la lettre
                        _currentLetter--;
                        if (_currentLetter < 2)
                        {
                            _currentLetter = 1;
                        }

                        _keyLeft = false;
                    }

                    // suivent la séléction de la lettre 
                    switch (_currentLetter)
                    {
                        case 1:
                            // changement de la position des flèches
                            _arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;
                            // appel de la fonction changeLetter avec letter1 en paramettre
                            _letter1 = changeLetter(_letter1);

                            break;
                        case 2:
                            // changement de la position des flèches
                            _arrowX = _graphics.PreferredBackBufferWidth / 2;
                            // appel de la fonction changeLetter avec letter2 en paramettre
                            _letter2 = changeLetter(_letter2);
                            break;
                        case 3:
                            // changement de la position des flèches
                            _arrowX = _graphics.PreferredBackBufferWidth / 2 + 200;
                            // appel de la fonction changeLetter avec letter3 en paramettre
                            _letter3 = changeLetter(_letter3);
                            break;
                    }
                    #endregion

                    #region validation du pseudo

                    // si touche G est presser 
                    if (Keyboard.GetState().IsKeyDown(Keys.G))
                    {
                        _validate = true;
                    }

                    // si la touche G est relacher apres avoir été presser
                    if (Keyboard.GetState().IsKeyUp(Keys.G) && _validate)
                    {
                        // stockage du pseudo
                        _name = Char.ToString(_letter1) + Char.ToString(_letter2) + Char.ToString(_letter3);
                        _validate = false;

                        // enregistrement des scores
                        bool breakAdd = true;

                        // actualisation de la liste de meilleur score
                        if (_tabScore.Count <= 10)
                        {
                            if (_tabScore.Count == 0)
                            {
                                // si la liste est vide ajout direct du score du joueur
                                _listhighScore.Add(new HighScore(_name, Math.Round(timerPrincipal, 2)));
                            }
                            else
                            {
                                List<HighScore> listTemp = new List<HighScore>();
                                _cpt = 1;
                                
                                // parcourir la liste pour verrifier l'emplacement du score du joueur
                                foreach (var item in _tabScore)
                                {
                                    if (_cpt <= 10)
                                    {
                                        // si le score du joueur est mieu que l'item
                                        if (timerPrincipal < item.TimeScore && breakAdd)
                                        {
                                            // ajout de score du joueur 
                                            listTemp.Add(new HighScore(_name, Math.Round(timerPrincipal, 2)));
                                            // si la liste est plus petite que 10 pour eviter de mettre plus de  10 score dans la liste
                                            if (_cpt < 10)
                                            {
                                                // ajout du score de l'item
                                                listTemp.Add(new HighScore(item.Name, item.TimeScore));
                                            }
                                            breakAdd = false;
                                        }
                                        // sinon si la liste est plus petite que 10 et que le score du joueur est plus grand que le dernier score
                                        else if (timerPrincipal > _tabScore[_tabScore.Count - 1].TimeScore && _cpt == _tabScore.Count)
                                        {
                                            // ajout en permier du dernier score
                                            listTemp.Add(new HighScore(item.Name, item.TimeScore));
                                            // ajout du score du joueur
                                            listTemp.Add(new HighScore(_name, Math.Round(timerPrincipal, 2)));
                                        }
                                        else
                                        {
                                            // ajout de l'item
                                            listTemp.Add(new HighScore(item.Name, item.TimeScore));
                                        }

                                        _cpt++;
                                    }
                                }
                                // acctualisation de la liste "listhighScore" avec la liste temporaire
                                _listhighScore.Clear();
                                _listhighScore = listTemp;
                            }
                        }

                        _posY = _graphics.PreferredBackBufferHeight / 3;
                        _posX = _graphics.PreferredBackBufferWidth / 8;
                        _tabScore.Clear();

                        // acctualisation de la liste "tabScore" avec les valeurs de la liste des meilleurs score en donnant une position
                        for (int i = 0; i < _listhighScore.Count; i++)
                        {
                            if (i <= 10)
                            {
                                _tabScore.Add(new Score(_listhighScore[i].Name, _listhighScore[i].Score, new Vector2(_posX, _posY), i + 1));
                            }

                            _posY += 60;

                            if (i >= 10)
                            {
                                _posX -= 30;
                            }
                        }


                        // enregistrement de la liste de score
                        XmlSerializer sauver = new XmlSerializer(typeof(ListHighScore));
                        using (StreamWriter f = new StreamWriter(_FILE))
                        {
                            sauver.Serialize(f, new ListHighScore(_listhighScore));
                        }

                        // retour dans le menu Win
                        _alreadyValidateName = true;
                        _isOnChooseName = false;
                    }

                    #endregion

                    Char changeLetter(char letter)
                    { 
                        // si la touche S ou la flèche du bas est pressée
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            // lancement du sound effect
                            if (_makeSound)
                            {
                                MenuSfx.Play();
                                _makeSound = false;
                            }
                            _keyDown = true;
                            _selectionChanged = true;
                        }
                        // sinon si la touche W ou flèche du haut est pressée
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            // lancement du sound effect
                            if (_makeSound)
                            {
                                MenuSfx.Play();
                                _makeSound = false;
                            }
                            _keyUp = true;
                            _selectionChanged = true;
                        }
                        else if (_selectionChanged)
                        {
                            // si la touche S ou flèche du haut est relachée
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && _keyDown)
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
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && _keyUp)
                            {
                                // décrémentation de la lettre 
                                int letterInt = Convert.ToInt32(letter);
                                letter = Convert.ToChar(letterInt -= 1);

                                if (letter < 'A')
                                {
                                    letter = 'Z';
                                }
                            }

                            _makeSound = true;

                            _selectionChanged = false;
                            _keyDown = false;
                            _keyUp = false;

                        }

                        // retoune la lettre 
                        return letter;
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Affichage du menu Win
        /// (Alec Piette)
        /// </summary>
        protected void winDraw()
        {
            if (_isWin)
            {
                // background + background animation
                _spriteBatch.Draw(_cercle, _listLevels[_level].trapdoor.position, null, Color.Black, 0f, new Vector2(_cercle.Width / 2, _cercle.Height / 2), _sizeCercle, SpriteEffects.None, 0f);

                if (_winTransition)
                {
                    if (!_isOnChooseName)
                    {
                        #region Menu Win
                        // Titre
                        _spriteBatch.DrawString(_font, "you win", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(_font.MeasureString("you win").X / 2, _font.MeasureString("you win").Y / 2), 2f, SpriteEffects.None, 0f);

                        // score
                        _spriteBatch.DrawString(_font, "your score : " + Convert.ToString(Math.Round(timerPrincipal, 2).ToString()), new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 1.16f), Color.DarkOrange, 0f, new Vector2(_font.MeasureString("your score : ").X / 2, _font.MeasureString("your score : ").Y / 2), 0.5f, SpriteEffects.None, 0f);

                        // tableau des scores
                        foreach (var item in _tabScore)
                        {
                            item.Draw(_spriteBatch, _font);
                        }

                        // affichage des boutons
                        foreach (var item in _listButton)
                        {
                            _spriteBatch.DrawString(_font, item._text, item._position, item._color, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        #endregion
                    }
                    else
                    {
                        #region Menu choix du nom
                        // Titre
                        _spriteBatch.DrawString(_font, "enter your name", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 7.25f), Color.White, 0f, new Vector2(_font.MeasureString("enter your name").X / 2, _font.MeasureString("enter your name").Y / 2), 2f, SpriteEffects.None, 0f);

                        // les flèches
                        _spriteBatch.Draw(_menuImages[1], new Vector2(_arrowX, 480), null, Color.White * 0.7f, (float)Math.PI * 3 / 2, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                        _spriteBatch.Draw(_menuImages[2], new Vector2(_arrowX, 680), null, Color.White * 0.7f, (float)Math.PI * 3 / 2, Vector2.Zero, 4f, SpriteEffects.None, 0f);

                        // pseudo
                        _spriteBatch.DrawString(_font, Convert.ToString(_letter1), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(_font, Convert.ToString(_letter2), new Vector2(_graphics.PreferredBackBufferWidth / 2, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(_font, Convert.ToString(_letter3), new Vector2(_graphics.PreferredBackBufferWidth / 2 + 200, 505), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

                        // info pour valider
                        if (_textDraw)
                        {
                            _spriteBatch.DrawString(_font, "validate your name with button interact", new Vector2(_graphics.PreferredBackBufferWidth / 2 + 20 , 770), Color.Lime * 0.8f, 0f, new Vector2(_font.MeasureString("validate your name with buttpn top left").X / 2, _font.MeasureString("validate your name with buttpn top left").Y / 2), 0.4f, SpriteEffects.None, 0f);
                        }
                        
                        _spriteBatch.Draw(_menuImages[4], new Vector2(_graphics.PreferredBackBufferWidth / 1.15f - 120, 860), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(_font, "interact", new Vector2(_graphics.PreferredBackBufferWidth / 1.15f - 10, 845), Color.White, 0f, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0f);

                        #endregion
                    }
                }
            }
        }
    }
}
