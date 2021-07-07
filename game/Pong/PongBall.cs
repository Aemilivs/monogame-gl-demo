using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongBall : PongGameComponent
    {
        private readonly RenderTarget2D _render;
        public Point Velocity;
        private ScoreSide scoreSide = ScoreSide.Left;
        public PongBall(PongGame game, RenderTarget2D render) : base(game)
        {
            _render = render;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MoveBall();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(
                    _texture,
                    _position,
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

        public void MoveBall()
        {
            _position.X += Velocity.X;
            _position.Y += Velocity.Y;
        }

        public void ResetBall()
        {
            _position = 
                new Rectangle(
                    _render.Width/2 - 4,
                    _render.Height/2 - 4,
                    8,
                    8
                );

            var random = new Random();
            Velocity =
                new Point(
                    scoreSide == ScoreSide.Left ?
                        random.Next(2, 7) :
                        random.Next(2, 7) * -1,
                    random.Next() > int.MaxValue / 2 ?
                        random.Next(2, 7) :
                        random.Next(2, 7) * -1
                );
        }

        public override void Collide(PongGameComponent component)
        {
            if(component is PongPaddle)
                Velocity.X *= -1;
        } 
    }
}