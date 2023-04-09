using TMPro;
using UnityEngine;

public class FlappyGM : Singleton<FlappyGM>, IGamesGM
{
    public enum State
    {
        WatingToStart,
        Playing,
        GameOver
    }

    [SerializeField] FlappySpooky spooky;
    [SerializeField] FlappyLevel level;
    [SerializeField] GameObject waitUI;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtSpeed;
    private int score;
    private int highScore;
    public State state;



    private void Start()
    {
        score = 0;
        txtScore.text = score.ToString();
        WaitState();
        Difficulty();
    }

    private void Update()
    {
        if (state == State.WatingToStart && Input.GetMouseButtonDown(0))
        {
            PlayingState();
        }

        txtSpeed.text = level.speed.ToString();
    }




    private void WaitState()
    {
        waitUI.SetActive(true);
        Time.timeScale = 0;
        state = State.WatingToStart;
    }

    private void PlayingState()
    {
        waitUI.SetActive(false);
        Time.timeScale = 1;
        state = State.Playing;
    }

    private void GameOverState()
    {
        state = State.GameOver;
    }

    private void SetHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighestScore", 0);
        if (score > highScore) highScore = score;
        PlayerPrefs.SetInt("HighestScore", highScore);
    }

    private void Difficulty()
    {
        switch (score)
        {
            case < 5:
                level.speed = 3;
                level.handGap = 8.5f;
                level.handDist = 6f;
                break;

            case < 10:
                level.speed = 4;
                level.handGap = 7f;
                level.handDist = 5f;
                break;

            case < 15:
                level.speed = 5.5f;
                level.handGap = 5f;
                level.handDist = 4f;
                break;

            case < 20:
                level.speed = 7;
                level.handGap = 4f;
                level.handDist = 3.5f;
                break;
        }
    }






    public void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
        Difficulty();
    }

    public void GameOver()
    {
        SetHighScore();
        spooky.GameOver();
        GameOverState();
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        txtScore.text = score.ToString();
        WaitState();
        level.Reset();
        spooky.Reset();

        level.speed -= 0.5f;
        level.handGap += 1f;
        level.handDist += 1f;
    }
}
