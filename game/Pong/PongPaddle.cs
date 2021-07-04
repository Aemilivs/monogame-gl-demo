using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game.Pong
{
    public class PongPaddle : PongGameComponent
    {
        private Point velocity;
        
        public PongPaddle(PongGame game, ScoreSide side) : base(game)
        {
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

            Position = 
                new Rectangle(
                        horizontalCoordinate,
                        verticalCoordinate,
                        8,
                        32 
                    );

            velocity = new Point(0, 0);
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if(Position.Y < 0)
                    return;

                velocity.Y = velocity.Y > 0 ? -1 : --velocity.Y;
                Position.Y += velocity.Y;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if(Position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                    return;

                velocity.Y = velocity.Y <= 0 ? 1 : ++velocity.Y;
                Position.Y += velocity.Y;
            }
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Texture = DrawTexture();
            base.LoadContent();
        }

        private Texture2D DrawTexture()
        {
            var Texture =
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
            Texture.SetData(data);

            return Texture;
        }
    }
}