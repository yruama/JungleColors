using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public int scoreValue;
    public int color;

    private GameController _gc;

    void Start()
    {
        _gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (_gc == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Shot")
        {
            if (coll.gameObject.GetComponent<ShotController>()._color == color)
            {

            }
        }
        
    }
}