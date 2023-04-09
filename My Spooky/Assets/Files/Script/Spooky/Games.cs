using UnityEngine;
using UnityEngine.SceneManagement;

public class Games : MonoBehaviour
{
    [SerializeField] BottomItem item;
    [SerializeField] Sleep sleep;
    [SerializeField] TMPro.TMP_Text txtCoin;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        txtCoin.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        item.OnClick += ActiveButton;
        CheckGamesTime();
    }

    private void OnDestroy()
    {
        item.OnClick -= ActiveButton;
    }


    private void CheckGamesTime()
    {
        float gamesTime = PlayerPrefs.GetFloat("GamesTime", 0);
        if (gamesTime > 0)
        {
            sleep.Change(gamesTime / 12);
        }
        PlayerPrefs.SetFloat("GamesTime", 0);
    }


    private void ActiveButton()
    {
        gameManager.ButtonGames();
    }

    public void ButtonGames(string gameName)
    {
        SceneManager.LoadScene(gameName);
    }
}
