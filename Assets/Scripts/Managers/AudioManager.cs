using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static float musicVolume;
    public static float sfxVolume;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicVolume = 1;
            sfxVolume = 1;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        if (musicSlider.value != musicVolume)
            musicSlider.value = musicVolume;
        if (sfxSlider.value != sfxVolume)
            sfxSlider.value = sfxVolume;
    }

    private void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20); 
        musicVolume = volume;
    }
    private void SetSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
        sfxVolume = volume;
    }
}
