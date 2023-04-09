using UnityEngine;

public class SnakeFood : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    private Bounds bounds;

    private void Start()
    {
        bounds = boxCollider.bounds;
        RandamizePos();
    }


    public void RandamizePos()
    {
        int minX = Mathf.RoundToInt(bounds.min.x);
        int maxX = Mathf.RoundToInt(bounds.max.x);

        int minY = Mathf.RoundToInt(bounds.min.y);
        int maxY = Mathf.RoundToInt(bounds.max.y);

        int xPos = Mathf.RoundToInt(Random.Range(minX, maxX));
        int yPos = Mathf.RoundToInt(Random.Range(minY, maxY));

        transform.position = new Vector3(xPos, yPos, 0);
    }
}
