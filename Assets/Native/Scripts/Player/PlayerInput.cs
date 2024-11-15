using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LoseTracker _loseTracker;
    [SerializeField] private GameObject _player;

    public bool _inputAllowed = true;
    bool _isNotMobile = true;
    float _sceenSide;
    int i;
    public float _side;

    public Vector3 _playerPosition;

    public void Awake()
    {
        _sceenSide = Screen.width / 2;

        if (Application.isMobilePlatform)
        {
            _isNotMobile = false;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && Input.GetAxisRaw("Horizontal") != 0 && _isNotMobile)
        {
            _side = Input.GetAxisRaw("Horizontal");
            Jump(_side);
        }
        if (Input.GetMouseButtonDown(0) && _isNotMobile == false)
        {
            if (Input.mousePosition.x < _sceenSide)
            {
                Jump(-1);
            }
            else
            {
                Jump(1);
            }
        }
    }

    public void Jump(float side)
    {
        if (_movement._isNotMoving && _inputAllowed)
        {
            PlayerPrefs.SetInt("isFallingOnPlatform", 0);
            _movement._isNotMoving = false;

            _player.GetComponent<BoxCollider>().enabled = true;

            if (i > 7)
            {
                _spawner.Pull();
            }

            _playerPosition = _player.transform.position;
            StartCoroutine(_movement.Jump(side < 0 ? true : false));
            i++;

            _loseTracker.Check();
        }
    }
}
