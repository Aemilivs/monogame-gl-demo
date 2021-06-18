using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongBackground : DrawableGameComponent
    {
        public readonly RenderTarget2D _render;
        public Rectangle screen;
        private Texture2D texture;
        private SpriteBatch spriteBatch;

        public PongBackground(Game game, RenderTarget2D render) : base(game)
        {
            _render = render;
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int index = 0; index < 31; index++)
                spriteBatch.Draw(
                    texture,
                    new Rectangle(
                            _render.Width/2,
                            index * _render.Height / 31,
                            2,
                            _render.Height / 62
                        ),
                    Color.White
                );
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = DrawTexture();
            base.LoadContent();
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
    }
}