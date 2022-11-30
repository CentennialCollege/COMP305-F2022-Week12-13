using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public List<AudioSource> channels;
    public List<AudioClip> soundFX;
    public List<AudioClip> music;

    // Start is called before the first frame update
    void Awake()
    {
        channels = GetComponents<AudioSource>().ToList();
        soundFX = new List<AudioClip>(); // empty List container of type AudioClip

        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        // pre-load soundFX
        soundFX.Add(Resources.Load<AudioClip>("Audio/jump-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/hurt-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/death-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/chest-sound"));

        // pre-load music
        music.Add(Resources.Load<AudioClip>("Audio/start-soundtrack"));
        music.Add(Resources.Load<AudioClip>("Audio/main-soundtrack"));
        music.Add(Resources.Load<AudioClip>("Audio/end-soundtrack"));

        // load the audioMixer
        mixer = Resources.Load<AudioMixer>("Audio/MasterAudioMixer");
    }

    public void PlaySoundFX(ChannelType channelType, SoundFXType type)
    {
        channels[(int)channelType].clip = soundFX[(int)type];
        channels[(int)channelType].Play();
    }

    public void PlayMusic(MusicType type)
    {
        channels[(int)ChannelType.MUSIC].clip = this.music[(int)type];
        channels[(int)ChannelType.MUSIC].Play();
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
