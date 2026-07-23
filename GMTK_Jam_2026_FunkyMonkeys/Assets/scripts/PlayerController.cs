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
    private bool FireDMG;
    private bool inflictBurn;

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

    private bool isFacingRight = true;

    public int playerscore;
    public TMP_Text score;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);


    }
}
