using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnOptionsButton_Pressed()
    {
        SceneManager.LoadScene("Options");
    }

    public void OnCloseButton_Pressed()
    {
        SceneManager.LoadScene("Start");
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
