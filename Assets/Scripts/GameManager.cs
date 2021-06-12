using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private float secondsBetweenSwitch = 45;

    private float elapsedTimeSinceSwitch = 15;
    private int emotionState = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeSinceSwitch += Time.deltaTime;
        if(elapsedTimeSinceSwitch > secondsBetweenSwitch - 3.0f)
        {
            emotionState = 5;
        }
        if(elapsedTimeSinceSwitch > secondsBetweenSwitch)
        {
            elapsedTimeSinceSwitch = 0;
            emotionState = Random.Range(1,5);
        }
    }

    public int GetCurrentEmotion()
    {
        return emotionState;
    }
}