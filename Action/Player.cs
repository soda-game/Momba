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

        //フラグ
        bool CollitionL; //0=当たってない 1=左 2=右　3=上 4=下 enum
        bool CollitionR;
        bool CollitionU;
        bool CollitionD;
        public Player()
        {
            position = new Vector2(64, 68);
            velocity = Vector2.Zero;
            scroll = Vector2.Zero;
            CollitionL = false;
            CollitionR = false;
            CollitionU = false;
            CollitionD = false;
        }

        public void SetTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("block");
        }

        public void Move()
        {


            if (Keyboard.GetState().IsKeyDown(Keys.A) && !CollitionL)
            {

                CollitionU = false;
                CollitionD = false;
                CollitionL = false;
                CollitionR = false;
                velocity.X = -SPEED;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !CollitionR)
            {
                CollitionU = false;
                CollitionD = false;
                CollitionL = false;
                CollitionR = false;
                velocity.X = +SPEED;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !CollitionU)
            {
                CollitionU = false;
                CollitionD = false;
                CollitionR = false;
                CollitionL = false;
                velocity.Y = -SPEED;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !CollitionD)
            {
                CollitionU = false;
                CollitionD = false;
                CollitionR = false;
                CollitionL = false;
                velocity.Y = +SPEED;
            }

            //フラグが一つでもtrueなら止める
            if (!CollitionL && !CollitionR && !CollitionD && !CollitionU)
            {
                position += velocity;
                if (position.X - scroll.X > SCROLL_RIGHT)
                {
                    scroll.X += velocity.X;
                }
                if (position.X - scroll.X < 10)
                {
                    scroll.X += velocity.X;
                }
            }
            else
            {
                velocity = Vector2.Zero;
            }



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
            int midleX = ((int)position.X + 32) / mapChipSize;
            int midleY = ((int)position.Y + 32) / mapChipSize;

            //プレイヤーの右が当たったら
            if ((mapChipNum[midleY, rightX] == 1) && (position.X + X_SIZE > rightX * mapChipSize))
            {
                CollitionR = true;
            }
            //プレイヤーの左が当たったら
            else if ((mapChipNum[midleY, leftX] == 1 ) && (position.X < leftX * mapChipSize + mapChipSize))
            {
                CollitionL = true;
            }
            else
            {
                CollitionL = false;
                CollitionR = false;

            }

            //プレイヤーの下が当たったら

            if (mapChipNum[downY, midleX] == 1 && position.Y + Y_SIZE >= downY * mapChipSize)
            {
                CollitionD = true;
            }
            //プレイヤーの上が当たったら
            else if (mapChipNum[upY, midleX] == 1 && position.Y <= upY * mapChipSize + mapChipSize)
            {

                CollitionU = true;
            }
            else
            {
                CollitionU = false;
                CollitionD = false;

            }

            Debug.WriteLine("R" + CollitionR);
            Debug.WriteLine("L" + CollitionL);
            Debug.WriteLine("D" + CollitionD);
            Debug.WriteLine("U" + CollitionU);
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)scroll.X, (int)position.Y, X_SIZE, Y_SIZE), new Rectangle(64, 0, X_SIZE, Y_SIZE), Color.White);


        }
    }
}
