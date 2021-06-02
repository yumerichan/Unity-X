using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin_Script : MonoBehaviour
{
    // Start is called before the first frame update

    const float PLAYER_WALK_MOVE_POS = 0.03f;
    const float PLAYER_RUN_MOVE_POS = 0.1f;
    const float PLAYER_EVASION = 6.0f;

    private Vector3 vPos;
    private Vector3 vOldPos;
    private Vector3 vMovePos;
    private Quaternion vRot;
    private Vector3 vVel;

    // Animator コンポーネント
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool AttackFlg = false;
    private bool JumpFlg = false;
    private bool LookFlg = true;
    private bool CrouchFlg = false;
    private bool IsAnime = false; //アニメ中で途中でフラグを折っては行けないものに

    public Vector3 Gravity_ = new Vector3( 0.0f, -20.0f, 0.0f );

    //体力
    public int player_HealthPoint = 100;

    enum PlayerState
    {
        RUN,
        JUMP,
        WALK,
        DAMAGE,
        DEATH,
        TRUN,
        CROUCH,
    }; 

    enum PlayerAttackKind
    {
        SLASH1,
        SLASH2,
        SLASH3,
        KICK,
        MELEE1,
        MELEE2,
        MELEE3,
    };

    // 設定したフラグの名前
    private string[] IsPlayerState = new string[] { "Is Runing", "Is Jumping" , "Is Walking" , "Is Damage" ,
    "Is Death","Is Trun","Is Crouch"};

    private string[] IsAttacking = new string[] { "Is Attaking", "Is Attaking2", "Is Attaking3", "Is Attaking4",
        "Is Attaking5" , "Is Attaking6" , "Is Attaking7" };

    void Start()
    {
        //プレイヤー情報
        vPos = GetComponent<Transform>().position;
        rigidbody_ = GetComponent<Rigidbody>();
        this.animator_ = GetComponent<Animator>();
        Physics.gravity = Gravity_;
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
                    animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], true);
                    animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], false);
            }
            else if(animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == false)
            {
                vMovePos.x -= PLAYER_WALK_MOVE_POS;
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            LookFlg = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {

                    vMovePos.x += PLAYER_RUN_MOVE_POS;
                    animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], true);
                    animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], false);
            }
            else
            {
                vMovePos.x += PLAYER_WALK_MOVE_POS;
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
            }
        }
        else
        {
            if(animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == true)
            {
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
            }

            if (animator_.GetBool(IsPlayerState[(int)PlayerState.WALK]) == true)
            {
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], false);
            }
        }

        //攻撃
        if (Input.GetKey(KeyCode.T))
        {
            AttackFlg = true;
        }

        //しゃがみまたはジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == true)
            {
                vVel.y = PLAYER_EVASION;
                animator_.SetBool(IsPlayerState[(int)PlayerState.JUMP], true);
                JumpFlg = true;
            }
            else
            {
                animator_.SetBool(IsPlayerState[(int)PlayerState.CROUCH], true);
            }
        }
        else if (vVel.y <= 0.0f)
        {
            JumpFlg = false;
            animator_.SetBool(IsPlayerState[(int)PlayerState.JUMP], false);
        }

        if(AttackFlg == true)
        {
            AttackFlg = false;

            //2回目から
            for (int attack_index = 0; attack_index < System.Enum.GetValues(typeof(PlayerAttackKind)).Length - 1; attack_index++)
            {
                int current_index = 1;

                bool First_Atk = true;

                if (animator_.GetBool(IsAttacking[attack_index]) == true)
                {
                    //今のままだと連打したら終わる
                   
                    current_index += attack_index;

                    //if(current_index >= System.Enum.GetValues(typeof(PlayerAttackKind)).Length)
                    //{
                    //    break;
                    //}
                    //else
                    //{
                    animator_.SetBool(IsAttacking[current_index], true);
                    First_Atk = false;
                    //break;
                    //}

                    //animator_.SetBool(IsAttacking[attack_index], false);
                }
                
                if(First_Atk == true)
                {
                    animator_.SetBool(IsAttacking[(int)PlayerAttackKind.SLASH1], true);
                }
            }
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

    //    相手の名前がStageだった時
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
