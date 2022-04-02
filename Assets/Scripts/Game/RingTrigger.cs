using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public delegate void Notify();

public class RingTrigger : MonoBehaviour
{
    public AudioClip winPoint;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = winPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.GetComponentInParent<Transform>().parent.CompareTag("Sheep")) {

            Players.Player cp = GameManager.Instance.Players.GetClosestPlayer(other.transform);

            if (other.GetComponentInParent<GhostSheepBehavior>().GetIsSheep()) {
                cp.AddScore();
                GetComponent<AudioSource> ().Play();
            }
            Debug.Log("Player: " + cp.gameObject.name + " with name: " + cp.name + " has points: " + GameManager.Instance.Players.GetClosestPlayer(other.transform).Score); 
        }
    }
}
