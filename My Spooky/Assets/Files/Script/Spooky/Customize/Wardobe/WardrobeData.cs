using UnityEngine;

[CreateAssetMenu(fileName = "WardrobeData", menuName = "ScriptableObjects/WardrobeData")]

public class WardrobeData : ScriptableObject
{
    [Header("Stats")]
    public float sleep;
    public float clean;

    [Header("Body")]
    public Sprite horns;
    public Sprite eyelashes;
    public Sprite eyes;
    public Sprite handband;
    public Sprite bodyColor;
    public Sprite helmet;
    public Sprite eyeglasses;
    public Sprite neckband;
    public Sprite bodyPatterns;
    public Sprite cape;
}
