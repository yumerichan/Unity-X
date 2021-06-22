using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating2 : MonoBehaviour
{
    bool IsUp;
    bool IsDown;
    // Start is called before the first frame update
    void Start()
    {
        IsUp = true;
        IsDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(IsUp)
        {
            pos.y += 0.1f;
        }
        if(IsDown)
        {
            pos.y -= 0.03f;
        }
        if(pos.y>13.0f)
        {
            IsUp = false;
            IsDown = true;
        }
        else if(pos.y<6.0f)
        {
            IsDown = false;
            IsUp = true;
        }




        transform.position = pos;
    }
}
