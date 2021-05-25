using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	const float PLAYER_MOVE_POS = 0.01f;
	const float PLAYER_EVASION = 5.0f;

	private Rigidbody rigidbody_;
	private Vector3 Lend;
	private Vector3 Pos;
    private Vector3 OldPos;
    private Vector3 MovePos;
	private Vector3 vel;
    private bool StageFlg = false;

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    private const string key_isFall = "isFall";

    // Start is called before the first frame update
    void Start()
    {
        Pos = GetComponent<Transform>().position;
        rigidbody_ = GetComponent<Rigidbody>();
		this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		Pos = transform.position;
		vel = rigidbody_.velocity;

        if (Input.GetKey(KeyCode.W))
        {
            MovePos.z += PLAYER_MOVE_POS;
            animator.SetBool(key_isRun, true);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            MovePos.z -= PLAYER_MOVE_POS;
            animator.SetBool(key_isRun, true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            MovePos.x -= PLAYER_MOVE_POS;
            animator.SetBool(key_isRun, true);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            MovePos.x += PLAYER_MOVE_POS;
            animator.SetBool(key_isRun, true);
        }

        // 斜め移動の移動量
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            {
                MovePos = 0.71f * MovePos;
            }
        }

        if (MovePos.x == 0.0f && MovePos.z == 0.0f && StageFlg == true) 
        {
            animator.SetBool(key_isRun, false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && StageFlg == true)
        {
            vel.y = PLAYER_EVASION;
            animator.SetBool(key_isRun, false);
            animator.SetBool(key_isJump, true);

            StageFlg = false;
        }

        else if (vel.y <= 0.0f && StageFlg == false && animator.GetBool(key_isFall) == false)   
        {
            animator.SetBool(key_isJump, false);
            animator.SetBool(key_isFall, true);
        }

        Pos = Pos + MovePos;
        MovePos = new Vector3(0.0f, 0.0f, 0.0f);

        //Lend.x = Input.GetAxis("Horizontal");   // x方向のInputの値を取得
        //Lend.z = Input.GetAxis("Vertical");     // z方向のInputの値を取得
        //rigidbody_.velocity = new Vector3(Lend.x * PLAYER_MOVE_POS, 0, Lend.z * PLAYER_MOVE_POS);

        Vector3 diff = Pos - OldPos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得
        diff.y = 0.0f;

        if (diff.magnitude > 0.001f)  //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            transform.rotation = Quaternion.LookRotation(diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
        }

        OldPos = Pos;
        transform.position = Pos;
        rigidbody_.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 相手の名前を取得
        string name = collision.gameObject.name;

        // 相手の名前がStageだった時
        if (name == "Stage")
        {
            StageFlg = true;
            animator.SetBool(key_isFall, false);
        }
    }
}