using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class Clean : Stat
{
    [SerializeField] ShowerRoomData data;
    [Header("Transforms")]
    [SerializeField] Transform pin;
    [SerializeField] Transform hand;
    [SerializeField] Transform cloud;

    [Header("Rrefrences")]
    [SerializeField] Rigidbody2D charRB;
    [SerializeField] Material matVibrate;

    [Header("Animator")]
    [SerializeField] Animator washingHolder;
    [SerializeField] Animator washingHandle;

    [Header("Sprites")]
    [SerializeField] Sprite spriteBtnBlue;
    [SerializeField] Sprite spriteBtnOrange;
    [SerializeField] Sprite spriteBtnGreen;

    [Header("UI")]
    [SerializeField] Image machine;
    [SerializeField] Image imgDryer;
    [SerializeField] Image bottelCap;
    [SerializeField] Image imgDirt;
    [SerializeField] Button bottel;
    [SerializeField] Button buttonDryer;
    [SerializeField] Button buttonMachine;
    [SerializeField] Button cleanCharacter;
    [SerializeField] TMP_Text txtTidy;
    [SerializeField] TMP_Text txtMachine;
    [SerializeField] TMP_Text txtMachineBtn;

    [Header("PS")]
    [SerializeField] ParticleSystem psWater;
    [SerializeField] ParticleSystem psDry;

    private GameManager gameManager;

    // private float dirttime = 7200;
    private int washingTime;
    private float dirttime = 1800;
    private bool isClicked;

    private Vector3 machinePos;
    private Vector3 machineStPos;
    private Vector3 machineScale;
    private Vector3 machineStScale;

    private Vector3 bottelPos;
    private Vector3 bottelStPos;
    private Vector3 bottelPorePos;
    private Vector3 bottelScale;
    private Vector3 bottelStScale;

    private Vector3 charStScale;
    private Vector3 charScale;
    private Vector3 charWashScale;
    private Vector3 charStPos;
    private Vector3 charPos;
    private Vector3 charWashPos;

    private Vector3 pinStPos;
    private Vector3 pinPos;


    private void Start()
    {
        gameManager = GameManager.instance;
        item.OnClick += ActiveButton;
        amount = 40;
        CheckAmount();
        SetTrasnforms();
    }

    private void OnDestroy()
    {
        item.OnClick -= ActiveButton;
    }


    private IEnumerator IECheckClick(Vector3 pos)
    {
        isClicked = false;
        yield return new WaitForSeconds(5);
        if (!isClicked) ActiveHand(pos);
    }

    private IEnumerator IEWashing()
    {
        machine.material = matVibrate;
        charRB.AddTorque(150);

        while (washingTime > 0)
        {
            yield return new WaitForSeconds(1);
            washingTime -= 1;
            txtMachineBtn.text = $"{washingTime}s";
            txtMachine.text = "Washing In progress";
        }

        charRB.angularVelocity = 0;
        machine.material = null;
        cleanCharacter.transform.localRotation = Quaternion.identity;
        buttonMachine.image.sprite = spriteBtnBlue;
        txtMachineBtn.text = "";
        txtMachine.text = "Completed";

        machine.transform.DOScale(Vector3.zero, 2);
        cleanCharacter.transform.DOScale(charStScale, 2);
        cleanCharacter.transform.DOLocalMove(charStPos, 2);
        buttonDryer.transform.DOScale(Vector3.one, 2).OnComplete(() =>
        {
            SetHand(buttonDryer.transform.localPosition);
            buttonDryer.interactable = true;
        });
    }


    private void ChangeSkin()
    {
        machine.sprite = data.machine;
        bottel.image.sprite = data.bottel;
        bottelCap.sprite = data.cap;
        imgDryer.sprite = data.dryer;
    }

    private void SetTrasnforms()
    {
        pinStPos = pin.localPosition;

        machineStScale = machine.transform.localScale;
        bottelStScale = bottel.transform.localScale;
        charStScale = cleanCharacter.transform.localScale;

        machineStPos = machine.transform.localPosition;
        bottelStPos = bottel.transform.localPosition;
        charStPos = cleanCharacter.transform.localPosition;

        machineScale = Vector3.one * 2;
        bottelScale = Vector3.one * 4;
        charScale = Vector3.one * 0.7f;
        charWashScale = Vector3.one * 0.4f;

        machinePos = new Vector3(0, -388, 0);
        bottelPos = new Vector3(429.4f, 706.67f, 0);
        bottelPorePos = new Vector3(-130, 1006, 0);
        charPos = new Vector3(515, -715, 0);
        charWashPos = new Vector3(41, -402, 0);
        pinPos = new Vector3(64.275f, -144, 0);
    }

    private void CheckAmount()
    {
        DateTime currTime = DateTime.Now;
        DateTime lastCleantime = DateTime.Parse(PlayerPrefs.GetString("WashTime", currTime.ToString()));
        double timeDiffernce = (currTime - lastCleantime).TotalSeconds;
        amount = Math.Clamp(100 - (100 / dirttime * (float)timeDiffernce), 0, 100);
        Change(0);
        SetDirt();
    }

    private void CheckClean()
    {
        CheckAmount();
        if (amount < 50) CleanMode();
        else NormalMode();
    }

    private void ActiveHand(Vector3 pos)
    {
        DisableHand();
        hand.transform.localPosition = pos;
        hand.gameObject.SetActive(true);
    }

    private void DisableHand()
    {
        StopAllCoroutines();
        hand.gameObject.SetActive(false);
    }

    private void SetHand(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(IECheckClick(pos));
    }

    private void Clicked()
    {
        DisableHand();
        isClicked = true;
    }

    private void NormalMode()
    {
        if (washingHandle.gameObject.activeInHierarchy) washingHandle.Play("Open");
        if (washingHolder.gameObject.activeInHierarchy) washingHolder.Play("Close");

        bottelCap.gameObject.SetActive(true);
        buttonDryer.interactable = false;
        buttonDryer.transform.localScale = Vector3.zero;
        washingTime = 10;
        machine.material = null;
        txtMachine.text = "";
        txtMachineBtn.text = "";
        buttonMachine.image.sprite = spriteBtnBlue;
        bottel.interactable = false;
        cleanCharacter.interactable = false;
        buttonMachine.interactable = false;
        cleanCharacter.transform.SetAsLastSibling();
        hand.transform.SetAsLastSibling();
        psWater.Stop();

        machine.transform.localPosition = machineStPos;
        machine.transform.localScale = machineStScale;

        bottel.transform.localScale = bottelStScale;
        bottel.transform.localPosition = bottelStPos;
        bottel.transform.localRotation = Quaternion.identity;

        cleanCharacter.transform.localScale = charStScale;
        cleanCharacter.transform.localPosition = charStPos;

        cloud.localScale = Vector3.zero;

        txtTidy.gameObject.SetActive(false);
    }

    private void CleanMode()
    {
        NormalMode();
        txtTidy.text = "I am tidey";
        txtTidy.gameObject.SetActive(true);
        SetHand(machine.transform.localPosition);
    }

    private void WaterPoreComplete()
    {
        washingHolder.Play("Close");
        psWater.Stop();
        bottel.transform.DOLocalRotate(Vector3.zero, 1);
        bottel.transform.DOScale(Vector3.zero, 2);

        washingHandle.Play("Open");

        txtMachine.text = "Tap on Spooky";

        cleanCharacter.interactable = true;
        SetHand(cleanCharacter.transform.localPosition);
    }

    private void SetDirt()
    {
        if (amount < 50)
        {
            gameManager.spookyData.clean = amount;

            Color dirtColor = Color.white;
            dirtColor.a = 1 - (amount / 100);
            imgDirt.color = dirtColor;
        }
    }

    private void CompleteWashing()
    {
        amount = 100;
        gameManager.spookyData.clean = amount;
        Change(0);
        PlayerPrefs.SetString("WashTime", DateTime.Now.ToString());
        psDry.Stop();
        buttonDryer.transform.DOScale(Vector3.zero, 2);
        txtTidy.text = "Feels Fresh";
        txtTidy.gameObject.SetActive(true);
        gameManager.AddCoin(10, cleanCharacter.transform.position, Vector3.zero);
    }


    private void ActiveButton()
    {
        ChangeSkin();
        CheckClean();
        DisableHand();
        gameManager.ButtonClean();
    }



    public void ButtonSetMachine()
    {
        Clicked();

        txtTidy.gameObject.SetActive(false);

        machine.transform.DOScale(machineScale, 3);
        bottel.transform.DOScale(bottelScale, 3);
        cleanCharacter.transform.DOScale(charScale, 3);
        cloud.DOScale(Vector3.one, 5);

        machine.transform.DOLocalMove(machinePos, 2);
        bottel.transform.DOLocalMove(bottelPos, 2).OnComplete(() =>
        {
            txtMachine.text = "POUR DETERGENT";
            SetHand(bottel.transform.localPosition);
            bottel.interactable = true;
        });
        cleanCharacter.transform.DOLocalMove(charPos, 2);
    }

    public void ButtonPore()
    {
        Clicked();

        bottel.interactable = false;
        bottel.transform.DOLocalMove(bottelPorePos, 2).OnComplete(() =>
        {
            bottelCap.gameObject.SetActive(false);
            washingHolder.Play("Open");
            bottel.transform.DOLocalRotate(new Vector3(0, 0, 90), 2).OnComplete(() =>
            {
                DisableHand();
                psWater.Play();
                Invoke(nameof(WaterPoreComplete), 3);
            });
        });
    }

    public void ButtonSpooky()
    {
        Clicked();
        cleanCharacter.interactable = false;

        cloud.DOScale(Vector3.zero, 1);

        cleanCharacter.transform.DOScale(charWashScale, 2);
        cleanCharacter.transform.DOLocalMove(charWashPos, 2).OnComplete(() =>
        {
            washingHandle.Play("Close");
            buttonMachine.image.sprite = spriteBtnGreen;
            cleanCharacter.transform.SetAsFirstSibling();
            buttonMachine.interactable = true;
            txtMachine.text = "PUSH BUTTON TO START";
            SetHand(buttonMachine.transform.localPosition);
        });
    }

    public void ButtonWashing()
    {
        Clicked();
        buttonMachine.interactable = false;

        buttonMachine.image.sprite = spriteBtnOrange;
        txtMachineBtn.text = washingTime.ToString();
        txtMachine.text = "Washing In progress";

        StartCoroutine(IEWashing());
    }

    public void ButtonDryer()
    {
        Clicked();
        buttonDryer.interactable = false;


        pin.DOLocalMove(pinPos, 1).OnComplete(() =>
        {
            psDry.Play();

            Color color = Color.white;
            color.a = 0;
            imgDirt.DOColor(color, 5);


            Invoke(nameof(CompleteWashing), 3);
        });
    }
}
