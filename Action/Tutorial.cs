using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace Action
{
    class Tutorial 
    {
        Texture2D tutorial;
        Vector2 tutorialPos;

        Texture2D pushEnter;
        Vector2 enterPos;
        bool pushF = false;

        SoundEffect pushSE;

        public Tutorial()
        {
            tutorialPos = new Vector2(0, -70);
            enterPos = new Vector2(195, 420);
        }
        public void Load(ContentManager content)
        {
            tutorial = content.Load<Texture2D>("tutorial");
            pushEnter = content.Load<Texture2D>("Enter");
            pushSE = content.Load<SoundEffect>("pushSE");
        }

        public bool PushEnter()
        {
            bool nextF = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                pushF = true;
                pushSE.Play();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter) && pushF)
            {
                nextF = true;
            }

            return nextF;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tutorial, tutorialPos, Color.White);
            spriteBatch.Draw(pushEnter, enterPos, Color.White);
        }
    }
}
