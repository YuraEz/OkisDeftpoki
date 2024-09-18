using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsManager : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;          // ������ ����
    [SerializeField] private float shotForce;              // ���� �������� (�� ������������, �� ����� ���� ������� �����)

    [SerializeField] private List<Transform> spawnPoints;  // ������ ����� ������
    [SerializeField] private List<Sprite> targetSprites;   // ������ �������� ��� �����

    [SerializeField] private float minSpawnInterval;       // ����������� ����� ����� ��������
    [SerializeField] private float maxSpawnInterval;       // ������������ ����� ����� ��������

    [SerializeField] private int minTargetsPerWave = 1;    // ����������� ���������� ����� ��� ������ �� ���
    [SerializeField] private int maxTargetsPerWave = 5;    // ������������ ���������� ����� ��� ������ �� ���

    [SerializeField] private float minTargetLifetime = 3f; // ����������� ����� ����� ����
    [SerializeField] private float maxTargetLifetime = 10f; // ������������ ����� ����� ����

    public List<Target> activeTargets;                     // ������ �������� �����
    public LayerMask spawnLayerMask;                       // ����� ����� ��� �������� ��������� ����� ������

    private float nextSpawnTime = 5f;                      // ����� �� ���������� ������

    private void Start()
    {
        activeTargets = new List<Target>();                // ������������� ������ �����
        StartCoroutine(SpawnTargetsRoutine());             // ������ �������� ������
    }

    // �������� ��� ������ ����� � ��������������
    private IEnumerator SpawnTargetsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(nextSpawnTime);    // ���� ��������� ����� ����� �������

            // ���������� ��������� ���������� ����� ��� ������
            int targetCount = Random.Range(minTargetsPerWave, maxTargetsPerWave + 1);

            for (int i = 0; i < targetCount; i++)              // ������� ��������� ��������� ���������� �����
            {
                TrySpawnTarget();
            }

            // ������ ��������� �������� �� ���������� ������
            nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    // ����� ��� ������� ������ ���� � ��������� �����
    private void TrySpawnTarget()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Count);  // �������� ��������� ����� ������
        Transform spawnPoint = spawnPoints[randomSpawnIndex];       // �������� ��������� ��������� �����

        // ���������, �������� �� ����� ������ (��� �� ������ ��������)
        if (Physics2D.OverlapCircle(spawnPoint.position, 0.1f, spawnLayerMask) == null)
        {
            // ������� ����� ������ ����
            Target newTarget = Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
            newTarget.transform.localPosition = spawnPoint.localPosition;

            // ��������� ��������� ������ ����
            int randomSpriteIndex = Random.Range(0, targetSprites.Count);
            newTarget.Init(targetSprites[randomSpriteIndex]);

            // ��������� ���� � ������ �������� �����
            activeTargets.Add(newTarget);

            // ���������� ��������� ����� ����� ����
            float targetLifetime = Random.Range(minTargetLifetime, maxTargetLifetime);

            // ���������� ���� ����� ��������� �����
            // Destroy(newTarget.gameObject, targetLifetime);
            newTarget.DestroyTarget(targetLifetime);
        }
        else
        {
            Debug.Log("����� ������ ������, ���������� �����.");  // �������� ���������, ���� ����� ������
        }
    }
}
