using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class taptap : MonoBehaviour
{
    public Vector3 scoreNeed;
    public float time;
    public Text t;
    public Text s;
    public GameObject c;
    public GameController gc;

    private int _score;
    private float _time;

	void Start ()
    {
        c.SetActive(true);
        s.text = "0";
        t.text = time.ToString();
        _time = Time.time;
        _score = 0;

	}
	
    void Awake()
    {
        _time = Time.time;
    }

	void Update ()
    {
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

    void OnMouseDown()
    {
        _score += 1;
    }

    public void SetTime()
    {
        c.SetActive(true);
        s.text = "0";
        t.text = time.ToString();
        _time = Time.time;
        _score = 0;
    }
}
