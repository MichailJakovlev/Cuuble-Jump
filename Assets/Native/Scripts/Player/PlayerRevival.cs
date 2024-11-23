using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRevival : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private GameObject _popupScreen;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Movement _movement;
    [SerializeField] private LoseTracker _loseTracker;

    private Transform nextPLatform;

    private void QueueChange()
    {
        _loseTracker._currentDirection = _loseTracker._queueDirection.Dequeue();
        _loseTracker._collison = _loseTracker._queueDecorationCollision.Dequeue();
    }

    private Transform FindNextPlatform(List<Platform> platforms, float y)
    {
        return platforms.Find(item => item.transform.position.y == _playerInput._playerPosition.y + y).transform;
    }

    public void Revive()
    {
        _loseTracker._isDefeated = true;
        _player.GetComponent<BoxCollider>().enabled = false;
        _gameOverMenu.SetActive(false);
        _popupScreen.SetActive(false);
        _player.transform.GetChild(0).gameObject.SetActive(true);
        _gameState.StartGame();
        _input._inputAllowed = true;

        List<Platform> platforms = _spawner._queuePlatforms.ToList();

        if (PlayerPrefs.GetInt("isFallingOnPlatform", 0) == 1)
        {
            QueueChange();
            QueueChange();
            nextPLatform = FindNextPlatform(platforms, 0.75f);
        }
        else
        {
            QueueChange();
            nextPLatform = FindNextPlatform(platforms, 0f);
        }

        Vector3 playerPosition = new(
            nextPLatform.position.x,
            nextPLatform.position.y + 1.5f,
            nextPLatform.position.z
        );

        _player.transform.position = playerPosition;

        _cameraController.isGameover = false;
        _cameraController.target = _player.transform;
        var targetPosition = playerPosition + _cameraController._offset;
        _cameraController.transform.position = targetPosition;

        _movement._currentTime = 0f;
        _movement._jumpStrenghtCurve = new AnimationCurve(new Keyframe(0, playerPosition.y), new Keyframe(_movement._animationTime / 2, playerPosition.y + 1.5f), new Keyframe(_movement._animationTime, playerPosition.y + 0.75f));
        _movement._jumpDirectionAxisX = new AnimationCurve(new Keyframe(0, playerPosition.x), new Keyframe(_movement._animationTime, playerPosition.x - 1.5f));
        _movement._jumpDirectionAxisZ = new AnimationCurve(new Keyframe(0, playerPosition.z), new Keyframe(_movement._animationTime, playerPosition.z - 1.5f));
    }
}
