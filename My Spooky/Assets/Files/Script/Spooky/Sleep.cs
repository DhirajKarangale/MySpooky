using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class Sleep : Stat
{
    [SerializeField] float animSpeed;

    [Header("Refrences")]
    [SerializeField] GraveyardData data;
    [SerializeField] Character sleepCharacter;

    [Header("Color")]
    [SerializeField] Color colorDay;
    [SerializeField] Color colorNight;

    [Header("UI")]
    [SerializeField] TMP_Text txtTap;
    [SerializeField] TMP_Text txtSleepy;
    [SerializeField] TMP_Text txtGreatNap;
    [SerializeField] TMP_Text txtSleepEffect;
    [SerializeField] Image imgBg;

    [SerializeField] Image imgMoon;
    [SerializeField] Image imgGrave;
    [SerializeField] Image imgBackground;

    [Header("Move Items")]
    [SerializeField] Transform moon;
    [SerializeField] Transform sun;
    [SerializeField] Button upperCoffin;
    [SerializeField] Transform backCoffin;

    [Header("Position")]
    [SerializeField] Transform sunEndPos;
    [SerializeField] Transform moonEndPos;

    private Vector3 sunStPos;
    private Vector3 moonStPos;
    private Vector3 sleepCharStPos;
    private Vector3 upperCoffinStPos;
    private Vector3 backCoffinStPos;

    [Header("Object")]
    [SerializeField] GameObject sleepEffectObj;
    [SerializeField] GameObject homeObj;
    [SerializeField] GameObject sleepObj;

    private GameManager gameManager;

    private bool isDay;


    private void Start()
    {
        gameManager = GameManager.instance;
        item.OnClick += ActiveButton;
        isDay = false;
        Change(0);
        SetStartPos();
        Night();

        StartCoroutine(IETextEffect());
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("amount", amount);
        item.OnClick -= ActiveButton;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Change(10);
    }

    private IEnumerator IETextEffect()
    {
        while (true)
        {
            txtSleepEffect.text = "Z";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
            txtSleepEffect.text = "Zz";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
            txtSleepEffect.text = "Zzz";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
            txtSleepEffect.text = "Zzz.";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
            txtSleepEffect.text = "Zzz..";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
            txtSleepEffect.text = "Zzz...";
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * animSpeed);
        }
    }



    private void ChangeSkin()
    {
        imgBackground.sprite = data.bg;
        imgGrave.sprite = data.grave;
        imgMoon.sprite = data.moon;
    }

    private void SetStartPos()
    {
        sunStPos = sun.transform.position;
        moonStPos = moon.transform.position;
        sleepCharStPos = sleepCharacter.transform.position;
        upperCoffinStPos = upperCoffin.transform.position;
        backCoffinStPos = backCoffin.transform.position;
    }

    private void WakeMode()
    {
        // mainCharacter.ChangeState(Character.State.Normal);
        sleepCharacter.ChangeState(Character.State.Normal);

        sleepCharacter.transform.localPosition = Vector3.zero;
        sleepCharacter.transform.localScale = Vector3.one;

        upperCoffin.gameObject.SetActive(false);
        backCoffin.gameObject.SetActive(false);

        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(true);

        txtGreatNap.gameObject.SetActive(true);
        sleepEffectObj.SetActive(false);

        imgBg.color = colorNight;
    }

    private void Night()
    {
        gameManager.spookyData.sleep = amount;
        if (amount > minValue)
        {
            WakeMode();
            return;
        }

        sleepCharacter.ChangeState(Character.State.Sleepy);

        moon.gameObject.SetActive(true);
        sun.gameObject.SetActive(true);
        backCoffin.gameObject.SetActive(true);
        upperCoffin.gameObject.SetActive(true);

        sleepCharacter.transform.DOMove(sleepCharStPos, 1).SetEase(Ease.Linear);
        upperCoffin.transform.DOMoveX(upperCoffinStPos.x, 1f).SetEase(Ease.Linear);
        backCoffin.DOMoveX(backCoffinStPos.x, 1f).SetEase(Ease.Linear);
        sleepCharacter.transform.DOScale(Vector3.one, 1.5f);

        txtTap.text = "Tap for Nap";
        txtSleepy.text = "Spooky is sleepy";

        moon.DOMoveX(moonStPos.x, 3f).SetEase(Ease.Linear);
        sun.DOMoveX(sunStPos.x, 3.5f).SetEase(Ease.Linear);

        imgBg.DOColor(colorNight, 6);

        txtGreatNap.gameObject.SetActive(false);
        sleepEffectObj.transform.DOScale(Vector3.zero, 1).SetEase(Ease.Linear);
        StopAllCoroutines();

        isDay = false;
    }

    private void Day()
    {
        sleepCharacter.ChangeState(Character.State.Sleeped);

        sleepCharacter.transform.DOMoveX(0, 1f).SetEase(Ease.Linear);
        upperCoffin.transform.DOMoveX(0, 1f).SetEase(Ease.Linear);
        backCoffin.DOMoveX(0, 1f).SetEase(Ease.Linear);
        sleepCharacter.transform.DOScale(Vector3.one * 0.6f, 1.5f);

        txtTap.text = "Tap to Wake Up";
        txtSleepy.text = "Spooky is Sleeping";

        sun.gameObject.SetActive(true);
        moon.DOMoveX(moonEndPos.position.x, 3f).SetEase(Ease.Linear);
        sun.DOMoveX(sunEndPos.position.x, 3.5f).SetEase(Ease.Linear);

        imgBg.DOColor(colorDay, 6);

        txtGreatNap.gameObject.SetActive(false);
        sleepEffectObj.SetActive(true);
        sleepEffectObj.transform.DOScale(Vector3.one, 2.5f).SetEase(Ease.Linear);
        StartCoroutine(IETextEffect());

        isDay = true;
    }

    private void EnableInterActivity()
    {
        upperCoffin.interactable = true;
    }

    private void IncreaseStat()
    {
        Change(-1);
    }

    private void ActiveButton()
    {
        if (amount > 80)
        {
            Msg.instance.Show("Can't sleep !!!", Color.white);
            return;
        }
        ChangeSkin();
        Night();
        gameManager.ButtonSleep();
    }


    public override void ChangeState()
    {
        gameManager.spookyData.sleep = amount;
    }

    public void SleepButton()
    {
        if (amount > 80)
        {
            Msg.instance.Show("Can't sleep !!!", Color.white);
            return;
        }

        if (!isDay)
        {
            upperCoffin.interactable = false;

            Day();
            CancelInvoke();
            Invoke(nameof(EnableInterActivity), 5);
            // InvokeRepeating(nameof(IncreaseStat), 0, 12);
            InvokeRepeating(nameof(IncreaseStat), 0, 6);
        }
        else
        {
            upperCoffin.interactable = false;
            CancelInvoke();
            Invoke(nameof(EnableInterActivity), 5);
            Night();
        }
    }
}
