using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator_;

    public Paladin_Script player_;

    enum BossState
    {
        WALK,
        RUN,
        JUMP,
        ATTACK,
        KICK,
        MELEE,
        MELEEATTACK,
    };

    private float WALK_SPEED = 0.03f;
    private float RUN_SPEED = 0.032f;
    private bool LookFlg = false;
    private bool AttackFlg = false;

    private string[] IsBossState = new string[] {"IsWalking", "IsRunning", "IsJump",
        "IsAttack", "IsKick", "IsMelee", "IsMeleeAttack", };

    BossState state_;

    float Attack_Interval;

    

    // Start is called before the first frame update
    void Start()
    {
        animator_ = GetComponent<Animator>();
        Attack_Interval = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //////ボスステージに入ってる
        //if (!player_.GetCheckBossStage()) return;


       
        //ボス
        Vector3 vPos = transform.position;
        Quaternion vRot = transform.rotation;

        //プレイヤー
        Vector3 vP_Pos = player_.GetPos();

        //距離
        float Distance = SubDistance(vP_Pos, vPos);

        Debug.Log(Distance);

        if(Distance > 2.3f)
        {
            if(LookFlg)
            {
                vPos.x -= RUN_SPEED;
            }
            else
            {
                vPos.x += RUN_SPEED;
            }

            state_ = BossState.WALK;
            animator_.SetBool(IsBossState[(int)state_], true);
        }
        else
        {
            Attack_Interval += 1.0f / 60.0f;

            if (Attack_Interval > 3.0f)
            {

                animator_.SetBool(IsBossState[(int)state_], false);

                int number = (int)Random.Range(1.0f, 4.0f);

                switch (number)
                {
                    case 1:
                        {
                            state_ = BossState.ATTACK;
                        }
                        break;
                    case 2:
                        {
                            state_ = BossState.MELEE;
                        }
                        break;
                    case 3:
                        {
                            state_ = BossState.MELEEATTACK;
                        }
                        break;
                    case 4:
                        {
                            state_ = BossState.KICK;
                        }
                        break;
                }

                animator_.SetBool(IsBossState[(int)state_], true);
            }
        }

        if (vPos.x < vP_Pos.x)
        {
            vRot.Set(0.0f, 1.0f, 0.0f, 1);
            LookFlg = false;
        }
        else
        {
            vRot.Set(0.0f, -1.0f, 0.0f, 1);
            LookFlg = true;
        }

        transform.position = vPos;
        transform.rotation = vRot;
    }

    float SubDistance(Vector3 a, Vector3 b)
    {
        float ans = 0;
        float x, y, z;
        x = a.x - b.x;
        y = a.y - b.y;
        z = a.z - b.z;

        ans = Mathf.Sqrt(x * x + y * y + z * z);
        return ans;
    }
}
