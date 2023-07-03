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
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        json = FindObjectOfType<SaveWithJson>();
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    private void Start()
    {
        musicVolume = json.SaveData.musicVolume;
        sfxVolume = json.SaveData.sfxVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
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
