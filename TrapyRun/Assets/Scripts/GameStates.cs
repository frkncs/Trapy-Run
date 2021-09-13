namespace Assets.Scripts
{
    abstract class GameStates
    {
        public static GameState currentState;

        public enum GameState
        {
            Stop,
            Start,
            GameOver,
            YouWin
        }
    }
}