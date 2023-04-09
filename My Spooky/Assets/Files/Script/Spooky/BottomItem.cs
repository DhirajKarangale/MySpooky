using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BottomItem : MonoBehaviour
{
    [SerializeField] Color colorRed;
    [SerializeField] Color colorGreen;
    [SerializeField] Color colorYellow;

    [SerializeField] Image imgBG;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text txtPercent;

    public Action OnClick;



    public void UpdateStat(float stat)
    {
        if (stat > 50)
        {
            imgBG.color = colorGreen;
        }
        else if (stat > 20)
        {
            imgBG.color = colorYellow;
        }
        else
        {
            imgBG.color = colorRed;
        }

        slider.value = stat / 100;
        txtPercent.text = stat.ToString("F0") + "%";
    }

    public void ButtonClick()
    {
        OnClick?.Invoke();
    }
}
