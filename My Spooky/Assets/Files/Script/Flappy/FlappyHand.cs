using UnityEngine;

public class FlappyHand : MonoBehaviour
{
    [SerializeField] Transform handUp;
    [SerializeField] Transform handDown;
    public bool isScoreAdded;

    private float startXPos = 9;

    public void Set(float gap, float xPos)
    {
        handUp.localPosition = new Vector3(0, 2.5f, 0);
        handDown.localPosition = new Vector3(0, -2.5f, 0);

        handUp.gameObject.SetActive(false);
        handDown.gameObject.SetActive(false);
        
        transform.localPosition = new Vector3(startXPos + xPos, 0, 0);
        
        handUp.gameObject.SetActive(true);
        handDown.gameObject.SetActive(true);
        
        handUp.localPosition += new Vector3(0, gap / 2, 0);
        handDown.localPosition -= new Vector3(0, gap / 2, 0);

        isScoreAdded = false;
    }
}
