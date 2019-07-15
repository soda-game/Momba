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
            scroll = Vector2.Zero;
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
                    scroll.X+=velocity.X;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) )
            {
                velocity.X += SPEED;
                if (position.X - scroll.X > 100)
                {
                    scroll.X += velocity.X;
                }
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

        //当たり判定
        public void Collition(int[,] mapChipNum,int chipSize)
        {
            //プレイヤーの座標(左端)を配列番号に
           int j = (int)position.X/chipSize;
           int i = (int)position.Y / chipSize;
            //プレイヤーの右端・下端を配列番号に
            int RightJ = ((int)position.X+X_SIZE) / chipSize;
            int DownI = (int)position.Y+Y_SIZE / chipSize;

            Debug.WriteLine("Chip:" + (RightJ*chipSize));
            Debug.WriteLine("Player:" + (position.X + X_SIZE));

            if (mapChipNum[i, j] == 1|| mapChipNum[i, RightJ] == 1)
            {
                if (position.X <= j*chipSize + chipSize&& position.X + X_SIZE >= RightJ * chipSize)
                {
                    Debug.WriteLine("a");
                }
                
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X-(int)scroll.X, (int)position.Y, X_SIZE, Y_SIZE), new Rectangle(64, 0, X_SIZE, Y_SIZE), Color.White);


        }
    }
}
