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
        int[,] mapChipNum =
            {
            {0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,5,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,2,0,0,0,0,0, 0,0,0,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
        const int WIDTH = 26;
        const int HEIGHT = 8;

        //テクスチャ
        Texture2D mapChip;
        const int CHIP_SIZE = 64;

        public Map(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("block");
        }

        public void MapDraw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    spriteBatch.Draw(mapChip, new Rectangle(j * CHIP_SIZE, i * CHIP_SIZE, CHIP_SIZE, CHIP_SIZE), new Rectangle(64 * mapChipNum[i, j], 0, CHIP_SIZE, CHIP_SIZE), Color.White);
                }
            }

        }
    }
}
