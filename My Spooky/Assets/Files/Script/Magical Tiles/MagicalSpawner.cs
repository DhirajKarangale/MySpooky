using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicalSpawner : MonoBehaviour
{
    [SerializeField] int initCount;
    [SerializeField] MagicalTiles tilesPrefab;
    [SerializeField] Transform[] tilesBG;

    private bool[] isUsed;

    private List<MagicalTiles> tiles;
    private int index;
    // internal float spwanPerSec;
    internal float spwanRate;
    internal float currSpeed;
    internal float speed;

    private void Start()
    {
        index = 0;
        currSpeed = 0.4f;
        tiles = new List<MagicalTiles>();
        isUsed = new bool[tilesBG.Length];
        ResetUsed();
        InitTiles();
    }


    private IEnumerator IESetUsed(int index)
    {
        yield return new WaitForSeconds(2);
        isUsed[index] = false;
    }

    private IEnumerator IESpwanTiles()
    {
        while (true)
        {
            // foreach (Transform tileBG in tilesBG)
            // {
            //     if (spwanPerSec > Random.value)
            //     {
            //         MagicalTiles tile = tiles[index];
            //         index = (index + 1) % initCount;
            //         tile.Set(tileBG, speed);
            //     }
            // }

            for (int i = 0; i < tilesBG.Length; i++)
            {
                int x = Random.Range(0, tilesBG.Length);
                if (!isUsed[x])
                {
                    Transform tileBG = tilesBG[x];
                    MagicalTiles tile = tiles[index];
                    index = (index + 1) % initCount;
                    if (tile) tile.Set(tileBG, speed);
                    isUsed[x] = true;
                    StartCoroutine(IESetUsed(x));
                    break;
                }
            }

            yield return new WaitForSeconds(spwanRate);
        }
    }


    private void InitTiles()
    {
        for (int i = 0; i < initCount; i++)
        {
            MagicalTiles tile = Instantiate(tilesPrefab);
            tile.gameObject.SetActive(false);
            tile.transform.SetParent(transform);
            tiles.Add(tile);
        }
    }

    private void ResetUsed()
    {
        for (int i = 0; i < isUsed.Length; i++)
        {
            isUsed[i] = false;
        }
    }


    public void StartGame()
    {
        ResetUsed();
        foreach (MagicalTiles tile in tiles)
        {
            tile.Moveable();
        }
        StartCoroutine(IESpwanTiles());
    }

    public void StopGame()
    {
        ResetUsed();
        StopAllCoroutines();
        foreach (MagicalTiles tile in tiles)
        {
            tile.Stop();
        }
    }
}
