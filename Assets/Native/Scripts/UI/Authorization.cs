using UnityEngine;
using System;

public class Authorization : MonoBehaviour
{
    [SerializeField] public GameObject _authorizationButton;
    [SerializeField] public GameObject _authorizationAlertPanel;
    [SerializeField] public GameObject _leaderboadrHead;
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
        _leaderboadrHead.SetActive(true);
        isAuthorization = true;
    }
    public void AuthorizationAlertPanel()
    {
        if (isAuthorization == false)
        {
            _authorizationAlertPanel.SetActive(true);
            _leaderboadrHead.SetActive(false);
        }
        else
        {
            _authorizationAlertPanel.SetActive(false);
            _leaderboadrHead.SetActive(true);
        }
    }
}
