using System.Collections;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer srPumpkin;
    [SerializeField] SpriteRenderer srLine;
    private PumpkinGM gameManager;
    public bool isOrange;

    private void Start()
    {
        gameManager = PumpkinGM.instance;
    }

    public IEnumerator IEReduceAlpha()
    {
        while (srPumpkin.color.a > 0)
        {
            Color color = srPumpkin.color;
            color.a -= 0.02f;
            srPumpkin.color = color;
            srLine.color = color;
            yield return null;
        }
        gameManager.ResetPumpkin(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
            if (name.Contains(collider.gameObject.name))
            {
                if (isOrange) gameManager.PlayOrangePS(transform.localPosition);
                else gameManager.PlayPurplePS(transform.localPosition);
                gameObject.SetActive(false);
                gameManager.ResetPumpkin(this);

                // StopAllCoroutines();
                // StartCoroutine(IEReduceAlpha());
                gameManager.AddScore();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(IEReduceAlpha());

                // if (isOrange) gameManager.PlayOrangePS(transform.localPosition);
                // else gameManager.PlayPurplePS(transform.localPosition);
                // gameObject.SetActive(false);
                // gameManager.ResetPumpkin(this);
                gameManager.GameOver();
            }
        }
    }

    public void ResetPos(float x, float y, float drag)
    {
        rigidBody.drag = drag;
        srPumpkin.color = Color.white;
        srLine.color = Color.white;
        rigidBody.velocity = Vector2.zero;
        rigidBody.isKinematic = true;
        gameObject.SetActive(false);
        transform.localPosition = new Vector3(x, y, 0);
    }

    public void Throw()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.isKinematic = false;
        gameObject.SetActive(true);
    }
}
