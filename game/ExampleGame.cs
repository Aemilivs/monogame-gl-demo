using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game
{
    public class ExampleGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly IConfiguration _configuration;
        private SpriteBatch spriteBatch;
        private RenderTarget2D render;
        private Rectangle screen;
        private Texture2D texture;

        private Texture2D charaset;
        private Vector2 position;
        public ExampleGame(IConfiguration configuration)
        {
            _configuration = configuration;
            _graphics = new GraphicsDeviceManager(this);
        }
        
        protected override bool BeginDraw()
        {
            return base.BeginDraw();
        }

        protected override void BeginRun()
        {
            base.BeginRun();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(render);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            for (int index = 0; index < 31; index++)
                spriteBatch.Draw(
                    texture,
                    new Rectangle(
                            render.Width/2,
                            index * render.Height / 31,
                            2,
                            render.Height / 62
                        ),
                    Color.White
                );
            spriteBatch.Draw(charaset, position, Color.Wheat);
            spriteBatch.End();


            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                    render, 
                    screen, 
                    Color.White
                );
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            base.EndDraw();
        }

        protected override void EndRun()
        {
            base.EndRun();
        }

        protected override void Initialize()
        {
            var displaySettings = _configuration.GetSection("DisplaySettings");
            _graphics.PreferredBackBufferWidth = displaySettings.GetValue<int>("Width");
            _graphics.PreferredBackBufferHeight = displaySettings.GetValue<int>("Height");
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            render = new RenderTarget2D(GraphicsDevice, 640, 480);

            this.Content.RootDirectory = "Content";
            this.TargetElapsedTime = new TimeSpan(333333);
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += OnWindowSizeChange;
            OnWindowSizeChange(null, null);

            base.Initialize();
        }

        private Texture2D DrawTexture()
        {
            var texture =
                new Texture2D(
                        GraphicsDevice,
                        1,
                        1
                    );
            var data =
                new Color[]
                {
                    Color.White
                };
            texture.SetData(data);

            return texture;
        }

        public void OnWindowSizeChange(object sender, EventArgs arguments)
        {
            screen = CalculateScreenRectangle(render);
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
            texture = DrawTexture();
            charaset = Content.Load<Texture2D>("charaset");
            position = new Vector2(100, 100);
            base.LoadContent();
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
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
    }
}