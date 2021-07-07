using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongWall : PongGameComponent
    {

        private readonly RenderTarget2D _render;
        private readonly PongGame _game;
        private readonly ScoreSide _side;

        public PongWall(
                PongGame game, 
                RenderTarget2D render, 
                Rectangle position, 
                ScoreSide side
            ) : base(game)
        {
            _game = game;
            _render = render;
            _position = position;
            _side = side;
        }

        public override void Collide(PongGameComponent component)
        {
            if(component is PongBall ball)
                if(_side is ScoreSide.Left || _side is ScoreSide.Right)
                    ball.Velocity.X *= -1;
                else
                    ball.Velocity.Y *= -1;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = base.DrawTexture();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                    _texture,
                    _position,
                    Color.White
                );
            _spriteBatch.End();
    
            base.Draw(gameTime);
        }
    }
}