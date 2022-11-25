using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusic(MusicType.START_SOUNDTRACK);
    }


}
