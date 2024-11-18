using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] public GameObject _guideDesktop;
    [SerializeField] public GameObject _guideMobile;

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            _guideDesktop.SetActive(false);
            _guideMobile.SetActive(true);
        }
        else
        {
            _guideDesktop.SetActive(true);
            _guideMobile.SetActive(false);
        }
    }

}
