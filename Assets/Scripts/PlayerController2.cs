using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Rigidbody2D rbody;      //Rigidbody2D型の変数
    float axisH = 0.0f;     //入力
    public float speed = 3.0f;  //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ



    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();   //Rigidbody2Dを取ってくる

    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");    //水平方向の入力をチェックする

        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        //地上判定
        onGround = Physics2D.Linecast(transform.position,
        transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)//地面の上or速度が0でない
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);  //速度を更新する
        }

        if (onGround && goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Debug.Log("ジャンプ！");
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;//ジャンプフラグを下ろす
        }
    }

    public void Jump()
    {
        goJump = true;//ジャンプフラグを立てる
        Debug.Log("ジャンプボタン押し！");
    }
}
