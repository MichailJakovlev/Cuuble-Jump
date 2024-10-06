using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Spawner _spawner;
    bool _isMobile = false;
    int i;

    public void Awake()
    {
        if(Application.isMobilePlatform)
        {
            _isMobile = true;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isMobile == false)
        {
            if (Input.GetAxisRaw("Horizontal") < 0 && _movement._isMoving == false)
            {
                Left();
            }

            if (Input.GetAxisRaw("Horizontal") > 0 && _movement._isMoving == false)
            {
                Right();
            }
        }
    }

    public void Left()
    {
        if (i > 7)
        {
            _spawner.Pull();
        }

        _movement._isMoving = true;
        StartCoroutine(_movement.JumpLeft());
        i++;
    }

    public void Right()
    {
        if (i > 7)
        {
            _spawner.Pull();
        }

        _movement._isMoving = true;
        StartCoroutine(_movement.JumpRight());
        i++;
    }
}
