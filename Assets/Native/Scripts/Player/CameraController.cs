using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothTime;

    [SerializeField] private GameObject _player;

    public Transform target;
    public Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;
    public bool isGameover = false;

    private void Start()
    {
        target = _player.transform;
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (isGameover == false)
        {
            Vector3 targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        }
        else
        {
            transform.position = transform.position;
        }
    }

}
