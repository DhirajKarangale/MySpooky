using UnityEngine;

public class CoffinSpawner : MonoBehaviour
{
    [SerializeField] Coffin coffinRedPrefab;
    [SerializeField] Coffin coffinGreenPrefab;

    public Coffin Spwan()
    {
        Coffin coffin;
        if (Random.value > 0.5f) coffin = Instantiate(coffinRedPrefab);
        else coffin = Instantiate(coffinGreenPrefab);
        coffin.transform.SetParent(transform);
        coffin.transform.localPosition = Vector3.zero;
        return coffin;
    }
}
