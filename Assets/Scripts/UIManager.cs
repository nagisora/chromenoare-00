using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    const float ShowTime = 4f;
    public int ScoreStage2 = 500;
    const int initScoreStage2 = 500;

    public Text scoreText; // Inspector����A�N�Z�X�ł���悤��public�ɂ��܂�
    public Text highScoreText; // �n�C�X�R�A��\������e�L�X�g
    public Text actionText;

    private int currentScore = 0; // ���݂̃X�R�A��ێ�����ϐ�
    private int highScore = 0; // �n�C�X�R�A��ێ�����ϐ�
    bool stage2flag = false;

    private void Start()
    {


        // �R���[�`�����J�n
        StartCoroutine(HideTextAfterDelay(ShowTime)); // 4�b��Ƀe�L�X�g���\���ɂ���

        // �n�C�X�R�A��PlayerPrefs����ǂݍ���
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        // �n�C�X�R�A��\��
        highScoreText.text = "Hi " + highScore.ToString();

        ScoreStage2 = initScoreStage2;

        if (highScore >= 1000)
        {
            ScoreStage2 = 100;
        }
    }

    private void Update()
    {
        if(currentScore >= ScoreStage2 && !stage2flag)
        {
            stage2flag = true;
            actionText.text = "Space�F�h���u��";
            actionText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay(ShowTime));
        }

        // �n�C�X�R�A�e�L�X�g���X�V
        //scoreManager.UpdateScore(currentScore);
        //highScoreText.text = "Hi�F" + scoreManager.HighScore.ToString();
    }

    // �w�肵���b����Ƀe�L�X�g���\���ɂ���R���[�`��
    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���҂�
        actionText.gameObject.SetActive(false); // �e�L�X�g���\���ɂ���
    }

    // �X�R�A���X�V���邽�߂̃��\�b�h
    public void AddScore(int score)
    {
        currentScore += score; // �n���ꂽ�X�R�A�����݂̃X�R�A�ɉ��Z
        scoreText.text = currentScore.ToString(); // �e�L�X�g���X�V

        // �n�C�X�R�A���X�V
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "Hi " + highScore.ToString();
        }
    }

    // ���݂̃X�R�A��Ԃ�
    public int GetScore()
    {
        return currentScore;
    }

    //public int GetStage2Score()
    //{
    //    return currentScore;
    //}
}
