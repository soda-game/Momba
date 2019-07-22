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
        const int SCROLL_RIGHT = 600;
        const int SCROLL_LEFT = 63;
        const int fixChip = 3; //プレイヤーが右から2の位置 左座標なので3  □ P 壁
        //テクスチャ
        Texture2D texture;
        const int WIDTH = 64;
        const int HEIGHT = 64;

        //別クラスで使うのでプロパティ化
        int middleX;
        public int MiddleX => middleX;
        int middleY;
        public int MiddleY => middleY;

        //まばたきアニメーション
        int blinkCount;
        int blinkF;

        //フラグ
        bool nowMove;
        int numbreOfMoves;
        public int NumberOfMoves => numbreOfMoves;
        bool keyPushF;

        public Player()
        {
            position = new Vector2(64, 64);
            velocity = Vector2.Zero;

            scroll = Vector2.Zero;

            middleX = 0;
            middleY = 0;

            nowMove = false;
            numbreOfMoves = 0;
            keyPushF = false;

            blinkCount = 0;
            blinkF = 0;
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }
        public void Blink()
        {
            blinkCount++;

            if (blinkCount > 140)
            {
                blinkCount = 0;
                blinkF = 0;
            }else if (blinkCount > 130)
            {
                blinkF = 1;
            }
        }

        public void Move()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (!nowMove)
                {
                    velocity.X = +SPEED;
                    keyPushF = true;
                }
                MoveCount();

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y = -SPEED;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y = +SPEED;
            }
            position += velocity;


        }

        //手数カウント制御
        void MoveCount()
        {
            if (numbreOfMoves < 100 && nowMove && keyPushF) //次のフレームで動いてる→壁ではない & 前フレームでキーが押されている
            {
                keyPushF = false;
                numbreOfMoves++;
            }
            nowMove = true;
        }

        //スクロール
        public void Scroll(int mapWidth, int chipSize)
        {
            if (nowMove && position.X <= (mapWidth - fixChip) * chipSize) //マップの横幅-壁
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
        public void Collition(int[,] mapChipNum, int ChipSize, int WallChipNum)
        {
            //プレイヤーの座標(左端)を配列番号に
            int leftX = ((int)position.X - 1) / ChipSize;
            int upY = ((int)position.Y - 1) / ChipSize;
            //プレイヤーの右端・下端を配列番号に
            int rightX = ((int)position.X - 1 + WIDTH) / ChipSize;
            int downY = ((int)position.Y - 1 + HEIGHT) / ChipSize;
            //プレイヤーの中心を配列番号に
            middleX = ((int)position.X - 1 + WIDTH / 2) / ChipSize;
            middleY = ((int)position.Y - 1 + HEIGHT / 2) / ChipSize;

            //プレイヤーの右が当たったら
            if (mapChipNum[middleY, rightX] == WallChipNum && position.X + WIDTH > rightX * ChipSize)
            {
                StopMove();
                FixPosiiton(new Vector2(rightX * ChipSize - WIDTH, position.Y)); //補正
            }
            //プレイヤーの左が当たったら
            if ((mapChipNum[middleY, leftX] == WallChipNum) && (position.X < leftX * ChipSize + ChipSize))
            {
                StopMove();
                FixPosiiton(new Vector2(leftX * ChipSize + ChipSize, position.Y));
            }
            //プレイヤーの下が当たったら
            if (mapChipNum[downY, middleX] == WallChipNum && position.Y + HEIGHT > downY * ChipSize)
            {
                StopMove();
                FixPosiiton(new Vector2(position.X, downY * ChipSize - HEIGHT));
            }
            //プレイヤーの上が当たったら
            if (mapChipNum[upY, middleX] == WallChipNum && position.Y < upY * ChipSize + ChipSize)
            {
                StopMove();
                FixPosiiton(new Vector2(position.X, upY * ChipSize + ChipSize));
            }
        }

        //止める
        void StopMove()
        {
            velocity = Vector2.Zero;
            nowMove = false;
        }

        //プレイヤーが当たった画像にめり込まないようする
        void FixPosiiton(Vector2 fixPos)
        {
            position = fixPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)scroll.X, (int)position.Y, WIDTH, HEIGHT), new Rectangle(64*blinkF, 0, WIDTH, HEIGHT), Color.White);


        }
    }
}
