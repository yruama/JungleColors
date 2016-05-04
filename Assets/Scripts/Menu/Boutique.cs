using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boutique : MonoBehaviour {

    [Header("GameObject")]
    public GameObject gold;
    public GameObject crystal;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        gold.GetComponent<Text>().text = PlayerPrefs.GetInt("gold").ToString();
        crystal.GetComponent<Text>().text = PlayerPrefs.GetInt("crystal").ToString();
    }
}
