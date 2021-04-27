using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
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

        bool run = false;

        //����
        if (Input.GetKey(KeyCode.LeftControl))
        {
            run = true;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if(run == true)
            {
                move.x -= 3.0f;
            }
            else
            {
                move.x -= 1.0f;
            }
        
        }

        //�E�ړ�
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (run == true)
            {
                move.x += 3.0f;
            }
            else
            {
                move.x += 1.0f;
            }
        }

        //�W�����v
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //�W�����v�͂��ق���

        }

        //���Ⴊ��
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //�A�j���[�V��������
        }

        //�ړ�
        pos += move;

        //�ړ��ς݂̍��W���i�[
        transform.position = pos;
    }
}
