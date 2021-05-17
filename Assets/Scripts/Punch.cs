using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    private CapsuleCollider2D punchCollider;
    // Start is called before the first frame update
    void Start()
    {
        punchCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
        { 
           //punchCollider.enabled = True
        }
    }
}
