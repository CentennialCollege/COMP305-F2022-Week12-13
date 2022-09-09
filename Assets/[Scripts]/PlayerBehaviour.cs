using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        switch (x)
        {
            case < 0:
                renderer.flipX = true;
                break;
            case > 0:
                renderer.flipX = false;
                break;
        }

        transform.position += new Vector3(x, 0.0f);
    }
}
