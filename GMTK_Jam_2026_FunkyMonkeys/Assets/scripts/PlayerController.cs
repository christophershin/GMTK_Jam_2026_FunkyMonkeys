using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal;

    public int MaxPlayerHP = 4;
    public int PlayerHP;
    public Slider PlayerHealthBar;

    public float speed;
    public float jumpingPower;
    private float gravityMultiplier;
    private bool doubleJumpCooldown;
    [SerializeField] private float maxGravityMulti;
    public float MaxVelocityY = 15f;
    [SerializeField] private float minFallingActivation;
    [SerializeField] private Camera _mainCamera;


    public int playerscore;
    public TMP_Text score;


    private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundLayer;


    public GameObject deathMessage;
    public GameObject winMessage;

    public Animator animator;

    [SerializeField] private AudioClip[] PlayerSounds;

    void Start()
    {
        PlayerHP = MaxPlayerHP;
        //deathMessage.SetActive(false);
        //winMessage.SetActive(false);
        Time.timeScale = 1.0f;
        //PlayerHealthBar.value = MaxPlayerHP;
        rb = GetComponent<Rigidbody2D>();
    }





    void Update()
    {


        horizontal = Input.GetAxisRaw("Horizontal");

        //if (horizontal != 0f)
        //{
        //    animator.SetBool("IsWalking", true);

        //}

        //if (horizontal == 0f)
        //{
        //    animator.SetBool("IsWalking", false);

        //}


        // jumping 
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            //float cliplength = PlayerSounds[0].length;
            //SoundManager.SoundInst.PlaySoundFXClip(PlayerSounds[0], transform, 1f, cliplength, 0.6f, 0.7f, 15);

            gravityMultiplier = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            doubleJumpCooldown = true;
        }

        // double jump
        if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && doubleJumpCooldown == true)
        {
            //float cliplength = PlayerSounds[0].length;
            //SoundManager.SoundInst.PlaySoundFXClip(PlayerSounds[0], transform, 1f, cliplength, 0.6f, 0.7f, 15);

            doubleJumpCooldown = false;
            gravityMultiplier = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        }



        fakeGravity();


        //Display Player Health
        //PlayerHealthBar.value = PlayerHP;

        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            Debug.Log("GAME OVER");
            deathMessage.SetActive(true);
            Time.timeScale = 0f;
        }


        //Flip();


    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);


    }



    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, groundLayer);
    }




    void fakeGravity()
    {
        if (rb.linearVelocity.y > MaxVelocityY * -1)
        {
            if (rb.linearVelocity.y < minFallingActivation && !IsGrounded())
            {

                gravityMultiplier += Time.deltaTime;

                if (gravityMultiplier > maxGravityMulti)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 1.5f);

                    gravityMultiplier = 0f;
                }

            }
            else
            {
                gravityMultiplier = 0f;
            }
        }
    }





    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "DeathBox")
        {
            PlayerHP = 0;
        }

        if (other.gameObject.layer == 3 && IsGrounded())
        {
            //SoundManager.SoundInst.PlaySoundFXClip(PlayerSounds[2], transform, 0.6f, 0.4f, 0.6f, 0.6f, 20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {

            //TakePlayerDamage(collision.gameObject.GetComponent<Projectiles>().damage);
            //collision.gameObject.GetComponent<Projectiles>().damage = 0;
        }

        if (collision.gameObject.layer == 3)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        if (collision.gameObject.tag == "LevelEnd")
        {
            winMessage.SetActive(true);
        }
    }



}
