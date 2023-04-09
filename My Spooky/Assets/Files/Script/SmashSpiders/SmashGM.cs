using UnityEngine;

public class SmashGM : Singleton<SmashGM>, IGamesGM
{
    [SerializeField] GameObject objWait;
    [SerializeField] SmashSpawner spawner;
    [SerializeField] TMPro.TMP_Text txtScore;

    private int spiderReached;
    private int score;


    private void Start()
    {
        ChangeDifficulty();
        txtScore.text = score.ToString();
        WaitState();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objWait.activeInHierarchy)
        {
            RuningState();
        }
    }


    private void WaitState()
    {
        objWait.SetActive(true);
        Time.timeScale = 0;
    }

    private void RuningState()
    {
        objWait.SetActive(false);
        Time.timeScale = 1;
    }

    private void ChangeDifficulty()
    {
        switch (score)
        {
            case < 7:
                spawner.speed = 0.2f;
                spawner.batRate = 0.1f;
                spawner.spwanRate = 3f;
                break;

            case < 12:
                spawner.speed = 0.3f;
                spawner.batRate = 0.25f;
                spawner.spwanRate = 2f;
                break;

            case < 20:
                spawner.speed = 0.35f;
                spawner.batRate = 0.3f;
                spawner.spwanRate = 1.2f;
                break;

            case < 25:
                spawner.speed = 0.4f;
                spawner.batRate = 0.35f;
                spawner.spwanRate = 0.7f;
                break;

            case < 30:
                spawner.speed = 0.5f;
                spawner.batRate = 0.4f;
                spawner.spwanRate = 0.3f;
                break;
        }
    }


    public void SpiderReached()
    {
        spiderReached++;
        if (spiderReached >= 10) GameOver();
    }

    public void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
        ChangeDifficulty();
    }

    public void GameOver()
    {
        spiderReached = 0;
        spawner.GameOver();
        int highScore = PlayerPrefs.GetInt("SmashHighScore", score);
        if (highScore < score) highScore = score;
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        spawner.StartGame();
        WaitState();
    }
}
