using System;

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
            var scoreSide = _ball.MoveBall(true);

            switch (scoreSide)
            {
                case ScoreSide.Left:
                    LeftScore += 1;
                    break;
                case ScoreSide.Right:
                    RightScore += 1;
                    break;
                case ScoreSide.None:
                default:
                    return;
            }

            Console.WriteLine($"{scoreSide} scored!");

            if(LeftScore > 3 || RightScore > 3)
            {
                var state = new CheckEndGameState(_game, _ball);
                _game.SetState(state);
            }

            _ball.ResetBall();
        }
    }
}