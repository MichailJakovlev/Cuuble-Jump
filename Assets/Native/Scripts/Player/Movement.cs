using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject _player;
    public AnimationCurve _jumpStrenghtCurve;
    public AnimationCurve _jumpDirectionAxisX;
    public AnimationCurve _jumpDirectionAxisZ;

    float _totalTime;
    float _currentTmie;

    bool _isMoving = false;

    public void Start()
    {
        _totalTime = _jumpStrenghtCurve.keys[_jumpStrenghtCurve.keys.Length - 1].time;
        
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.A) && _isMoving == false)
        {
             StartCoroutine(JumpLeft());
            _isMoving = true;
        }
        
        if (Input.GetKeyUp(KeyCode.D) && _isMoving == false)
        {
             StartCoroutine(JumpRight());
            _isMoving = true;
        }
    }

   IEnumerator JumpLeft()
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

        _jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, _player.transform.position.y), new Keyframe(0.3f, _player.transform.position.y + 1.5f), new Keyframe(0.6f, _player.transform.position.y + 0.75f));
        _jumpDirectionAxisZ = new AnimationCurve(new Keyframe(0, _player.transform.position.z), new Keyframe(0.6f, _player.transform.position.z - 1.5f));
        _isMoving = false;
    }

    IEnumerator JumpRight()
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
        
        _jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, _player.transform.position.y), new Keyframe(0.3f, _player.transform.position.y + 1.5f), new Keyframe(0.6f, _player.transform.position.y + 0.75f));
        _jumpDirectionAxisX = new AnimationCurve(new Keyframe(0, _player.transform.position.x), new Keyframe(0.6f, _player.transform.position.x - 1.5f));
        _isMoving = false;
    }
}
