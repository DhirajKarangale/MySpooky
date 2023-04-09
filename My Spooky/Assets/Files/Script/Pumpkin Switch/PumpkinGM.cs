using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinGM : Singleton<PumpkinGM>, IGamesGM
{
    [Header("UI")]
    [SerializeField] TMPro.TMP_Text txtScore;
    [SerializeField] GameObject waitObj;

    [Header("PS")]
    [SerializeField] ParticleSystem psOrange;
    [SerializeField] ParticleSystem psPurple;

    [Header("Pumpkin")]
    [SerializeField] Pumpkin orangePrefab;
    [SerializeField] Pumpkin purplePrefab;

    [Header("Attributes")]
    [SerializeField] float delay;
    [SerializeField] float speed;
    [SerializeField] int pumpkinCount;

    internal GameOverUI gameOverUI;

    private int score;
    private int currPumpkin;
    public bool isThrowAllow;
    private List<Pumpkin> orangeList;
    private List<Pumpkin> purpleList;

    private void Start()
    {
        score = 0;
        txtScore.text = score.ToString();
        currPumpkin = 0;
        orangeList = new List<Pumpkin>();
        purpleList = new List<Pumpkin>();

        isThrowAllow = false;
        gameOverUI = GameOverUI.instance;

        ChangeDifficulty();
        SpwanPumpkin();
    }

    public IEnumerator IEThrowPumpkin(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (isThrowAllow)
        {
            Pumpkin orange = orangeList[currPumpkin];
            Pumpkin purple = purpleList[currPumpkin];

            if (Random.value > 0.5f)
            {
                orange.ResetPos(-1.5f, Random.Range(8, 15), speed);
                purple.ResetPos(1.5f, Random.Range(12, 19), speed);
            }
            else
            {
                orange.ResetPos(1.5f, Random.Range(8, 15), speed);
                purple.ResetPos(-1.5f, Random.Range(12, 19), speed);
            }

            orange.Throw();
            purple.Throw();
            currPumpkin = (currPumpkin + 1) % pumpkinCount;
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
        }

        StartCoroutine(IEThrowPumpkin(this.delay));
    }


    private void SpwanPumpkin()
    {
        for (int i = 0; i < pumpkinCount; i++)
        {
            Pumpkin pumpkin = Instantiate(orangePrefab);
            pumpkin.gameObject.SetActive(false);
            pumpkin.transform.SetParent(transform);
            pumpkin.transform.localScale = Vector3.one * 0.55f;
            orangeList.Add(pumpkin);

            pumpkin = Instantiate(purplePrefab);
            pumpkin.gameObject.SetActive(false);
            pumpkin.transform.SetParent(transform);
            pumpkin.transform.localScale = Vector3.one * 0.55f;
            purpleList.Add(pumpkin);
        }
    }

    private void ChangeDifficulty()
    {
        switch (score)
        {
            case < 10:
                delay = 4f;
                speed = 2;
                break;
            case < 20:
                delay = 2f;
                speed = 1.6f;
                break;
            case < 30:
                delay = 1.5f;
                speed = 1.5f;
                break;
            case < 45:
                delay = 1.3f;
                speed = 1f;
                break;
        }
    }

    private void ResetAllPumpkin()
    {
        foreach (Pumpkin pumpkin in orangeList)
        {
            pumpkin.ResetPos(1.5f, 10, speed);
        }

        foreach (Pumpkin pumpkin in purpleList)
        {
            pumpkin.ResetPos(-1.5f, 10, speed);
        }
    }

    public void ResetPumpkin(Pumpkin pumpkin)
    {
        pumpkin.ResetPos(1.5f, 10, speed);
    }


    public void PlayOrangePS(Vector3 pos)
    {
        psOrange.transform.localPosition = pos;
        psOrange.Play();
    }

    public void PlayPurplePS(Vector3 pos)
    {
        psPurple.transform.localPosition = pos;
        psPurple.Play();
    }

    public void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
        ChangeDifficulty();
    }

    public void ResetThrow()
    {
        StopAllCoroutines();
        StartCoroutine(IEThrowPumpkin(0));
    }

    public void GameOver()
    {
        ResetAllPumpkin();
        isThrowAllow = false;
        int highScore = PlayerPrefs.GetInt("PumpkinHighScore", 0);
        if (score > highScore) highScore = score;
        PlayerPrefs.SetInt("PumpkinHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        txtScore.text = score.ToString();
        waitObj.SetActive(true);
        delay += 0.5f;
        speed += 0.2f;
    }
}
