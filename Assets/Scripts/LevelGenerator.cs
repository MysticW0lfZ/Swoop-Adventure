

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public GameObject hazardPrefab;
    public GameObject enemyPrefab;      // NEW
    public Transform player;

    [Header("Layout Settings")]
    public int platformCount = 15;
    public float startX = 8f;
    public float minGap = 2f;
    public float maxGap = 5f;
    public float minY = 0f;
    public float maxY = 2f;

    [Header("Vertical Step Limit")]
    public float maxStepUp = 1.5f;      // how far up next platform can be
    public float maxStepDown = 2f;      // how far down next platform can be

    [Header("Coins")]
    [Range(0f, 1f)]
    public float coinChance = 0.5f;
    public float coinHeightOffset = 1f;

    [Header("Hazards")]
    [Range(0f, 1f)]
    public float hazardChance = 0.3f;
    public float hazardHeightOffset = 0.5f;

    [Header("Enemies")]
    [Range(0f, 1f)]
    public float enemyChance = 0.3f;
    public float enemyHeightOffset = 0.5f;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        if (platformPrefab == null)
        {
            Debug.LogWarning("LevelGenerator: No platformPrefab assigned");
            return;
        }

        float currentX = player != null ? player.position.x + startX : startX;
        float currentY = 0f;    // start height

        for (int i = 0; i < platformCount; i++)
        {
            float gap = Random.Range(minGap, maxGap);
            currentX += gap;

            float targetY = Random.Range(minY, maxY);

            // clamp vertical change so jumps are reachable
            float deltaY = targetY - currentY;
            if (deltaY > maxStepUp)
                targetY = currentY + maxStepUp;
            else if (deltaY < -maxStepDown)
                targetY = currentY - maxStepDown;

            currentY = targetY;

            Vector3 spawnPos = new Vector3(currentX, currentY, 0f);
            Instantiate(platformPrefab, spawnPos, Quaternion.identity);

            TrySpawnCoin(spawnPos);
            TrySpawnHazard(spawnPos);
            TrySpawnEnemy(spawnPos);
        }
    }

    private void TrySpawnCoin(Vector3 platformPosition)
    {
        if (coinPrefab == null) return;

        if (Random.value <= coinChance)
        {
            Vector3 coinPos = platformPosition + new Vector3(0f, coinHeightOffset, 0f);
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }

    private void TrySpawnHazard(Vector3 platformPosition)
    {
        if (hazardPrefab == null) return;

        if (Random.value <= hazardChance)
        {
            Vector3 hazardPos = platformPosition + new Vector3(0f, hazardHeightOffset, 0f);
            Instantiate(hazardPrefab, hazardPos, Quaternion.identity);
        }
    }

    private void TrySpawnEnemy(Vector3 platformPosition)
    {
        if (enemyPrefab == null) return;

        if (Random.value <= enemyChance)
        {
            Vector3 enemyPos = platformPosition + new Vector3(0f, enemyHeightOffset, 0f);
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        }
    }
}
