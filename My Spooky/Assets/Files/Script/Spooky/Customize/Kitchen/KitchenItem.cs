using TMPro;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] Kitchen kitchen;
    [SerializeField] TMP_Text txtCost;
    [SerializeField] TMP_Text txtCount;
    [SerializeField] GameObject objCount;
    [SerializeField] UnityEngine.UI.Image image;
    internal int count;
    internal int cost;


    public void SetData(ItemData itemData)
    {
        this.cost = itemData.cost;
        this.count = itemData.count;
        txtCost.text = cost.ToString();
        txtCount.text = count.ToString();
        objCount.SetActive(count > 0);
        image.sprite = itemData.sprite;
    }

    public void ButtonClick()
    {
        kitchen.OnItemClick?.Invoke(this);
    }
}
