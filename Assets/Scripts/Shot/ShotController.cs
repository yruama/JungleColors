using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour
{
    public float speed;
    public string ignore;
    public int _color;

    private Rigidbody2D _rb;

	void Start ()
    {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        _rb.velocity = new Vector2(0, speed * Time.deltaTime * 100);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "DestroyLimit")
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(int i)
    {
        _color = i;
    }

    public int GetColor()
    {
        return _color;
    }
}