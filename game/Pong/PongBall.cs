using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongBall : PongGameComponent
    {
        private readonly RenderTarget2D _render;
        public Point velocity;
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
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(
                    Texture,
                    Position,
                    Color.White
                );
            SpriteBatch.End();
            
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Texture = DrawTexture();
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

        public ScoreSide MoveBall(bool bounceOffSides)
        {
            Position.X += velocity.X;
            Position.Y += velocity.Y;

            if(Position.Y < 0)
            {
                Position.Y *= -1;
                velocity.Y *= -1;
            }

            if(Position.Y + Position.Height > _render.Height)
            {
                Position.Y = _render.Height - Position.Height - (Position.Y + Position.Height - _render.Height);
                velocity.Y *= -1;
            }

            if(Position.X < 0)
                if(bounceOffSides) 
                {
                    Position.X = 0;
                    velocity.X *= -1;
                }
                else return ScoreSide.Left;

            if(Position.X + Position.Height > _render.Width)
                if(bounceOffSides)
                {
                    Position.X = _render.Width - Position.Width;
                    velocity.X *= -1;
                }
                else return ScoreSide.Right;

            return ScoreSide.None;
        }

        public void ResetBall()
        {
            Position = 
                new Rectangle(
                    _render.Width/2 - 4,
                    _render.Height/2 - 4,
                    8,
                    8
                );

            var random = new Random();
            velocity =
                new Point(
                    scoreSide == ScoreSide.Left ?
                        random.Next(2, 7) :
                        random.Next(2, 7) * -1,
                    random.Next() > int.MaxValue / 2 ?
                        random.Next(2, 7) :
                        random.Next(2, 7) * -1
                );
        }
    }
}