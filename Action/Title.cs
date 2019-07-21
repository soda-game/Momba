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
    class Title
    {
        Texture2D title;
        Vector2 titlePos;
        Vector2 velocity;
        const float SPEED = 1.3f;

        Texture2D pushEnter;
        Vector2 enterPos;

        bool pushF = false;

        public Title()
        {
            titlePos = new Vector2(133, 100);//vec2の数字をconstにする暇はなかった //これってconstしなくてもいいかな…
            velocity = new Vector2(0, SPEED);
            enterPos = new Vector2(200, 340);
        }

        public void SetTexture(ContentManager content)
        {
            title = content.Load<Texture2D>("Title");
            pushEnter = content.Load<Texture2D>("Enter");
        }

        //タイトル上下
        public void UpAndDown()
        {
            if (titlePos.Y >= 110) velocity.Y -= SPEED;
            else if (titlePos.Y <= 55) velocity.Y += SPEED;
            titlePos += velocity;
        }

        public bool PushEnter()
        {
            bool nextF = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                pushF=true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter)&&pushF)
            {
                nextF = true;
            }

            return nextF;
        }

       

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(title, titlePos, Color.White);
            spriteBatch.Draw(pushEnter, enterPos, Color.White);
        }

    }
}
