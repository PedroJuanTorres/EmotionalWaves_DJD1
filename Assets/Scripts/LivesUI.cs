using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField]private PlayerMovement player;

    private Animator    animator;
    private int         health;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.GetHealth();
        if(health==3)
        {
            animator.SetBool("isThree",true);
            animator.SetBool("isTwo",false);
            animator.SetBool("isOne",false);
        }
        if(health==2)
        {
            animator.SetBool("isTwo",true);
            animator.SetBool("isThree",false);
            animator.SetBool("isOne",false);
        }
        if(health==1)
        {
            animator.SetBool("isOne",true);
            animator.SetBool("isThree",false);
            animator.SetBool("isTwo",false);
        }
    }
}
