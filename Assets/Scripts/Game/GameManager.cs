using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager
{
    //used to make the game manager instance pop in the scenes
    private GameObject gameObject;

    //Singleton system with an instance of this Game Manager
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
            }
            return m_Instance;
        }
    }
}
