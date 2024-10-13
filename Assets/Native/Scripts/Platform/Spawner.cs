using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private List<Decoration> _decorationList;
    [SerializeField] private Coin _coin;
    [SerializeField] private LoseTracker _loseTracker;
    [SerializeField] private float _platformQuantity, _lateralDeviation, _verticalDeviation, _coinSpawnChance, _decorationSpawnChance;
    [SerializeField] private Vector3 _platformPosition, _leftDecorationPosition, _rightDecorationPosition;
    private Decoration _leftDecoration, _rightDecoration;

    public Queue<Platform> _queuePlatforms;
    private Queue<Decoration> _queueLeftDecorations;
    private Queue<Decoration> _queueRightDecorations;
    private Queue<Coin> _queueCoins;

    float _angle = 0.7071068f;

    private void Awake()
    {
        _queuePlatforms = new Queue<Platform>();
        _queueLeftDecorations = new Queue<Decoration>();
        _queueRightDecorations = new Queue<Decoration>();
        _queueCoins = new Queue<Coin>();
        _loseTracker._queueDirection = new Queue<float>();

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
        Get();
        PositionChanger(Random.value);

        _platform.transform.position = _platformPosition;
        _leftDecoration.transform.position = _leftDecorationPosition;
        _rightDecoration.transform.position = _rightDecorationPosition;
        _coin.transform.position = new Vector3(_platformPosition.x, _platformPosition.y + 0.8f, _platformPosition.z);

        ChanceChanger();
        Realise();
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
    }
}
