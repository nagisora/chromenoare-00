using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public GameObject enemyPrefab; // Enemy��Prefab��Inspector����A�^�b�`
    public List<GameObject> enemyPrefabs;
    public GameObject groundObject; // �C���X�y�N�^�[����Ground�I�u�W�F�N�g���A�^�b�`

    public float minSpawnTime = 1.5f; // �ŏ��o������
    public float maxSpawnTime = 5f; // �ő�o������

    private float timer; // ����Enemy�𐶐�����܂ł̎���
    private float spawnTime;

    private float elapsedTime; // �Q�[���J�n����̌o�ߎ���

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void Update()
    {
        if (GameManager.currentState == GameState.GameOver)
        {
            return; // �Q�[���I�[�o�[�̏ꍇ�A�ȍ~�̏������s��Ȃ�
        }

        elapsedTime += Time.deltaTime;

        // �^�C�}�[�����炵�܂�
        timer -= Time.deltaTime;

        // �^�C�}�[��0�ȉ��ɂȂ�����AEnemy�𐶐����ă^�C�}�[�����Z�b�g���܂�
        if (timer <= 0)
        {
            int enemyType = Random.Range(0, enemyPrefabs.Count);
            SpawnEnemy(enemyType);
            ResetSpawnTimer();
        }
    }

    // �G���w�肳�ꂽ�C���f�b�N�X�Ɋ�Â��Đ������郁�\�b�h
    public void SpawnEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Count)
        {
            Debug.LogError("Index out of range. Cannot spawn enemy.");
            return;
        }

        // Ground�I�u�W�F�N�g��Collider2D���擾
        Collider2D groundCollider = groundObject.GetComponent<Collider2D>();

        // Ground�̏�ʂ�Y���W���v�Z
        float groundTop = groundObject.transform.position.y + groundCollider.offset.y + (groundCollider.bounds.size.y / 2);

        // �w�肳�ꂽ�C���f�b�N�X�̓G�v���t�@�u��BoxCollider2D����I�u�W�F�N�g�̍������擾�i2D�̏ꍇ�j
        BoxCollider2D collider = enemyPrefabs[enemyIndex].GetComponent<BoxCollider2D>();
        float objectHeight = collider != null ? collider.size.y * enemyPrefabs[enemyIndex].transform.localScale.y : 0;

        // �J�����̉E�[��X���W���v�Z
        Vector2 cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

        // �J�����̉E�[��菭���O�i��: 1���j�b�g���E�Ɂj�ɏo�������邽�߂�X���W�𒲐�
        float spawnX = cameraRightEdge.x + 1f; // 1���j�b�g���E�ɒ���

        // �G�̏o���ʒu��ݒ�iY���W��Ground�̏�ʁAX���W�̓J�����̉E�[���O�j
        Vector3 spawnPosition = new(spawnX, groundTop + objectHeight / 2, 0);

        // �w�肳�ꂽ�C���f�b�N�X�̓G��Prefab���v�Z�����ʒu�ɃC���X�^���X��
        GameObject newInstance = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        newInstance.GetComponent<EnemyController>().type = (EnemyType) enemyIndex;

    }

    void ResetSpawnTimer()
    {
        // ���Ԍo�߂ɉ����ăp�����[�^���X�V
        float newMax = Mathf.Max(minSpawnTime, maxSpawnTime - elapsedTime / 20.0f); // 1�����Ƃɏo���Ԋu��Z������i�ŏ�0.5�b�j

        // ����Enemy���o������܂ł̎��Ԃ������_���ɐݒ肵�܂��B
        spawnTime = Random.Range(minSpawnTime, newMax);

        timer = spawnTime;
        Debug.Log(spawnTime);

    }
}
