using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LoseTracker _loseTracker;

    bool _isNotMobile = true;
    public bool _inputAllowed = true;
    int i;

    public void Awake()
    {
        if(Application.isMobilePlatform)
        {
            _isNotMobile = false;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && Input.GetAxisRaw("Horizontal") != 0 && _movement._isNotMoving && _inputAllowed && _isNotMobile)
        {
            float _side = Input.GetAxisRaw("Horizontal");   
            Jump(_side);
        }
    }

    public void Jump(float side)
    {
        _movement._isNotMoving = false;

        if (i > 7)
        {
            _spawner.Pull();
        }

        StartCoroutine(_movement.Jump(side < 0 ? true : false));
        i++;

        _loseTracker.Check();
    }
}
