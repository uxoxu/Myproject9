using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;      //Rigidbody2D�^�̕ϐ�
    float axisH = 0.0f;     //����
    public float speed = 3.0f;  //�ړ����x

    bool isJump = false;
    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�n�ʂɗ����Ă���t���O

    //�A�j���[�V�����Ή�
    Animator animator;//�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";

    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "playing";//�Q�[�����ɂ���

    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();   //Rigidbody2D�����Ă���
        animator = GetComponent<Animator>();        //Animator�����Ă���
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        gameState = "playing";//�Q�[�����ɂ���
    }

    // Update is called once per frame
    void Update()
    {

        if(gameState != "playing")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");    //���������̓��͂�`�F�b�N����

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

        //�L�����N�^�[��W�����v������
        if (JumpCount <= 2&&Input.GetButtonDown("Jump"))
        {
            isJump = true; 
        }

    }

    void OnCollisionEnter2D(Collision collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            JumpCount = 0;
        }
    }

    void FixedUpdate()
    {

        if(isJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
            jumpCount++;
            isJump = false;
        }

        if(gameState!="playing")
        {
            return ;
        }

        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position,
        transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)//�n�ʂ̏�or���x��0�łȂ�
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);  //���x��X�V����
        }
        if (onGround && goJump)
        {
            //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
            //�W�����v������
            Debug.Log("�W�����v�I");
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;//�W�����v�t���O����낷
        }

        if(onGround)
        {
            //�n�ʂ̏�
            if(axisH==0)
            {
                nowAnime = stopAnime;//��~��
            }
            else
            {
                nowAnime = moveAnime;//�ړ���
            }
        }
        else
        {
            //��
            nowAnime = jumpAnime;
        }
        if(nowAnime!=oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);//�A�j���[�V�����Đ�
        }
    }

    //�W�����v
    public void Jump()
    {
        goJump = true;//�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^�������I");
    }
    //�ڐG�J�n
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Goal")
        {
            Goal();//�S�[��
        }
        else if(collision.gameObject.tag=="Dead")
        {
            GameOver();//�Q�[���I�[�o�[ 
        }

        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score= item.value;

            Destroy(collision.gameObject);
        }
    }


    //�S�[��
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop();
    }

    //�Q�[���I�[�o�[
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    //�Q�[����~
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
    
}
