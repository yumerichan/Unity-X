using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 move = pos - pos;

        //=======================
        //  �L�[����
        //=======================

        //���ړ�
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            move.x -= 0.1f;
        }

        //�E�ړ�
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move.x += 0.1f;
        }

        //�ړ�
        pos += move;

        //�ړ��ς݂̍��W���i�[
        transform.position = pos;
    }
}
