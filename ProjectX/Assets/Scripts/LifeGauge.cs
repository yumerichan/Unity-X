using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    private float Life = 100.0f;

    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(Life>=1)
            {
                Life--;
            }
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            if(Life<100)
            {
                Life++;
            }
        }

        _image.fillAmount = Life / 100.0f;
    }
}
