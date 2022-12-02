using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance().BuildPool();
        SoundManager.Instance().PlayMusic(MusicType.START_SOUNDTRACK);
    }


}
