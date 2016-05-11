using UnityEngine;
using UnityEngine.UI;
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
    private int[] colorPower;
    private int[] _bestColor = new int[3];

    [Header("Textes")]
    public Text goldScore;
    public Text diamondScore;
    public Text finalScore;

    [Header("Autres")]
    public GameObject gameOverImage;
    public float goldReduce;
    public PlayerController pc;

    [Header("Mini Games")]
    public GameObject[] minigames;
    private bool _miniGame;

    private bool gameOver;
    private bool restart;
    private int _score;
    private bool _wave;
    private int _waveNb;

    void Start()
    {
        _miniGame = false;
        gameOver = false;
        restart = false;

        _waveNb = 0;
        _minEnemy = 0;
        _maxEnemy = 1;

        _score = 0;

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
        yield return new WaitForSeconds(2);
        while (gameOver == false)
        {
            for (int i = 0; i < _listEnemy.Count; i++)
            {
                Vector3 spawnValue = new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, spawnPosition.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy[_listEnemy[i]], spawnValue, spawnRotation);
                yield return new WaitForSeconds(timeToNextEnemy);
            }
            EnemyList();
            getBestColor();
            yield return new WaitForSeconds(3);
            int mg = Random.Range(0, minigames.Length);
            if (gameOver == false)
                minigames[mg].SetActive(true);
            if (mg == 0)
            {
                minigames[mg].GetComponent<taptap>().SetTime();
            }
            else
            {
                minigames[mg].GetComponent<taupe>().SetTime();
            }
            _miniGame = true;
            _waveNb += 1;

            yield return new WaitForSeconds(timeToNextWave);
            _miniGame = false;
        }
    }

    public void EnemyList()
    {
        int i = 0;

        _listEnemy.Clear();

        while (i < _waveNb + 3)
        {
            _listEnemy.Add(Random.Range(0, 3));
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


        int boucle = 0;

        while (boucle < 3)
        {
            if (i[boucle] == r)
            {
                _bestColor[boucle] = 0;
            }
            else if (i[boucle] == b)
            {
                _bestColor[boucle] = 1;
            }
            else if (i[boucle] == y)
            {
                _bestColor[boucle] = 2;
            }
            boucle = boucle + 1;
        }

        int save = _bestColor[0];
        _bestColor[0] = _bestColor[2];
        _bestColor[2] = save; 

        return i;
    }

    public void SetScore(int i)
    {
        _score += i;
    }

    public int GetScore()
    {
        return _score;
    }

    public void GameOver()
    {
        finalScore.text = _score.ToString();
        goldScore.text = (_score / goldReduce).ToString();
        diamondScore.text = "73";

        /* *** Sauvegarde des monnaies *** */
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + (int)(_score / goldReduce));
        PlayerPrefs.SetInt("doamond", PlayerPrefs.GetInt("diamond") + 1);
        SetBestScore();

        gameOverImage.SetActive(true);
        gameOver = true;
    }

    void SetBestScore()
    {
        Debug.Log(_score + " XDLOLHERST NON");
        int[] i = new int[4];

        i[0] = PlayerPrefs.GetInt("scoreOne");
        i[1] = PlayerPrefs.GetInt("scoreTwo");
        i[2] = PlayerPrefs.GetInt("scoreThree");
        i[3] = _score;

        Debug.Log("AVANT Score 0 : " + i[0] + "Score 1 : " + i[1] + "Score 2 : " + i[2] + "Score 3 : " + i[3]);

        System.Array.Sort(i);

        Debug.Log("APRES Score 0 : " + i[0] + "Score 1 : " + i[1] + "Score 2 : " + i[2] + "Score 3 : " + i[3]);

        PlayerPrefs.SetInt("scoreOne", i[3]);
        PlayerPrefs.SetInt("scoreTwo", i[2]);
        PlayerPrefs.SetInt("scoreThree", i[1]);
    }

    public bool getMiniGame()
    {
        return _miniGame;
    }

    public void SetColor(int y)
    {
        pc.color = _bestColor[y];
    }
}