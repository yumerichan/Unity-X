using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating4 : MonoBehaviour
{
    bool IsRight, IsLeft;
    // Start is called before the first frame update
    void Start()
    {
        IsLeft = true;
        IsRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

       
        if (pos.x < 10.0f)
        {
            IsLeft = false;
            IsRight = true;
        }
        else if (pos.x > 50.0f)
        {
            IsLeft = true;
            IsRight = false;
        }
        if (IsLeft)
        {
            pos.x += 0.1f;
        }
        if (IsRight)
        {
            pos.x -= 0.1f;
        }



        transform.position = pos;
    }
}
