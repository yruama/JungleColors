using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{
    public int color;
    public float speed;
    public float dodge;
    public GameObject explosion;

    public Vector2 boundary;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float targetManeuver;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Evade());
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

    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(targetManeuver, speed);
        _rb.position = new Vector3
        (
           Mathf.Clamp(_rb.position.x, boundary.x, boundary.y),
            _rb.position.y
        );
    }
}