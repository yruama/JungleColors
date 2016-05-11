using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bonus : MonoBehaviour
{
    public Text[] t;

    void Start()
    {
        t[0].text = PlayerPrefs.GetInt("scoreOne").ToString();
        t[1].text = PlayerPrefs.GetInt("scoreTwo").ToString();
        t[2].text = PlayerPrefs.GetInt("scoreThree").ToString();
    }
}