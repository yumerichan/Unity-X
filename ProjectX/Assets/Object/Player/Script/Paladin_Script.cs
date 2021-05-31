using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin_Script : MonoBehaviour
{
    // Start is called before the first frame update

    const float PLAYER_WALK_MOVE_POS = 0.01f;
    const float PLAYER_RUN_MOVE_POS = 0.03f;
    const float PLAYER_EVASION = 5.0f;

    private Vector3 vPos;
    private Vector3 vOldPos;
    private Vector3 vMovePos;
    private Vector3 vVel;

    // Animator �R���|�[�l���g
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool JumpFlg = false;
    private bool TrunFlg = false;

    // �ݒ肵���t���O�̖��O
    private const string IsRun       = "Is Runing";
    private const string IsJump      = "Is Jumping";
    private const string IsWalking   = "Is Walking";
    private const string IsDamage    = "Is Damage";
    private const string IsDeath     = "Is Death";
    private const string IsTrun      = "Is Trun";
    private const string IsCrouch    = "Is Crouch";
    private const string IsAttaking  = "Is Attaking";
    private const string IsAttaking2 = "Is Attaking2";
    private const string IsAttaking3 = "Is Attaking3";


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

        //���E�ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vMovePos.x -= PLAYER_RUN_MOVE_POS;
                animator_.SetBool(IsRun, true);
                animator_.SetBool(IsWalking, false);
            }
            else if(animator_.GetBool(IsRun) == false)
            {
                vMovePos.x -= PLAYER_WALK_MOVE_POS;
                animator_.SetBool(IsWalking, true);
                animator_.SetBool(IsRun, false);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vMovePos.x += PLAYER_RUN_MOVE_POS;
                animator_.SetBool(IsRun, true);
                animator_.SetBool(IsWalking, false);
            }
            else
            {
                vMovePos.x += PLAYER_WALK_MOVE_POS;
                animator_.SetBool(IsWalking, true);
                animator_.SetBool(IsRun, false);
            }
        }
        else
        {
            if(animator_.GetBool(IsRun) == true)
            {
                animator_.SetBool(IsRun, false);
            }

            if (animator_.GetBool(IsWalking) == true)
            {
                animator_.SetBool(IsWalking, false);
            }
        }

        //�U��
        if (Input.GetKey(KeyCode.Z))
        {
            animator_.SetBool(IsAttaking, true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpFlg == true)
        {
            vVel.y = PLAYER_EVASION;
            animator_.SetBool(IsJump, true);

            JumpFlg = false;
        }
        else if (vVel.y <= 0.0f && JumpFlg == false)
        {
            animator_.SetBool(IsJump, false);
        }

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
