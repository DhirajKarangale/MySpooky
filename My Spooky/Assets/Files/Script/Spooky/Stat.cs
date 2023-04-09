using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected BottomItem item;
    [SerializeField] protected float minValue;

    internal float amount;

    private void Awake()
    {
        amount = PlayerPrefs.GetFloat("amount", 100);
    }


    public void Change(float value)
    {
        if ((amount < 0) || (amount > 100)) return;
        amount -= value;
        item.UpdateStat(amount);
        if (amount < minValue) ChangeState();
    }

    public virtual void ChangeState()
    {

    }
}
