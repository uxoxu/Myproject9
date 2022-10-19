using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 10.0f;

    public float jump = 20.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //地面に立っているフラグ

    public static string gameState = "playing";//ゲームの状態


    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        gameState = "playing";//ゲーム中にする

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }
        //水平方向の入力をチェックする

        axisH = Input.GetAxisRaw("Horizontal");
        //向きの調整

        if (axisH > 0.0f)
        {
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);
        }
        //キャラクターをジャンプさせる
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
        //地上判定
        onGround = Physics2D.Linecast(transform.position,
        transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            //地面の上or速度が0でない
            //速度を更新する
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
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

    //ジャンプ
    public void Jump()
    {
        goJump = true;//ジャンプフラグを立てる
        Debug.Log("ジャンプボタン押し！");
    }
}
