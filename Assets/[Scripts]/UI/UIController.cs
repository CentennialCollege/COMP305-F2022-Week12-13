using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
        SoundManager.Instance().DestroyPool();
    }

    public void OnOptionsButton_Pressed()
    {
        GameConfig.Instance().PreviousScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Options");
        SoundManager.Instance().DestroyPool();
    }

    public void OnCloseButton_Pressed()
    {
        SceneManager.LoadScene(GameConfig.Instance().PreviousScene);
        SoundManager.Instance().DestroyPool();
    }

    public void OnMasterVolume_Changed(float volume)
    {
        SoundManager.Instance().mixer.SetFloat("MasterVolume", volume);
    }

    public void OnSoundFXVolume_Changed(float volume)
    {
        SoundManager.Instance().mixer.SetFloat("SoundFXVolume", volume);
    }

    public void OnMusicVolume_Changed(float volume)
    {
        SoundManager.Instance().mixer.SetFloat("MusicVolume", volume);
    }



    public void OnExitButton_Pressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
