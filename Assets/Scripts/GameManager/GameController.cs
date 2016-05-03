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
    private List<int> _listEnemy = new List<int>();
    public int enemyCount;

    [Header("Color")]
    public GameObject[] colorPower;
    private int[] _bestColor = new int[3];

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

        _listEnemy.Add(0);
        _listEnemy.Add(4);
        _listEnemy.Add(8);

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
            for (int i = 0; i < _listEnemy.Count; i++)
            {
                Vector3 spawnValue = new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, spawnPosition.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy[_listEnemy[i]], spawnValue, spawnRotation);
                yield return new WaitForSeconds(timeToNextEnemy);
            }
            _waveNb += 1;
            EnemyList();
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

        _listEnemy.Clear();

        while (i < _waveNb + 3)
        {
            _listEnemy.Add(Random.Range(0, 9));
            i = i + 1;
        }
    }

    public int[] getBestColor()
    {
        int[] i = new int[3];
        int r = 0;
        int b = 0;
        int y = 0;

        int x = 0;

        while (x < _listEnemy.Count)
        {
            if (_listEnemy[x] == 0 || _listEnemy[x] == 3 || _listEnemy[x] == 6)
            {
                b = b + 1;
            }
            if (_listEnemy[x] == 1 || _listEnemy[x] == 4 || _listEnemy[x] == 7)
            {
                r = r + 1;
            }
            if (_listEnemy[x] == 2 || _listEnemy[x] == 5 || _listEnemy[x] == 8)
            {
                y = y + 1;
            }
            x = x + 1;
        }

        i[0] = b;
        i[1] = r;
        i[2] = y;

        System.Array.Sort(i);

        return i;
    }
}