using UnityEngine;

public class SmashSpider : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;


    private void OnMouseDown()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play($"Dye{Random.Range(1, 3)}");
        Destroy(gameObject, 10);
        SmashGM.instance.AddScore();
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
            SmashGM.instance.SpiderReached();
            Destroy(this.gameObject);
        }
    }


    public void Set(Transform parent, float speed)
    {
        rigidBody.drag = 1 / speed;
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }
}
