using UnityEngine;

public class SmashBat : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;


    private void OnMouseDown()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("Dye");
        SmashGM.instance.GameOver();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
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
