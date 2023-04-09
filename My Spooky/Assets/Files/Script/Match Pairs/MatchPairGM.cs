using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemSprites
{
    public Sprite sprite;
    public int count;
}

public class MatchPairGM : Singleton<MatchPairGM>, IGamesGM
{
    [SerializeField] float mxTime;
    [SerializeField] MatchPairCard cardPrefab;
    [SerializeField] Transform cardsParent;
    [SerializeField] ItemSprites[] itemSprites;

    [SerializeField] TMP_Text txtTime;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] Slider slider;

    private MatchPairCard previousCard;
    private List<MatchPairCard> cards;
    private bool isBothCardActive;
    private float currTime;
    private int score;

    private void Start()
    {
        score = 0;
        isBothCardActive = false;
        txtScore.text = score.ToString();
        cards = new List<MatchPairCard>();

        ResetCards();
        SpwanCards();
        SetSprites();
        FlipAllCards();

        currTime = mxTime;
        StopAllCoroutines();
        StartCoroutine(IEUpdateTime());
    }


    private IEnumerator IEUpdateTime()
    {
        while (currTime > 0)
        {
            currTime -= Time.deltaTime;
            slider.value = currTime / mxTime;

            string minutes = Mathf.Floor(currTime / 60).ToString("00");
            string seconds = (currTime % 60).ToString("00");

            txtTime.text = string.Format("{0}:{1}S", minutes, seconds);
            yield return new WaitForEndOfFrame();
        }

        GameOver();
    }


    private void SpwanCards()
    {
        for (int i = 0; i < 8; i++)
        {
            MatchPairCard card = Instantiate(cardPrefab);
            card.Disable();
            card.transform.SetParent(cardsParent);
            card.transform.localScale = Vector3.one;
            cards.Add(card);
        }
    }

    private void SetSprites()
    {
        int counter = 0;
        while (counter < cards.Count)
        {
            ItemSprites currSprite = itemSprites[Random.Range(0, itemSprites.Length)];
            if (currSprite.count < 2)
            {
                cards[counter].spriteItem = currSprite.sprite;
                currSprite.count++;
                counter++;
            }
        }
    }

    private void ResetSprites()
    {
        foreach (MatchPairCard card in cards)
        {
            card.isItemFound = false;
        }

        FlipAllCards();
        ResetCards();

        foreach (ItemSprites item in itemSprites)
        {
            item.count = 0;
        }

        SetSprites();
    }

    private void ResetCards()
    {
        previousCard = null;
    }

    private void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("MatchPairHighScore", 0);
        if (score > highScore) highScore = score;
        PlayerPrefs.SetInt("MatchPairHighScore", highScore);
        GameOverUI.instance.Active(score, highScore);
    }

    private void AddScore()
    {
        score++;
        txtScore.text = score.ToString();

        if (score % 4 == 0)
        {
            Invoke(nameof(ResetSprites), 1);
        }
    }


    public void FlipAllCards()
    {
        foreach (MatchPairCard card in cards)
        {
            card.Disable();
        }
        ResetCards();
        isBothCardActive = false;
    }

    public void CardActive(MatchPairCard matchPairCard)
    {
        CancelInvoke();

        if (isBothCardActive)
        {
            FlipAllCards();
            matchPairCard.Active();
        }

        if (previousCard == null)
        {
            previousCard = matchPairCard;
            CancelInvoke();
            Invoke(nameof(FlipAllCards), 3);
        }
        else
        {
            isBothCardActive = true;
            if (matchPairCard.spriteItem.name == previousCard.spriteItem.name)
            {
                matchPairCard.isItemFound = true;
                previousCard.isItemFound = true;
                AddScore();
            }
            else
            {
                CancelInvoke();
                Invoke(nameof(FlipAllCards), 3);
            }
            ResetCards();
        }
    }

    public void Continue()
    {
        currTime = mxTime;
        StopAllCoroutines();
        StartCoroutine(IEUpdateTime());
    }
}
