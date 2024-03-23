using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public static GameState currentState = GameState.Play;

    private void Start()
    {
        currentState = GameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームオーバーPanelが表示されている状態でスペースキーが押された場合
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            // シーンをリロードしてゲームを再スタート
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        // その他のゲームオーバー処理
    }
}
