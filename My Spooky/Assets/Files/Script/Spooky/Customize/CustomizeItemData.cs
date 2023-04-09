using UnityEngine;

[CreateAssetMenu(fileName = "CustomizeItemData", menuName = "ScriptableObjects/CustomizeItemData")]

public class CustomizeItemData : ScriptableObject
{
    [SerializeField] internal ItemData[] itemsData;
}

[System.Serializable]
public struct ItemData
{
    public Sprite sprite;
    public string name;
    public int cost;
    public int count;
    public bool isPurchased;
    public bool isSelect;
}
