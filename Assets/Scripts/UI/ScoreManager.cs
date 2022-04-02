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
        scoreTextPlayer1.text = scorePlayer1.ToString() + " Points";
        scoreTextPlayer2.text = scorePlayer2.ToString() + " Points";
    }

    // Update is called once per frame
    public void AddPoint()
    {
        scorePlayer1 += 1;
        scorePlayer2 += 1;
        scoreTextPlayer1.text = scorePlayer1.ToString() + " Points";
        scoreTextPlayer2.text = scorePlayer2.ToString() + " Points";
    }
}
