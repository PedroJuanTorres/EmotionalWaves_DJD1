using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float followSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(followTarget.position.x>-635) 
        {
            Vector3 currentPos = transform.position;

            if(followTarget != null)
            {
                Vector3 targetPos = followTarget.position;
                Vector3 error = targetPos - currentPos;

                targetPos = currentPos + error * followSpeed;

                currentPos = new Vector3(targetPos.x, currentPos.y,currentPos.z);
            }

            transform.position = currentPos;
        }
        
    }
}
