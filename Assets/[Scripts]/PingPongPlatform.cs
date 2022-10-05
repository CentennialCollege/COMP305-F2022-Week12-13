using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public enum PlatformDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN
}

[System.Serializable]
public struct Arrows
{
    public GameObject right;
    public GameObject left;
    public GameObject up;
    public GameObject down;
    public GameObject diagonalUp;
    public GameObject diagonalDown;
}


public class PingPongPlatform : MonoBehaviour
{
    public Vector2 startPoint;
    public PlatformDirection direction;

    public Arrows arrows;
    

    [Range(1.0f, 20.0f)]
    public float horizontalDistance = 8.0f;
    [Range(1.0f, 20.0f)]
    public float verticalDistance = 0.0f;
    [Range(1.0f, 20.0f)]
    public float horizontalSpeed = 1.0f;
    [Range(1.0f, 20.0f)]
    public float verticalSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case PlatformDirection.HORIZONTAL:
                transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalDistance) + startPoint.x, startPoint.y, 0.0f);
                arrows.right.SetActive(true);
                arrows.left.SetActive(true);
                break;
            case PlatformDirection.VERTICAL:
                transform.position = new Vector3(startPoint.x,Mathf.PingPong(Time.time * verticalSpeed, verticalDistance) + startPoint.y, 0.0f);
                arrows.up.SetActive(true);
                arrows.down.SetActive(true);
                break;
            case PlatformDirection.DIAGONAL_UP:
                transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalDistance) + startPoint.x, Mathf.PingPong(Time.time * verticalSpeed, verticalDistance) + startPoint.y, 0.0f);
                arrows.diagonalUp.SetActive(true);
                break;
            case PlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalDistance) + startPoint.x, startPoint.y - Mathf.PingPong(Time.time * verticalSpeed, verticalDistance), 0.0f);
                arrows.diagonalDown.SetActive(true);
                break;
        }

        
    }
}
