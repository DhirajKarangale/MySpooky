using TMPro;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverUI : Singleton<GameOverUI>
{
    [SerializeField] internal GameObject obj;
    [SerializeField] TMP_Text txtTitle;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtHighest;
    [SerializeField] GameObject highestObj;
    [SerializeField] GameObject continueObj;
    [SerializeField] GameObject menuObj;
    [SerializeField] UnityEngine.UI.Slider slider;

    private float adTime = 5;
    private int score;

    private void Start()
    {
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        SetPlayedTime();
        SetScore();
    }


    private IEnumerator IEAdTime()
    {
        AdState();
        float currAdTime = adTime;
        while (currAdTime >= 0)
        {
            yield return new WaitForEndOfFrame();
            slider.value = currAdTime / adTime;
            currAdTime -= Time.deltaTime;
        }
        GameOverState();
    }


    private void SetPlayedTime()
    {
        float currTime = Time.timeSinceLevelLoad;
        float preTime = PlayerPrefs.GetFloat("GamesTime", 0);
        if (preTime <= 0) preTime = 0;
        PlayerPrefs.SetFloat("GamesTime", preTime + currTime);
    }

    private void SetScore()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0) + score;
        PlayerPrefs.SetInt("Coin", coin);
    }

    private void AdState()
    {
        txtTitle.text = "REVIVE";
        highestObj.SetActive(false);
        slider.gameObject.SetActive(true);
        menuObj.SetActive(false);
        continueObj.SetActive(true);
    }

    private void GameOverState()
    {
        txtTitle.text = "Game Over";
        highestObj.SetActive(true);
        slider.gameObject.SetActive(false);
        menuObj.SetActive(true);
        continueObj.SetActive(false);
    }

    public void Active(int score, int highScore)
    {
        txtScore.text = score.ToString();
        txtHighest.text = highScore.ToString();

        this.score = score;

        obj.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(IEAdTime());
    }

    public void ContinueButton()
    {
        var gameManagers = FindObjectsOfType<MonoBehaviour>().OfType<IGamesGM>();
        foreach (var gameManager in gameManagers)
        {
            gameManager.Continue();
        }

        obj.SetActive(false);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
