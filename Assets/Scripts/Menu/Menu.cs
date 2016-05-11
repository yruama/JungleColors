using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    if (PlayerPrefs.GetInt("conexion") != 1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("gold", 0);
            PlayerPrefs.SetInt("diamond", 0);
            PlayerPrefs.SetInt("conexion", 1);
            PlayerPrefs.SetInt("skinAvatr", 0);
            PlayerPrefs.SetInt("scoreOne", 0);
            PlayerPrefs.SetInt("scoreTwo", 0);
            PlayerPrefs.SetInt("scoreThree", 0);
        }
	}
	
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Option()
    {
        SceneManager.LoadScene(2);
    }

    public void Custum()
    {
        Debug.Log("rfend");
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Score()
    {
        SceneManager.LoadScene(3);
    }
}