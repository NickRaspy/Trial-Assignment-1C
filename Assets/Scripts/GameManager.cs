using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool IsGameGoing { get; private set; } //stops game when false
    private int lastSpawnPositionIndex = -1; //need to save previous spawnpoint for repeatance excluding

    [Header("Game")] //game objects and values
    public Transform finishLine;

    [SerializeField, Range(1, 100)] private int minWinScore = 5, maxWinScore = 30;
    private int winScore;
    private int currentScore;

    private int health = 3;
    public int Health
    {
        get { return health; }
        set { health = value; healthText.text = "Здоровье: " + health; if (health <= 0) EndGame(false); }
    }

    public int CurrentScore
    {
        get { return currentScore; }
        set 
        {
            if (!IsGameGoing) return;
            currentScore = value; 
            if (currentScore >= winScore) EndGame(true); 
        }
    }

    [Header("Enemy")] //enemy objects and values
    [SerializeField] private List<Transform> spawnpoints;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private float minEnemySpeed = 0.25f, maxEnemySpeed = 1.5f;
    [SerializeField] private int enemyHealth;
    [SerializeField] private float minEnemySpawnInterval = 0.5f, maxEnemySpawnInterval = 2f;

    [Header("UI")] //UI objects
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject finalScreen;
    [SerializeField] private Text finalText;

    void Start()
    {
        IsGameGoing = true;
        healthText.text = "Здоровье: " + health;
        winScore = Random.Range(minWinScore, maxWinScore + 1);
        Debug.Log(winScore); //to check how many enemies need for win in debug
        StartCoroutine(RepeatableSpawn());
    }
    private void EnemySpawn()
    {
        //random spawnpoint choice method (excluding previous point)
        List<int> currentSpawnpoints = new(); int n = 0; spawnpoints.ForEach(a => { currentSpawnpoints.Add(n); n++; });
        if(lastSpawnPositionIndex >= 0) currentSpawnpoints.Remove(lastSpawnPositionIndex);
        int r = currentSpawnpoints[Random.Range(0, currentSpawnpoints.Count)];
        lastSpawnPositionIndex = r;

        //value set
        Vector3 pos = spawnpoints[r].position;
        int health = enemyHealth;
        float speed = Random.Range(minEnemySpeed, maxEnemySpeed);

        enemyFactory.GetNewEnemy(pos, speed, health);
    }
    private void EndGame(bool isWon)
    {
        IsGameGoing = false;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) Destroy(enemy); //clear all enemies on scene
        //final UI appear
        finalText.text = isWon ? "Победа" : "Поражение";
        finalScreen.SetActive(true);
    }
    IEnumerator RepeatableSpawn()
    {
        yield return new WaitForSeconds(2f);
        while (IsGameGoing)
        {
            float t = Random.Range(minEnemySpawnInterval, maxEnemySpawnInterval);
            EnemySpawn();
            yield return new WaitForSeconds(t);
        }
    }
}
