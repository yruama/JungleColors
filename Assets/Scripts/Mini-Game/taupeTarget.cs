using UnityEngine;
using System.Collections;

public class taupeTarget : MonoBehaviour {

    public int score;

    void OnMouseDown()
    {
        GameObject.Find("taupe").GetComponent<taupe>().SetScore(score);
        Destroy(gameObject);
    }
}
