﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Action
{
    class Map
    {
        //マップ
        int[,] mapChipNumBase =             //数字変えない！！！！
            {
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,1,0,0,0,0,0,2,1, 1,1,0,0,0,2,0,0,0,1,2,0,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,1,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,2,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,2,0,0,0,0,1,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,2,0,0,0,0,1, 1,1,0,1,0,0,0,0,2,0,1,0,1},
            {1,0,0,1,0,2,0,0,0,0,0,0,1, 1,0,0,2,1,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

        int[,] mapChipNum; //変える用
        public int[,] MapChipNum => mapChipNum;
        const int WIDTH = 26;
        public int Width => WIDTH;
        const int HEIGHT = 8;

        int enemyCount;
        public int EnemyConut => enemyCount;

        //テクスチャ
        Texture2D mapChip;
        const int CHIP_SIZE = 64;
        public int ChipSize => CHIP_SIZE;

        int count;
        int scaling;

        //チップ
        enum MapNum
        {
            EmptyNum,
            WallNum,
            EnemyNum,
        }
        public int WallChipNum => (int)MapNum.WallNum;

        //スケール
        const int END_COUNT = 60;
        const int BIG_COUNT = 30;
        const int BIG_SCALE=2;
        const int SMALL_SCALE = -5;

        //SE
        SoundEffect hokoriGetSE;

        public Map()
        {
            mapChipNum = mapChipNumBase;
            scaling = 0;
            count = 0;
            enemyCount = 0;
        }

        public void Load(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("block");
            hokoriGetSE = content.Load<SoundEffect>("hokoriGetSE");
        }

        //拡縮アニメーション
        public void ChipScaling()
        {
            count++;

            if (count >= END_COUNT)
            {
                count = 0;
            }
            else if (count < BIG_COUNT)
            {
                scaling = BIG_SCALE;
            }
            else
            {
                scaling = SMALL_SCALE;
            }

        }

        //敵に触ったら空白に
        public void EnemyTach(int middleX, int middleY)
        {
            if (mapChipNum[middleY, middleX] == (int)MapNum.EnemyNum)
            {
                hokoriGetSE.Play();
                mapChipNum[middleY, middleX] = (int)MapNum.EmptyNum;
            }

        }

        //敵が残っているか
        public bool EnemyCheck()
        {
            //int a = mapChipNum.Count(n => n == 1); //ダメだった
            //if (a <= 0) ;

            int subEnemyCount = 0;
            bool DeadEnmy = true;

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (MapChipNum[i, j] == (int)MapNum.EnemyNum)
                    {
                        subEnemyCount++;
                        DeadEnmy = false;
                    }
                }
            }

            enemyCount = subEnemyCount;
            return DeadEnmy;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    //ホコリのアニメーション
                    int chipScal = CHIP_SIZE;
                    if (mapChipNum[i, j] == (int)MapNum.EnemyNum)
                    {
                        chipScal += scaling;
                    }

                    spriteBatch.Draw(mapChip, new Rectangle((j * CHIP_SIZE) - (int)scroll.X, i * CHIP_SIZE, chipScal, chipScal), new Rectangle(CHIP_SIZE * mapChipNum[i, j], 0, CHIP_SIZE, CHIP_SIZE), Color.White);

                }
            }
        }
    }
}
