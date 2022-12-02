using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Channel : MonoBehaviour
{
    public ChannelType channelType;
    public AudioMixer audioMixer;
    public AudioMixerGroup mixerGroup;
    private AudioSource audioSource;    

    // Start is called before the first frame update
    void Awake()
    {
        channelType = ChannelType.SOUND_FX;
        audioSource = GetComponent<AudioSource>();
    }

    public void SetAudioMixer(AudioMixer mixer)
    {
        audioMixer = mixer;
    }

    public void SetAudioMixerGroup(ChannelType type)
    {
        channelType = type;

        try
        {
            switch (channelType)
            {
                case ChannelType.SOUND_FX:
                    Debug.Log("In SOUND_FX: " + audioMixer.FindMatchingGroups("SoundFX")[0]);
                    mixerGroup = audioMixer.FindMatchingGroups("SoundFX")[0];
                    break;
                case ChannelType.MUSIC:
                    mixerGroup = audioMixer.FindMatchingGroups("Music")[0];
                    audioSource.loop = true;
                    break;
            }

            audioSource.outputAudioMixerGroup = mixerGroup;
        }
        catch
        {
            throw new Exception("ERROR: AudioMixer not set");
        }
    }

    public void Play(AudioClip clip, ChannelType type = ChannelType.SOUND_FX)
    {
        SetAudioMixerGroup(type);
        audioSource.clip = clip;
        audioSource.Play();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
