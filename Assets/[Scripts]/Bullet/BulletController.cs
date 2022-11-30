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
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Activate()
    {
        Move();
        Invoke("DestroyYourself", 2.0f);
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
        if (gameObject.activeInHierarchy)
        {
            BulletManager.Instance().ReturnBullet(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO: Make Sound and do some damage
            BulletManager.Instance().ReturnBullet(gameObject);
        }
        
        
    }
}
