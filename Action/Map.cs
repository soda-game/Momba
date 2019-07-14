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
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,5,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1, 1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
        const int WIDTH = 26;
        const int HEIGHT = 8;

        List<Vector2> floorChipPos= new List<Vector2>();
        public List<Vector2> FloorChipPos => floorChipPos;

        //テクスチャ
        Texture2D mapChip;
        const int CHIP_SIZE = 64;
        public int ChipSize=>CHIP_SIZE;

        public Map()
        {
            
        }

        public void SetTexture(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("block");
        }

        public void Scoll(Vector2 playerPos)
        {

        }


        public  void Draw(SpriteBatch spriteBatch,Vector2 scroll)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    spriteBatch.Draw(mapChip, new Rectangle((j * CHIP_SIZE)-(int)scroll.X, i * CHIP_SIZE, CHIP_SIZE, CHIP_SIZE), new Rectangle(CHIP_SIZE * mapChipNum[i, j], 0, CHIP_SIZE, CHIP_SIZE), Color.White);

                    if (mapChipNum[i, j] == 1)
                    {
                        floorChipPos.Add(new Vector2(j * CHIP_SIZE, i * CHIP_SIZE));
                    }
                }

            }
    }
}}
