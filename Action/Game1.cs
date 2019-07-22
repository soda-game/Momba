using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        Title title;
        Tutorial tutorial;
        StageUI stageUi;
        Map map;
        Player player;
        Result result;

        enum SceneNum
        {
            Title,
            Tutorial,
            Start,
            Game,
            Clear,
            Result,
        }
        SceneNum sceneNum;

        Song bgm;

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
            title.Load(Content);
            sceneNum = SceneNum.Title;
            Window.Title = "ルンバじゃないよモンバだよ！■■";
            
        }

        void TutorialInit()
        {
            tutorial = new Tutorial();
            tutorial.Load(Content);
            sceneNum = SceneNum.Tutorial;
            Window.Title = "ここに移動回数とホコリの残数が表示されるよ！";
        }

        void StageBarStart()
        {
            stageUi = new StageUI();
            stageUi.Load(Content);
            stageUi.SetStartTexture();
            sceneNum = SceneNum.Start;
            GameInit();
        }

        void GameInit()
        {
            map = new Map();
            player = new Player();

            //クラスに持たせてると結局ロードしなきゃいけない…うーん
            map.Load(Content);
            player.Load(Content);

        }

        void StageBarClear()
        {
            stageUi = new StageUI();
            stageUi.Load(Content);
            stageUi.SetClearTexture();
            sceneNum = SceneNum.Clear;
        }

        void ResultInit()
        {
            sceneNum = SceneNum.Result;
            result = new Result();
            result.SetText(Content);
            Window.Title = "遊んでくれてありがとう！（面白いこと書こうとしたけど何も思いつかなかったよ！）";
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
            bgm = Content.Load<Song>("BGM");
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

            //BGM
            if (MediaPlayer.State != MediaState.Playing) //state 状態
            {
                MediaPlayer.Play(bgm);
            }

            //操作制御
            switch (sceneNum)
            {
                case SceneNum.Title:
                    title.UpAndDown();
                    if (title.PushEnter())
                    {
                        TutorialInit();
                    }
                    break;

                case SceneNum.Tutorial:
                    if (tutorial.PushEnter()) StageBarStart();
                    break;

                case SceneNum.Start:
                    if (stageUi.BarSlide())
                    {
                        sceneNum = SceneNum.Game;
                    }

                    break;

                case SceneNum.Game:
                    Window.Title = "移動回数：" + player.NumberOfMoves + "　 残りのホコリ："+map.EnemyConut+"　　　　　　　リトライ：Kキー";
                    player.Move();
                    player.Blink();
                    player.Collition(map.MapChipNum, map.ChipSize, map.WallChipNum);
                    player.Scroll(map.Width, map.ChipSize);

                    map.ChipScaling();
                    map.EnemyTach(player.MiddleX, player.MiddleY);

                    if (map.EnemyCheck()) StageBarClear();

                    if (Keyboard.GetState().IsKeyDown(Keys.K)) StageBarStart();//初期化
                    break;

                case SceneNum.Clear:
                    if (stageUi.BarSlide())
                    {
                        ResultInit();
                    }
                    break;

                case SceneNum.Result:

                    if (Keyboard.GetState().IsKeyDown(Keys.H)) TitleInit();
                    if (Keyboard.GetState().IsKeyDown(Keys.K)) StageBarStart();


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

            switch (sceneNum)
            {
                case SceneNum.Title:
                    title.Draw(spriteBatch);
                    break;
                case SceneNum.Tutorial:
                    tutorial.Draw(spriteBatch);
                    break;
                case SceneNum.Start:
                case SceneNum.Game:
                case SceneNum.Clear:
                    map.Draw(spriteBatch, player.scroll);
                    player.Draw(spriteBatch);
                    stageUi.Draw(spriteBatch);
                    break;

                case SceneNum.Result:
                    result.Draw(player.NumberOfMoves, spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
