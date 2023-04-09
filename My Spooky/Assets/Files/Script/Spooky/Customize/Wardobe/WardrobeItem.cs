using UnityEngine;
using UnityEngine.UI;

public class WardrobeItem : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] WardrobeBase wardrobe;
    [SerializeField] GameObject objTriangle;

    [Header("UI")]
    [SerializeField] Image imgBG;
    [SerializeField] Image imgItem;

    [Header("Sprites")]
    [SerializeField] Sprite spriteNormal;
    [SerializeField] Sprite spriteSelected;

    [Header("Items")]
    public CustomizeItemData itemData;
    // public Sprite[] sprites;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        // for (int i = 0; i < itemData.itemsData.Length; i++)
        // {
        //     itemData.itemsData[i].sprite = sprites[i];
        //     itemData.itemsData[i].name = name + i;
        //     itemData.itemsData[i].cost = (10 * (i + 1));
        // }
        // UnityEditor.EditorUtility.SetDirty(itemData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }


    public void Normal()
    {
        imgBG.sprite = spriteNormal;
        objTriangle.SetActive(false);
    }

    public void Selected()
    {
        imgBG.sprite = spriteSelected;
        objTriangle.SetActive(true);
    }

    public void ButtonClick()
    {
        wardrobe.OnItemClick?.Invoke(this);
    }
}


