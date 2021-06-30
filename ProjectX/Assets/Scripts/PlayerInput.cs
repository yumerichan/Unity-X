using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public AudioClip se_run;
    public AudioClip se_walk;
    public AudioClip se_jump;
    private AudioSource audio_source;

    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
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
                audio_source.PlayOneShot(se_run);
            }
            else
            {
                move.x -= 1.0f;
                audio_source.PlayOneShot(se_walk);
            }
        
        }

        //右移動
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (run == true)
            {
                move.x += 3.0f;
                audio_source.PlayOneShot(se_run);
            }
            else
            {
                move.x += 1.0f;
                audio_source.PlayOneShot(se_walk);
            }
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //ジャンプ力がほしい
            audio_source.PlayOneShot(se_jump);
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
