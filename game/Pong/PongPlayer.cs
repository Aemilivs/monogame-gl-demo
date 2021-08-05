namespace game.Pong
{
    public class PongPlayer
    {
        private readonly PongWall _wall;
        private readonly PongScore _score;
        private readonly PongPaddle _paddle;   

        public PongPlayer(PongGame game, ScoreSide side)
        {
            _wall = new PongWall(game, side);
        }
    }
}