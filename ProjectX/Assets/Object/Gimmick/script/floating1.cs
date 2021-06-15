using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool IsUp = true;
    bool IsDown = false;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.localEulerAngles;
        Vector3 scale = transform.localScale;
        

        if(pos.y<1)
        {
            IsUp = true;
            IsDown = false;
        }
        else if(pos.y>15)
        {
            IsUp = false;
            IsDown = true;
        }

        if(IsUp)
        {
            pos.y += 0.5f;
        }
        else if(IsDown)
        {
            pos.y -= 0.5f;
        }

        transform.position = pos;
        transform.localEulerAngles = rot;
        transform.localScale = scale;
    }
}
