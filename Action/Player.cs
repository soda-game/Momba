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
        //移動
        Vector2 position;
        public Vector2 Postion => position;
        Vector2 velocity;
        const float SPEED = 3;

        public Vector2 scroll;
        const int SCROLL_RIGHT = 500;
        const int SCROLL_LEFT = 100;

        //テクスチャ
        Texture2D texture;
        const int X_SIZE = 64;
        const int Y_SIZE = 64;

        public Player()
        {
            position = new Vector2(64, 68);
            velocity = Vector2.Zero;
            scroll = Vector2.Zero;
  
        }

        public void SetTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("block");
        }

        public void Move()
        {


            if (Keyboard.GetState().IsKeyDown(Keys.A) )
            {

                velocity.X = -SPEED;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) )
            {

                velocity.X = +SPEED;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {

                velocity.Y = -SPEED;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) )
            {

                velocity.Y = +SPEED;
            }

               position += velocity;
            //    if (position.X - scroll.X > SCROLL_RIGHT)
            //    {
            //        scroll.X += velocity.X;
            //    }
            //    if (position.X - scroll.X < 10)
            //    {
            //        scroll.X += velocity.X;
            //    }
            
               velocity = Vector2.Zero;
           



        }

        //当たり判定
        public void Collition(int[,] mapChipNum, int mapChipSize)
        {
            //プレイヤーの座標(左端)を配列番号に
            int leftX = (int)position.X / mapChipSize;
            int upY = (int)position.Y / mapChipSize;
            //プレイヤーの右端・下端を配列番号に
            int rightX = ((int)position.X + X_SIZE) / mapChipSize;
            int downY = ((int)position.Y + Y_SIZE) / mapChipSize;
            //プレイヤーの中心を配列番号に
            int middleX = ((int)position.X + 32) / mapChipSize;
            int middleY = ((int)position.Y + 32) / mapChipSize;

            //プレイヤーの右が当たったら
            if ((mapChipNum[middleY, rightX] == 1) && (position.X + X_SIZE > rightX * mapChipSize))
            {

                FixPosiiton(new Vector2(rightX * mapChipSize - X_SIZE, position.Y));
            }
            //プレイヤーの左が当たったら
            else if ((mapChipNum[middleY, leftX] == 1) && (position.X < leftX * mapChipSize + mapChipSize))
            {
                FixPosiiton(new Vector2(leftX * mapChipSize + mapChipSize, position.Y));

            }
            else
            {


            }

            //プレイヤーの下が当たったら

            if (mapChipNum[downY, middleX] == 1 && position.Y + Y_SIZE >= downY * mapChipSize)
            {
                FixPosiiton(new Vector2(position.X, downY * mapChipSize - Y_SIZE));

            }
            //プレイヤーの上が当たったら
            else if (mapChipNum[upY, middleX] == 1 && position.Y <= upY * mapChipSize + mapChipSize)
            {
                FixPosiiton(new Vector2(position.X, upY * mapChipSize + mapChipSize));

            }
            else
            {

            }
        }

        public void FixPosiiton(Vector2 fixPos)
        {
            position = fixPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)scroll.X, (int)position.Y, X_SIZE, Y_SIZE), new Rectangle(64, 0, X_SIZE, Y_SIZE), Color.White);


        }
    }
}
