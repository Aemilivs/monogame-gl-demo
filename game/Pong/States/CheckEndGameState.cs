using Microsoft.Xna.Framework.Input;

namespace game.Pong.States
{
    public class CheckEndGameState : GameState
    {
        public CheckEndGameState(PongGame game, PongBall ball) : base(game, ball)
        {
        }

        public override void Update()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;
            
            var state = new StartGameState(_game, _ball);
            _game.SetState(state);
        }
    }
}