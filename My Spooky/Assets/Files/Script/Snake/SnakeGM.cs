using UnityEngine;

public class SnakeGM : Singleton<SnakeGM>, IGamesGM
{
    [SerializeField] Snake snake;
    [SerializeField] TMPro.TMP_Text txtScore;
    [SerializeField] GameObject objWait;
    private int score;
    private int lastSize;


    private void Start()
    {
        score = 0;
        txtScore.text = score.ToString();

        WaitState();
    }

    private void Update()
    {
        if (objWait.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            ResumeState();
        }
    }


    private void WaitState()
    {
        Time.timeScale = 0;
        objWait.SetActive(true);
    }

    private void ResumeState()
    {
        Time.timeScale = 1;
        objWait.SetActive(false);
    }


    public void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
    }

    public void GameOver()
    {
        snake.Stop();
        lastSize = snake.LastSize();
        int highScore = PlayerPrefs.GetInt("SnakeHighScore", score);
        if (highScore < score) highScore = score;
        PlayerPrefs.SetInt("SnakeHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        WaitState();
        snake.initialSize = lastSize;
        snake.ResetState();
    }
}
