using System;
using UnityEngine;

public class BG : MonoBehaviour
{
    public Action<BGItem> OnBuyClick;
    public Action<BGItem> OnSelectClick;

    private GameManager gameManager;
    private Camera cam;

    [SerializeField] CustomizeItemData itemData;
    [SerializeField] BGData data;
    [SerializeField] TMPro.TMP_Text txtCoin;
    // [SerializeField] Sprite[] sprites;
    [SerializeField] BGItem[] bgItems;


    private void Start()
    {
        cam = Camera.main;
        gameManager = GameManager.instance;
        txtCoin.text = gameManager.coin.ToString();
        SetData();
        OnBuyClick += BuyButton;
        OnSelectClick += SelectButton;
        // LoadData();
    }

    private void OnDestroy()
    {
        OnBuyClick -= BuyButton;
        OnSelectClick -= SelectButton;
    }

    private void SetData()
    {
        for (int i = 0; i < itemData.itemsData.Length; i++)
        {
            bgItems[i].SetData(itemData.itemsData[i]);
        }
    }

    // private void LoadData()
    // {
    //     for (int i = 0; i < sprites.Length; i++)
    //     {
    //         itemData.itemsData[i].sprite = sprites[i];
    //         itemData.itemsData[i].cost = 10 * (i + 1);
    //         itemData.itemsData[i].name = "BG" + (i + 1);
    //     }
    // }

    private void ChangeCoin(int amount)
    {
        gameManager.AddCoin(-amount, cam.ScreenToWorldPoint(Input.mousePosition), txtCoin.transform.position);
        txtCoin.text = gameManager.coin.ToString();
    }

    private void BuyButton(BGItem bgItem)
    {
        for (int i = 0; i < bgItems.Length; i++)
        {
            if (bgItems[i] == bgItem)
            {
                if (gameManager.coin < bgItems[i].cost)
                {
                    Msg.instance.Show("Not Enough Coin", Color.red);
                    return;
                }
                else
                {
                    ChangeCoin(bgItems[i].cost);
                    itemData.itemsData[i].isPurchased = true;
                    bgItem.SetData(itemData.itemsData[i]);
                }
            }
        }
    }

    private void SelectButton(BGItem bgItem)
    {
        for (int i = 0; i < bgItems.Length; i++)
        {
            if (bgItems[i] == bgItem)
            {
                if (itemData.itemsData[i].isSelect)
                {
                    for (int j = 0; j < itemData.itemsData.Length; j++)
                    {
                        itemData.itemsData[j].isSelect = false;
                        bgItems[j].SetData(itemData.itemsData[j]);
                    }
                    itemData.itemsData[0].isSelect = true;
                    data.sprite = itemData.itemsData[0].sprite;
                }
                else
                {
                    for (int j = 0; j < itemData.itemsData.Length; j++)
                    {
                        itemData.itemsData[j].isSelect = false;
                        bgItems[j].SetData(itemData.itemsData[j]);
                    }
                    itemData.itemsData[i].isSelect = true;
                    data.sprite = itemData.itemsData[i].sprite;
                }
            }
            bgItems[i].SetData(itemData.itemsData[i]);
        }

        // UnityEditor.EditorUtility.SetDirty(data);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }
}
