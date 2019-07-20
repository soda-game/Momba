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
    class Title
    {
        Texture2D title;
        Vector2 titlePosi;
        Vector2 velocity;
        const float SPEED = 1.3f;

        Texture2D pushEnter;
        Vector2 enterPosi;

        int test;

        public Title()
        {
            titlePosi = new Vector2(133, 100);
            velocity = new Vector2(0, SPEED);
            enterPosi = new Vector2(200, 340);
        }

        public void SetTexture(ContentManager content)
        {
            title = content.Load<Texture2D>("Title");
            pushEnter = content.Load<Texture2D>("Enter");
        }

        //タイトル上下
        public void UpAndDown()
        {
            if (titlePosi.Y >= 110) velocity.Y -= SPEED;
            else if (titlePosi.Y <= 55) velocity.Y += SPEED;
            titlePosi.Y += velocity.Y;
        }

        public bool PushEnter()
        {
            bool pushF=false;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                pushF=true;
                
            }

            return pushF;
        }

        //public void Test()
        //{
        //    test++;
        //    if (test > 30)
        //    {

        //    }
        //}

        public void Draw(SpriteBatch spriteBatch,int alpha)
        {
            spriteBatch.Draw(title, titlePosi, Color.White*alpha);
            spriteBatch.Draw(pushEnter, enterPosi, Color.White*alpha);
        }
    }
}
