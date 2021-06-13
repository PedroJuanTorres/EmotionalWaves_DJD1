using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMainMenu : MonoBehaviour
{
    [SerializeField]private float secondsBetweenSwitch = 3;

    private float elapsedTimeSinceSwitch;
    private int emotionState = 1;
    private Animator    animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         elapsedTimeSinceSwitch += Time.deltaTime;
        if(elapsedTimeSinceSwitch > secondsBetweenSwitch)
        {
            emotionState ++;
            if(emotionState==5)
            {
                emotionState = 1;
            }
            elapsedTimeSinceSwitch = 0;
        }
        if(emotionState==1)
        {
            animator.SetBool("isAnger",true);
            animator.SetBool("isDisgust",false);
            animator.SetBool("isFear",false);
            animator.SetBool("isSad",false);
        }
        else if(emotionState == 2)
        {
            animator.SetBool("isAnger",false);
            animator.SetBool("isDisgust",true);
            animator.SetBool("isFear",false);
            animator.SetBool("isSad",false);
        }
        else if(emotionState == 3)
        {
            animator.SetBool("isAnger",false);
            animator.SetBool("isDisgust",false);
            animator.SetBool("isFear",true);
            animator.SetBool("isSad",false);
        }
        else
        {
            animator.SetBool("isAnger",false);
            animator.SetBool("isDisgust",false);
            animator.SetBool("isFear",false);
            animator.SetBool("isSad",true);
        }
    }
}
