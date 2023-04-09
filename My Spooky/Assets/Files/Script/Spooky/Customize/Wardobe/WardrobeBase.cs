using TMPro;
using System;
using UnityEngine;

public class WardrobeBase : MonoBehaviour
{
    public Action<WardrobeItem> OnItemClick;

    [SerializeField] protected WardrobeItem[] wardrobeItems;
    protected WardrobeItem wardrobeItem;
    private GameManager gameManager;
    private Camera cam;
    [SerializeField] TMP_Text txtCoin;

    [Header("Select Item")]
    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtCost;
    [SerializeField] TMP_Text txtLevel;
    [SerializeField] GameObject objBuy;
    [SerializeField] GameObject objNotBuy;
    [SerializeField] GameObject objSelect;
    [SerializeField] GameObject objNotSelect;
    protected int currItem;


    private void Start()
    {
        gameManager = GameManager.instance;
        cam = Camera.main;
        txtCoin.text = gameManager.coin.ToString();
        OnItemClicked(wardrobeItems[0]);
        OnItemClick += OnItemClicked;
    }

    private void OnDestroy()
    {
        OnItemClick -= OnItemClicked;
    }


    private void OnItemClicked(WardrobeItem item)
    {
        foreach (var wardrobeItem in wardrobeItems)
        {
            if (wardrobeItem == item)
            {
                currItem = 0;
                wardrobeItem.Selected();
                this.wardrobeItem = wardrobeItem;
                SelectItem();
            }
            else
            {
                wardrobeItem.Normal();
            }
        }
    }

    private void SelectItem()
    {
        txtName.text = wardrobeItem.itemData.itemsData[currItem].name;
        txtLevel.text = $"Locked {currItem}";
        txtCost.text = wardrobeItem.itemData.itemsData[currItem].cost.ToString();
        objSelect.SetActive(wardrobeItem.itemData.itemsData[currItem].isSelect);
        objNotSelect.SetActive(!wardrobeItem.itemData.itemsData[currItem].isSelect);
        objBuy.SetActive(wardrobeItem.itemData.itemsData[currItem].isPurchased);
        objNotBuy.SetActive(!wardrobeItem.itemData.itemsData[currItem].isPurchased);

        TryItem();
    }

    protected virtual void TryItem()
    {

    }

    protected virtual void SetItem()
    {

    }

    public void ChangeCoin(int amount)
    {
        gameManager.AddCoin(-amount, cam.ScreenToWorldPoint(Input.mousePosition), txtCoin.transform.position);
        txtCoin.text = gameManager.coin.ToString();
    }

    public void ButtonLeft()
    {
        currItem--;
        if (currItem < 0) currItem = wardrobeItem.itemData.itemsData.Length - 1;
        SelectItem();
    }

    public void ButtonRight()
    {
        currItem++;
        if (currItem > wardrobeItem.itemData.itemsData.Length - 1) currItem = 0;
        SelectItem();
    }

    public void ButtonSelect()
    {
        if (wardrobeItem.itemData.itemsData[currItem].isSelect)
        {
            for (int i = 0; i < wardrobeItem.itemData.itemsData.Length; i++)
            {
                wardrobeItem.itemData.itemsData[i].isSelect = false;
            }

            wardrobeItem.itemData.itemsData[0].isSelect = true;
            SetItem();
        }
        else
        {
            for (int i = 0; i < wardrobeItem.itemData.itemsData.Length; i++)
            {
                wardrobeItem.itemData.itemsData[i].isSelect = false;
            }

            wardrobeItem.itemData.itemsData[currItem].isSelect = true;
            SetItem();
        }

        SelectItem();
    }

    public void ButtonBuy()
    {
        if (gameManager.coin < wardrobeItem.itemData.itemsData[currItem].cost)
        {
            Msg.instance.Show("Not Enouth Coin", Color.red);
            return;
        }

        ChangeCoin(wardrobeItem.itemData.itemsData[currItem].cost);
        wardrobeItem.itemData.itemsData[currItem].isPurchased = true;
        SelectItem();
    }
}
