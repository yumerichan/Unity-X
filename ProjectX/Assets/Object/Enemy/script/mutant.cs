using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutant : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animator_;
    void Start()
    {
        Animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.localEulerAngles;
        Vector3 scale = transform.localScale;

        

        transform.position = pos;
        transform.localEulerAngles = rot;
        transform.localScale = scale;
    }
}
