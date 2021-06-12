using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionsPanel : MonoBehaviour
{
    private GameManager gm;
    private int         emotionState;
    private Animator    animator;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionState = gm.GetCurrentEmotion();
        if (emotionState == 1)
        {
            animator.SetBool("isAnger",true);
            animator.SetBool("isSwitching",false);
        }
        else if (emotionState == 2)
        {
            animator.SetBool("isDisgust",true);
            animator.SetBool("isSwitching",false);
        }
        else if (emotionState == 3)
        {
            animator.SetBool("isFear",true);
            animator.SetBool("isSwitching",false);
        }
        else if (emotionState == 4)
        {
            animator.SetBool("isSad",true);
            animator.SetBool("isSwitching",false);
        }
        else
        {
            animator.SetBool("isAnger",false);
            animator.SetBool("isDisgust",false);
            animator.SetBool("isFear",false);
            animator.SetBool("isSad",false);
            animator.SetBool("isSwitching",true);
        }
        
    }
}
