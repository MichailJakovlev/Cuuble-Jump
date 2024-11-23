using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] public AnimationCurve _jumpStrenghtCurve;
    [SerializeField] public AnimationCurve _jumpDirectionAxisX;
    [SerializeField] public AnimationCurve _jumpDirectionAxisZ;
    [SerializeField] public float _animationTime;

   [HideInInspector] public float _totalTime;
   [HideInInspector] public float _currentTime;
    int i = 0;

    public Vector3 pos;
    [HideInInspector] public bool _inputAllowed = true;
    [HideInInspector] public bool _isNotMoving = true;

    public void Start()
    {
        _totalTime = _jumpStrenghtCurve.keys[_jumpStrenghtCurve.keys.Length - 1].time;
        PlayerPrefs.SetInt("isFallingOnPlatform", 0);
    }

    public IEnumerator Jump(bool side)
    {

        _currentTime = 0;

        if(i > 7)
        {
            StartCoroutine(_spawner.ObjectsAnimation());
        }

        pos = _player.transform.position;

        while (_currentTime < _totalTime)
        {
            pos.y = _jumpStrenghtCurve.Evaluate(_currentTime);

            if(side)
            {
                pos.z = _jumpDirectionAxisZ.Evaluate(_currentTime);
                _player.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }

            else
            {
                pos.x = _jumpDirectionAxisX.Evaluate(_currentTime);
                _player.transform.rotation = Quaternion.Euler(0f, 90, 0f);
            }

            _player.transform.position = pos;
            _currentTime += Time.fixedDeltaTime * 1.5f;

            yield return new WaitForSeconds(0.001f);
        }

        if(side)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, _jumpStrenghtCurve.Evaluate(_totalTime), _jumpDirectionAxisZ.Evaluate(_totalTime));
        }
        else
        {
            _player.transform.position = new Vector3(_jumpDirectionAxisX.Evaluate(_totalTime), _jumpStrenghtCurve.Evaluate(_totalTime), _player.transform.position.z);
        }

        _jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, _player.transform.position.y), new Keyframe(_animationTime / 2, _player.transform.position.y + 1.5f), new Keyframe(_animationTime, _player.transform.position.y + 0.75f));
        _jumpDirectionAxisZ = new AnimationCurve(new Keyframe(0, _player.transform.position.z), new Keyframe(_animationTime, _player.transform.position.z - 1.5f));

        _jumpDirectionAxisX = new AnimationCurve(new Keyframe(0, _player.transform.position.x), new Keyframe(_animationTime, _player.transform.position.x - 1.5f));
        _isNotMoving = true;
        i++;
    }
}
