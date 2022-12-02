using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSceneGameController : MonoBehaviour
{
    private Slider masterVolumeSlider;
    private Slider soundFXVolumeSlider;
    private Slider musicVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        soundFXVolumeSlider = GameObject.Find("SoundFXSlider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();

        SoundManager.Instance().BuildPool();
        SoundManager.Instance().mixer.GetFloat("MasterVolume", out var masterVolume);
        masterVolumeSlider.value = masterVolume;

        SoundManager.Instance().mixer.GetFloat("SoundFXVolume", out var soundFXVolume);
        soundFXVolumeSlider.value = soundFXVolume;

        SoundManager.Instance().mixer.GetFloat("MusicVolume", out var musicVolume);
        musicVolumeSlider.value = musicVolume;

        SoundManager.Instance().PlayMusic(MusicType.MAIN_SOUNDTRACK);

    }

}
