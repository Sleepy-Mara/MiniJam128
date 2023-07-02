using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private SaveWithJson json;
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
            musicVolume = json.SaveData.musicVolume;
            sfxVolume = json.SaveData.sfxVolume;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        json = FindObjectOfType<SaveWithJson>();
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
        SaveData saveData = json.SaveData;
        saveData.musicVolume = musicVolume;
        json.SaveData = saveData;
    }
    private void SetSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
        sfxVolume = volume;
        SaveData saveData = json.SaveData;
        saveData.sfxVolume = sfxVolume;
        json.SaveData = saveData;
    }
}
