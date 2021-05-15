using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;

    private float hAxis;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
    }
}
