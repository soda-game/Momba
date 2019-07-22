using System;
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
        Texture2D result;
        Vector2 resultPos;

        SpriteFont spriteFont;
        Vector2 fontPos;
        const int FONT_SIZE=5;

        public Result()
        {
            resultPos = new Vector2(0, -30);
            fontPos = new Vector2(440, 110);
        }

        public void SetText(ContentManager content)
        {
            result = content.Load<Texture2D>("result");
            spriteFont = content.Load<SpriteFont>("Arial");
        }
        public void Draw(int numberOfMoves,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(result,  resultPos, Color.White);
            spriteBatch.DrawString(spriteFont, numberOfMoves.ToString(), fontPos, Color.Black,MathHelper.ToRadians(0),Vector2.Zero,FONT_SIZE,SpriteEffects.None,0);
        }
    }
}
