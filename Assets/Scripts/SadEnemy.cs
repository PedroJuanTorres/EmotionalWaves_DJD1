using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadEnemy : MonoBehaviour
{
    [SerializeField]
    private float moveDirection = -1.0f;
    [SerializeField]
    private float moveSpeed = 100.0f;
    [SerializeField]
    private float maxTimeMoving = 2.2f;

    private Rigidbody2D rb;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeLeft = maxTimeMoving;
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
    }
}

