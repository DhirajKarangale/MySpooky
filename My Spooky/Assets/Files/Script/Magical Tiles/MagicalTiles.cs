using UnityEngine;

public class MagicalTiles : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] UnityEngine.UI.Button button;
    [SerializeField] Rigidbody2D rigidBody;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
            if (button.interactable)
            {
                MagicalGM.instance.GameOver();
            }
            else
            {
                MagicalGM.instance.AddScore();
                CancelInvoke();
                Invoke(nameof(Desable), 1);
            }
        }
    }


    private void Desable()
    {
        gameObject.SetActive(false);
    }


    public void Moveable()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        rigidBody.AddForce(Vector3.down);
    }

    public void Stop()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Set(Transform parent, float speed)
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.drag = 1 / speed;
        rigidBody.isKinematic = false;
        gameObject.SetActive(true);
        button.interactable = true;
        transform.SetParent(parent);
        // transform.localPosition = new Vector3(-5, 2200, 0);
        transform.localPosition = new Vector3(-5, Random.Range(2000, 3000), 0);
        transform.localScale = Vector3.one;
    }

    public void Button()
    {
        MagicalGM.instance.PlayPS(transform.position);
        audioSource.Play();
        button.interactable = false;
    }
}
