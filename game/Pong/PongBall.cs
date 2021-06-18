using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public class PongBall : DrawableGameComponent
    {
        private readonly RenderTarget2D _render;
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private Rectangle position;
        private Point velocity;
        private ScoreSide scoreSide = ScoreSide.Left;
        public PongBall(Game game, RenderTarget2D render) : base(game)
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
            spriteBatch.Begin();
            spriteBatch.Draw(
                    texture,
                    position,
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

        public ScoreSide MoveBall(bool bounceOffSides)
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            if(position.Y < 0)
            {
                position.Y *= -1;
                velocity.Y *= -1;
            }

            if(position.Y + position.Height > _render.Height)
            {
                position.Y = _render.Height - position.Height - (position.Y + position.Height - _render.Height);
                velocity.Y *= -1;
            }

            if(position.X < 0)
                if(bounceOffSides) 
                {
                    position.X = 0;
                    velocity.X *= -1;
                }
                else return ScoreSide.Left;

            if(position.X + position.Height > _render.Width)
                if(bounceOffSides)
                {
                    position.X = _render.Width - position.Width;
                    velocity.X *= -1;
                }
                else return ScoreSide.Right;

            return ScoreSide.None;
        }

        public void ResetBall()
        {
            position = 
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