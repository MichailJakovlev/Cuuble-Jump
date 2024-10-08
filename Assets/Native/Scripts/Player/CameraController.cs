using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothTime;
    [SerializeField] private GameObject _player;
    
    private Transform target;
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Start()
    {
        target = _player.transform;
        _offset = transform.position - target.position;
    }
    
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
    
}
