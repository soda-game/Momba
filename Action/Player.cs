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
        const float SPEED = 7;

        public Vector2 scroll;
        const int SCROLL_RIGHT = 500;
        const int SCROLL_LEFT = 100;

        //テクスチャ
        Texture2D texture;
        const int WIDTH = 64;
        const int HEIGHT = 64;

        //フラグ
        bool nowMove;

        public Player()
        {
            position = new Vector2(64, 68);
            velocity = Vector2.Zero;
            scroll = Vector2.Zero;
            nowMove = false;
        }

        public void SetTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("block");
        }

        public void Move()
        {
            if (!nowMove)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    nowMove = true;
                    velocity.X = -SPEED;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    nowMove = true;
                    velocity.X = +SPEED;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    nowMove = true;
                    velocity.Y = -SPEED;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    nowMove = true;
                    velocity.Y = +SPEED;
                }
            }
            else
            {
                if (position.X - scroll.X > SCROLL_RIGHT)
                {
                    scroll.X += velocity.X;
                }
                if (position.X - scroll.X < SCROLL_LEFT)
                {
                    scroll.X += velocity.X;
                }
            }

            position += velocity;

        }

        //当たり判定
        public void Collition(int[,] mapChipNum, int mapChipSize)
        {
            //プレイヤーの座標(左端)を配列番号に
            int leftX = (int)position.X / mapChipSize;
            int upY = (int)position.Y / mapChipSize;
            //プレイヤーの右端・下端を配列番号に
            int rightX = ((int)position.X + WIDTH) / mapChipSize;
            int downY = ((int)position.Y + HEIGHT) / mapChipSize;
            //プレイヤーの中心を配列番号に
            int middleX = ((int)position.X + 32) / mapChipSize;
            int middleY = ((int)position.Y + 32) / mapChipSize;

            //プレイヤーの右が当たったら
            if ((mapChipNum[middleY, rightX] == 1) && (position.X + WIDTH > rightX * mapChipSize))
            {
                velocity = Vector2.Zero;
                nowMove = false;
                FixPosiiton(new Vector2(rightX * mapChipSize - WIDTH, position.Y)); //補正
            }
            //プレイヤーの左が当たったら
            if ((mapChipNum[middleY, leftX] == 1) && (position.X < leftX * mapChipSize + mapChipSize))
            {
                FixPosiiton(new Vector2(leftX * mapChipSize + mapChipSize, position.Y));
                velocity = Vector2.Zero;
                nowMove = false;

            }
            //プレイヤーの下が当たったら
            if (mapChipNum[downY, middleX] == 1 && position.Y + HEIGHT > downY * mapChipSize)
            {
                FixPosiiton(new Vector2(position.X, downY * mapChipSize - HEIGHT));
                nowMove = false;
                velocity = Vector2.Zero;

            }
            //プレイヤーの上が当たったら
            if (mapChipNum[upY, middleX] == 1 && position.Y < upY * mapChipSize + mapChipSize)
            {
                FixPosiiton(new Vector2(position.X, upY * mapChipSize + mapChipSize));
                velocity = Vector2.Zero;
                nowMove = false;

            }

        }

        //プレイヤーが当たった画像にめり込まないように補正する
        public void FixPosiiton(Vector2 fixPos)
        {
            position = fixPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)scroll.X, (int)position.Y, WIDTH, HEIGHT), new Rectangle(64, 0, WIDTH, HEIGHT), Color.White);


        }
    }
}
