using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BGItem : MonoBehaviour
{
    [SerializeField] BG bg;

    [SerializeField] Image image;
    [SerializeField] TMP_Text txtCost;
    [SerializeField] GameObject objBuyed;
    [SerializeField] GameObject objNotBuyed;
    [SerializeField] GameObject objOn;
    [SerializeField] GameObject objOff;
    internal int cost;

    public void SetData(ItemData itemData)
    {
        this.cost = itemData.cost;
        txtCost.text = cost.ToString();
        image.sprite = itemData.sprite;
        objBuyed.SetActive(!itemData.isPurchased);
        objNotBuyed.SetActive(itemData.isPurchased);
        objOn.SetActive(itemData.isSelect);
        objOff.SetActive(!itemData.isSelect);
    }

    public void ButtonBuy()
    {
        bg.OnBuyClick?.Invoke(this);
    }

    public void ButtonSelect()
    {
        bg.OnSelectClick?.Invoke(this);
    }
}
