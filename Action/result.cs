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
        Texture2D tesuu;
        SpriteFont spriteFont;
        Vector2 position;
        const int s = 30;

        public Result()
        {
            position = new Vector2(0, 0);
        }

        public void SetText(ContentManager content)
        {
            tesuu = content.Load<Texture2D>("tesuu");
            spriteFont = content.Load<SpriteFont>("Arial");
        }
        public void Draw(int numberOfMoves,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tesuu,  position, Color.Black);
            spriteBatch.DrawString(spriteFont, numberOfMoves.ToString(), new Vector2(position.X+300, position.Y), Color.Black,MathHelper.ToRadians(0),Vector2.Zero,5,SpriteEffects.None,0);
        }
    }
}
