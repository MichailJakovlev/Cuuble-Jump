using UnityEngine;
using UnityEngine.Audio;

public class AudioState : MonoBehaviour
{
    [SerializeField] private AudioSource _newRecordSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _buttonClickSound;
    [SerializeField] private AudioSource _music;
    public AudioMixer Mixer;

    bool _isSoundsOff = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("Volume", 1) == 1)
        {
            StartSounds();
        }
        else
        {
            _isSoundsOff = true;
            StopSounds();
        }
    }

    public void StopSounds()
    {
        Mixer.SetFloat("MasterVolume", -80);
        Mixer.SetFloat("MusicVolume", -80);
        _music.mute = true;
        _music.Stop();
    }

    public void StartSounds()
    {
        if (_isSoundsOff == false)
        {
            Mixer.SetFloat("MasterVolume", 0);
            Mixer.SetFloat("MusicVolume", -30);
            _music.mute = false;
            _music.Play();
        }
    }

    public void StopMusic()
    {
        _isSoundsOff = true;
        PlayerPrefs.SetInt("Volume", 0);
        PlayerPrefs.Save();
    }

    public void StartMusic()
    {
        _isSoundsOff = false;
        PlayerPrefs.SetInt("Volume", 1);
        PlayerPrefs.Save();
    }

    public void PlayNewRecordSound()
    {
        _newRecordSound.Play();
    }

    public void PlayCrashSound()
    {
        _crashSound.Play();
    }

    public void PlayFallSound()
    {
        _fallSound.Play();
    }

    public void PlayButtonClickSound()
    {
        _buttonClickSound.Play();
    }
}
