using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<SoundManager>().Play(SoundFX.DEATH);
    }

}
