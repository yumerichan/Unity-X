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
        //  キー入力
        //=======================        

        bool run = false;

        //走る
        if (Input.GetKey(KeyCode.LeftControl))
        {
            run = true;
        }

        //左移動
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

        //右移動
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

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //ジャンプ力がほしい

        }

        //しゃがみ
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //アニメーション次第
        }

        //移動
        pos += move;

        //移動済みの座標を格納
        transform.position = pos;
    }
}
