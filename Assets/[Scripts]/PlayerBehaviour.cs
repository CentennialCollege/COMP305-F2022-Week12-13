using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speed = 10.0f;
    public float horizontalForce = 10.0f;
    public float verticalForce = 10.0f;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;


    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Jump");

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);

        if ((isGrounded) && (x != 0.0f))
        {
            Flip(x);

            rigidBody2D.AddForce(Vector2.right * ((x > 0.0) ? 1.0f : -1.0f) * horizontalForce);

            rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, speed);
        }

        if ((isGrounded) && (y > 0.0f))
        {
            rigidBody2D.AddForce(Vector2.up * verticalForce);
        }

    }

    public void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
