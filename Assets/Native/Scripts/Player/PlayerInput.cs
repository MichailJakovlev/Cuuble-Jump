using UnityEngine;

public class PlayerInput : MonoBehaviour
{  
    [SerializeField] private Movement _movement;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LoseTracker _loseTracker;

    public bool _inputAllowed = true;
    bool _isNotMobile = true;
    float _sceenSide;
    int i;

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
        float _side;
        if (Input.anyKeyDown && Input.GetAxisRaw("Horizontal") != 0 && _isNotMobile)
        {
            _side = Input.GetAxisRaw("Horizontal");   
            Jump(_side);
        }
        
        if (Input.GetMouseButtonDown(0) && _isNotMobile == false)
        {
            if(Input.mousePosition.x < _sceenSide)
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
        if(_movement._isNotMoving && _inputAllowed)
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
}
