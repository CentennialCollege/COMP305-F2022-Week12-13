using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneGameController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance().BuildPool();
        SoundManager.Instance().PlaySoundFX(SoundFXType.DEATH);
        SoundManager.Instance().PlayMusic(MusicType.END_SOUNDTRACK);
    }

}
