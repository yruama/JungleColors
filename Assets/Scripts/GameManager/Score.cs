using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    float score;
	
	void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public void SetScore(int i)
    {
        GetComponent<Text>().text = (score += i).ToString();
    }
}
