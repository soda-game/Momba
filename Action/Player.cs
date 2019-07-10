using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Action
{
    class Player:Sprite
    {
        Vector2 position;
        public Vector2 Postion => position;
        Vector2 velocity;
        const float SPEED=3;
        Vector2 grabity;

        Texture2D texture;
        const int X_SIZE = 41;
        const int Y_SIZE = 64;
        public int Y_size => Y_SIZE;

       public  bool test=false;

        public Player(ContentManager content)
        {
            position = new Vector2(200, 0);
            velocity = Vector2.Zero;
            grabity = new Vector2(0, 3);

            texture = content.Load<Texture2D>("player");
        }

        public void Move()
        {
            velocity = Vector2.Zero;
            grabity = new Vector2(0,2);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X -= SPEED;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = SPEED;
            }

            if (test)
            {
                grabity=Vector2.Zero;
            }


            position += velocity + grabity;
        }

        public void  stand()
        {
            
        } 


        public override void Draw(SpriteBatch spriteBatch)
        {
            
                    spriteBatch.Draw(texture, new Rectangle((int)position.X,(int)position.Y, X_SIZE, Y_SIZE), new Rectangle(X_SIZE * 0, 0, X_SIZE, Y_SIZE), Color.White);
           

        }
    }
}
