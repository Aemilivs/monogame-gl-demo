using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Pong
{
    public abstract class PongGameComponent : DrawableGameComponent
    {
        protected Rectangle _position;
        protected Texture2D _texture;
        protected SpriteBatch _spriteBatch;

        protected PongGameComponent(PongGame game) : base(game)
        {
        }

        protected virtual Texture2D DrawTexture()
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

        // TODO: Rewrite to mind the velocities.
        public bool DetectCollision(PongGameComponent component) 
            => this.Equals(component) ?
                false : 
                _position.Intersects(component._position);

        public abstract void Collide(PongGameComponent component);
    }
}