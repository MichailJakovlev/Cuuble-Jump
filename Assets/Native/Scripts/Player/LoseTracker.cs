using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTracker : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Lose _lose;
    [SerializeField] private float _losingAnimationTime;
    private float _currentDirection;
    
    public Queue<float> _queueDirection;

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
        _currentDirection = _queueDirection.Dequeue();
        if(_currentDirection != _player.transform.rotation.y)
        {
            StartLose();
        }
    }

    public void StartLose()
    {
        StartCoroutine(Losing());
    }

    public IEnumerator Losing()
    {
        _input._inputAllowed = false;
        while (_losingAnimationTime > 0)
        {
            _losingAnimationTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }
        
        _lose.GameOver();
    }
}
