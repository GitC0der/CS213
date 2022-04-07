using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        try { GameManager.Instance.Players.AddPlayer(gameObject, gameObject.name); } catch { Debug.Log("Error adding player"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
