using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class CameraFocus : MonoBehaviour, Interactable
{
    public CinemachineVirtualCamera lowPriorityCamera;
    public CinemachineVirtualCamera highPriorityCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            lowPriorityCamera.Priority = 5;
            highPriorityCamera.Priority = 10;
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            lowPriorityCamera.Priority = 10;
            highPriorityCamera.Priority = 5;
        }
    }

    public abstract void Activate();
  
}
