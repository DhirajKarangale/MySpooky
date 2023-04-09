using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] internal int initialSize;
    [SerializeField] Transform bodyPrefab;
    [SerializeField] SnakeFood food;

    private SnakeGM snakeGM;
    private List<Transform> segments;
    private Vector2 direction;

    private void Start()
    {
        segments = new List<Transform>();
        direction = Vector2.right;
        snakeGM = SnakeGM.instance;
        ResetState();
    }

    private IEnumerator IEMove()
    {
        while (true)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            float x = Mathf.Round(transform.position.x) + direction.x;
            float y = Mathf.Round(transform.position.y) + direction.y;
            transform.position = new Vector2(x, y);

            yield return new WaitForSeconds(Time.fixedDeltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Food"))
        {
            AddSegment();
            food.RandamizePos();
            snakeGM.AddScore();
        }
        else if (collider.CompareTag("Finish"))
        {
            snakeGM.GameOver();
        }
    }


    private void AddSegment()
    {
        Transform segment = Instantiate(bodyPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void SwitchMat(Material mat)
    {
        foreach (var item in segments)
        {
            SpriteRenderer renderer = item.GetComponent<SpriteRenderer>();
            renderer.material = mat;
        }
    }


    public void ResetState()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            AddSegment();
        }

        StartCoroutine(IEMove());
    }

    public int LastSize()
    {
        return segments.Count;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void ButtonUp()
    {
        direction = Vector2.up;
    }

    public void ButtonDown()
    {
        direction = Vector2.down;
    }

    public void ButtonLeft()
    {
        direction = Vector2.left;
    }

    public void ButtonRight()
    {
        direction = Vector2.right;
    }
}
