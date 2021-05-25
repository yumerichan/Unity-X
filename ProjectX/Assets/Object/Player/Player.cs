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

    // Animator �R���|�[�l���g
    private Animator animator;

    // �ݒ肵���t���O�̖��O
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

        // �΂߈ړ��̈ړ���
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

        //Lend.x = Input.GetAxis("Horizontal");   // x������Input�̒l���擾
        //Lend.z = Input.GetAxis("Vertical");     // z������Input�̒l���擾
        //rigidbody_.velocity = new Vector3(Lend.x * PLAYER_MOVE_POS, 0, Lend.z * PLAYER_MOVE_POS);

        Vector3 diff = Pos - OldPos; //�v���C���[���ǂ̕����ɐi��ł��邩���킩��悤�ɁA�����ʒu�ƌ��ݒn�̍��W�������擾
        diff.y = 0.0f;

        if (diff.magnitude > 0.001f)  //�x�N�g���̒�����0.01f���傫���ꍇ�Ƀv���C���[�̌�����ς��鏈��������(0�ł͓���Ȃ��̂Łj
        {
            transform.rotation = Quaternion.LookRotation(diff);  //�x�N�g���̏���Quaternion.LookRotation�Ɉ����n����]�ʂ��擾���v���C���[����]������
        }

        OldPos = Pos;
        transform.position = Pos;
        rigidbody_.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����̖��O���擾
        string name = collision.gameObject.name;

        // ����̖��O��Stage��������
        if (name == "Stage")
        {
            StageFlg = true;
            animator.SetBool(key_isFall, false);
        }
    }
}