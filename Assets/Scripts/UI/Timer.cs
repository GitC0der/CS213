using UnityEngine;
using UnityEngine.UI;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
    private Text timerText;
    public Slider slider;
    public float maxMinutes = 5;
    public GameManager gameManager;

    public void Awake() {
        initTimerValue = Time.time; 
    }

    // Start is called before the first frame update
    public void Start() {
        timerText = GetComponent<Text>();
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
    }

    // Update is called once per frame
    public void Update() {
        float t = Time.time - initTimerValue;

        int minutesCount = (int) t / 60;
        int secondsCount = (int)t % 60;
        string minutesText = (minutesCount).ToString();
        string secondsText = (secondsCount).ToString("f0");
        if (secondsCount < 10) secondsText = "0" + secondsText;

        timerText.text = minutesText + ":" + secondsText;
        
    }
}
