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

    public float initialSpeed = 5f; // �G�̏������x
    public float speedIncreaseRate = 1f; // ���Ԍo�߂ɂ�鑬�x�̑������i1�b������̑������x�j
    private float currentSpeed; // ���݂̑��x
    //private float elapsedTime = 0f; // �o�ߎ���

    void Start()
    {
        currentSpeed = initialSpeed; // �������x��ݒ�
    }

    void Update()
    {
        if (GameManager.currentState == GameState.GameOver)
        {
            return; // �Q�[���I�[�o�[�̏ꍇ�A�ȍ~�̏������s��Ȃ�
        }

        // �o�ߎ��Ԃ��X�V
        //elapsedTime += Time.deltaTime;

        // �o�ߎ��Ԃɉ����đ��x�𑝉�
        //currentSpeed = initialSpeed + (speedIncreaseRate * elapsedTime);
        currentSpeed = initialSpeed;

        // ���x���g���������i��F�G�̈ړ��j
        MoveEnemy(currentSpeed);

        DestroyEnemy();
    }

    void MoveEnemy(float speed)
    {
        // �����ɓG���ړ������鏈��������
        // ��: transform.Translate(Vector2.left * speed * Time.deltaTime);
        // Enemy�I�u�W�F�N�g�����ɓ������i�E���獶�ցj
        transform.position += speed * Time.deltaTime * Vector3.left;
    }

    void DestroyEnemy()
    {
        // �J�����̃r���[�|�[�g���烏�[���h���W�ւ̕ϊ��𗘗p���āA��ʂ̍��[��x���W���擾
        float leftEdgeOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        // �I�u�W�F�N�g�̈ʒu����ʂ̍��[�����O�ɂ��邩���m�F
        if (transform.position.x + transform.localScale.x < leftEdgeOfScreen)
        {
            // �����𖞂����Ă���΃I�u�W�F�N�g��j��
            Destroy(gameObject);
            //Debug.Log(currentSpeed);
        }
    }
}
