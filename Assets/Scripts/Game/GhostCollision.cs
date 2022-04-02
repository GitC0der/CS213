using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    void onCollisionEnter(Collider other)
    {

        Debug.Log("Collision occured");

        if (other.GetComponentInParent<Transform>().parent.CompareTag("Player")) {

            Debug.Log("Collision with a player");

            Players.Player player = GameManager.Instance.Players.GetClosestPlayer(other.transform); 
            if (!this.GetComponentInParent<GhostSheepBehavior>().GetIsSheep()) {

                Debug.Log("Collision with " + player.name);

                player.RemoveScore();
            }
        }
    }
}
