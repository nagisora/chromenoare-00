using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public Sprite newSprite; // エディタから新しいスプライトをアサインするための公開変数
    public UIManager uiManager;
    public GameObject gameOverPanel;
    public GameManager gameManager;

    public AudioSource audioSourceJump01;
    public AudioSource audioSourceJump02;
    public AudioSource audioSourceGameOver;

    public float jumpForce = 700f; // ジャンプの力
    public float newBounciness = 0.7f; // 新しいBouncinessの値

    private Rigidbody2D rb;
    private bool isGrounded = true; // 地面に触れているかどうか
    private bool isStage1 = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log(GameManager.currentState);
        if (GameManager.currentState == GameState.GameOver)
        {
            return; // ゲームオーバーの場合、以降の処理を行わない
        }

        // スペースキーが押された瞬間、かつ地面に触れている場合にジャンプする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");

            //int segment = uiManager.GetScore() / ScoreStage2;

            //if(segment % 2 == 0)
            //{
            //    if (isGrounded)
            //    {
            //        rb.AddForce(new Vector2(0, jumpForce));
            //        isGrounded = false; // ジャンプしたので、isGroundedをfalseに設定
            //    }
            //}
            //else
            //{
            //    rb.AddForce(new Vector2(0, -jumpForce));
            //}

            if (uiManager.GetScore() < uiManager.ScoreStage2 && isGrounded)
            {
                audioSourceJump01.Play();
                rb.AddForce(new Vector2(0, jumpForce));
                isGrounded = false; // ジャンプしたので、isGroundedをfalseに設定
                

            }
            else if (uiManager.GetScore() >= uiManager.ScoreStage2)
            {
                audioSourceJump02.Play();
                rb.AddForce(new Vector2(0, -jumpForce));
                


            }
        }

        if (uiManager.GetScore() >= uiManager.ScoreStage2 && isStage1)
        {
            isStage1 = false;
            rb.AddForce(new Vector2(0, jumpForce));
            SetMaterial();
            ChangeSprite();
        }
    }

    // 地面に触れているかの判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground") // "Ground"タグのオブジェクトに触れたら
        {
            isGrounded = true; // 地面に触れている
        }
    }

    // 敵に当たった時
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ゲームオーバー処理をここに書く
            // 例えば、ゲームオーバーのシーンをロードするなど
            //Debug.Log("Game Over!");
            if (GameManager.currentState == GameState.Play) audioSourceGameOver.Play();
            gameOverPanel.SetActive(true);
            gameManager.GameOver();

        }

        else if (other.gameObject.CompareTag("Score"))
        {
            EnemyType enemyType = other.gameObject.GetComponentInParent<EnemyController>().type;
            int scoreToAdd = 0;
            switch (enemyType)
            {
                case EnemyType.Medium:
                    scoreToAdd = 100;
                    break;
                case EnemyType.Large:
                    scoreToAdd = 200;
                    break;
                case EnemyType.Tall:
                    scoreToAdd = 200;
                    break;
            }

            if (GameManager.currentState == GameState.Play) uiManager.AddScore(scoreToAdd);

        }
    }

    private void SetMaterial()
    {
        // オブジェクトのCollider2Dコンポーネントを取得
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            // Collider2Dが使用しているPhysics Material 2Dを取得（なければ新たに作成）
            PhysicsMaterial2D material = collider.sharedMaterial;
            if (material == null)
            {
                material = new PhysicsMaterial2D();
                collider.sharedMaterial = material;
            }

            // Bouncinessプロパティを新しい値に設定
            material.bounciness = newBounciness;

            // 重要: 変更したマテリアルをColliderに再割り当てして更新を反映させる
            collider.sharedMaterial = material;
        }
    }

    // 画像を変更するメソッド
    public void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
