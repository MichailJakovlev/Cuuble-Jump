using UnityEngine;
using UnityEngine.Audio;

public class AudioState : MonoBehaviour
{
    [SerializeField] private AudioSource _newRecordSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _buttonClickSound;
    public AudioMixer Mixer;

    void Start()
    {
        if (PlayerPrefs.GetInt("Volume", 1) == 1)
        {
            StartSounds();
        }
        else
        {
            StopSounds();
        }
    }

    public void StopSounds()
    {
        Mixer.SetFloat("MasterVolume", -80);
        PlayerPrefs.SetInt("Volume", 0);
        PlayerPrefs.Save();
    }

    public void StartSounds()
    {
        Mixer.SetFloat("MasterVolume", 0);
        PlayerPrefs.SetInt("Volume", 1);
        PlayerPrefs.Save();
    }

    public void StopMusic()
    {
        Mixer.SetFloat("MusicVolume", -80);
    }

    public void StartMusic()
    {
        Mixer.SetFloat("MusicVolume", -30);
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
