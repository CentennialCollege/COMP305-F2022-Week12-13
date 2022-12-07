using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneGameController : MonoBehaviour
{
    void Start()
    {
        BulletManager.Instance().BuildPool();
        SoundManager.Instance().BuildPool();
        //SoundManager.Instance().PlayMusic(MusicType.MAIN_SOUNDTRACK);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameConfig.Instance().PreviousScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Options");
            BulletManager.Instance().DestroyPool();
            SoundManager.Instance().DestroyPool();
        }
    }
}
