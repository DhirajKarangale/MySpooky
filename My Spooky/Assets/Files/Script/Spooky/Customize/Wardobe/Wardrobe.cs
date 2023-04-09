using UnityEngine;

public class Wardrobe : WardrobeBase
{
    [SerializeField] WardrobeData spookyData;
    [SerializeField] Character character;

    protected override void SetItem()
    {
        base.SetItem();
        switch (wardrobeItem.name)
        {
            case "HornsItem":
                spookyData.horns = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "EyelashesItem":
                spookyData.eyelashes = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "EyesItem":
                spookyData.eyes = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "HandbandItem":
                spookyData.handband = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "BodycolorItem":
                spookyData.bodyColor = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "HelmetItem":
                spookyData.helmet = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "EyeglassesmaskItem":
                spookyData.eyeglasses = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "NeckbandItem":
                spookyData.neckband = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "BodypatternsItem":
                spookyData.bodyPatterns = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "CapeItem":
                spookyData.cape = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
        }

        // UnityEditor.EditorUtility.SetDirty(spookyData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }

    protected override void TryItem()
    {
        base.TryItem();
        switch (wardrobeItem.name)
        {
            case "BodypatternsItem":
                character.imgBody.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "EyelashesItem":
                character.imgEyebroLeft.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                character.imgEyebroRight.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "EyesItem":
                character.imgEyeLeft.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                character.imgEyeRight.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "EyeglassesmaskItem":
                character.imgSpecs.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "HelmetItem":
                character.imgMouth.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "NeckbandItem":
                character.imgNecklace.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "HandbandItem":
                character.imgHandLeft.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                character.imgHandRight.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "HornsItem":
                character.imgHornLeft.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                character.imgHornRight.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

            case "CapeItem":
                character.imgCapeLeft.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                character.imgCapeRight.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
        }
    }
}