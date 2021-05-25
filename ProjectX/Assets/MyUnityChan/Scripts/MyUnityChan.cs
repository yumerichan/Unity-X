using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnityChan : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator_;

    void Start()
    {
        animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            animator_.SetBool("IsWalking", true);
            pos += transform.forward * 0.004f;
        }
        else
        {
            animator_.SetBool("IsWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rot.y -= 0.01f;

            if (rot.y < -1.0f)
            {
                rot.y = 1.0f;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rot.y += 0.01f;

            if(rot.y > 1.0f)
            {
                rot.y = -1.0f;
            }
        }

        transform.position = pos;
        transform.rotation = rot;
    }
}
