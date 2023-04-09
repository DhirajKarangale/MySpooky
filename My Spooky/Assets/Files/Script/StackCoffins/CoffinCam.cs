using UnityEngine;

public class CoffinCam : MonoBehaviour
{
    [SerializeField] float smoothMove = 1;
    
    internal Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothMove * Time.deltaTime);
    }
}
