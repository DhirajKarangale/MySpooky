using UnityEngine;
using DG.Tweening;

public class Msg : Singleton<Msg>
{
    [SerializeField] TMPro.TMP_Text txtMsg;
    [SerializeField] Transform obj;
    [SerializeField] Transform showPos;
    [SerializeField] Transform desablePos;

    private float desableTime = 3;
    private float duration = 1;

    private void Start()
    {
        Desable();
    }

    private void Desable()
    {
        obj.DOMoveX(desablePos.position.x, duration).SetEase(Ease.InBack);
    }

    public void Show(string msg, Color color)
    {
        CancelInvoke();

        if (obj.transform.position.x == showPos.transform.position.x)
        {
            obj.DOMoveX(desablePos.position.x, duration).SetEase(Ease.InBack).
            OnComplete(() => { Show(msg, color); });
            return;
        }

        txtMsg.text = msg;
        txtMsg.color = color;
        obj.DOMoveX(showPos.position.x, duration).SetEase(Ease.OutBack);
        Invoke("Desable", desableTime);
    }
}
