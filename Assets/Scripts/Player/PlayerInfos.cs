using UnityEngine;
using System.Collections;

public class PlayerInfos : MonoBehaviour
{
    private int gold;
    private int crystal;
    private int scoreOne;
    private int scoreTwo;
    private int scoreThree;
    private int skin;
    private int health;
    private int fireRate;
    private int speed;

	void Start ()
    {
        DontDestroyOnLoad(this);
	}
}
