using UnityEngine;
using System;

public class Authorization : MonoBehaviour
{
    [SerializeField] public GameObject _authorizationButton;
    [SerializeField] public GameObject _authorizationAlertPanel;
    [HideInInspector] public bool isAuthorization = false;

    void Start()
    {
        isAuthorization = false;
        AuthorizationAlertPanel();
    }

    public void AuthorizationClick()
    {
        _authorizationButton.SetActive(false);
        _authorizationAlertPanel.SetActive(false);
        isAuthorization = true;
    }
    public void AuthorizationAlertPanel()
    {
        if (isAuthorization == false)
        {
            _authorizationAlertPanel.SetActive(true);
        }
        else
        {
            _authorizationAlertPanel.SetActive(false);
        }
    }
}
