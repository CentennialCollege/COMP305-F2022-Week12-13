using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalSpeed = 10.0f;
    public float horizontalForce = 10.0f;
    public float verticalForce = 10.0f;
    public float airFactor = 0.5f;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    public float passThroughFactor = 0.1f;
    public float savedVerticalForce;

    [Header("Animations")]
    public Animator animator;

    [Header("Dust Trail Properties")] 
    public ParticleSystem dustTrailParticleSystem;
    public Color dustTrailColor;
    

    [Header("Health System")] 
    public HealthBarController health;
    public LifeCounterController life;


    private DeathPlaneController deathPlane;
    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GameObject.Find("Player Health System").GetComponent<HealthBarController>();
        life = GameObject.FindObjectOfType<LifeCounterController>();
        deathPlane = GameObject.FindObjectOfType<DeathPlaneController>();
        dustTrailParticleSystem = GetComponentInChildren<ParticleSystem>();
        savedVerticalForce = verticalForce;
    }

    private void Update()
    {
        if (health.value <= 0)
        {
            life.LoseLife();
            
            if (life.value > 0)
            {
                health.ResetHealth();
                deathPlane.ReSpawn(this.gameObject);
                SoundManager.Instance().PlaySoundFX(SoundFXType.DEATH);
            }
        }


        if (life.value <= 0)
        {
            SceneManager.LoadScene("End");
            BulletManager.Instance().DestroyPool();
            SoundManager.Instance().DestroyPool();
        }
    }

    private void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;

        if (isGrounded)
        {
            verticalForce = (hit.CompareTag("PassThroughPlatform")) ? verticalForce * passThroughFactor : savedVerticalForce;
        }

        Move();
        Jump();
        AirCheck();
    }

    public void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        if (x != 0.0f)
        {
            Flip(x);

            rigidBody2D.AddForce(Vector2.right * ((x > 0.0) ? 1.0f : -1.0f) * horizontalForce * ((isGrounded) ? 1 : airFactor));

           rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, horizontalSpeed);
           animator.SetInteger("AnimationState", 1);

           if (isGrounded)
           {
               CreateDustTrail();
           }

        }

        if ((isGrounded) && (x == 0))
        {
            animator.SetInteger("AnimationState", 0);
        }
    }

    public void CreateDustTrail()
    {
        dustTrailParticleSystem.GetComponent<Renderer>().material.SetColor("_Color", dustTrailColor);
        dustTrailParticleSystem.Play();
    }

    public void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0f))
        {
            rigidBody2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            SoundManager.Instance().PlaySoundFX(SoundFXType.JUMP);
        }
    }

    public void AirCheck()
    {
        if (!isGrounded)
        {
            animator.SetInteger("AnimationState", 2);
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            health.TakeDamage(30);
            if (life.value > 0)
            {
                SoundManager.Instance().PlaySoundFX(SoundFXType.HURT);
            }
        }

        if (other.CompareTag("Bullet"))
        {
            health.TakeDamage(10);
            if (life.value > 0)
            {
                SoundManager.Instance().PlaySoundFX(SoundFXType.HURT);
            }
        }
    }
}
