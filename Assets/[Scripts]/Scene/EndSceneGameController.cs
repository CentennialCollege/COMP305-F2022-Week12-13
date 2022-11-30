using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneGameController : MonoBehaviour
{
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        soundManager.PlaySoundFX(SoundFXType.DEATH);

        soundManager.PlayMusic(MusicType.END_SOUNDTRACK);
    }

}
