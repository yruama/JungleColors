using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    [Header("Caractéristiques")]
    public int color;
    public float speed;
    public int score;
    public int health;
    private int _currentHealth;

    [Header("Movement")]
    public Vector2 boundary;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    private float targetManeuver;

    [Header("Shot")]
    public Transform[] firePosition;
    public GameObject shot;
    public float fireRate;
    private float _timeToAttack;

    [Header("Autes")]
    public GameObject explosion;
    private Rigidbody2D _rb;
    public AudioClip[] sound;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Evade());

        //transform.eulerAngles = new Vector3(180, 0);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void Update()
    {
        if (Time.time - _timeToAttack > fireRate)
        {
            int i = 0;
            while (i < firePosition.Length)
            {
                GetComponent<AudioSource>().clip = sound[0];
                GetComponent<AudioSource>().Play();
                Instantiate(shot, firePosition[i].position, Quaternion.identity);
                i = i + 1;
            }
            _timeToAttack = Time.time;
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(targetManeuver, speed * -1);
        _rb.position = new Vector3
        (
           Mathf.Clamp(_rb.position.x, boundary.x, boundary.y),
            _rb.position.y
        );
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
        if (coll.gameObject.tag == "Shot")
        {
            if (coll.gameObject.GetComponent<ShotController>().GetColor() == color)
            {
                GetComponent<AudioSource>().clip = sound[1];
                GetComponent<AudioSource>().Play();
                GameObject.Find("GameController").GetComponent<GameController>().SetScore(score);
                SetHealth(1);
            }
            Destroy(coll.gameObject);
        }
    }
}