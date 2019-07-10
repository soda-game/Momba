﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Action
{
    abstract class Sprite
    {
        
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
