using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffectorController : MonoBehaviour
{
    public float verticalSpeed = 5.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().velocity 
            = Vector2.ClampMagnitude(other.gameObject.GetComponent<Rigidbody2D>().velocity, verticalSpeed);

    }
}
