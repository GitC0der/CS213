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
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        if (other.gameObject.CompareTag("Sheep")) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject closestPlayer = FindClosestPlayer(players);
            // OnGivePoints();
            closestPlayer.GetComponent<PlayerPoints>().incrementPoints();
            // closestPlayer.incrementPoints;

            Debug.Log("Player " + closestPlayer.transform.parent.gameObject.name + " has points: " + closestPlayer.GetComponent<PlayerPoints>().getPoints().ToString());
        }
    }

    protected virtual void OnGivePoints()
    {
        //if ProcessCompleted is not null then call delegate
        GivePoints?.Invoke(); 
    }


    private GameObject FindClosestPlayer(GameObject[] players)
    {
        GameObject g = players.FirstOrDefault();
        foreach (GameObject p in players.Skip(1))
        {
            g = (p.transform.position - transform.position).magnitude < (g.transform.position - transform.position).magnitude ? p : g;
        }
        return g;
    }
}
