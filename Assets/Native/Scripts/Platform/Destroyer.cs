using System;
using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public static event Action<bool> LosingStart;
    [SerializeField] private float _timer;
    [SerializeField] private AudioState _audioState;

    float _startTime;
    bool _isLosed = true;

    public IEnumerator Package(GameObject platform, GameObject player)
    {
        _startTime = _timer;

        while (_timer >= 0)
        {
            _timer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }

        _timer = _startTime * 2;

        while (_timer >= 0 && platform.transform.position.y <= player.transform.position.y)
        {
            _timer -= Time.fixedDeltaTime;

            if (player.transform.position.y - platform.transform.position.y <= 1.6f)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.1f, player.transform.position.z);
                if (_isLosed)
                {
                    LosingStart?.Invoke(true);
                    _isLosed = false;
                }

               // _audioState = FindAnyObjectByType<GameState>().GetComponent<AudioState>();
               // _audioState.PlayFallSound();

                PlayerPrefs.SetInt("isFallingOnPlatform", 1);
                yield return new WaitForSeconds(0.001f);
            }
            platform.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y - 0.1f, platform.transform.position.z);

            yield return new WaitForSeconds(0.001f);
        }
        _timer = _startTime;
    }
}
