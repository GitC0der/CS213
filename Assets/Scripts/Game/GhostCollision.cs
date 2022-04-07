using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("Collision occured");
        Debug.Log("other.collider.transform.parent.gameObject: " + other.collider.transform.parent.gameObject + "   other.collider.gameObject: " + other.collider.gameObject);

        if (other.collider.transform.parent.gameObject.CompareTag("Player")) {

            Debug.Log("Collision with a player");

            Players.Player player = GameManager.Instance.Players.GetClosestPlayer(other.collider.transform); 
            if (!this.GetComponentInParent<GhostSheepBehavior>().GetIsSheep()) {

                Debug.Log("Collision with " + player.name);

                player.RemoveScore();
            }
        }
    }
}
