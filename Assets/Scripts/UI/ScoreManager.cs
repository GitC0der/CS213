using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreTextPlayer1;
    public Text scoreTextPlayer2;
    int scorePlayer1 = 0;
    int scorePlayer2 = 0;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTextPlayer1.text = "P1: " + scorePlayer1.ToString() + " Points";
        scoreTextPlayer2.text = "P2: " + scorePlayer2.ToString() + " Points";
    }

    // Update is called once per frame
    public void AddPoint(string name)
    {
        if (name.Equals("Player CelluloAgent_1")) {
            scorePlayer1 += 1;
            scoreTextPlayer1.text = "P1: " + scorePlayer1.ToString() + " Points";
        } else /*if (name.Equals("Player CelluloAgent_2"))*/ {
            scorePlayer2 += 1;
            scoreTextPlayer2.text = "P2: " + scorePlayer2.ToString() + " Points";
        }
    }

    public void RemovePoint(string name)
    {
        if (name.Equals("Player CelluloAgent_1")) {
            scorePlayer1 -= 1;
            scoreTextPlayer1.text = "P1: " + scorePlayer1.ToString() + " Points";
        } else /*if (name.Equals("Player CelluloAgent_2"))*/ {
            scorePlayer2 -= 1;
            scoreTextPlayer2.text = "P2: " + scorePlayer2.ToString() + " Points";
        }
    }
}
