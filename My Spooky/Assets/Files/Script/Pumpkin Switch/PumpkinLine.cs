using UnityEngine;

public class PumpkinLine : MonoBehaviour
{
    [SerializeField] GameObject waitObj;
    [SerializeField] Transform orange;
    [SerializeField] Transform purple;

    private Vector3 leftPos;
    private Vector3 rightPos;
    private bool isSwitch;
    private PumpkinGM gameManager;


    private void Start()
    {
        Time.timeScale = 0;
        isSwitch = false;
        leftPos = orange.localPosition;
        rightPos = purple.localPosition;
        gameManager = PumpkinGM.instance;
    }

    private void OnMouseDown()
    {
        if (gameManager.gameOverUI.obj.activeInHierarchy) return;

        if (waitObj.activeInHierarchy)
        {
            waitObj.SetActive(false);
            Time.timeScale = 1;
            gameManager.isThrowAllow = true;
            gameManager.ResetThrow();
            return;
        }

        if (isSwitch)
        {
            orange.localPosition = leftPos;
            purple.localPosition = rightPos;
        }
        else
        {
            orange.localPosition = rightPos;
            purple.localPosition = leftPos;
        }

        isSwitch = !isSwitch;
    }
}
