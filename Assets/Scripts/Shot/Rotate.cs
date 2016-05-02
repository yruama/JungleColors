using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;

    void Start()
    {
        int i = Random.Range(0, 100);
        int x = 1;

        if (i % 2 == 0)
        {
            x = -1;
        }

        Debug.Log(x);
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = Random.Range(2, 6) * speed * x;
    }
}