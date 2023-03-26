using UnityEngine;

public enum GameState { Prepare, Playing, GameOver, Win }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public bool isGameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        state = GameState.Prepare;
    }

    public void SetState(int s)
    {
        state = (GameState)s;
    }

    public void RestartLevel()
    {
        SetState(1);
        isGameOver = false;
    }

    public void Win()
    {
        state = GameState.Win;
        UIManager.instance.ChangeStateUI(3);
        LevelManager.instance.LevelUp();
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        isGameOver = true;
        UIManager.instance.ChangeStateUI(2);
    }
}