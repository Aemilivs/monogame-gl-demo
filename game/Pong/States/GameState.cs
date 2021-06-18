namespace game.Pong.States
{
    public abstract class GameState
    {
        protected readonly PongGame _game;
        protected readonly PongBall _ball;
        public GameState(PongGame game, PongBall ball)
        {
            _game = game;
            _ball = ball;
        }
        public abstract void Update();
    }
}