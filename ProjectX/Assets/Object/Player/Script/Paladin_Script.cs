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
    private float Interval;

    // Animator コンポーネント
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool AttackFlg = false;
    private bool AttakingFlg = false;
    private bool JumpFlg = false;
    private bool LookFlg = true;
    private bool CrouchFlg = false;
    private bool IsAnime = false; //アニメ中で途中でフラグを折っては行けないものに
    private bool IntervalFlg = false;
    private int AttackType = -1;

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

    private string IsAttack = "Is Attaking";

    void Start()
    {
        //プレイヤー情報
        vPos = GetComponent<Transform>().position;
        rigidbody_ = GetComponent<Rigidbody>();
        this.animator_ = GetComponent<Animator>();
        Physics.gravity = Gravity_;
        animator_.SetInteger("AttackType", AttackType);
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
                if(AttakingFlg == false)
                {
                    vMovePos.x -= PLAYER_RUN_MOVE_POS;
                }
               
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], false);
            }
            else if(animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == false)
            {
                if (AttakingFlg == false)
                {
                    vMovePos.x -= PLAYER_WALK_MOVE_POS;
                }
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            LookFlg = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (AttakingFlg == false)
                {
                    vMovePos.x += PLAYER_RUN_MOVE_POS;
                }
              
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], false);
            }
            else
            {
                if (AttakingFlg == false)
                {
                    vMovePos.x += PLAYER_WALK_MOVE_POS;
                }
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

        //攻撃
        if (IntervalFlg == false && Input.GetKey(KeyCode.T))
        {
            AttackFlg = true;
            //AttakingFlg = true;
            IntervalFlg = true;
        }
        else if (IntervalFlg == true)
        {
            Interval += 1.0f / 60.0f;

            if (Interval >= 0.4f)
            {
                IntervalFlg = false;
                Interval = 0.0f;
            }
        }

        if (AttackFlg == true)
        {
            AttackFlg = false;
            animator_.SetBool(IsAttack, true);
            AttackType++;
            animator_.SetInteger("AttackType", AttackType);
        }

        if (Input.GetKey(KeyCode.H))
        {
            animator_.SetBool(IsAttack, false);
            AttackType = -1;
            animator_.SetInteger("AttackType", AttackType);
        }

        print(animator_.GetInteger("AttackType"));

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
