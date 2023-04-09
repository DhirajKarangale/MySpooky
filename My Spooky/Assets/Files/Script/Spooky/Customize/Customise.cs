using UnityEngine;

public class Customise : Stat
{
    [SerializeField] TMPro.TMP_Text txtCoin;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        item.OnClick += ButtonActive;
    }


    private void OnDestroy()
    {
        item.OnClick -= ButtonActive;
    }


    public void ButtonActive()
    {
        gameManager.ButtonCustomize();
        txtCoin.text = gameManager.coin.ToString();
    }

    public void ButtonCustomization(GameObject obj)
    {
        gameManager.ButtonCustomization(obj);
    }

    public void ButtonBack()
    {
        gameManager.ButtonCustomize();
    }
}
