using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject _stopSoundButton;
    public GameObject _startSoundButton;
    [SerializeField] private Button _reviewButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Volume", 1) == 1)
        {
            EnableSoundButton();
        }
        else
        {
            DisableSoundButton();
        }
    }

    public void ReviewButtonOff()
    {
        _reviewButton.interactable = false;
    }

    public void DisableSoundButton()
    {
        _stopSoundButton.SetActive(false);
        _startSoundButton.SetActive(true);
    }

    public void EnableSoundButton()
    {
        _stopSoundButton.SetActive(true);
        _startSoundButton.SetActive(false);
    }
}
