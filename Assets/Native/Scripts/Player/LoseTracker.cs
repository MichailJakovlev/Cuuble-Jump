using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTracker : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _deadParticles;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Lose _lose;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private AudioState _audioState;
    [SerializeField] private float _losingAnimationTime, _fallingTime;
    public float _currentDirection;
    public int _collison;

    public Queue<float> _queueDirection;
    public Queue<int> _queueDecorationCollision;

    private void OnEnable()
    {
        Destroyer.LosingStart += StartLose;
    }

    private void OnDisable()
    {
        Destroyer.LosingStart -= StartLose;
    }

    public void Check()
    {
        _collison = _queueDecorationCollision.Dequeue();
        _currentDirection = _queueDirection.Dequeue();

        if (_currentDirection != _player.transform.rotation.y)
        {
            if (_currentDirection == 0 && _collison == 1 || _currentDirection == 0.7071068f && _collison == -1 || _collison == 2)
            {
                Instantiate(_deadParticles, _player.transform.position, _player.transform.rotation);
                _player.transform.GetChild(0).gameObject.SetActive(false);

                _audioState.PlayCrashSound();

                StartLose(true);
            }
            else
            {
                _audioState.PlayFallSound();
                StartLose(false);
            }
        }
        else
        {
            _scoreCounter.Count();
        }
    }

    public void StartLose(bool isCollison)
    {
        _cameraController.isGameover = true;
        _input._inputAllowed = false;
        _gameOverScreen.Show();
        if (isCollison)
        {
            StartCoroutine(Losing(_losingAnimationTime));
        }
        else
        {
            StartCoroutine(FallLose(_fallingTime));
        }
    }

    public IEnumerator FallLose(float _fallingTime)
    {
        while (_fallingTime > 0)
        {
            _player.transform.position = new Vector3(
                _player.transform.position.x,
                _player.transform.position.y - 0.1f,
                _player.transform.position.z
            );

            _fallingTime -= Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.001f);
        }
        StartCoroutine(Losing(_losingAnimationTime));
    }

    public IEnumerator Losing(float _losingAnimationTime)
    {
        while (_losingAnimationTime > 0)
        {
            _input._inputAllowed = false;
            _losingAnimationTime -= Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.001f);
        }
        _lose.GameOver();
    }
}
