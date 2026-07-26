using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public float horizontal;
    private bool isFacingRight = false;

    public int MaxPlayerHP = 4;
    public int PlayerHP;
    //public Slider PlayerHealthBar;

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
    public Scrollbar bombscrollbar;
    [HideInInspector]
    public float bombDiffuseValue = 0;
    private float scrollbarTimer = 0;


    private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundLayer;


    public Animator animator;

    private GameObject gameManager;

    [SerializeField] private List<AudioClip> playersounds;
    private AudioSource playerAudioSource;

    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
        PlayerHP = MaxPlayerHP;
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
    }





    void Update()
    {

        if (PlayerHP>0 && gameManager.GetComponent<GameManager>().isPlayerWin == false)
        {
            horizontal = Input.GetAxisRaw("Horizontal");


            // jumping 
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {

                gravityMultiplier = 0f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                doubleJumpCooldown = true;

                playerAudioSource.clip = playersounds[1];
                playerAudioSource.Play();
            }

            // double jump
            if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && doubleJumpCooldown == true)
            {


                doubleJumpCooldown = false;
                gravityMultiplier = 0f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

                playerAudioSource.clip = playersounds[1];
                playerAudioSource.Play();

            }


            Flip();
        }
            
        

        fakeGravity();


        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            Debug.Log("GAME OVER");
           
        }


        

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 3 * Time.deltaTime);

        Bomb_UI();
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        

    }

    private void LateUpdate()
    {
        _mainCamera.transform.rotation = Quaternion.identity;
    }



    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, groundLayer);
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }


    private void Bomb_UI()
    {
        if (bombscrollbar.IsActive() && PlayerHP>0 && gameManager.GetComponent<GameManager>().isPlayerWin == false)
        {

            if (Input.GetKey(KeyCode.E))
            {

                bombDiffuseValue += 0.7f*Time.deltaTime;
                scrollbarTimer = 0.5f;
            }



            bombscrollbar.GetComponent<Image>().fillAmount = bombDiffuseValue;

            if (bombDiffuseValue >=0 && bombDiffuseValue<1)
            {
                scrollbarTimer -= Time.deltaTime;

                if (scrollbarTimer <= 0)
                {
                    bombDiffuseValue -= 0.5f*Time.deltaTime;


                }
            }else if (bombDiffuseValue >= 1)
            {
                bombDiffuseValue = 1;
            }


        }

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

        if (other.gameObject.layer == 3 && IsGrounded())
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            gameManager.GetComponent<GameManager>().playerPoints += 50;
            gameManager.GetComponent<GameManager>().CreatePointsText("+", 50);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 50);
            collision.gameObject.GetComponentInParent<EnemyController>().EnemyTakeDamage(50);
        }

        if (collision.gameObject.tag == "DeathBox")
        {
            PlayerHP = 0;
        }


        if (collision.gameObject.layer == 3)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        if (collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    public void TakeDamage(int num)
    {
        PlayerHP -= num;
        playerAudioSource.clip = playersounds[0];
        playerAudioSource.Play();

        gameManager.GetComponent<GameManager>().playerPoints += 10;
        gameManager.GetComponent<GameManager>().CreatePointsText("+", 10);

    }


}
