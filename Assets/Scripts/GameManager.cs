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
        // �Q�[���I�[�o�[Panel���\������Ă����ԂŃX�y�[�X�L�[�������ꂽ�ꍇ
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            // �V�[���������[�h���ăQ�[�����ăX�^�[�g
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        // ���̑��̃Q�[���I�[�o�[����
    }
}
