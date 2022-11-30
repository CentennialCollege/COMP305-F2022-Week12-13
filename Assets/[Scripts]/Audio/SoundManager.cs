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
    public List<AudioClip> soundFX;
    public List<AudioClip> music;

    public int maxChannels;
    public GameObject channelPrefab;
    private Transform channelParent;
    private Queue<GameObject> channelPool;

    // Start is called before the first frame update
    void Awake()
    {
        soundFX = new List<AudioClip>(); // empty List container of type AudioClip
        music = new List<AudioClip>(); // empty List container of type AudioClip

        InitializeSoundFX();

        maxChannels = 10;
        BuildPool();
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

    private void BuildPool()
    {
        for (var i = 0; i < maxChannels; i++)
        {
            var tempChannel = CreateChannel();
            channelPool.Enqueue(tempChannel);
        }
    }

    private GameObject CreateChannel()
    {
        var tempChannel = MonoBehaviour.Instantiate(channelPrefab, channelParent);
        tempChannel.GetComponent<Channel>().SetAudioMixer(mixer);
        tempChannel.SetActive(false);
        return tempChannel;
    }

    public GameObject GetChannel(ChannelType type)
    {
        var tempChannel = channelPool.Count < 1 ? CreateChannel() : channelPool.Dequeue();
        tempChannel.GetComponent<Channel>().SetAudioMixerGroup(type);
        tempChannel.SetActive(true);
        return tempChannel;
    }

    public void ReturnChannel(GameObject channel)
    {
        channel.SetActive(false);
        channelPool.Enqueue(channel);
    }

    public void PlaySoundFX(SoundFXType type)
    {
        var channel = GetChannel(ChannelType.SOUND_FX);
        channel.GetComponent<Channel>().Play(soundFX[(int)type]);
    }

    public void PlayMusic(MusicType type)
    {
        var channel = GetChannel(ChannelType.MUSIC);
        channel.GetComponent<Channel>().Play(soundFX[(int)type], ChannelType.MUSIC);
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
