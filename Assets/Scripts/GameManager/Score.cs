using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    public GameController gc;

    [Header("Temps & points")]
    public float timeToWait;
    public float scoreByTime;

    private float _time;

    int score;
	
	void Awake ()
    {
        score = 0;
        _time = Time.time;
	}

	void Update ()
    {
        if (Time.time - _time > timeToWait)
        {
            gc.SetScore((int)scoreByTime);
            _time = Time.time;
        }
        GetComponent<Text>().text = gc.GetScore().ToString();
    }
}