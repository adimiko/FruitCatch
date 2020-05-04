using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] GoodFruitPrefabs;
    [SerializeField] GameObject[] BadFruitPrefabs;
    [SerializeField] GameObject player;
    

    private GameManager gameManager;
    private PlayerController playerController;

    private float spawnRangeX;
    private const float spawnPositionY = 10.3f;

    [SerializeField] float initialSpawnIntervalOfTime = 5f;
    private float spawnIntervalOfTime;

    public float maxDifficultyLevelSpawnInterval = 1.4f;

    public int probabilityGoodFruit = 70;
    public int probabilityBadFruit => 100 - probabilityGoodFruit;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = player.GetComponent<PlayerController>();

        spawnRangeX = gameManager.xGameplayRange;
        spawnIntervalOfTime = initialSpawnIntervalOfTime;
    }

    public IEnumerator SpawnRandom()
    {
        while (gameManager.isGameActive)
        {
            int random = Random.Range(0, 101);

            if(random <= probabilityGoodFruit) SpawnGoodFruit();
            else SpawnBadFruit();

            yield return new WaitForSeconds(spawnIntervalOfTime);
        }
    }

    void SpawnGoodFruit()
    {
        int index = Random.Range(0, GoodFruitPrefabs.Length);

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPositionY, 0);
        Instantiate(GoodFruitPrefabs[index], spawnPosition, GoodFruitPrefabs[index].transform.rotation);
    }

    void SpawnBadFruit()
    {
        int index = Random.Range(0, BadFruitPrefabs.Length);

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPositionY, 0);
        Instantiate(BadFruitPrefabs[index], spawnPosition, BadFruitPrefabs[index].transform.rotation);
    }

    private bool IsPossibleIncreaseDifficultyLevel()
    {
        if (spawnIntervalOfTime > maxDifficultyLevelSpawnInterval) return true;
        return false;
    }

    public IEnumerator IncreaseTheDifficultyLevel(int timeBetweenIncreaseDifficultyLevel)
    {
        while (IsPossibleIncreaseDifficultyLevel())
        {
            yield return new WaitForSeconds(timeBetweenIncreaseDifficultyLevel);
            spawnIntervalOfTime -= 0.1f;
        }
    }
}
