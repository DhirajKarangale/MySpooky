using UnityEngine;
using System.Collections;

public class MagicalGM : Singleton<MagicalGM>, IGamesGM
{
    public MagicalSpawner spawner;

    [SerializeField] ParticleSystem[] psTags;
    [SerializeField] ParticleSystem psMusic;
    [SerializeField] AudioSource musicSource;
    [SerializeField] GameObject objWait;
    [SerializeField] TMPro.TMP_Text txtScore;

    private int score;
    private bool isChangeDifficulty;
    private bool isGameOverAllow;


    private void Start()
    {
        isChangeDifficulty = false;
        isGameOverAllow = true;
        score = 0;
        txtScore.text = score.ToString();
        Difficulty();
        WaitState();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objWait.activeInHierarchy)
        {
            ResumeState();
            spawner.StartGame();
            musicSource.Play();
        }
    }

    private IEnumerator IEResetChangeDiff()
    {
        yield return new WaitForSecondsRealtime(5);
        isChangeDifficulty = false;
    }

    private IEnumerator IEAllowGameOver()
    {
        yield return new WaitForSecondsRealtime(1);
        isGameOverAllow = true;
    }


    private void WaitState()
    {
        isGameOverAllow = false;
        Time.timeScale = 0;
        objWait.SetActive(true);
    }

    private void ResumeState()
    {
        Time.timeScale = 1;
        objWait.SetActive(false);
        StartCoroutine(IEAllowGameOver());
    }

    private void Difficulty()
    {
        if (isChangeDifficulty) return;


        if ((score > 0) && (score % 80) == 0)
        {
            // Debug.Log("score mod 100 : " + score);
            spawner.currSpeed = Mathf.Clamp(spawner.speed + 0.2f, 0.1f, 1f);
        }
        spawner.speed = spawner.currSpeed;

        switch (score)
        {
            case < 5:
                // spawner.speed = 0.4f;
                spawner.spwanRate = 1.5f;
                // spawner.spwanPerSec = 0.1f;
                break;
            case < 10:
                // spawner.speed = 0.3f;
                spawner.spwanRate = 1.3f;
                // spawner.spwanPerSec = 0.14f;
                break;
            case < 15:
                // spawner.speed = 0.35f;
                spawner.spwanRate = 1.1f;
                // spawner.spwanPerSec = 0.17f;
                break;
            case < 20:
                // spawner.speed = 0.4f;
                spawner.spwanRate = 1f;
                // spawner.spwanPerSec = 0.2f;
                break;
            case < 30:
                // spawner.speed = 0.5f;
                spawner.spwanRate = 0.7f;
                // spawner.spwanPerSec = 0.25f;
                break;

            default:
                // spawner.speed = 0.55f;
                spawner.spwanRate = 0.5f;
                // spawner.spwanPerSec = 0.3f;
                break;
        }
    }


    public void PlayPS(Vector3 pos)
    {
        int tag = Random.Range(0, psTags.Length);
        psTags[tag].transform.position = pos;
        psTags[tag].Play();
        psMusic.transform.position = pos;
        psMusic.Play();
    }

    public void AddScore()
    {
        score++;
        // score += 100;
        txtScore.text = score.ToString();
        Difficulty();
    }

    public void GameOver()
    {
        if (!isGameOverAllow) return;

        musicSource.Pause();
        spawner.StopGame();
        int highScore = PlayerPrefs.GetInt("MagicalHighScore", score);
        if (highScore < score) highScore = score;
        PlayerPrefs.SetInt("MagicalHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }


    public void Continue()
    {
        isChangeDifficulty = true;
        StartCoroutine(IEResetChangeDiff());
        spawner.spwanRate = Mathf.Clamp(spawner.spwanRate + 2f, 1f, 6f);
        spawner.speed = Mathf.Clamp(spawner.speed - 0.1f, 0.1f, 0.6f);
        WaitState();
    }
}
