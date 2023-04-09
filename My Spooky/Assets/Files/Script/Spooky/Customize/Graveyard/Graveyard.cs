using UnityEngine;
using UnityEngine.UI;

public class Graveyard : WardrobeBase
{
    [SerializeField] Image imgBG;
    [SerializeField] Image imgGrave;
    [SerializeField] Image imgMoon;

    [SerializeField] GraveyardData graveyardData;


    protected override void TryItem()
    {
        base.TryItem();
        switch (wardrobeItem.name)
        {
            case "Background":
                imgBG.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Coffin":
                imgGrave.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Moon":
                imgMoon.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
        }
    }

    protected override void SetItem()
    {
        base.SetItem();

        switch (wardrobeItem.name)
        {
            case "Background":
                graveyardData.bg = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Coffin":
                graveyardData.grave = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Moon":
                graveyardData.moon = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

        }
        // UnityEditor.EditorUtility.SetDirty(graveyardData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }
}
