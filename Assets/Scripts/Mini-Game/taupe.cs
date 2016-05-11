using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class taupe : MonoBehaviour
{
    public Vector3 scoreNeed;
    public Text t;
    public Text s;
    public GameObject c;
    public GameController gc;

    public GameObject[] spawn;
    public GameObject[] prefabs;

    public float time;
    private float _time;
    private int _score;

    public float timeNewSpawn;
    private float _timeNewSpawn;

	void Start ()
    {
        _time = Time.time;
        _timeNewSpawn = Time.time;
        _score = 0;
        c.SetActive(true);
        s.text = "0";
        t.text = time.ToString();
    }
	
	void Update ()
    {
        if (Time.time - _timeNewSpawn > timeNewSpawn)
        {
            int sp = Random.Range(0, spawn.Length);

            GameObject go = Instantiate(prefabs[0], spawn[sp].transform.position, Quaternion.identity) as GameObject;
            Destroy(go, 0.5f);
            int spp = -1;
            while (spp == sp || spp == -1)
            {
                spp = Random.Range(0, spawn.Length);
            }
            GameObject goo = Instantiate(prefabs[1], spawn[spp].transform.position, Quaternion.identity) as GameObject;
            Destroy(goo, 0.5f);

            _timeNewSpawn = Time.time;

        }

        if (Time.time - _time > time)
        {
            if (_score >= scoreNeed.z)
            {
                gc.SetColor(0);
            }
            if (_score >= scoreNeed.y)
            {
                gc.SetColor(1);
            }
            else
            {
                gc.SetColor(2);
            }

            gameObject.SetActive(false);
            c.SetActive(false);
        }
        t.text = ((int)(time - (Time.time - _time) + 1)).ToString();
        s.text = _score.ToString();
    }

    public void SetScore(int i)
    {
        _score += i;
        if (_score < 0)
            _score = 0;
    }

    public void SetTime()
    {
        _time = Time.time;
        _timeNewSpawn = Time.time;
        _score = 0;
        c.SetActive(true);
        s.text = "0";
        t.text = time.ToString();
    }
}
