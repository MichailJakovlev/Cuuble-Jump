using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private Platform _platform;

    private void Start()
    {
        _platform = GetComponent<Platform>();
    }
}
