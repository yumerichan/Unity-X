using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Paladin_Script : MonoBehaviour
{
    // Start is called before the first frame update

    const float PLAYER_CROUCH_MOVE_POS = 0.03f;
    const float PLAYER_WALK_MOVE_POS = 0.05f;
    const float PLAYER_RUN_MOVE_POS = 0.12f;
    const float PLAYER_EVASION = 12.0f;
    const float PLAYER_ATTACK_MOVE_POS = 0.08f;

    private Vector3 vPos;
    private Vector3 vOldPos;
    private Vector3 vMovePos;
    private Quaternion vRot;
    private Vector3 vVel;
    private float Interval;

    // Animator 
    private Animator animator_;

    private Rigidbody rigidbody_;

    private bool AttakingFlg = false;
    private bool JumpFlg = false;
    private bool LookFlg = true;
    private bool CrouchFlg = false;
    private bool IntervalFlg = false;
    private bool AttackMove = false;
    private int AttackType = -1;
    private bool IsNotAttack = false;
    private float NotAttackInterval = 0.0f;
    private bool InBossStage = true;
    private bool IsDamage = false;
    private float DamageInterVal = 0.0f;
    private bool IsDeath = false;

    public Vector3 Gravity_ = new Vector3( 0.0f, -20.0f, 0.0f );

    //�̗�
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

    // �ݒ肵���t���O�̖��O
    private string[] IsPlayerState = new string[] { "Is Runing", "Is Jumping" , "Is Walking" , "Is Damage" ,
    "Is Death","Is Trun","Is Crouch"};

    private string IsAttack = "Is Attaking";

    void Start()
    {
        //�v���C���[���
        vPos = GetComponent<Transform>().position;
        rigidbody_ = GetComponent<Rigidbody>();
        this.animator_ = GetComponent<Animator>();
        Physics.gravity = Gravity_;
        animator_.SetInteger("AttackType", AttackType);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath) return;

        vOldPos = vPos = transform.position;
        vVel = rigidbody_.velocity;
        vRot = transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            //��ɓ��肽��
        }

        animator_.SetBool(IsPlayerState[(int)PlayerState.DAMAGE], false); 

        //���E�ړ�
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
            else if (animator_.GetBool(IsPlayerState[(int)PlayerState.CROUCH]) == true)
            {
                if (AttakingFlg == false)
                {
                    vMovePos.x -= PLAYER_CROUCH_MOVE_POS;
                }

                animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], true);
                animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
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
            //else if (animator_.GetBool(IsPlayerState[(int)PlayerState.CROUCH]) == true)
            //{
            //    if (AttakingFlg == false)
            //    {
            //        vMovePos.x += PLAYER_CROUCH_MOVE_POS;
            //    }

            //    animator_.SetBool(IsPlayerState[(int)PlayerState.WALK], true);
            //    animator_.SetBool(IsPlayerState[(int)PlayerState.RUN], false);
            //}
            else if(animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == false)
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

        //���Ⴊ�݂܂��̓W�����v
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpFlg == false && animator_.GetBool(IsPlayerState[(int)PlayerState.RUN]) == true)
            {
                vVel.y = PLAYER_EVASION;
                animator_.SetBool(IsPlayerState[(int)PlayerState.JUMP], true);
                JumpFlg = true;
            }
            else
            {
                //if (animator_.GetBool(IsPlayerState[(int)PlayerState.CROUCH]) == false)
                //{
                //    animator_.SetBool(IsPlayerState[(int)PlayerState.CROUCH], true);
                //    CrouchFlg = true;
                //}
                //else
                //{
                //    animator_.SetBool(IsPlayerState[(int)PlayerState.CROUCH], false);
                //    CrouchFlg = false;
                //}
            }
        }
        else if (vVel.y <= 0.0f)
        {
            animator_.SetBool(IsPlayerState[(int)PlayerState.JUMP], false);
        }

        {
            AnimatorStateInfo anim_info = animator_.GetCurrentAnimatorStateInfo(0);
            switch (animator_.GetInteger("AttackType"))
            {

                case 0:
                    {
                        if(anim_info.normalizedTime > 0.3 && 0.4 > anim_info.normalizedTime)
                        {
                            AttackMove = true;
                        }
                    }
                    break;
                case 1:
                    {
                        if (anim_info.normalizedTime > 0.38 && 0.48 > anim_info.normalizedTime)
                        {
                            AttackMove = true;
                        }
                    }
                    break;
                case 3:
                    {
                        if (anim_info.normalizedTime > 0.18 && 0.28 > anim_info.normalizedTime)
                        {
                            AttackMove = true;
                        }
                    }
                    break;
                case 4:
                    {
                        if (anim_info.normalizedTime > 0.28 && 0.38 > anim_info.normalizedTime)
                        {
                            AttackMove = true;
                        }
                    }
                    break;
                case 6:
                    {
                        if (anim_info.normalizedTime > 0.3 && 0.4 > anim_info.normalizedTime)
                        {
                            AttackMove = true;
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        if (AttackMove == true)
        {
            if (LookFlg)
            {
                vMovePos.x += PLAYER_ATTACK_MOVE_POS;

                AttackMove = false;
            }
            else
            {
                vMovePos.x -= PLAYER_ATTACK_MOVE_POS;

                AttackMove = false;
            }
        }

        //�U��
        if (IntervalFlg == false && Input.GetKey(KeyCode.T))
        {
            NotAttackInterval = 0.0f;
            animator_.SetBool(IsAttack, true);

            if (animator_.GetBool(IsPlayerState[(int)PlayerState.CROUCH]) == false)
            {
                AttackType++;
            }

            //�Ō�
            if (animator_.GetInteger("AttackType") == 7)
            {
                AttackType = 0;
            }

            AttakingFlg = true;

            animator_.SetInteger("AttackType", AttackType);
            IntervalFlg = true;
        }
        else if (IntervalFlg == true)
        {
            Interval += 1.0f / 60.0f;

            AttakingFlg = false;

            if (Interval >= 0.7f)
            {
                IntervalFlg = false;
                Interval = 0.0f;
            }
        }

        if (Input.GetKey(KeyCode.N))
        {
            animator_.SetBool(IsPlayerState[(int)PlayerState.DAMAGE], true);
        }

            if (animator_.GetBool(IsAttack) == true)
        {
            NotAttackInterval += 1.0f / 60.0f;

            if(NotAttackInterval >= 1.0f)
            {
                animator_.SetBool(IsAttack, false);
                AttackType = -1;
                animator_.SetInteger("AttackType", AttackType);
            }
        }

        //print(animator_.GetInteger("AttackType"));

        if (LookFlg == true)
        {
            vRot.Set(0.0f, 1.0f, 0.0f, 1);
        }
        else
        {
            vRot.Set(0.0f, -1.0f, 0.0f, 1);
        }

        if(IsDamage)
        {
            DamageInterVal += 1.0f / 60.0f;

            if(DamageInterVal >= 2.0f)
            {
                IsDamage = false;
                DamageInterVal = 0.0f;
            }
        }

        if(player_HealthPoint <= 0)
        {
            IsDeath = true;
            FadeManager.Instance.LoadLevel("GameoverScene", 2.0f);
        }

        vPos = vPos + vMovePos;
        vMovePos = new Vector3(0.0f, 0.0f, 0.0f);
        vPos.z = 13.28368f;
        transform.position = vPos;
        rigidbody_.velocity = vVel;
        transform.rotation = vRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string name = collision.gameObject.name;

        if (name == "goal1_2")
        {
            transform.position = new Vector3(162.5f, vPos.y, vPos.z);
        }

        else if (name == "goal1_3")
        {
            transform.position = new Vector3(-140.5f, vPos.y, vPos.z);
        }

        else if (name == "goal1_L")
        {
            transform.position = new Vector3(-425.0f, vPos.y, vPos.z);
        }

        else if (name == "goal1_R")
        {
            transform.position = new Vector3(-425.0f, vPos.y, vPos.z);
        }

        if (collision.gameObject.tag == "Stage")
        {
            JumpFlg = false;
        }

        if (IsDamage) return;

        if (collision.gameObject.tag == "Enemy")
        {
            player_HealthPoint -= 50;
            IsDamage = true;
        }

    }

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

    public bool GetCheckBossStage()
    {
        return InBossStage;
    }
}
