using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

    [Header("Screen Shake Properties")] 
    public CinemachineVirtualCamera playerCamera;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float shakeIntensity;
    public float shakeDuration;
    public float shakeTimer;
    public bool isCameraShaking;

    [Header("Collision Response")] 
    public float bounceForce;

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

        // Camera Shake Setup
        playerCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        perlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        isCameraShaking = false;
        shakeTimer = shakeDuration;
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

        // camera shake control
        if (isCameraShaking)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0.0f)
            {
                perlin.m_AmplitudeGain = 0.0f;
                shakeTimer = shakeDuration;
                isCameraShaking = false;
            }
        }
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

    public void ShakeCamera()
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        isCameraShaking = true;
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

    private void DamagePlayer(GameObject go)
    {
        switch (go.tag)
        {
            case "Enemy":
                health.TakeDamage(20);
                PlayFXAndShakeCamera();
                rigidBody2D.AddForce(new Vector2(bounceForce * (rigidBody2D.velocity.x > 0.0 ? -1.0f : 1.0f), 0.0f), ForceMode2D.Impulse);
                break;
            case "Hazard":
                health.TakeDamage(30);
                PlayFXAndShakeCamera();
                rigidBody2D.AddForce(new Vector2(bounceForce * (rigidBody2D.velocity.x > 0.0 ? -1.0f : 1.0f), 0.0f), ForceMode2D.Impulse);
                break;
            case "Bullet":
                health.TakeDamage(10);
                PlayFXAndShakeCamera();

                var velocity = go.GetComponent<Rigidbody2D>().velocity;
                rigidBody2D.AddForce(new Vector2(bounceForce * (velocity.x > 0.0 ? 1.0f : -1.0f), 0.0f), ForceMode2D.Impulse);
                break;
        }
    }

    private void PlayFXAndShakeCamera()
    {
        if (life.value > 0)
        {
            SoundManager.Instance().PlaySoundFX(SoundFXType.HURT);
            ShakeCamera();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DamagePlayer(other.gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        DamagePlayer(other.gameObject);
    }
}
