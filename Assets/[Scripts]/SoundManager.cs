using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    private AudioMixer mixer;
    private AudioSource audioSource;
    private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips = new List<AudioClip>(); // empty List container of type AudioClip

        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/jump-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/hurt-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/death-sound"));
        mixer = Resources.Load<AudioMixer>("Audio/MasterAudioMixer");
    }

    public void Play(SoundFX sound)
    {
        audioSource.clip = audioClips[(int)sound];
        audioSource.Play();
    }

    public void OnMasterVolume_Changed(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }

    public void OnSoundFXVolume_Changed(float volume)
    {
        mixer.SetFloat("SoundFXVolume", volume);
    }

    public void OnMusicVolume_Changed(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }


}
