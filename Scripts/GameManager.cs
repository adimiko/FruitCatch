using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int healthPoints;

    public bool isGameActive;

    public float xGameplayRange = 8.5f;

    public GameObject player;

    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthPointsText;

    [SerializeField] TextMeshProUGUI endScoreText;

    private SpawnManager spawnManager;
    private DestroyOutOfBounds destroyOutOfBounds;

    [SerializeField] int timeBetweenIncreaseDifficultyLevel = 3;

    void Start()
    {
        AddToScore(0);
        healthPoints = 3;
        destroyOutOfBounds = GameObject.Find("Lower Band").GetComponent<DestroyOutOfBounds>();

        player.GetComponent<PlayerController>().FruitCollided += AddToScore;

        destroyOutOfBounds.GoodFruitTouchedLowerBand += DecreaseHealthPoints;

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0) GameOver();
    }

    public void StartGame()
    {
        isGameActive = true;
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        player.SetActive(true);

        StartCoroutine(spawnManager.SpawnRandom());
        StartCoroutine(spawnManager.IncreaseTheDifficultyLevel(timeBetweenIncreaseDifficultyLevel));
    }
    
    public void GameOver()
    {
        isGameActive = false;
        endScoreText.text = "Score \n" + score;
        gameOverScreen.SetActive(true);

        player.SetActive(false);
        gameScreen.SetActive(false);
    }

    public void RestartGame()
    => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void DecreaseHealthPoints()
    {
        healthPoints--;
        DecreaseHealthPointsText();
    }

    private void DecreaseHealthPointsText()
    {
        string tempHealthPointsText = "";
        for (int i = 0; i < healthPoints; i++)
        {
            tempHealthPointsText += "♥";
        }

        healthPointsText.text = tempHealthPointsText;
    }

    private void AddToScore(int addToScore)
    {
        score += addToScore;
        RefreshScoreText();
    }

    private void RefreshScoreText()
    => scoreText.text = "SCORE: " + score;
}