using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Logo : MonoBehaviour
{
    public float fadeSpeed;

    private bool _fade;
    private bool _loadScene;
    private Color c;
    private Color saveColor;
    private bool _wait;
    private float _time;

    void Start()
    {
        c = GetComponent<Image>().color;
        saveColor = c;

        _loadScene = false;
        _wait = false;
        _fade = true;
    }

    void Update()
    {
        if (_wait == false)
        {
            if (_fade == false)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
        }

        if (Time.time - _time > 3)
        {
            _wait = false;
        }

    }

    void FadeIn()
    {
        if (c.a < 0.9f)
            GetComponent<Image>().color = new Vector4(c.r, c.b, c.g, c.a = c.a + fadeSpeed);
        else
        {
            _time = Time.time;
            _wait = true;
            _loadScene = true;
            _fade = !_fade;
        }
    }

    void FadeOut()
    {
        if (c.a > 0.1f)
            GetComponent<Image>().color = new Vector4(c.r, c.b, c.g, c.a = c.a - fadeSpeed);
        else
        {
            _fade = !_fade;

            if (_loadScene == true)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
