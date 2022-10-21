using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            
            other.gameObject.GetComponent<PlayerBehaviour>().life.LoseLife();
            other.gameObject.GetComponent<PlayerBehaviour>().health.ResetHealth();

            if (other.gameObject.GetComponent<PlayerBehaviour>().life.value > 0)
            {
                ReSpawn(other.gameObject);
            }
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = playerSpawnPoint.position;
    }
}
