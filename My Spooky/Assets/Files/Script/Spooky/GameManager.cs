using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] BGData bgData;
    [SerializeField] UnityEngine.UI.Image imgImage;
    [SerializeField] internal AnimateCoin animateCoin;
    [SerializeField] internal Character character;
    [SerializeField] internal WardrobeData spookyData;

    [SerializeField] GameObject objHome;
    [SerializeField] GameObject objGames;
    [SerializeField] GameObject objSleep;
    [SerializeField] GameObject objClean;
    [SerializeField] GameObject objGlobal;
    [SerializeField] GameObject objCustomize;
    [SerializeField] GameObject objWardobe;
    [SerializeField] GameObject objShowerRoom;
    [SerializeField] GameObject objGraveyard;
    [SerializeField] GameObject objKitchen;
    [SerializeField] GameObject objBG;

    [SerializeField] TMPro.TMP_Text txtCoin;

    public int coin;


    private void Start()
    {
        character.ChangeState(Character.State.Normal);
        coin = PlayerPrefs.GetInt("Coin", 0);
        if (coin < 20) coin = 99999999;
        txtCoin.text = coin.ToString();
        ButtonHome();
    }


    public void AddCoin(int amount, Vector3 stPos, Vector3 endPos)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coin", coin);
        txtCoin.text = coin.ToString();
        animateCoin.Move(amount, stPos, endPos);
    }


    public void ButtonCustomization(GameObject obj)
    {
        ButtonBack();
        obj.SetActive(true);
    }

    public void ButtonCustomize()
    {
        ButtonBack();
        objCustomize.SetActive(true);
    }

    public void ButtonGames()
    {
        ButtonBack();
        objGames.SetActive(true);
    }

    public void ButtonSleep()
    {
        ButtonBack();
        objGlobal.SetActive(true);
        objSleep.SetActive(true);
    }

    public void ButtonClean()
    {
        ButtonBack();
        objGlobal.SetActive(true);
        objClean.SetActive(true);
    }

    public void ButtonHome()
    {
        ButtonBack();
        objHome.SetActive(true);
        objGlobal.SetActive(true);

        imgImage.sprite = bgData.sprite;
    }

    public void ButtonBack()
    {
        objHome.SetActive(false);
        objGames.SetActive(false);
        objSleep.SetActive(false);
        objClean.SetActive(false);
        objGlobal.SetActive(false);
        objCustomize.SetActive(false);
        objWardobe.SetActive(false);
        objShowerRoom.SetActive(false);
        objGraveyard.SetActive(false);
        objBG.SetActive(false);
        objKitchen.SetActive(false);
    }
}
