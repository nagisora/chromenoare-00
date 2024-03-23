using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public Sprite newSprite; // �G�f�B�^����V�����X�v���C�g���A�T�C�����邽�߂̌��J�ϐ�
    public UIManager uiManager;
    public GameObject gameOverPanel;
    public GameManager gameManager;

    public AudioSource audioSourceJump01;
    public AudioSource audioSourceJump02;
    public AudioSource audioSourceGameOver;

    public float jumpForce = 700f; // �W�����v�̗�
    public float newBounciness = 0.7f; // �V����Bounciness�̒l

    private Rigidbody2D rb;
    private bool isGrounded = true; // �n�ʂɐG��Ă��邩�ǂ���
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
            return; // �Q�[���I�[�o�[�̏ꍇ�A�ȍ~�̏������s��Ȃ�
        }

        // �X�y�[�X�L�[�������ꂽ�u�ԁA���n�ʂɐG��Ă���ꍇ�ɃW�����v����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");

            //int segment = uiManager.GetScore() / ScoreStage2;

            //if(segment % 2 == 0)
            //{
            //    if (isGrounded)
            //    {
            //        rb.AddForce(new Vector2(0, jumpForce));
            //        isGrounded = false; // �W�����v�����̂ŁAisGrounded��false�ɐݒ�
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
                isGrounded = false; // �W�����v�����̂ŁAisGrounded��false�ɐݒ�
                

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

    // �n�ʂɐG��Ă��邩�̔���
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground") // "Ground"�^�O�̃I�u�W�F�N�g�ɐG�ꂽ��
        {
            isGrounded = true; // �n�ʂɐG��Ă���
        }
    }

    // �G�ɓ���������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // �Q�[���I�[�o�[�����������ɏ���
            // �Ⴆ�΁A�Q�[���I�[�o�[�̃V�[�������[�h����Ȃ�
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
        // �I�u�W�F�N�g��Collider2D�R���|�[�l���g���擾
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            // Collider2D���g�p���Ă���Physics Material 2D���擾�i�Ȃ���ΐV���ɍ쐬�j
            PhysicsMaterial2D material = collider.sharedMaterial;
            if (material == null)
            {
                material = new PhysicsMaterial2D();
                collider.sharedMaterial = material;
            }

            // Bounciness�v���p�e�B��V�����l�ɐݒ�
            material.bounciness = newBounciness;

            // �d�v: �ύX�����}�e���A����Collider�ɍĊ��蓖�Ă��čX�V�𔽉f������
            collider.sharedMaterial = material;
        }
    }

    // �摜��ύX���郁�\�b�h
    public void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
