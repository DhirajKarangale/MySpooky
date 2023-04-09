using UnityEngine;

public class FlappyDye : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            FlappyGM.instance.GameOver();
        }
    }
}
