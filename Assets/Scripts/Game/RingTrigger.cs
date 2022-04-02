using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public delegate void Notify();

public class RingTrigger : MonoBehaviour
{
    public event Notify GivePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (other.GetComponentInParent<Transform>().parent.CompareTag("Sheep")) {

            Players.Player cp = GameManager.Instance.Players.GetClosestPlayer(other.transform);

            if (other.GetComponentInParent<GhostSheepBehavior>().GetIsSheep())
                cp.AddScore();

            Debug.Log("Player: " + cp.gameObject.name + " with name: " + cp.name + " has points: " + GameManager.Instance.Players.GetClosestPlayer(other.transform).Score);

            // Update the displayed score
            ScoreManager.instance.AddPoint();
        }
    }
}
