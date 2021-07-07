using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongBackground : PongGameComponent
    {
        public readonly RenderTarget2D _render;
        public Rectangle screen;

        public PongBackground(PongGame game, RenderTarget2D render) : base(game)
        {
            _render = render;
            LoadContent();
        }

        public override void Collide(PongGameComponent component) { }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            for (int index = 0; index < 31; index++)
                _spriteBatch.Draw(
                    _texture,
                    new Rectangle(
                            _render.Width/2,
                            index * _render.Height / 31,
                            2,
                            _render.Height / 62
                        ),
                    Color.White
                );
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = DrawTexture();
            base.LoadContent();
        }
    }
}