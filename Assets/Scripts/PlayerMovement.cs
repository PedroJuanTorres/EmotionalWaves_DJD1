using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables that can be changed on the editor
    [SerializeField]private int         maxHealth = 3;
    [SerializeField]private float       moveSpeed = 10.0f;
    [SerializeField]private float       jumpSpeed = 100.0f;
    [SerializeField]private float       maxJumpTime = 0.1f;
    [SerializeField]private int         jumpGravityStart = 1;
    [SerializeField]private Transform   groundCheckObject;
    [SerializeField]private float       groundCheckRadius = 3.0f;
    [SerializeField]private LayerMask   groundCheckLayer;
    [SerializeField]private float       invulnerabilityDuration = 2.0f;
    [SerializeField]private float       blinkDuration = 0.1f;

    //Variables
    private float           hAxis;
    private Rigidbody2D     rb;
    private GameManager     gm;
    private Animator        animator;
    private SpriteRenderer  spriteRenderer;
    private float           timeOfJump;
    private bool            isPunching = false;
    private bool            isCrouching = false;
    private int             emotionState;
    private int             health;
    private float           invulnerabilityTimer = 0;
    private float           blinkTimer;

    // Function that runs when the game start.( Most of the code here is to get components)
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        gm = FindObjectOfType<GameManager>();

        health = maxHealth;
    }

    // Function that runs every frame, before the update
    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
    }


    // Function that runs every frame 
    private void Update()
    {
        //Moving on Horizontal
        hAxis = Input.GetAxis("Horizontal");

        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);

        bool isGround = (collider != null);

        Vector2 currentVelocity = rb.velocity;

        //Jumping mechanic
        if(Input.GetButtonDown("Jump") && (isGround))
        {
            currentVelocity.y = jumpSpeed;

            rb.velocity = currentVelocity;

            rb.gravityScale = jumpGravityStart;

            timeOfJump = Time.time;
        }
        else
        {
            float elapsedTimeSinceJump = Time.time - timeOfJump;
            if(Input.GetButton("Jump") && (elapsedTimeSinceJump < maxJumpTime))
            {
                rb.gravityScale = jumpGravityStart;
            }
            else
            {
                rb.gravityScale = 5.0f;
            }
        }

        if(currentVelocity.x < -0.1)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 180;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        else if(currentVelocity.x > 0.1)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 0;
            transform.rotation = Quaternion.Euler(currentRotation);
        }

        //Punch
        if(Input.GetButton("Fire3") && (isGround))
        { 
           //punchCollider.enabled = True

            isPunching = true;
        }  
        if(Input.GetButtonUp("Fire3") || (isGround == false))
        {
            isPunching = false;
        }
       

       //Crouch
        if(Input.GetButton("Vertical") && (isGround))
        { 
           

            isCrouching = true;
        }  
        if(Input.GetButtonUp("Vertical") || isGround == false || currentVelocity.x > 0.1 || currentVelocity.x < -0.1)
        {
            isCrouching = false;
        }
        


        //Set animations
        animator.SetFloat("AbsSpeedX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("SpeedY", currentVelocity.y);
        animator.SetBool("IsPunching",isPunching);
        animator.SetBool("IsCrouching",isCrouching);
        


        if (invulnerabilityTimer > 0 )
        {
            invulnerabilityTimer = invulnerabilityTimer - Time.deltaTime;

            blinkTimer = blinkTimer - Time.deltaTime;
            if(blinkTimer <= 0)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;

                blinkTimer = blinkDuration;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
        }
        



        emotionState = gm.GetCurrentEmotion();
        if (emotionState == 1)
        {

        }
        else if (emotionState == 2)
        {

        }
        else if (emotionState == 3)
        {

        }
        else if (emotionState == 4)
        {

        }
        else
        {

        }
        
    }


    // Function to make the player take damage
    public void TakeDamage(int damage)
    {

        if (invulnerabilityTimer > 0) return;
        if (health <= 0) return;

        health = health - damage;

        if(health<0)
        {
            health = 0;
        }
        if(health==0)
        {
            Destroy(gameObject);
        }
        else
        {
            invulnerabilityTimer = invulnerabilityDuration;
            blinkTimer = blinkDuration;
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

    }
}
