namespace Assets.Scripts
{
    abstract class GameManager
    {
        public static GameStates currentState;

        public enum GameStates
        {
            Stop,
            Start,
            GameOver,
            YouWin
        }
    }
}