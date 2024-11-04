using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _stopSoundButton;
    public GameObject _startSoundButton;

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
