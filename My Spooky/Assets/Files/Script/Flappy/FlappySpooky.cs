using UnityEngine;

public class FlappySpooky : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpForce;
    private FlappyGM flappyGM;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        animator.Play("Play");
        flappyGM = FlappyGM.instance;
    }

    private void Update()
    {
        if (flappyGM.state == FlappyGM.State.Playing && Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
    }

    public void GameOver()
    {
        animator.Play("Dye");
    }

    public void Reset()
    {
        transform.position = startPos;
        animator.Play("Play");
    }
}
