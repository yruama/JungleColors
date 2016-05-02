using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour
{
    [Header("Caracteristiques")]
    public float speed;
    public float health;
    public int color;

    [Header("Autres")]
    public AudioClip sound;
    public GameObject explosion;

    [Header("Slow")]
    public float slowDuration;
    public float slowForce;


    private float _currentHealth;
    private Rigidbody2D _rb;

    void Start()
    {
        _currentHealth = health;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetHealth(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        _rb.velocity = new Vector2(0, -1 * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().SlowMovement(slowForce, slowForce);
        }
        if (coll.gameObject.tag == "Shot")
        {
            GameObject.Find("Score").GetComponent<Score>().SetScore(100);
            SetHealth(1);
            Destroy(coll.gameObject);
        }
    }
}
