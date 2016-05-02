using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Header("Caractéritiques")]
    public float health;
    private float _currentHealth;
    public float speed;
    public float energie;
    public float fireRate;

    [Header("Autres")]
    public GameObject shot;
    public Transform[] firePosition;
    public float boundary;
    public Text healthText;
    public float timeToBeInvincible;
    public GameObject explosion;

    private bool _isSlow;
    private float _slowValue;
    private float _timeToBeSlow;
    private float _timeToSlow;
    private float _timeToInvincible;
    private bool _invincible;
    private float _timeToAttack;
    private Rigidbody2D _rb;
    private bool _isSlowFireRate;
    private float _timeToSlowFireRate;
    private float _timeToBeSlowFireRate;
    private float _slowFireRateValue;

    [Header("Pouvoir")]
    public Color[] c;
    public GameObject bullet;

    void Start ()
    {
        _invincible = false;
        healthText.text = health.ToString();
        _currentHealth = health;
        _rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        Debug.Log("Vitesse : " + speed + " Inivincible : " + _invincible + " isSLow : " + _isSlow);
        if (_invincible == false)
        {
            if (Time.time - _timeToAttack > fireRate)
            {
                int i = 0;
                while (i < firePosition.Length)
                {
                    Instantiate(shot, firePosition[i].position, Quaternion.identity);
                    i = i + 1;
                }
                _timeToAttack = Time.time;
            }
        }
        else
        {
            if (Time.time - _timeToInvincible > timeToBeInvincible)
            {
                _invincible = false;
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
            }
            if (_isSlow == true)
            {
                _timeToSlow = Time.time;
            }
            if (_isSlowFireRate == true)
            {
                _timeToBeSlowFireRate = Time.time;
            }
        }

        if (_isSlow == true && _invincible == false && (Time.time - _timeToSlow) > _timeToBeSlow)
        {
            _isSlow = false;
            speed += _slowValue;
        }
        if (_isSlowFireRate == true && _invincible == false && (Time.time - _timeToSlowFireRate) > _timeToBeSlowFireRate)
        {
            _isSlow = false;
            fireRate -= _slowFireRateValue;
        }
    }

    void FixedUpdate()
    {
         float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //float h = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(h * speed * Time.deltaTime * 100, transform.position.y);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundary, boundary), transform.position.y, -1.0f);
    }

    public void TakeDamage()
    {
        _timeToInvincible = Time.time;
        _currentHealth -= 1;
        _invincible = true;
        healthText.text = _currentHealth.ToString();
       // Instantiate(explosion, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.2f);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SlowMovement(float value, float duration)
    {
        _isSlow = true;
        TakeDamage();
        _slowValue = value;
        _timeToBeSlow = duration;
        speed -= value;
    }

    public void SlowFireRate(float value, float duration)
    {
        _isSlowFireRate = true;
        TakeDamage();
        _slowFireRateValue = value;
        _timeToBeSlowFireRate = duration;
        fireRate += _slowFireRateValue;
    }
}
