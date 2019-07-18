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
        const int HEIGHT = 8;

        //テクスチャ
        Texture2D mapChip;
        const int CHIP_SIZE = 64;
        public int ChipSize => CHIP_SIZE;

        //チップ
        const int EMPTY_NUM = 0;
        const int WALL_NUM = 1;
        const int ENEMY_NUM = 2;

        public int WallChipNum => WALL_NUM;

        public Map()
        {
            mapChipNum = mapChipNumBase;
        }

        public void SetTexture(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("block");
        }

        //アイテムに触ったら空白に
        public void ItemChipTach(int middleX, int middleY)
        {
            if (mapChipNum[middleY, middleX] == ENEMY_NUM)
            {
                mapChipNum[middleY, middleX] = EMPTY_NUM;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scroll)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    spriteBatch.Draw(mapChip, new Rectangle((j * CHIP_SIZE) - (int)scroll.X, i * CHIP_SIZE, CHIP_SIZE, CHIP_SIZE), new Rectangle(CHIP_SIZE * mapChipNum[i, j], 0, CHIP_SIZE, CHIP_SIZE), Color.White);
                }
            }
        }
    }
}
