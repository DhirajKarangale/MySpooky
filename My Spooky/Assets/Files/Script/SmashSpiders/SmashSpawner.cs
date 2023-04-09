using System.Collections;
using UnityEngine;

public class SmashSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spwanPoints;
    [SerializeField] SmashSpider spiderPrefab;
    [SerializeField] SmashBat batPrefab;

    internal float speed;
    internal float batRate;
    internal float spwanRate;


    private void Start()
    {
        StartGame();
    }


    private IEnumerator IESpwan()
    {
        while (true)
        {
            Spwan();
            yield return new WaitForSeconds(spwanRate);
        }
    }


    private void Spwan()
    {
        Transform spwanPoint = spwanPoints[Random.Range(0, spwanPoints.Length)];
        if (batRate > Random.value)
        {
            SmashBat bat = Instantiate(batPrefab);
            bat.Set(spwanPoint, speed);
        }
        else
        {
            SmashSpider spider = Instantiate(spiderPrefab);
            spider.Set(spwanPoint, speed);
        }
    }



    public void GameOver()
    {
        StopAllCoroutines();

        speed = 0.3f;
        batRate = 0.25f;
        spwanRate = 2f;

        for (int i = 0; i < spwanPoints.Length; i++)
        {
            foreach (Transform child in spwanPoints[i].transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(IESpwan());
    }
}
