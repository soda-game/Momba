using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Action
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;
        Player player;
        Title title;
        Stage stage;



        enum SceneNum
        {
            Title,
            Game,
        }
        SceneNum sceneNum;

        //Drawで分岐処理しないためのアルファ
        int titleAlpha;
        int gameAlpha;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameInit();
            TitleInit();


            base.Initialize();
        }

        void TitleInit()
        {
            title = new Title();

            title.SetTexture(Content);

            sceneNum = SceneNum.Title;

            titleAlpha = 1;
            gameAlpha = 0;
        }

        void GameInit()
        {
            stage = new Stage();
            map = new Map();
            player = new Player();

            //クラスに持たせてると結局ロードしなきゃいけない…うーん
            stage.SetTexture(Content);
            map.SetTexture(Content);
            player.SetTexture(Content);


            sceneNum = SceneNum.Game;

            gameAlpha = 1;
            titleAlpha = 0;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (sceneNum)
            {
                case SceneNum.Title:
                    title.UpAndDown();
                    if (title.PushEnter())  GameInit(); 
                    break;

                case SceneNum.Game:
                    stage.Slide();
                    player.Move();
                    player.Collition(map.MapChipNum, map.ChipSize, map.WallChipNum);
                    player.Scroll();
                    map.ItemChipTach(player.MiddleX, player.MiddleY);

                    //初期化
                    if (Keyboard.GetState().IsKeyDown(Keys.I)) GameInit();
                    break;
            }
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            title.Draw(spriteBatch,titleAlpha);

            map.Draw(spriteBatch, player.scroll, gameAlpha);
            player.Draw(spriteBatch, gameAlpha);
            stage.Draw(spriteBatch, gameAlpha);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
