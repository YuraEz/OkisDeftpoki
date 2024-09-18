using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsManager : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;          // Префаб цели
    [SerializeField] private float shotForce;              // Сила выстрела (не используется, но может быть полезна позже)

    [SerializeField] private List<Transform> spawnPoints;  // Список точек спавна
    [SerializeField] private List<Sprite> targetSprites;   // Список спрайтов для целей

    [SerializeField] private float minSpawnInterval;       // Минимальное время между спавнами
    [SerializeField] private float maxSpawnInterval;       // Максимальное время между спавнами

    [SerializeField] private int minTargetsPerWave = 1;    // Минимальное количество целей для спавна за раз
    [SerializeField] private int maxTargetsPerWave = 5;    // Максимальное количество целей для спавна за раз

    [SerializeField] private float minTargetLifetime = 3f; // Минимальное время жизни цели
    [SerializeField] private float maxTargetLifetime = 10f; // Максимальное время жизни цели

    public List<Target> activeTargets;                     // Список активных целей
    public LayerMask spawnLayerMask;                       // Маска слоев для проверки занятости точки спавна

    private float nextSpawnTime = 5f;                      // Время до следующего спавна

    private void Start()
    {
        activeTargets = new List<Target>();                // Инициализация списка целей
        StartCoroutine(SpawnTargetsRoutine());             // Запуск корутины спавна
    }

    // Корутина для спавна целей с периодичностью
    private IEnumerator SpawnTargetsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(nextSpawnTime);    // Ждем указанное время перед спавном

            // Генерируем случайное количество целей для спавна
            int targetCount = Random.Range(minTargetsPerWave, maxTargetsPerWave + 1);

            for (int i = 0; i < targetCount; i++)              // Спавним указанное случайное количество целей
            {
                TrySpawnTarget();
            }

            // Задаем случайный интервал до следующего спавна
            nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    // Метод для попытки спавна цели в случайной точке
    private void TrySpawnTarget()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Count);  // Выбираем случайную точку спавна
        Transform spawnPoint = spawnPoints[randomSpawnIndex];       // Получаем трансформ выбранной точки

        // Проверяем, свободна ли точка спавна (нет ли других объектов)
        if (Physics2D.OverlapCircle(spawnPoint.position, 0.1f, spawnLayerMask) == null)
        {
            // Создаем новый объект цели
            Target newTarget = Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
            newTarget.transform.localPosition = spawnPoint.localPosition;

            // Назначаем случайный спрайт цели
            int randomSpriteIndex = Random.Range(0, targetSprites.Count);
            newTarget.Init(targetSprites[randomSpriteIndex]);

            // Добавляем цель в список активных целей
            activeTargets.Add(newTarget);

            // Генерируем случайное время жизни цели
            float targetLifetime = Random.Range(minTargetLifetime, maxTargetLifetime);

            // Уничтожаем цель через случайное время
            // Destroy(newTarget.gameObject, targetLifetime);
            newTarget.DestroyTarget(targetLifetime);
        }
        else
        {
            Debug.Log("Точка спавна занята, пропускаем спавн.");  // Логируем сообщение, если точка занята
        }
    }
}
