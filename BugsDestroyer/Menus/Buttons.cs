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
    public class Buttons
    {
        public string _text;
        public Vector2 _position;
        public Color _color;
        public Buttons(string text, Vector2 position, Color color)
        {
            this._text = text;
            this._position = position;
            this._color = color;
        }
    }
}
