using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public abstract class PongGameComponent : DrawableGameComponent
    {
        public Rectangle Position;
        public Texture2D Texture;
        public SpriteBatch SpriteBatch;

        protected PongGameComponent(PongGame game) : base(game)
        {
        }

        // TODO: Rewrite to mind the velocities.
        public bool DetectCollision(PongGameComponent component) => 
            Position.Intersects(component.Position);
    }
}