using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 10.0f;

    public float jump = 20.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O

    public static string gameState = "playing";//�Q�[���̏��


    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        gameState = "playing";//�Q�[�����ɂ���

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }
        //���������̓��͂��`�F�b�N����

        axisH = Input.GetAxisRaw("Horizontal");
        //�����̒���

        if (axisH > 0.0f)
        {
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);
        }
        //�L�����N�^�[���W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position,
        transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            //�n�ʂ̏�or���x��0�łȂ�
            //���x���X�V����
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
            //�W�����v������
            Debug.Log("�W�����v�I");
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;//�W�����v�t���O�����낷
        }
    }

    //�W�����v
    public void Jump()
    {
        goJump = true;//�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^�������I");
    }
}
