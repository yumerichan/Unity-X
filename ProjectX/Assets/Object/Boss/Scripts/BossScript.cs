using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int Boss_Hp = 100;
    private float RUN_SPEED = 0.032f;
    private bool LookFlg = false;
    private bool AttackFlg = false;
    private bool IsDeath = false;

    private string[] IsBossState = new string[] {"IsWalking", "IsRunning", "IsJump",
        "IsAttack", "IsKick", "IsMelee", "IsMeleeAttack", };

    BossState state_;

    float Attack_Interval;

    public BossAttack bossattack_;

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
        if (!player_.GetCheckBossStage()) return;

        if (IsDeath) return;
       
        //ボス
        Vector3 vPos = transform.position;
        Quaternion vRot = transform.rotation;

        //プレイヤー
        Vector3 vP_Pos = player_.GetPos();

        //距離
        float Distance = SubDistance(vP_Pos, vPos);

        Debug.Log(Boss_Hp);

        if(Distance > 3.3f)
        {
            animator_.SetBool(IsBossState[(int)state_], false);
            if (LookFlg)
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

            if (Attack_Interval > 4.0f)
            {
                animator_.SetBool(IsBossState[(int)state_], false);

                state_ = BossState.ATTACK;
                AttackFlg = true;
                Attack_Interval = 0.0f;
                animator_.SetBool(IsBossState[(int)state_], true);
            }
        }

        if(AttackFlg)
        {
            AnimatorStateInfo info_ = animator_.GetCurrentAnimatorStateInfo(0);

            if(info_.normalizedTime >= 0.34f && info_.normalizedTime <= 0.80f)
            {
                bossattack_.AttackStart();
            }

            if(info_.normalizedTime >= 0.94f)
            {
                AttackFlg = false;
                bossattack_.AttackEnd();
            }
            else
            {
                for(int i= 0; i < 7;i++)
                {
                    animator_.SetBool(i, false);
                }
            }
        }

        if(Boss_Hp <= 0)
        {
            IsDeath = true;
            Destroy(gameObject);
            FadeManager.Instance.LoadLevel("ClearScene", 2.0f);
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

        if (Input.GetKey(KeyCode.K))
        {
            Boss_Hp --;
        }

        transform.position = vPos;
        transform.rotation = vRot;
    }

    

    float SubDistance(Vector3 a, Vector3 b)
    {
        float ans;
        float x, y, z;
        x = a.x - b.x;
        y = a.y - b.y;
        z = a.z - b.z;

        ans = Mathf.Sqrt(x * x + y * y + z * z);
        return ans;
    }

    public int GetHp()
    {
        return Boss_Hp;
    }
    public void SetHp(int hp)
    {
        Boss_Hp = hp;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
