using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin_Script : MonoBehaviour
{
    // Start is called before the first frame update

    const float PLAYER_WALK_MOVE_POS = 0.01f;
    const float PLAYER_RUN_MOVE_POS = 0.03f;
    const float PLAYER_EVASION = 4.0f;

    private Vector3 vPos;
    private Vector3 vOldPos;
    private Vector3 vMovePos;
    private Quaternion vRot;
    private Vector3 vVel;

    // Animator コンポーネント
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool AttackFlg = false;
    private bool TrunFlg = false;
    private bool LookFlg = true;
    private bool CrouchFlg = false;
    private bool IsAnime = false; //アニメ中で途中でフラグを折っては行けないものに

    //体力
    public int player_HealthPoint = 100;

    // 設定したフラグの名前
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
        vVel = rigidbody_.velocity;
        vRot = transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            //扉に入りたい
        }

        //左右移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LookFlg = false;

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
            LookFlg = true;

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

        //しゃがみ
        if(Input.GetKey(KeyCode.LeftControl))
        {
            animator_.SetBool(IsCrouch, true);
        }

        //攻撃
        if (Input.GetKey(KeyCode.T))
        {
            animator_.SetBool(IsAttaking, true);
            AttackFlg = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && AttackFlg == false)
        {
            vVel.y = PLAYER_EVASION;
            animator_.SetBool(IsJump, true);
        }
        else if (vVel.y <= 0.0f)
        {
            animator_.SetBool(IsJump, false);
        }

        if (LookFlg == true)
        {
            vRot.Set(0.0f, 1.0f, 0.0f, 1);
        }
        else
        {
            vRot.Set(0.0f, -1.0f, 0.0f, 1);
        }

        vPos = vPos + vMovePos;
        vMovePos = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = vPos;
        rigidbody_.velocity = vVel;
        transform.rotation = vRot;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 相手の名前を取得
    //    string name = collision.gameObject.name;

    //    // 相手の名前がStageだった時
    //    if (name == "Stage")
    //    {
    //        JumpFlg = true;
    //        animator_.SetBool(IsFall, false);
    //    }
    //}

    public int GetHp()
    {
        return player_HealthPoint;
    }

    public void SetHp(int hp)
    {
        player_HealthPoint = hp;
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
}
