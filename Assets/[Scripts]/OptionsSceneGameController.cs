using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSceneGameController : MonoBehaviour
{
    private Slider masterVolumeSlider;
    private Slider soundFXVolumeSlider;
    private Slider musicVolumeSlider;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        soundFXVolumeSlider = GameObject.Find("SoundFXSlider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();


        soundManager.mixer.GetFloat("MasterVolume", out var masterVolume);
        masterVolumeSlider.value = masterVolume;

        soundManager.mixer.GetFloat("SoundFXVolume", out var soundFXVolume);
        soundFXVolumeSlider.value = soundFXVolume;

        soundManager.mixer.GetFloat("MusicVolume", out var musicVolume);
        musicVolumeSlider.value = musicVolume;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
