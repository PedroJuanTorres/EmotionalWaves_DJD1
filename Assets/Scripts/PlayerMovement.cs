using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpSpeed = 100.0f;
    [SerializeField]
    private float maxJumpTime = 0.1f;
    [SerializeField]
    private int jumpGravetyStart = 1;
    [SerializeField]
    private Transform groundCheckObject;
    [SerializeField]
    private float groundCheckRadius = 3.0f;
    [SerializeField]
    private LayerMask groundCheckLayer;


    private float hAxis;
    private Rigidbody2D rb;
    private Animator animator;
    private float timeOfJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        //Moving on Axis
        hAxis = Input.GetAxis("Horizontal");

        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);

        bool isGround = (collider != null);

        Vector2 currentVelocity = rb.velocity;

        //Jumping mechanic
        if(Input.GetButtonDown("Jump") && (isGround))
        {
            currentVelocity.y = jumpSpeed;

            rb.velocity = currentVelocity;

            rb.gravityScale = jumpGravetyStart;

            timeOfJump = Time.time;
        }
        else
        {
            float elapsedTimeSinceJump = Time.time - timeOfJump;
            if(Input.GetButton("Jump") && (elapsedTimeSinceJump < maxJumpTime))
            {
                rb.gravityScale = jumpGravetyStart;
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

        animator.SetFloat("AbsSpeedX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("SpeedY", currentVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

    }
}
