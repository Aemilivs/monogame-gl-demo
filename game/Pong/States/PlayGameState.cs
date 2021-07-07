using System;
using Microsoft.Xna.Framework.Input;

namespace game.Pong.States
{
    public class PlayGameState : GameState
    {
        private int LeftScore = 0;
        private int RightScore = 0;
        public PlayGameState(PongGame game, PongBall ball) : base(game, ball)
        {
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                var state = new IdleGameState(_game, _ball);
                _game.SetState(state);
                _ball.ResetBall();
            }

            _ball.MoveBall();

            if(LeftScore > 3 || RightScore > 3)
            {
                var state = new CheckEndGameState(_game, _ball);
                _game.SetState(state);
            }

            _ball.ResetBall();
        }
    }
}