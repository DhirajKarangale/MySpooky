using UnityEngine;
using System.Collections;

public class CatchGhost : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    private CatchGM gameManager;

    [SerializeField] Sprite spriteNormal;
    [SerializeField] Sprite spriteHard;
    [SerializeField] Sprite spriteHit;
    [SerializeField] Sprite spriteBomb;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    private float showDuration = 0.5f;
    private float duration = 1f;

    private bool isHitable;

    public enum GhostType { Standard, Hard, Bomb };
    private GhostType ghostType;
    private float hardRate = 0.25f;
    private float bombRate = 0f;
    private int lives;
    private int ghostIndex;

    private void Start()
    {
        isHitable = true;
        startPos = new Vector2(0, -2);
        endPos = Vector2.zero;
        boxOffset = boxCollider.offset;
        boxSize = boxCollider.size;
        boxOffsetHidden = new Vector2(boxOffset.x, startPos.y / 2f);
        boxOffsetHidden = new Vector2(boxSize.x, 0f);

        gameManager = CatchGM.instance;
    }

    private void OnMouseDown()
    {
        if (isHitable)
        {
            switch (ghostType)
            {
                case GhostType.Standard:
                    spriteRenderer.sprite = spriteHit;
                    gameManager.AddScore(ghostIndex);
                    StopAllCoroutines();
                    StartCoroutine(IEQuickHide());
                    isHitable = false;
                    break;

                case GhostType.Hard:
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = spriteNormal;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = spriteHit;
                        gameManager.AddScore(ghostIndex);
                        StopAllCoroutines();
                        StartCoroutine(IEQuickHide());
                        isHitable = false;
                    }
                    break;

                case GhostType.Bomb:
                    gameManager.GameOver(1);
                    break;
            }
        }
    }

    private IEnumerator IEShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;

        float elapsed = 0;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = end;
        boxCollider.offset = boxOffset;
        boxCollider.size = boxSize;

        yield return new WaitForSeconds(duration);

        elapsed = 0;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
        boxCollider.offset = boxOffsetHidden;
        boxCollider.size = boxSizeHidden;

        if (isHitable)
        {
            isHitable = false;
            gameManager.Missed(ghostIndex, ghostType != GhostType.Bomb);
        }
    }

    private IEnumerator IEQuickHide()
    {
        yield return new WaitForSeconds(0.25f);

        if (!isHitable) Hide();
    }


    private void CreateNext()
    {
        float random = Random.Range(0, 1f);
        if (random < bombRate)
        {
            ghostType = GhostType.Bomb;
            spriteRenderer.sprite = spriteBomb;
        }
        else
        {
            random = Random.Range(0, 1f);
            if (random < hardRate)
            {
                ghostType = GhostType.Hard;
                spriteRenderer.sprite = spriteHard;
                lives = 2;
            }
            else
            {
                ghostType = GhostType.Standard;
                spriteRenderer.sprite = spriteNormal;
                lives = 1;
            }
        }

        isHitable = true;
    }

    private void SetLevel(int level)
    {
        bombRate = Mathf.Min(level * 0.025f, 0.25f);
        hardRate = Mathf.Min(level * 0.025f, 1f);

        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = Random.Range(durationMin, durationMax);
    }


    public void Hide()
    {
        transform.localPosition = startPos;
        boxCollider.offset = boxOffsetHidden;
        boxCollider.size = boxSizeHidden;
    }

    public void SetIndex(int index)
    {
        ghostIndex = index;
    }

    public void StopGame()
    {
        isHitable = false;
        StopAllCoroutines();
    }

    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(IEShowHide(startPos, endPos));
    }
}
