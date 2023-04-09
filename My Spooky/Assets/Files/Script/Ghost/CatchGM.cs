using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class CatchGM : Singleton<CatchGM>, IGamesGM
{
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtTime;
    [SerializeField] GameObject objWait;
    [SerializeField] List<CatchGhost> ghosts;

    private float startTime = 30f;
    private float remainTime;
    private int score;
    private bool isPlaying;
    private HashSet<CatchGhost> currentGhost;


    private void Start()
    {
        isPlaying = false;
        currentGhost = new HashSet<CatchGhost>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                remainTime = 0;
                GameOver(0);
            }

            txtTime.text = $"{(int)remainTime / 60}:{(int)remainTime % 60:D2}";

            if (currentGhost.Count <= (score / 10))
            {
                int index = Random.Range(0, ghosts.Count);
                if (!currentGhost.Contains(ghosts[index]))
                {
                    currentGhost.Add(ghosts[index]);
                    ghosts[index].Activate(score / 10);
                }
            }
        }
    }


    public void GameOver(int type)
    {
        if (type == 0)
        {
            Debug.Log("Display Time Over");
        }
        else
        {
            Debug.Log("Display Bomb");
        }

        foreach (CatchGhost ghost in ghosts)
        {
            ghost.StopGame();
        }

        isPlaying = false;

        int highScore = PlayerPrefs.GetInt("CatchGhostHighScore", score);
        if (highScore < score) highScore = score;
        PlayerPrefs.SetInt("CatchGhostHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    public void StartGame()
    {
        objWait.SetActive(false);

        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].Hide();
            ghosts[i].SetIndex(i);
        }
        currentGhost.Clear();
        remainTime = startTime;
        score = 0;
        txtScore.text = score.ToString();
        isPlaying = true;
    }

    public void AddScore(int ghostIndex)
    {
        score++;
        remainTime++;
        txtScore.text = score.ToString();
        currentGhost.Remove(ghosts[ghostIndex]);
    }

    public void Missed(int ghostIndex, bool isGhost)
    {
        if (isGhost)
        {
            remainTime -= 2;
        }
        currentGhost.Remove(ghosts[ghostIndex]);
    }

    public void Continue()
    {
        objWait.SetActive(false);

        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].Hide();
            ghosts[i].SetIndex(i);
        }
        currentGhost.Clear();
        remainTime = startTime;
        txtScore.text = score.ToString();
        isPlaying = true;
    }
}
