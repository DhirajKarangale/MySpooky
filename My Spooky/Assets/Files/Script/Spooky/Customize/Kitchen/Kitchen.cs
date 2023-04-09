using System;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    public Action<KitchenItem> OnItemClick;
    private GameManager gameManager;
    private Camera cam;

    [SerializeField] CustomizeItemData itemData;
    // [SerializeField] KitchenData kitchenData;
    [SerializeField] TMPro.TMP_Text txtCoin;
    [SerializeField] KitchenItem[] kitchenItems;
    // [SerializeField] Sprite[] sprites;

    private void Start()
    {
        gameManager = GameManager.instance;
        cam = Camera.main;
        SetData();
        // LoadData();
        txtCoin.text = gameManager.coin.ToString();
        OnItemClick += OnItemClicked;
    }

    private void LoadData()
    {
        // for (int i = 0; i < sprites.Length; i++)
        // {
        //     itemData.itemsData[i].sprite = sprites[i];
        //     itemData.itemsData[i].cost = 10 * (i + 1);
        //     itemData.itemsData[i].name = "Food" + i;
        // }

        // UnityEditor.EditorUtility.SetDirty(itemData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }

    private void OnDestroy()
    {
        OnItemClick -= OnItemClicked;
    }

    private void ChangeCoin(int amount)
    {
        gameManager.AddCoin(-amount, cam.ScreenToWorldPoint(Input.mousePosition), txtCoin.transform.position);
        txtCoin.text = gameManager.coin.ToString();
    }

    private void SetData()
    {
        for (int i = 0; i < itemData.itemsData.Length; i++)
        {
            kitchenItems[i].SetData(itemData.itemsData[i]);
        }
    }

    private void OnItemClicked(KitchenItem item)
    {
        for (int i = 0; i < kitchenItems.Length; i++)
        {
            if (kitchenItems[i] == item)
            {
                if (gameManager.coin < kitchenItems[i].cost)
                {
                    Msg.instance.Show("Not Enough Coin", Color.red);
                    return;
                }
                else
                {
                    ChangeCoin(kitchenItems[i].cost);
                    itemData.itemsData[i].count++;
                    item.SetData(itemData.itemsData[i]);
                }
            }
        }

        // UnityEditor.EditorUtility.SetDirty(itemData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }
}
