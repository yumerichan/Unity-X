using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
        


        if (pos.x < 115)
        {
            IsUp = true;
            IsDown = false;
        }
        else if (pos.x > 150)
        {
            IsUp = false;
            IsDown = true;
        }

        if (IsUp)
        {
            pos.x += 0.5f;
        }
        else if (IsDown)
        {
            pos.x -= 0.5f;
        }

        transform.position = pos;
       
    }
}
