using System;
using game.Pong;
using game.Pong.States;
using Microsoft.Extensions.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game
{
    public class PongGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly IConfiguration _configuration;
        private GameState state;
        private PongBall ball;
        private SpriteBatch spriteBatch;
        private RenderTarget2D render;
        private PongBackground background;
        public PongGame(IConfiguration configuration)
        {
            _configuration = configuration;
            _graphics = new GraphicsDeviceManager(this);
        }
       
        protected override void Initialize()
        {
            var displaySettings = _configuration.GetSection("DisplaySettings");
            _graphics.PreferredBackBufferWidth = displaySettings.GetValue<int>("Width");
            _graphics.PreferredBackBufferHeight = displaySettings.GetValue<int>("Height");
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            this.Content.RootDirectory = "Content";

            render = new RenderTarget2D(GraphicsDevice, 640, 480);
            
            background = new PongBackground(this, render);
            background.screen = CalculateScreenRectangle(render);
            Components.Add(background);
            
            ball = new PongBall(this, render);
            ball.ResetBall();
            Components.Add(ball);
            state = new IdleGameState(this, ball);

            var ticks = displaySettings.GetValue<long>("ElapsedTimeTicks");
            this.TargetElapsedTime = TimeSpan.FromTicks(ticks);

            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += 
                (sender, args) => background.screen = CalculateScreenRectangle(render);

            base.Initialize();
        }

        public void SetState(GameState state) 
        {
            this.state = state;
        }

        private Texture2D DrawTexture()
        {
            var texture =
                new Texture2D(
                        GraphicsDevice,
                        width: 1,
                        height: 1
                    );
            var data =
                new Color[]
                {
                    Color.White
                };
            texture.SetData(data);

            return texture;
        }

        private Rectangle CalculateScreenRectangle(RenderTarget2D render)
        {
            var width = Window.ClientBounds.Width;
            var height = Window.ClientBounds.Height;

            if (height < width / (float)render.Width / render.Height)
                width = (int)(height / (float)render.Height * render.Width);
            else
                height = (int)(width / (float)render.Width * render.Height);

            var x = (Window.ClientBounds.Width - width) / 2;
            var y = (Window.ClientBounds.Height - height) / 2;
            return new Rectangle(x, y, width, height);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            state.Update();
            if (
                    GamePad
                        .GetState(PlayerIndex.One)
                        .Buttons.Back == ButtonState.Pressed || 
                    Keyboard
                        .GetState()
                        .IsKeyDown(Keys.Escape)
                )
                Exit();
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.SetRenderTarget(render);
            // GraphicsDevice.Clear(Color.Black);

            // GraphicsDevice.SetRenderTarget(null);
            // GraphicsDevice.Clear(Color.CornflowerBlue);

            // spriteBatch.Begin();
            // spriteBatch.Draw(
            //         render, 
            //         screen, 
            //         Color.White
            //     );
            // spriteBatch.End();
            GraphicsDevice.SetRenderTarget(render);
            base.Draw(gameTime);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                    render, 
                    background.screen, 
                    Color.White
                );
            spriteBatch.End();
            
        }
    }
}