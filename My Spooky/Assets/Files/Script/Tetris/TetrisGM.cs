using TMPro;
using UnityEngine;

public class TetrisGM : MonoBehaviour, IGamesGM
{
    private int score;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] GameObject objWait;
    public bool isPaussed;
    public bool isGameover;

    private void Start()
    {
        isGameover = false;
        WaitState();
        score = 0;
        txtScore.text = score.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objWait.activeInHierarchy)
        {
            Invoke(nameof(PlayingState), 0.2f);
        }
    }


    private void WaitState()
    {
        isPaussed = true;
        objWait.SetActive(true);
    }

    private void PlayingState()
    {
        isPaussed = false;
        objWait.SetActive(false);
    }


    public void AddScore()
    {
        if (isGameover) return;
        score++;
        txtScore.text = score.ToString();
    }

    public void GameOver()
    {
        isPaussed = true;
        isGameover = true;
        int highScore = PlayerPrefs.GetInt("TetrisHighScore", 0);
        if (score > highScore) highScore = score;
        PlayerPrefs.SetInt("TetrisHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        WaitState();
        isGameover = false;
    }
}
