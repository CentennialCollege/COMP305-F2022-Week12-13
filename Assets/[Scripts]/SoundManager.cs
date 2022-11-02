using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
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
    }

    public void Play(SoundFX sound)
    {
        audioSource.clip = audioClips[(int)sound];
        audioSource.Play();
    }

    
}
