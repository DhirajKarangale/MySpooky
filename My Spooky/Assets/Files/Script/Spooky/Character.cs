using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum State { Normal, Sleepy, Sleeped }

    [SerializeField] Animator animatorSleepEye;
    [SerializeField] Animator animatorMouth;
    [SerializeField] Image imgDirt;
    [SerializeField] WardrobeData spookyData;

    [Header("Objects")]
    [SerializeField] GameObject sleep;
    [SerializeField] GameObject normal;

    [Header("Sprites")]
    [SerializeField] internal Image imgBody;
    [SerializeField] internal Image imgEyebroLeft;
    [SerializeField] internal Image imgEyebroRight;
    [SerializeField] internal Image imgEyeLeft;
    [SerializeField] internal Image imgEyeRight;
    [SerializeField] internal Image imgSpecs;
    [SerializeField] internal Image imgMouth;
    [SerializeField] internal Image imgNecklace;
    [SerializeField] internal Image imgHandLeft;
    [SerializeField] internal Image imgHandRight;
    [SerializeField] internal Image imgCapeRight;
    [SerializeField] internal Image imgCapeLeft;
    [SerializeField] internal Image imgHornLeft;
    [SerializeField] internal Image imgHornRight;

    private Sprite oldMouthSprite;


    private void OnEnable()
    {
        CloathChange();
        UpdateState();
        SetDirt(spookyData.clean / 100);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { Eat(); }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopEat();
        }
    }


    private void UpdateState()
    {
        if (spookyData.sleep > 50)
        {
            ChangeState(State.Normal);
        }
        else
        {
            ChangeState(State.Sleepy);
        }
    }

    private void Normal()
    {
        sleep.SetActive(false);
        normal.SetActive(true);

        StopEat();
    }

    private void Sleepy()
    {
        sleep.SetActive(true);
        normal.SetActive(false);

        if (this.animatorSleepEye.gameObject.activeInHierarchy) animatorSleepEye.Play("Sleepy");
    
        StopEat();
    }

    private void Sleeped()
    {
        sleep.SetActive(true);
        normal.SetActive(false);
        if (this.animatorSleepEye.gameObject.activeInHierarchy) animatorSleepEye.Play("Sleeped");

        StopEat();
    }

    private void StopEat()
    {
        animatorMouth.enabled = false;
        if (oldMouthSprite) imgMouth.sprite = oldMouthSprite;
    }

    private void Eat()
    {
        oldMouthSprite = imgMouth.sprite;
        animatorMouth.enabled = true;
        animatorMouth.Play("Play");
    }


    public void CloathChange()
    {
        // imgBody.sprite = spookyData.bodyColor;
        imgBody.sprite = spookyData.bodyPatterns;
        imgEyebroLeft.sprite = spookyData.eyelashes;
        imgEyebroRight.sprite = spookyData.eyelashes;
        imgEyeLeft.sprite = spookyData.eyes;
        imgEyeRight.sprite = spookyData.eyes;
        imgSpecs.sprite = spookyData.eyeglasses;
        imgMouth.sprite = spookyData.helmet;
        imgNecklace.sprite = spookyData.neckband;
        imgHandLeft.sprite = spookyData.handband;
        imgHandRight.sprite = spookyData.handband;
        imgCapeLeft.sprite = spookyData.cape;
        imgCapeRight.sprite = spookyData.cape;
        imgHornLeft.sprite = spookyData.horns;
        imgHornRight.sprite = spookyData.horns;
    }

    public void SetDirt(float value)
    {
        Color color = Color.white;
        color.a = 1 - value;
        imgDirt.color = color;
    }

    public void ChangeState(State state)
    {
        switch (state)
        {
            case State.Normal:
                Normal();
                break;
            case State.Sleepy:
                Sleepy();
                break;
            case State.Sleeped:
                Sleeped();
                break;
        }
    }
}
