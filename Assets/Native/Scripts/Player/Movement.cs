using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private AnimationCurve _jumpStrenghtCurve;
    [SerializeField] private AnimationCurve _jumpDirectionAxisX;
    [SerializeField] private AnimationCurve _jumpDirectionAxisZ;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _middleAnimationTime;
    [SerializeField] private float _endAnimationTime;

    float _totalTime;
    float _currentTmie;


    public bool _isMoving = false;

    public void Start()
    {
        _totalTime = _jumpStrenghtCurve.keys[_jumpStrenghtCurve.keys.Length - 1].time;

    }
    
    public IEnumerator JumpLeft()
    {
        _currentTmie = 0;
        _player.transform.rotation = Quaternion.Euler(0f, 0, 0f);
        var pos = transform.position;
        while (_currentTmie < _totalTime)
        {
            pos.y = _jumpStrenghtCurve.Evaluate(_currentTmie);
            pos.z = _jumpDirectionAxisZ.Evaluate(_currentTmie);

            _player.transform.position = pos;
            _currentTmie += Time.fixedDeltaTime;

            yield return null;
        }

        _jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, _player.transform.position.y), new Keyframe(_middleAnimationTime, _player.transform.position.y + 1.5f), new Keyframe(_endAnimationTime, _player.transform.position.y + 0.75f));
        _jumpDirectionAxisZ = new AnimationCurve(new Keyframe(0, _player.transform.position.z), new Keyframe(_endAnimationTime, _player.transform.position.z - 1.5f));
        _isMoving = false;
    }

    public IEnumerator JumpRight()
    {
        _currentTmie = 0;
        _player.transform.rotation = Quaternion.Euler(0f,90,0f);
        var pos = transform.position;
        while (_currentTmie < _totalTime)
        {
            pos.y = _jumpStrenghtCurve.Evaluate(_currentTmie);
            pos.x = _jumpDirectionAxisX.Evaluate(_currentTmie);

            _player.transform.position = pos;
            _currentTmie += Time.fixedDeltaTime;

            yield return null;
        }

        _jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, _player.transform.position.y), new Keyframe(_middleAnimationTime, _player.transform.position.y + 1.5f), new Keyframe(_endAnimationTime, _player.transform.position.y + 0.75f));
        _jumpDirectionAxisX = new AnimationCurve(new Keyframe(0, _player.transform.position.x), new Keyframe(_endAnimationTime, _player.transform.position.x - 1.5f));
        _isMoving = false;
    }
}
