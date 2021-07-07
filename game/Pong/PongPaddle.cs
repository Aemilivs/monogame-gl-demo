using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game.Pong
{
    public class PongPaddle : PongGameComponent
    {
        private Point velocity;
        private readonly PongGame _game;
        private readonly ScoreSide _side;
        
        // TODO: Redesign.
        private const int WALL_WIDTH = 5;

        public PongPaddle(PongGame game, ScoreSide side) : base(game)
        {
            _side = side;
            _game = game;
            int horizontalCoordinate;
            int verticalCoordinate;

            switch (side)
            {
                case ScoreSide.Left:
                    horizontalCoordinate = 32;
                    verticalCoordinate = game.Render.Height/2-16;
                    break;
                case ScoreSide.Right:
                    horizontalCoordinate = game.Render.Width - 49;
                    verticalCoordinate = game.Render.Height/2-16;
                    break;
                default:
                    throw new ArgumentException("Score side is invalid.");
            }

            _position = 
                new Rectangle(
                        horizontalCoordinate,
                        verticalCoordinate,
                        8,
                        32 
                    );

            velocity = new Point(0, 0);
        }

        public override void Collide(PongGameComponent component)
        {
            if(component is PongBall ball)
                ball.Velocity.X *= -1;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if(_position.Y <= WALL_WIDTH)
                {
                    _position.Y = WALL_WIDTH;
                    return;
                }

                velocity.Y = velocity.Y > 0 ? -1 : --velocity.Y;
                _position.Y += velocity.Y;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                
                if(_position.Y >= _game.Render.Height - _position.Height - WALL_WIDTH)
                {
                    _position.Y = _game.Render.Height - _position.Height - WALL_WIDTH;
                    return;
                }

                velocity.Y = velocity.Y <= 0 ? 1 : ++velocity.Y;
                _position.Y += velocity.Y;
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = DrawTexture();
            base.LoadContent();
        }
    }
}