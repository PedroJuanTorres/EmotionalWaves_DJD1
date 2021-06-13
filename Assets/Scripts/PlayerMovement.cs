using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //Variables that can be changed on the editor
    [SerializeField]private int         maxHealth = 3;
    [SerializeField]private float       moveSpeed = 10.0f;
    [SerializeField]private float       jumpSpeed = 100.0f;
    [SerializeField]private float       maxJumpTime = 0.1f;
    [SerializeField]private Collider2D  groundCollider;
    [SerializeField]private Collider2D  airCollider;
    [SerializeField]private Collider2D  crouchCollider;
    [SerializeField]private Collider2D  punchCollider;
    [SerializeField]private int         jumpGravityStart = 1;
    [SerializeField]private Transform   groundCheckObject;
    [SerializeField]private float       groundCheckRadius = 3.0f;
    [SerializeField]private LayerMask   groundCheckLayer;
    [SerializeField]private float       invulnerabilityDuration = 2.0f;
    [SerializeField]private float       blinkDuration = 0.1f;
    [SerializeField]private float       knockbackVelocity = 400.0f;
    [SerializeField]private float       knockbackDuration = 0.25f;
    [SerializeField]private AudioSource jumpSound;
    [SerializeField]private AudioSource punchSound;
    [SerializeField]private AudioSource hurtSound;

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
    private float           knockbackTimer;

    private bool    isDead          { get {return health <= 0;}}
    private bool    isInvulnerable  { get {return invulnerabilityTimer > 0;}}
    private bool    canHit          { get {return (!isInvulnerable) && (!isDead);}}
    private bool    canMove         { get {return (knockbackTimer <= 0) && (!isDead);}}

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
        if(canMove)
        {
            rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
        }  
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
            jumpSound.Play();

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
            punchSound.Play();

            punchCollider.enabled = true;

            isPunching = true;
        }  
        if(Input.GetButtonUp("Fire3") || (!isGround)|| currentVelocity.x > 150.0 || currentVelocity.x < -150.0)
        {
            punchCollider.enabled = false;

            isPunching = false;
        }
       

       //Crouch
        if(Input.GetButton("Vertical") && (isGround) && (!isPunching))
        { 
            crouchCollider.enabled = true;
            groundCollider.enabled = false;

            isCrouching = true;
        }  
        if(Input.GetButtonUp("Vertical") || !isGround  || currentVelocity.x > 0.1 || currentVelocity.x < -0.1)
        {
            crouchCollider.enabled = false;
            groundCollider.enabled = true;

            isCrouching = false;
        }
        


        //Set animations
        animator.SetFloat("AbsSpeedX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("SpeedY", currentVelocity.y);
        animator.SetBool("IsPunching",isPunching);
        animator.SetBool("IsCrouching",isCrouching);
        
        groundCollider.enabled = isGround;
        airCollider.enabled = !isGround;
        if(isCrouching)
        {
            groundCollider.enabled = false;
        }


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

        if (knockbackTimer > 0)
        {
            knockbackTimer = knockbackTimer - Time.deltaTime;
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
    public void TakeDamage(int damage, Vector2 hitDirection)
    {

        if(!canHit) return;

        hurtSound.Play();

        health = health - damage;

        if(health<0)
        {
            health = 0;
        }
        if(health==0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            invulnerabilityTimer = invulnerabilityDuration;
            blinkTimer = blinkDuration;

            Vector2 knockback = hitDirection.normalized * knockbackVelocity + Vector2.up * knockbackVelocity;

            rb.velocity = knockback;

            knockbackTimer = knockbackDuration;
        }
    }

    public int GetHealth()
    {
        return health;
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

    }
}
