using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Rigidbody2D rbody;      //Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f;     //����
    public float speed = 3.0f;  //�ړ����x

    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O



    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();   //Rigidbody2D������Ă���

    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");    //���������̓��͂��`�F�b�N����

        //�L�����N�^�[���W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position,
        transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)//�n�ʂ̏�or���x��0�łȂ�
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);  //���x���X�V����
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

    public void Jump()
    {
        goJump = true;//�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^�������I");
    }
}
