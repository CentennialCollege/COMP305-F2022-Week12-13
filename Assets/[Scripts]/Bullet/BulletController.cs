using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector2 direction;
    public Rigidbody2D rigidbody2D;
    [Range(1.0f, 100.0f)]
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Move();
        Invoke("DestroyYourself", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Move()
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void Rotate()
    {
        transform.RotateAround(transform.position, Vector3.forward, 5.0f);
    }

    public void DestroyYourself()
    {
        Destroy(this.gameObject);
    }
}
