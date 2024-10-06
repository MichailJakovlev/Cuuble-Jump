using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private GameObject _coin;

    float _currentTime, _totalTime;

    private void Start()
    {
        _totalTime = _curve.keys[_curve.keys.Length - 1].time;
    }

    void Update()
    {            
        var _rotation = transform.rotation;
        
        _rotation.y = _curve.Evaluate(_currentTime);
        _coin.transform.rotation = Quaternion.Euler(0,_rotation.y,0);

        _currentTime += Time.fixedDeltaTime;
        if( _currentTime >= _totalTime)
        {
            _currentTime = 0;
        }
    }
}
