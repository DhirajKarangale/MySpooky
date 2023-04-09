using UnityEngine;
using UnityEngine.UI;

public class ShowerRoom : WardrobeBase
{
    [SerializeField] Sprite[] spritesCap;
    [SerializeField] Image imgMachine;
    [SerializeField] Image imgBottel;
    [SerializeField] Image imgCap;
    [SerializeField] Image imgDryer;

    [SerializeField] ShowerRoomData showerRoomData;


    protected override void TryItem()
    {
        base.TryItem();
        switch (wardrobeItem.name)
        {
            case "WashingMachine":
                imgMachine.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Detergent":
                imgBottel.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                imgCap.sprite = spritesCap[currItem];
                break;
            case "Dryer":
                imgDryer.sprite = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
        }
    }

    protected override void SetItem()
    {
        base.SetItem();

        switch (wardrobeItem.name)
        {
            case "WashingMachine":
                showerRoomData.machine = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;
            case "Detergent":
                showerRoomData.bottel = wardrobeItem.itemData.itemsData[currItem].sprite;
                showerRoomData.cap = spritesCap[currItem];
                break;
            case "Dryer":
                showerRoomData.dryer = wardrobeItem.itemData.itemsData[currItem].sprite;
                break;

        }
        // UnityEditor.EditorUtility.SetDirty(showerRoomData);
        // UnityEditor.AssetDatabase.CreateAsset(material, "Assets/Artifacts/" + materialName);
        // UnityEditor.AssetDatabase.SaveAssets();
    }
}
