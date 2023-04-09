using UnityEngine;
using System.Collections.Generic;

public class CoffinGM : Singleton<CoffinGM>, IGamesGM
{
    [SerializeField] TMPro.TMP_Text txtScore;
    [SerializeField] GameObject objWait;
    [SerializeField] CoffinCam cam;
    [SerializeField] CoffinSpawner spawner;

    private List<Transform> coffins;

    private Coffin currCoffin;
    private int score;
    private bool isGameOver;

    private void Start()
    {
        coffins = new List<Transform>();

        isGameOver = false;
        score = 0;
        txtScore.text = score.ToString();
    }

    private void Update()
    {
        if (!isGameOver && Input.GetMouseButtonDown(0))
        {
            if (objWait.activeInHierarchy)
            {
                objWait.SetActive(false);
                SpwanCoffin();
                return;
            }
            if (currCoffin) currCoffin.Drop();
        }
    }

    private void SpwanCoffin()
    {
        cam.targetPos.y = LeastCoffin();
        currCoffin = spawner.Spwan();
    }

    private void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
    }

    private float LeastCoffin()
    {
        float dist = 0;
        for (int i = 0; i < coffins.Count; i++)
        {
            if (dist < coffins[i].position.y)
            {
                dist = coffins[i].position.y;
            }
        }

        return dist;
    }

    private void RemoveSpawningCoffins()
    {
        foreach (Transform child in spawner.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void AddCoffin(Transform item)
    {
        coffins.Add(item);
    }

    public void RemoveCoffin(Transform item)
    {
        coffins.Remove(item);
    }

    public void MoveCam()
    {
        cam.targetPos.y += 1;
    }

    public void CoffinLanded()
    {
        currCoffin = null;
        AddScore();
        Invoke(nameof(SpwanCoffin), 2);
    }

    public void GameOver()
    {
        RemoveSpawningCoffins();
        isGameOver = true;
        CancelInvoke();
        int highScore = PlayerPrefs.GetInt("CoffinsHighScore", score);
        if (highScore < score) highScore = score;
        PlayerPrefs.SetInt("CoffinsHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    public void Continue()
    {
        objWait.SetActive(true);
        isGameOver = false;
        cam.targetPos.y = LeastCoffin();
    }
}
