using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ConveyorDirection
{
    RIGHT,
    LEFT
}


public class ConveyorPlatformController : MonoBehaviour
{
    public ConveyorDirection direction;
    public SurfaceEffector2D surfaceEffector;
    public GameObject rightArrows;
    public GameObject leftArrows;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();
        speed = surfaceEffector.speed;

        switch (direction)
        {
            case ConveyorDirection.RIGHT:
                surfaceEffector.speed = speed;
                rightArrows.SetActive(true);
                break;
            case ConveyorDirection.LEFT:
                surfaceEffector.speed = -speed;
                leftArrows.SetActive(true);
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
