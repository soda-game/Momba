using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Action
{
    class Player 
    {
        Vector2 position;
        public Vector2 Postion => position;
        Vector2 velocity;
        const float SPEED = 3;
       public Vector2 scroll;

        Texture2D texture;
        const int X_SIZE = 64;
        const int Y_SIZE = 64;
        

        public Player()
        {
            position = new Vector2(200, 200);
            velocity = Vector2.Zero;

        }

        public void SetTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("block");
        }

        public void Move()
        {
            velocity = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X -= SPEED;
                if (position.X-scroll.X < 10)
                {
                    scroll.X-=velocity.X;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) )
            {
                velocity.X += SPEED;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y -= SPEED;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y += SPEED;
            }
            position += velocity;
        }

        public void Collition(Vector2 otherPos, int height)
        {
           
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Rectangle((int)position.X+(int)scroll.X, (int)position.Y, X_SIZE, Y_SIZE), new Rectangle(64, 0, X_SIZE, Y_SIZE), Color.White);


        }
    }
}
