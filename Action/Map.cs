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
    class Map
    {
        //マップ
        int[,] mapChipNumBase =             //数字変えない！！！！
            {
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,0,2,0,2,0,2,0,2,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

        int[,] mapChipNum; //変える用
        public int[,] MapChipNum => mapChipNum;
        const int WIDTH = 26;
        public int Width => WIDTH;
        const int HEIGHT = 8;

        //テクスチャ
        Texture2D mapChip;
        const int CHIP_SIZE = 64;
        public int ChipSize => CHIP_SIZE;

        int count;
        int scaling;
        int chipScal;

        //チップ
        enum MapNum
        {
            EmptyNum,
            WallNum,
            EnemyNum,
        }
        public int WallChipNum => (int)MapNum.WallNum;

        public Map()
        {
            mapChipNum = mapChipNumBase;
            count = 0;
            scaling = 0;
            chipScal = 0;
        }

        public void SetTexture(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("block");
        }

        public void ChipScaling()
        {
            count++;

            if (count >= 60) count = 0;
            else if (count < 30) scaling = 2;
            else scaling = -5;

        }

        //敵に触ったら空白に
        public void ItemChipTach(int middleX, int middleY)
        {
            if (mapChipNum[middleY, middleX] == (int)MapNum.EnemyNum)
            {
                mapChipNum[middleY, middleX] = (int)MapNum.EmptyNum;
            }

        }

        //敵が残っているか
        public bool ItemCount()
        {
            //int a = mapChipNum.Count(n => n == 1); //ダメだった
            //if (a <= 0) ;

            bool NoEnmy = false;

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (MapChipNum[i, j] == (int)MapNum.EnemyNum)
                    {
                        chipScal += scaling; //ついでに 大きさ処理
                        NoEnmy = true;
                    }
                    else
                    {
                        chipScal = CHIP_SIZE;
                    }
                }
            }

            return NoEnmy;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scroll, int alpha)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    spriteBatch.Draw(mapChip, new Rectangle((j * CHIP_SIZE) - (int)scroll.X, i * CHIP_SIZE, chipScal, chipScal), new Rectangle(CHIP_SIZE * mapChipNum[i, j], 0, CHIP_SIZE, CHIP_SIZE), Color.White * alpha);

                }
            }
        }
    }
}
