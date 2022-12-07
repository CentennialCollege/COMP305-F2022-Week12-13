using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneGameController : MonoBehaviour
{
    public GameObject miniMap;


    void Start()
    {
        BulletManager.Instance().BuildPool();
        SoundManager.Instance().BuildPool();
        //SoundManager.Instance().PlayMusic(MusicType.MAIN_SOUNDTRACK);

        miniMap = GameObject.Find("MiniMap");
        miniMap.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }
    }
}
