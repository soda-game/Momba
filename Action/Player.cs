﻿using System;
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
        const int SCROLL_RIGHT = 600;
        const int SCROLL_LEFT = 63;

        //テクスチャ
        Texture2D texture;
        const int WIDTH = 64;
        const int HEIGHT = 64;

        //別クラスで使うのでプロパティ化
        int middleX;
        public int MiddleX => middleX;
        int middleY;
        public int MiddleY => middleY;

        //フラグ
        bool nowMove;

        public Player()
        {
            position = new Vector2(64, 64);
            velocity = Vector2.Zero;
            scroll = Vector2.Zero;
            middleX = 0;
            middleY = 0;
            nowMove = false;
        }

        public void SetTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
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

            position += velocity;

        }

        //スクロール
       public void Scroll()
        {
            if (nowMove)
            {
                if (position.X - scroll.X > SCROLL_RIGHT)
                {
                    scroll.X += velocity.X;
                }
                else if (position.X - scroll.X < SCROLL_LEFT)
                {
                    scroll.X += velocity.X;
                }
            }
        }

        //当たり判定
        public void Collition(int[,] mapChipNum, int mapChipSize,int WallChipNum)
        {
            //プレイヤーの座標(左端)を配列番号に
            int leftX = ((int)position.X-1) / mapChipSize;
            int upY = ((int)position.Y -1)/ mapChipSize;
            //プレイヤーの右端・下端を配列番号に
            int rightX = ((int)position.X + WIDTH-1) / mapChipSize;
            int downY = ((int)position.Y + HEIGHT-1) / mapChipSize;
            //プレイヤーの中心を配列番号に
            middleX = ((int)position.X + WIDTH/2 -1) / mapChipSize;
             middleY = ((int)position.Y + HEIGHT/2 -1) / mapChipSize;

            //プレイヤーの右が当たったら
            if (mapChipNum[middleY, rightX] == WallChipNum && position.X + WIDTH > rightX * mapChipSize)
            {
                StopMove();
                //velocity = new Vector2(0, SPEED);
                FixPosiiton(new Vector2(rightX * mapChipSize - WIDTH, position.Y)); //補正
            }
            //プレイヤーの左が当たったら
            if ((mapChipNum[middleY, leftX] ==WallChipNum) && (position.X < leftX * mapChipSize + mapChipSize))
            {
                StopMove();
                FixPosiiton(new Vector2(leftX * mapChipSize + mapChipSize, position.Y));
            }
            //プレイヤーの下が当たったら
            if (mapChipNum[downY, middleX] == WallChipNum && position.Y + HEIGHT > downY * mapChipSize)
            {
                StopMove();
                FixPosiiton(new Vector2(position.X, downY * mapChipSize - HEIGHT));
            }
            //プレイヤーの上が当たったら
            if (mapChipNum[upY, middleX] == WallChipNum && position.Y < upY * mapChipSize + mapChipSize)
            {
                StopMove();
                FixPosiiton(new Vector2(position.X, upY * mapChipSize + mapChipSize));
            }
        }

        //止める
        void StopMove()
        {
            velocity = Vector2.Zero;
            nowMove = false;
        }

        //プレイヤーが当たった画像にめり込まないように補正する
       void FixPosiiton(Vector2 fixPos)
        {
            position = fixPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)scroll.X, (int)position.Y, WIDTH, HEIGHT), new Rectangle(0, 0, WIDTH, HEIGHT), Color.White);


        }
    }
}
