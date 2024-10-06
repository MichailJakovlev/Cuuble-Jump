using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _timer;
    float _startTime;

    public IEnumerator Package(GameObject _platform, GameObject player)
    {
        _startTime = _timer;
        while (_timer >= 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        _timer = _startTime;

        if(gameObject.transform.position.y <= player.transform.position.y)
        {
            _platform.gameObject.SetActive(false);
        }
    }
}
