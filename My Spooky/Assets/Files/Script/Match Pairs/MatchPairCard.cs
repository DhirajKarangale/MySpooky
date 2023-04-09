using UnityEngine;
using UnityEngine.UI;

public class MatchPairCard : MonoBehaviour
{
    internal bool isItemFound;
    [Header("Images")]
    [SerializeField] Image imgBG;
    [SerializeField] Image imgItem;

    [Header("Sprites")]
    [SerializeField] Sprite spriteActiveBG;
    [SerializeField] Sprite spriteDisableBG;
    [SerializeField] Sprite spriteDisable;
    [SerializeField] internal Sprite spriteItem;


    private void Start()
    {
        Disable();
    }


    public void Active()
    {
        if (isItemFound) return;
        imgBG.sprite = spriteActiveBG;
        imgItem.sprite = spriteItem;
    }

    public void Disable()
    {
        if (isItemFound) return;
        imgBG.sprite = spriteDisableBG;
        imgItem.sprite = spriteDisable;
    }


    public void OnClick()
    {
        if (isItemFound) return;

        if (imgBG.sprite == spriteActiveBG)
        {
            Disable();
            MatchPairGM.instance.FlipAllCards();
        }
        else
        {
            Active();
            MatchPairGM.instance.CardActive(this);
        }
    }
}
