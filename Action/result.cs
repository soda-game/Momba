﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;


namespace Action
{
    class Result
    {
        SpriteFont spriteFont;
        Vector2 position;
        public void Load(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("Arial");
            position = new Vector2(0, 0);
        }
        public void Tesuu(int numberOfMoves,SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, numberOfMoves.ToString(), position, Color.White);
        }
    }
}
