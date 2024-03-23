using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public GameObject enemyPrefab; // EnemyのPrefabをInspectorからアタッチ
    public List<GameObject> enemyPrefabs;
    public GameObject groundObject; // インスペクターからGroundオブジェクトをアタッチ

    public float minSpawnTime = 1.5f; // 最小出現時間
    public float maxSpawnTime = 5f; // 最大出現時間

    private float timer; // 次にEnemyを生成するまでの時間
    private float spawnTime;

    private float elapsedTime; // ゲーム開始からの経過時間

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void Update()
    {
        if (GameManager.currentState == GameState.GameOver)
        {
            return; // ゲームオーバーの場合、以降の処理を行わない
        }

        elapsedTime += Time.deltaTime;

        // タイマーを減らします
        timer -= Time.deltaTime;

        // タイマーが0以下になったら、Enemyを生成してタイマーをリセットします
        if (timer <= 0)
        {
            int enemyType = Random.Range(0, enemyPrefabs.Count);
            SpawnEnemy(enemyType);
            ResetSpawnTimer();
        }
    }

    // 敵を指定されたインデックスに基づいて生成するメソッド
    public void SpawnEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Count)
        {
            Debug.LogError("Index out of range. Cannot spawn enemy.");
            return;
        }

        // GroundオブジェクトのCollider2Dを取得
        Collider2D groundCollider = groundObject.GetComponent<Collider2D>();

        // Groundの上面のY座標を計算
        float groundTop = groundObject.transform.position.y + groundCollider.offset.y + (groundCollider.bounds.size.y / 2);

        // 指定されたインデックスの敵プレファブのBoxCollider2Dからオブジェクトの高さを取得（2Dの場合）
        BoxCollider2D collider = enemyPrefabs[enemyIndex].GetComponent<BoxCollider2D>();
        float objectHeight = collider != null ? collider.size.y * enemyPrefabs[enemyIndex].transform.localScale.y : 0;

        // カメラの右端のX座標を計算
        Vector2 cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

        // カメラの右端より少し外（例: 1ユニット分右に）に出現させるためにX座標を調整
        float spawnX = cameraRightEdge.x + 1f; // 1ユニット分右に調整

        // 敵の出現位置を設定（Y座標はGroundの上面、X座標はカメラの右端より外）
        Vector3 spawnPosition = new(spawnX, groundTop + objectHeight / 2, 0);

        // 指定されたインデックスの敵のPrefabを計算した位置にインスタンス化
        GameObject newInstance = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        newInstance.GetComponent<EnemyController>().type = (EnemyType) enemyIndex;

    }

    void ResetSpawnTimer()
    {
        // 時間経過に応じてパラメータを更新
        float newMax = Mathf.Max(minSpawnTime, maxSpawnTime - elapsedTime / 20.0f); // 1分ごとに出現間隔を短くする（最小0.5秒）

        // 次のEnemyが出現するまでの時間をランダムに設定します。
        spawnTime = Random.Range(minSpawnTime, newMax);

        timer = spawnTime;
        Debug.Log(spawnTime);

    }
}
