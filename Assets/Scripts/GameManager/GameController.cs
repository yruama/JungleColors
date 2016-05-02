using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Timer")]
    public float timeToNextWave;
    public float timeToNextEnemy;

    private float _timeToEnemy;
    private float _timeToWave;
    private bool _nextWave;

    [Header("Position")]
    public Vector3 spawnPosition;

    [Header("Enemy Prefabs")]
    public GameObject[] enemy;
    private int _minEnemy;
    private int _maxEnemy;
    private List<GameObject> _listEnemy;
    public int enemyCount;

    [Header("Color")]
    public GameObject[] colorPower;

    [Header("Bird")]
    public GameObject[] bird;

    [Header("Bear")]
    public GameObject[] bear;

    [Header("Elephant")]
    public GameObject[] elephant;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool _wave;
    private int _waveNb;

    void Start()
    {
        gameOver = false;
        restart = false;

        _waveNb = 0;
        _minEnemy = 0;
        _maxEnemy = 1;

        score = 0;

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject hazard = enemy[Random.Range(_minEnemy, _maxEnemy)];
                Vector3 spawnValue = new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, spawnPosition.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnValue, spawnRotation);
                yield return new WaitForSeconds(timeToNextEnemy);
            }
            _waveNb += 1;
            yield return new WaitForSeconds(timeToNextWave);

            if (gameOver)
            {
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
    }


    public void GameOver()
    {

        gameOver = true;
    }

    public void EnemyList()
    {
        int i = 0;
        int e = 0;
        int c = 0;

        if (_waveNb < 3)
        {
            e = Random.Range(0, 2);
        }
        else
        {
            e = Random.Range(0, 3);
        }
        
        if (_waveNb > 4)
        {
            c = 3;
        }

        while (i < enemyCount)
        {
            switch (e)
            {
                case 0:
                    _listEnemy.Add(bear[Random.Range(c, c + 3)]);
                    break;
                case 1:
                    _listEnemy.Add(bird[Random.Range(c, c + 3)]);
                    break;
                case 2:
                    _listEnemy.Add(elephant[Random.Range(c, c + 3)]);
                    break;
            }   
            i = i + 1;
        }
    }
}