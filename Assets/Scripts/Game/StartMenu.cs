using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public bool gameStarted = false;
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameStarted == false)
            {
                Time.timeScale = 1;
                gameStarted = true;
                Cursor.visible = false;
                startMenu.SetActive(false);
            }
        }
    }

    public void startGame()
    {
        startMenu.SetActive(false);
        Cursor.visible = false;
        gameStarted = true;
        Time.timeScale = 1;   
    }
}
