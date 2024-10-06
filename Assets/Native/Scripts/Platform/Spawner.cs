using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private Decoration _leftDecoration, _rightDecoration;
    [SerializeField] private Coin _coin;
    [SerializeField] private float _platformQuantity, _lateralDeviation;
    [SerializeField] private Vector3 _platformPosition, _leftDecorationPosition, _rightDecorationPosition;

    private Queue<Platform> _queuePlatforms;
    private Queue<Decoration> _queueLeftDecorations;
    private Queue<Decoration> _queueRightDecorations;
    private Queue<Coin> _queueCoins;

    private void Awake()
    {
        _queuePlatforms = new Queue<Platform>();
        _queueLeftDecorations = new Queue<Decoration>();
        _queueRightDecorations = new Queue<Decoration>();
        _queueCoins = new Queue<Coin>();
        
        Spawn();
    }

    void Spawn()
    {
        for(int i = 0; i < _platformQuantity; i++)
        {
            if (i < 4)
            {
                _platformPosition.x -= 1.5f;
                _platformPosition.y += 0.75f;
                _leftDecorationPosition.x -= 1.5f;
                _leftDecorationPosition.y += 0.75f;
                _rightDecorationPosition.x -= 1.5f;
                _rightDecorationPosition.y += 0.75f;
                
                _platform = Instantiate(_platform, _platformPosition, Quaternion.identity);
                _leftDecoration = Instantiate(_leftDecoration, _leftDecorationPosition, Quaternion.identity);
                _rightDecoration = Instantiate(_rightDecoration, _rightDecorationPosition, Quaternion.identity);
                _coin = Instantiate(_coin, new Vector3(_platformPosition.x, _platformPosition.y + 0.8f, _platformPosition.z), Quaternion.identity);

                _leftDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
                _rightDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
                _coin.gameObject.SetActive(Random.value < 0.2f ? true : false);
            }

            else
            {
                if (Random.value < 0.5f)
                {
                    _platformPosition = new Vector3(_platformPosition.x -= _lateralDeviation, _platformPosition.y += 0.75f, _platformPosition.z);
                    _leftDecorationPosition = new Vector3(_leftDecorationPosition.x -= _lateralDeviation, _leftDecorationPosition.y += 0.75f, _leftDecorationPosition.z);
                    _rightDecorationPosition = new Vector3(_rightDecorationPosition.x -= _lateralDeviation, _rightDecorationPosition.y += 0.75f, _rightDecorationPosition.z);
                }

                else
                {
                    _platformPosition = new Vector3(_platformPosition.x, _platformPosition.y += 0.75f, _platformPosition.z -= _lateralDeviation);
                    _leftDecorationPosition = new Vector3(_leftDecorationPosition.x, _leftDecorationPosition.y += 0.75f, _leftDecorationPosition.z -= _lateralDeviation);
                    _rightDecorationPosition = new Vector3(_rightDecorationPosition.x, _rightDecorationPosition.y += 0.75f, _rightDecorationPosition.z -= _lateralDeviation);
                }

                _leftDecoration = Instantiate(_leftDecoration, _leftDecorationPosition, Quaternion.identity);
                _rightDecoration = Instantiate(_rightDecoration, _rightDecorationPosition, Quaternion.identity);
                _platform = Instantiate(_platform, _platformPosition, Quaternion.identity);
                _coin = Instantiate(_coin, new Vector3(_platformPosition.x, _platformPosition.y + 0.8f, _platformPosition.z), Quaternion.identity);

                _leftDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
                _rightDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
                _coin.gameObject.SetActive(Random.value < 0.2f ? true : false);
            }

            _queuePlatforms.Enqueue(_platform);
            _queueLeftDecorations.Enqueue(_leftDecoration);
            _queueRightDecorations.Enqueue(_rightDecoration);
            _queueCoins.Enqueue(_coin);
        }
    }

    public void Pull()
    {
        _platform = _queuePlatforms.Dequeue();
        _leftDecoration = _queueLeftDecorations.Dequeue();
        _rightDecoration = _queueRightDecorations.Dequeue();
        _coin = _queueCoins.Dequeue();
        
        if (Random.value < 0.5f)
        {
            _platformPosition = new Vector3(_platformPosition.x -= _lateralDeviation, _platformPosition.y += 0.75f, _platformPosition.z);
            _leftDecorationPosition = new Vector3(_leftDecorationPosition.x -= _lateralDeviation, _leftDecorationPosition.y += 0.75f, _leftDecorationPosition.z);
            _rightDecorationPosition = new Vector3(_rightDecorationPosition.x -= _lateralDeviation, _rightDecorationPosition.y += 0.75f, _rightDecorationPosition.z);
        }

        else
        {
            _platformPosition = new Vector3(_platformPosition.x, _platformPosition.y += 0.75f, _platformPosition.z -= _lateralDeviation);
            _leftDecorationPosition = new Vector3(_leftDecorationPosition.x, _leftDecorationPosition.y += 0.75f, _leftDecorationPosition.z -= _lateralDeviation);
            _rightDecorationPosition = new Vector3(_rightDecorationPosition.x, _rightDecorationPosition.y += 0.75f, _rightDecorationPosition.z -= _lateralDeviation);
        }

        _platform.transform.position = _platformPosition;
        _leftDecoration.transform.position = _leftDecorationPosition;
        _rightDecoration.transform.position = _rightDecorationPosition;
        _coin.transform.position = new Vector3(_platformPosition.x, _platformPosition.y + 0.8f, _platformPosition.z);

        _platform.gameObject.SetActive(true);
        _leftDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
        _rightDecoration.gameObject.SetActive(Random.value < 0.2f ? true : false);
        _coin.gameObject.SetActive(Random.value < 0.2f ? true : false);
        
        _queuePlatforms.Enqueue(_platform);
        _queueLeftDecorations.Enqueue(_leftDecoration);
        _queueRightDecorations.Enqueue(_rightDecoration);
        _queueCoins.Enqueue(_coin);
    }
}
