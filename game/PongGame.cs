using System;
using System.Linq;
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
        private PongPaddle[] paddles; 
        private SpriteBatch spriteBatch;
        public RenderTarget2D Render { get; private set; }
        private PongBackground background;
        public PongGame(IConfiguration configuration)
        {
            _configuration = configuration;
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            var displaySettings = _configuration.GetSection("DisplaySettings");

            var width = displaySettings.GetValue<int>("Width");
            var hegiht = displaySettings.GetValue<int>("Height");

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = hegiht;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            this.Content.RootDirectory = "Content";

            Render = new RenderTarget2D(GraphicsDevice, width, hegiht);
            
            background = new PongBackground(this, Render);
            background.screen = CalculateScreenRectangle(Render);
            Components.Add(background);

            var walls = 
                new PongWall[]
                {
                    new PongWall(
                            this, 
                            Render, 
                            new Rectangle(
                                    Point.Zero,
                                    new Point(GraphicsDevice.Viewport.Width, 5)
                                ), 
                            ScoreSide.Top
                        ),
                    new PongWall(
                            this, 
                            Render, 
                            new Rectangle(
                                    GraphicsDevice.Viewport.Width - 5, 
                                    5, 
                                    5, 
                                    GraphicsDevice.Viewport.Height
                                ), 
                            ScoreSide.Right
                        ),
                    new PongWall(
                            this, 
                            Render, 
                            new Rectangle(
                                    0, 
                                    GraphicsDevice.Viewport.Height - 5, 
                                    GraphicsDevice.Viewport.Width, 
                                    5
                                ), 
                            ScoreSide.Bottom
                        ),
                    new PongWall(
                            this, 
                            Render, 
                            new Rectangle(
                                    0, 
                                    0, 
                                    5,
                                    GraphicsDevice.Viewport.Height
                                ), 
                            ScoreSide.Left
                        )
                };

            foreach (var wall in walls)
                Components.Add(wall);
            
            ball = new PongBall(this, Render);
            ball.ResetBall();
            Components.Add(ball);
            state = new IdleGameState(this, ball);

            paddles =
                new PongPaddle[] 
                {
                    new PongPaddle(this, ScoreSide.Left),
                    new PongPaddle(this, ScoreSide.Right)
                };
            
            foreach (var paddle in paddles)
                Components.Add(paddle);

            var scores =
                new PongScore[]
                {
                    new PongScore(this, ScoreSide.Left),
                    new PongScore(this, ScoreSide.Right)
                };
            
            foreach (var score in scores)
                Components.Add(score);

            var ticks = displaySettings.GetValue<long>("ElapsedTimeTicks");
            this.TargetElapsedTime = TimeSpan.FromTicks(ticks);

            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += 
                (sender, args) => background.screen = CalculateScreenRectangle(Render);

            base.Initialize();
        }

        public void SetState(GameState state) 
        {
            this.state = state;
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
                

            
            var components = Components;

            foreach (var gameComponent in components)
            {
                if(gameComponent is PongGameComponent pongComponent)
                {
                    if(pongComponent.DetectCollision(ball))
                    {
                        pongComponent.Collide(ball);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Render);
            base.Draw(gameTime);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                    Render, 
                    background.screen, 
                    Color.White
                );
            spriteBatch.End();
        }
    }
}