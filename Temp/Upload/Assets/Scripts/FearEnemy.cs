using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEnemy : MonoBehaviour
{
    [SerializeField]
    private float moveDirection = -1.0f;
    [SerializeField]
    private float moveSpeed = 100.0f;
    [SerializeField]
    private float maxTimeMoving = 2.2f;
    [SerializeField]
    private Collider2D collider;

    private Rigidbody2D rb;
    private GameManager gm;
    private Animator animator;
    private float timeLeft;
    private int emotionState;
    private bool isFearMonster = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        timeLeft = maxTimeMoving;

        gm = FindObjectOfType<GameManager>();

        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rb.velocity;

        currentVelocity.x = moveDirection * moveSpeed;

        rb.velocity = currentVelocity;

        timeLeft = timeLeft - Time.deltaTime;
        if(timeLeft < 0)
        {
            moveDirection = moveDirection * (-1);
            if(moveDirection>0)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.y = 180;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
            if(moveDirection<0)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.y = 0;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
            
            timeLeft = maxTimeMoving;
        }

        emotionState = gm.GetCurrentEmotion();
        if (emotionState == 3)
        {
            isFearMonster = true;
            collider.enabled = true;
        }
        else
        {
            isFearMonster = false;
            collider.enabled = false;
        }

        animator.SetBool("IsFearMonster",isFearMonster);
    }
}
