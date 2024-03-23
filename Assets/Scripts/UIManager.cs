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

    public Text scoreText; // Inspectorからアクセスできるようにpublicにします
    public Text highScoreText; // ハイスコアを表示するテキスト
    public Text actionText;

    private int currentScore = 0; // 現在のスコアを保持する変数
    private int highScore = 0; // ハイスコアを保持する変数
    bool stage2flag = false;

    private void Start()
    {


        // コルーチンを開始
        StartCoroutine(HideTextAfterDelay(ShowTime)); // 4秒後にテキストを非表示にする

        // ハイスコアをPlayerPrefsから読み込む
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        // ハイスコアを表示
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
            actionText.text = "Space：ドリブル";
            actionText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay(ShowTime));
        }

        // ハイスコアテキストを更新
        //scoreManager.UpdateScore(currentScore);
        //highScoreText.text = "Hi：" + scoreManager.HighScore.ToString();
    }

    // 指定した秒数後にテキストを非表示にするコルーチン
    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待つ
        actionText.gameObject.SetActive(false); // テキストを非表示にする
    }

    // スコアを更新するためのメソッド
    public void AddScore(int score)
    {
        currentScore += score; // 渡されたスコアを現在のスコアに加算
        scoreText.text = currentScore.ToString(); // テキストを更新

        // ハイスコアを更新
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "Hi " + highScore.ToString();
        }
    }

    // 現在のスコアを返す
    public int GetScore()
    {
        return currentScore;
    }

    //public int GetStage2Score()
    //{
    //    return currentScore;
    //}
}
