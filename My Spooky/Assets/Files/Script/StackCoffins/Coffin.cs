using UnityEngine;

public class Coffin : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float moveSpeed;

    [SerializeField] Rigidbody2D rigidBody;

    private bool isMove;
    private bool isLanded;
    private bool isDropped;
    private bool dirRight = true;


    private void Start()
    {
        isDropped = false;
        isMove = true;
        rigidBody.gravityScale = 0;
    }

    private void Update()
    {
        if (!isDropped) rigidBody.gravityScale = 0;
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDropped) return;

        if (collision.gameObject.CompareTag("Finish"))
        {
            CoffinGM.instance.GameOver();
            CoffinGM.instance.RemoveCoffin(transform);
            Destroy(this.gameObject);
        }
        else if (!isLanded)
        {
            CoffinGM.instance.AddCoffin(transform);
            isLanded = true;
            if (transform.position.y >= 0)
            {
                CoffinGM.instance.MoveCam();
            }

            CoffinGM.instance.CoffinLanded();
        }
    }

    private void Move()
    {
        if (isMove)
        {
            if (dirRight)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= maxX)
            {
                dirRight = false;
            }

            if (transform.position.x <= minX)
            {
                dirRight = true;
            }
        }
    }


    public void Drop()
    {
        isDropped = true;
        transform.SetParent(null);
        isMove = false;
        rigidBody.gravityScale = 3;
    }
}
