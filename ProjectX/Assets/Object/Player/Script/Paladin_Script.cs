using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin_Script : MonoBehaviour
{
    // Start is called before the first frame update

    const float PLAYER_MOVE_POS = 0.01f;
    const float PLAYER_EVASION = 5.0f;

    private Vector3 vPos;
    private Vector3 vOldPos;
    private Vector3 vMovePos;
    private Vector3 vVel;

    // Animator �R���|�[�l���g
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool JumpFlg = false;

    // �ݒ肵���t���O�̖��O
    private const string IsRun = "isRun";
    private const string IsJump = "isJump";
    private const string IsFall = "isFall";

    void Start()
    {
        vPos = GetComponent<Transform>().position;
        rigidbody_ = GetComponent<Rigidbody>();
        this.animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vOldPos = vPos = transform.position;
        //vVel = rigidbody_.velocity;

        if (Input.GetKey(KeyCode.W))
        {
            //���ɓ��肽��
        }

        if (Input.GetKey(KeyCode.A))
        {
            vMovePos.x -= PLAYER_MOVE_POS;
            //animator_.SetBool(IsRun, true);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            vMovePos.x += PLAYER_MOVE_POS;
            //animator_.SetBool(IsRun, true);
        }

        //if (Input.GetKeyDown(KeyCode.Space) && JumpFlg == true)
        //{
        //    vVel.y = PLAYER_EVASION;
        //    animator_.SetBool(IsRun, false);
        //    animator_.SetBool(IsJump, true);

        //    JumpFlg = false;
        //}
        //else if (vVel.y <= 0.0f && JumpFlg == false && animator_.GetBool(IsFall) == false)
        //{
        //    animator_.SetBool(IsJump, false);
        //    animator_.SetBool(IsFall, true);
        //}

        vPos = vPos + vMovePos;
        vMovePos = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = vPos;
        //rigidbody_.velocity = vVel;

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // ����̖��O���擾
    //    string name = collision.gameObject.name;

    //    // ����̖��O��Stage��������
    //    if (name == "Stage")
    //    {
    //        JumpFlg = true;
    //        animator_.SetBool(IsFall, false);
    //    }
    //}
}
