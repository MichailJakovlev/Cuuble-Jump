using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ThemeSkinDB _themeSkinDB;
    [SerializeField] private Coin _coin;
    [SerializeField] private LoseTracker _loseTracker;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _platformQuantity, _lateralDeviation, _verticalDeviation, _coinSpawnChance, _decorationSpawnChance;
    [SerializeField] private Vector3 _platformPosition, _leftDecorationPosition, _rightDecorationPosition;

    private Decoration _leftDecoration, _rightDecoration;
    private bool _isNotAnimating = true;

    private Queue<Platform> _queuePlatforms;
    private Queue<Decoration> _queueLeftDecorations;
    private Queue<Decoration> _queueRightDecorations;
    private Queue<Coin> _queueCoins;

    private Platform _platform;
    private List<Decoration> _decorationList;
    private List<Decoration> skinModelsCopy;

    private float _angle = 0.7071068f;
    private float _currentTime;
    private float _totalTime;

    private void Awake()
    {
        var skinModels = _themeSkinDB.skins.FirstOrDefault(m => m.name.ToString() == PlayerPrefs.GetString("ThemeSelected", "Forest"))?.skinModel;

        _platform = skinModels[0].GetComponent<Platform>();

        Instantiate(_platform);

        skinModelsCopy = skinModels.Select(item => item.GetComponent<Decoration>()).ToList();
        skinModelsCopy.Remove(skinModelsCopy[0]);

        _decorationList = skinModelsCopy.OfType<Decoration>().ToList();

        _queuePlatforms = new Queue<Platform>();
        _queueLeftDecorations = new Queue<Decoration>();
        _queueRightDecorations = new Queue<Decoration>();
        _queueCoins = new Queue<Coin>();
        _loseTracker._queueDirection = new Queue<float>();
        _loseTracker._queueDecorationCollision = new Queue<int>();

        _totalTime = _curve.keys[_curve.keys.Length - 1].time;

        for (int i = 0; i < _platformQuantity; i++)
        {
            if (i < 4)
            {
                PositionChanger(0);
                Create();
                ChanceChanger();
            }

            else
            {
                PositionChanger(Random.value);
                Create();
                ChanceChanger();
            }

            

            Realise();
        }
    }

    public void Pull()
    {
        if(_isNotAnimating)
        {
            Get();
            PositionChanger(Random.value);

            StartCoroutine(ObjectsAnimation());

            ChanceChanger();
            Realise();
        }
    }

    private void Create()
    {
        _platform = Instantiate(_platform, _platformPosition, Quaternion.identity);
        _leftDecoration = Instantiate(_decorationList[Random.Range(0, _decorationList.Count)], _leftDecorationPosition, Quaternion.identity);
        _rightDecoration = Instantiate(_decorationList[Random.Range(0, _decorationList.Count)], _rightDecorationPosition, Quaternion.identity);
        _coin = Instantiate(_coin, new Vector3(_platformPosition.x, _platformPosition.y + 0.8f, _platformPosition.z), Quaternion.identity);
    }

    private void Get()
    {
        _platform = _queuePlatforms.Dequeue();
        _leftDecoration = _queueLeftDecorations.Dequeue();
        _rightDecoration = _queueRightDecorations.Dequeue();
        _coin = _queueCoins.Dequeue();
    }

    private void Realise()
    {
        _queuePlatforms.Enqueue(_platform);
        _queueLeftDecorations.Enqueue(_leftDecoration);
        _queueRightDecorations.Enqueue(_rightDecoration);
        _queueCoins.Enqueue(_coin);

        _loseTracker._queueDirection.Enqueue(_angle);
    }

    private void PositionChanger(float direction)
    {
        if (direction < 0.5f)
        {
            _platformPosition = new Vector3(_platformPosition.x -= _lateralDeviation, _platformPosition.y += _verticalDeviation, _platformPosition.z);
            _leftDecorationPosition = new Vector3(_leftDecorationPosition.x -= _lateralDeviation, _leftDecorationPosition.y += _verticalDeviation, _leftDecorationPosition.z);
            _rightDecorationPosition = new Vector3(_rightDecorationPosition.x -= _lateralDeviation, _rightDecorationPosition.y += _verticalDeviation, _rightDecorationPosition.z);
            _angle = 0.7071068f;
        }

        else
        {
            _platformPosition = new Vector3(_platformPosition.x, _platformPosition.y += _verticalDeviation, _platformPosition.z -= _lateralDeviation);
            _leftDecorationPosition = new Vector3(_leftDecorationPosition.x, _leftDecorationPosition.y += _verticalDeviation, _leftDecorationPosition.z -= _lateralDeviation);
            _rightDecorationPosition = new Vector3(_rightDecorationPosition.x, _rightDecorationPosition.y += _verticalDeviation, _rightDecorationPosition.z -= _lateralDeviation);
            _angle = 0;
        }
    }

    private void ChanceChanger()
    {
        _platform.gameObject.SetActive(true);
        _leftDecoration.gameObject.SetActive(Random.value < _decorationSpawnChance ? true : false);
        _rightDecoration.gameObject.SetActive(Random.value < _decorationSpawnChance ? true : false);
        _coin.gameObject.SetActive(Random.value < _coinSpawnChance ? true : false);

        if (_leftDecoration.isActiveAndEnabled)
        {
            _loseTracker._queueDecorationCollision.Enqueue(-1);
        }
        else if(_rightDecoration.isActiveAndEnabled)
        {
            _loseTracker._queueDecorationCollision.Enqueue(1);
        }
        else
        {
            _loseTracker._queueDecorationCollision.Enqueue(0);
        }
    }

    IEnumerator ObjectsAnimation()
    {
        _isNotAnimating = false;
        _currentTime = 0;
        var pos = transform.position.y;
        while(_currentTime < _totalTime)
        {
            pos = _curve.Evaluate(_currentTime);

            _platform.transform.position = new Vector3(_platformPosition.x, pos, _platformPosition.z);
            _leftDecoration.transform.position = new Vector3(_leftDecorationPosition.x, pos + 0.75f, _leftDecorationPosition.z);
            _rightDecoration.transform.position = new Vector3(_rightDecorationPosition.x, pos + 0.75f, _rightDecorationPosition.z);
            _coin.transform.position = new Vector3(_platformPosition.x, pos + 0.8f, _platformPosition.z);

            _currentTime += Time.fixedDeltaTime;

            yield return new WaitForSeconds(0.001f);
        }

        _platform.transform.position = new Vector3(_platformPosition.x, _curve.Evaluate(_totalTime), _platformPosition.z);
        _leftDecoration.transform.position = new Vector3(_leftDecorationPosition.x, _curve.Evaluate(_totalTime) + 0.75f, _leftDecorationPosition.z);
        _rightDecoration.transform.position = new Vector3(_rightDecorationPosition.x, _curve.Evaluate(_totalTime) + 0.75f, _rightDecorationPosition.z);
        _coin.transform.position = new Vector3(_platformPosition.x, _curve.Evaluate(_totalTime) + 0.8f, _platformPosition.z);

        _curve = new AnimationCurve(new Keyframe(0, _platform.transform.position.y + 3f), new Keyframe(0.25f, _platform.transform.position.y + 0.75f));
        _isNotAnimating = true;
    }
}
