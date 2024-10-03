using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private float _platformQuantity, _lateralDeviation;
    [SerializeField] private Vector3 _platformPosition;

    private void Awake()
    {
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
                Instantiate(_platform, _platformPosition, Quaternion.identity);
            }
            else if (i > (_platformQuantity / 100 * 85))
            {
                _platformPosition = Random.value < 0.5f ? new Vector3(_platformPosition.x -= _lateralDeviation, _platformPosition.y += 0.75f, _platformPosition.z) : new Vector3(_platformPosition.x, _platformPosition.y += 0.75f, _platformPosition.z -= _lateralDeviation);
                Instantiate(_platform, _platformPosition, Quaternion.identity);
            }
            else
            {
                _platformPosition = Random.value < 0.5f ? new Vector3(_platformPosition.x -= _lateralDeviation, _platformPosition.y += 0.75f, _platformPosition.z) : new Vector3(_platformPosition.x, _platformPosition.y += 0.75f, _platformPosition.z -= _lateralDeviation);
                Instantiate(_platform, _platformPosition, Quaternion.identity);
            }
        }
    }
}
