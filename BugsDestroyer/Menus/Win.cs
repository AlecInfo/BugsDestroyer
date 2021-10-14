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


        private float currentTimeMiliWin = 0f;
        private float countDurationMiliWin = 1f;
        private int timerMiliWin = 0;
        private float sizeCercle = 0f;

        private List<Buttons> listButton = new List<Buttons>();
        private int _selectedButton = 0;

        private Color notChoose = Color.Black * 0.4f;

        private Texture2D _cercle;
        private static Texture2D _boxScore;

        private char letter1 = 'A';
        private char letter2 = 'A';
        private char letter3 = 'A';
        private int currentLetter = 1;

        private int arrowX;


        protected void winLoad()
        {
            _cercle = Content.Load<Texture2D>("Img/Menu/transitionWin");

            _boxScore = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _boxScore.SetData(new[] { Color.White });

            listButton.Add(new Buttons("save your time", new Vector2(900, 530), notChoose));
            listButton.Add(new Buttons("play again", new Vector2(900, 600), notChoose));
            listButton.Add(new Buttons("quit", new Vector2(900, 670), notChoose));

            arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;
        }

        protected void winUpdate(GameTime game)
        {
            if (isWin)
            {
                if (!isOnChooseName)
                {
                    #region menu Win

                    // timer pour faire un effet avec un cercle qui est de plus en plus grand
                    currentTimeMiliWin += (float)game.ElapsedGameTime.TotalMilliseconds;

                    if (currentTimeMiliWin >= countDurationMiliWin)
                    {
                        timerMiliWin++;
                        countDurationMiliWin -= countDurationMiliWin;
                    }

                    if (timerMiliWin >= 1.5)
                    {
                        isOpacityDark = true;

                        if (sizeCercle >= 2f)
                        {
                            winTransition = true;
                        }

                        sizeCercle = sizeCercle + 0.1f;
                        timerMiliWin = 0;
                    }


                    if (winTransition)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyDown = true;
                            selectionChanged = true;
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
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
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && keyDown)
                            {
                                _selectedButton = _selectedButton + 1;
                                if (_selectedButton >= listButton.Count - 1)
                                    _selectedButton = listButton.Count - 1;
                            }
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && keyUp)
                            {
                                _selectedButton = _selectedButton - 1;
                                if (_selectedButton <= 0)
                                    _selectedButton = 0;
                            }

                            makeSound = true;

                            selectionChanged = false;
                            keyDown = false;
                            keyUp = false;

                        }

                        foreach (var item in listButton)
                        {
                            item._color = Color.Black * 0.4f;
                        }
                        listButton[_selectedButton]._color = Color.Black;

                        if (Keyboard.GetState().IsKeyDown(Keys.D8))
                        {
                            switch (_selectedButton)
                            {
                                case 0:
                                    isOnChooseName = true;
                                    break;
                                case 1:
                                    level = 0;

                                    StartSfx.Play();
                                    this.Exit();
                                    Game1 game1 = new Game1();
                                    game1.Run();
                                    break;
                                case 2:
                                    StartSfx.Play();
                                    this.Exit();
                                    break;
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    //currentLetter = changeChar(currentLetter);

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        keyRight = true;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.D) && keyRight)
                    {
                        currentLetter++;
                        if (currentLetter > 2)
                        {
                            currentLetter = 3;
                        }

                        keyRight = false;
                    }
                        
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        keyLeft = true;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.A) && keyLeft)
                    {
                        currentLetter--;
                        if (currentLetter < 2)
                        {
                            currentLetter = 1;
                        }

                        keyLeft = false;
                    }

                    switch (currentLetter)
                    {
                        case 1:
                            arrowX = _graphics.PreferredBackBufferWidth / 2 - 200;
                            letter1 = changeLetter(letter1);

                            break;
                        case 2:
                            arrowX = _graphics.PreferredBackBufferWidth / 2;
                            letter2 = changeLetter(letter2);
                            break;
                        case 3:
                            arrowX = _graphics.PreferredBackBufferWidth / 2 + 200;
                            letter3 = changeLetter(letter3);
                            break;
                    }


                    Char changeLetter(char letter)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            if (makeSound)
                            {
                                MenuSfx.Play();
                                makeSound = false;
                            }
                            keyDown = true;
                            selectionChanged = true;
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
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
                            if ((Keyboard.GetState().IsKeyUp(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.Down)) && keyDown)
                            {
                                int letterInt = Convert.ToInt32(letter);
                                letter = Convert.ToChar(letterInt += 1);


                                if (letter > 'Z')
                                {
                                    letter = 'A';
                                }
                            }
                            else if ((Keyboard.GetState().IsKeyUp(Keys.W) || Keyboard.GetState().IsKeyUp(Keys.Up)) && keyUp)
                            {
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

                        return letter;
                    }
                }
            }
        }

        protected void winDraw()
        {
            if (isWin)
            {
                _spriteBatch.Draw(_cercle, listLevels[level]._trapdoor._position, null, Color.White, 0f, new Vector2(_cercle.Width / 2, _cercle.Height / 2), sizeCercle, SpriteEffects.None, 0f);

                if (winTransition)
                {
                    if (!isOnChooseName)
                    {
                        // Titre
                        _spriteBatch.DrawString(font, "you win", new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), Color.Black * 0.8f, 0f, new Vector2(font.MeasureString("you win").X / 2, font.MeasureString("you win").Y / 2), 2f, SpriteEffects.None, 0f);

                        // border liste de score
                        _spriteBatch.Draw(_boxScore, new Rectangle(430, 350, 350, 650), Color.Black * 0.6f);

                        // affichage des boutons
                        foreach (var item in listButton)
                        {
                            _spriteBatch.DrawString(font, item._text, item._position, item._color, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        // Titre
                        _spriteBatch.DrawString(font, "enter your name", new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), Color.Black * 0.8f, 0f, new Vector2(font.MeasureString("enter your name").X / 2, font.MeasureString("enter your name").Y / 2), 2f, SpriteEffects.None, 0f);

                        // les flèches
                        _spriteBatch.Draw(_menuImages[1], new Vector2(arrowX, 500), null, Color.Black, (float)Math.PI * 3 / 2, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                        _spriteBatch.Draw(_menuImages[2], new Vector2(arrowX, 700), null, Color.Black, (float)Math.PI * 3/2, Vector2.Zero, 4f, SpriteEffects.None, 0f);

                        //
                        _spriteBatch.DrawString(font, Convert.ToString(letter1), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, 525), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(font, Convert.ToString(letter2), new Vector2(_graphics.PreferredBackBufferWidth / 2, 525), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(font, Convert.ToString(letter3), new Vector2(_graphics.PreferredBackBufferWidth / 2 + 200, 525), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);


                        _spriteBatch.DrawString(font, "enter your name", new Vector2(_graphics.PreferredBackBufferWidth / 2, 200), Color.Black * 0.8f, 0f, new Vector2(font.MeasureString("enter your name").X / 2, font.MeasureString("enter your name").Y / 2), 2f, SpriteEffects.None, 0f);

                    }
                }
            }
        }
    }
}
