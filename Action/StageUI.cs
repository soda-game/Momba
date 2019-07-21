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
    class StageUI
    {
        Texture2D stageBar;
        Vector2 position;
        Vector2 velocity;
        const int SLOW = 2;
        const int FAST = 30;

        const int CHANGE_FAST_POS = 80;
        const int CHANGE_SLOW_POS = 19;

        int count;
        public StageUI()
        {
            position = new Vector2(-700, 180);
            velocity = new Vector2(FAST, 0);
        }

        public void SetStartTexture(ContentManager content)
        {
            stageBar = content.Load<Texture2D>("start");
        }
        public void SetClearTexture(ContentManager content)
        {
            stageBar = content.Load<Texture2D>("clear");
        }

        public bool BarSlide()
        {
            bool slideEnd = false;
            count++;
            if (count > 110)
            {
                velocity.X = 0;
                slideEnd = true;
            }
            else if (count > CHANGE_FAST_POS)
            {
                velocity.X = FAST;
            }
            else
            if (count > CHANGE_SLOW_POS)
            {
                velocity.X = SLOW;
            }

            position += velocity;
            return slideEnd;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(stageBar, position, Color.White);
        }
    }
}
