public class FailWindow : PauseWindowBase
{
    private Game _game;

    public void Initialize(Game game)
    {
        _game = game;
        _game.Lose += OnLose;
    }

    private void OnLose()
    {
        gameObject.SetActive(true);
        _game.Lose -= OnLose;
    }
}
