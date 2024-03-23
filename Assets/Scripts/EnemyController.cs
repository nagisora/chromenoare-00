using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Medium,
    Large,
    Tall
}

public class EnemyController : MonoBehaviour
{
    public EnemyType type;

    public float initialSpeed = 5f; // 敵の初期速度
    public float speedIncreaseRate = 1f; // 時間経過による速度の増加率（1秒あたりの増加速度）
    private float currentSpeed; // 現在の速度
    //private float elapsedTime = 0f; // 経過時間

    void Start()
    {
        currentSpeed = initialSpeed; // 初期速度を設定
    }

    void Update()
    {
        if (GameManager.currentState == GameState.GameOver)
        {
            return; // ゲームオーバーの場合、以降の処理を行わない
        }

        // 経過時間を更新
        //elapsedTime += Time.deltaTime;

        // 経過時間に応じて速度を増加
        //currentSpeed = initialSpeed + (speedIncreaseRate * elapsedTime);
        currentSpeed = initialSpeed;

        // 速度を使った処理（例：敵の移動）
        MoveEnemy(currentSpeed);

        DestroyEnemy();
    }

    void MoveEnemy(float speed)
    {
        // ここに敵を移動させる処理を書く
        // 例: transform.Translate(Vector2.left * speed * Time.deltaTime);
        // Enemyオブジェクトを左に動かす（右から左へ）
        transform.position += speed * Time.deltaTime * Vector3.left;
    }

    void DestroyEnemy()
    {
        // カメラのビューポートからワールド座標への変換を利用して、画面の左端のx座標を取得
        float leftEdgeOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        // オブジェクトの位置が画面の左端よりも外にあるかを確認
        if (transform.position.x + transform.localScale.x < leftEdgeOfScreen)
        {
            // 条件を満たしていればオブジェクトを破棄
            Destroy(gameObject);
            //Debug.Log(currentSpeed);
        }
    }
}
