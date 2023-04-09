using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    [SerializeField] float offset = 0.95f;
    private Vector3 initial;

    private void Start()
    {
        initial = transform.localScale;
        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Scaling()));
        GetComponent<RectTransform>().localScale = initial;
    }

    private IEnumerator Scaling()
    {
        GetComponent<RectTransform>().localScale = initial * offset;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        GetComponent<RectTransform>().localScale = initial;
    }
}