using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundManager
{
    /********************** SINGLETON SECTION ******************************/

    // Step 1. - Make the Constructor Private
    private SoundManager()
    {
        Initialize();
    }

    // Step 2. - Define private static instance member
    private static SoundManager m_instance;

    // Step 3. - Include a public static Creational Method named Instance
    public static SoundManager Instance()
    {
        return m_instance ??= new SoundManager();
    }

    /***********************************************************************/

    public AudioMixer mixer;
    private List<AudioClip> soundFX;
    private List<AudioClip> music;

    private int maxChannels;
    private GameObject channelPrefab;
    private Transform channelParent;
    private Queue<GameObject> channelPool;

    // Start is called before the first frame update
    void Initialize()
    {
        soundFX = new List<AudioClip>(); // empty List container of type AudioClip
        music = new List<AudioClip>(); // empty List container of type AudioClip
        channelPool = new Queue<GameObject>(); // creates an empty container
        InitializeSoundFX();
        maxChannels = 10;
    }

    private void InitializeSoundFX()
    {
        // pre-load soundFX
        soundFX.Add(Resources.Load<AudioClip>("Audio/jump-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/hurt-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/death-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/chest-sound"));
        soundFX.Add(Resources.Load<AudioClip>("Audio/bullet-sound"));

        // pre-load music
        music.Add(Resources.Load<AudioClip>("Audio/start-soundtrack"));
        music.Add(Resources.Load<AudioClip>("Audio/main-soundtrack"));
        music.Add(Resources.Load<AudioClip>("Audio/end-soundtrack"));

        // load the audioMixer
        mixer = Resources.Load<AudioMixer>("Audio/MasterAudioMixer");

        // load the channelPrefab
        channelPrefab = Resources.Load<GameObject>("Prefabs/Channel");
    }

    public void BuildPool()
    {
        channelParent = GameObject.Find("[CHANNELS]").transform;
        for (var i = 0; i < maxChannels; i++)
        {
            var tempChannel = CreateChannel();
            channelPool.Enqueue(tempChannel);
        }
    }

    public void DestroyPool()
    {
        for (var i = 0; i < channelPool.Count; i++)
        {
            var tempChannel = channelPool.Dequeue();
            MonoBehaviour.Destroy(tempChannel);
        }
        channelPool.Clear();
    }

    private GameObject CreateChannel()
    {
        var tempChannel = MonoBehaviour.Instantiate(channelPrefab, channelParent);
        tempChannel.GetComponent<Channel>().SetAudioMixer(mixer);
        tempChannel.GetComponent<Channel>().SetAudioMixerGroup(ChannelType.SOUND_FX);
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
        channel.GetComponent<Channel>().Play(music[(int)type], ChannelType.MUSIC);
    }

}
