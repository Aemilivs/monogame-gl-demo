using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongScore : PongGameComponent
    {
        private readonly PongGame _game;
        private readonly SpriteFont _font;
        private readonly ScoreSide _side;
        public PongScore(PongGame game, ScoreSide side) : base(game)
        {
            _game = game;
            _font = game.Content.Load<SpriteFont>("font");
            _side = side;

            _position =
                side == ScoreSide.Left ? 
                    new Rectangle(
                            100, 
                            150, 
                            5, 
                            5
                        ) :
                    new Rectangle(
                            _game.Render.Width - 100, 
                            150, 
                            5, 
                            5
                        );
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                    _font, 
                    "A", 
                    _position.Location.ToVector2(), 
                    Color.White
                );
            _spriteBatch.End();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = base.DrawTexture();
        }
        
        // TODO: Some funny interaction upon being hit by a ball.    
        public override void Collide(PongGameComponent component) { }
    }
}
