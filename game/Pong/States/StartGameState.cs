namespace game.Pong.States
{
    public class StartGameState : GameState
    {
        public StartGameState(PongGame game, PongBall ball) : base(game, ball)
        {
        }

        public override void Update()
        {
            _ball.ResetBall();

            var state = new PlayGameState(_game, _ball);
            _game.SetState(state);
        }
    }
}