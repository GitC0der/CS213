using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("other.collider.transform.parent.gameObject: " + other.collider.transform.parent.gameObject + "   other.collider.gameObject: " + other.collider.gameObject);
        if (other.collider.transform.parent.gameObject.CompareTag("Player")) {
            Players.Player player = GameManager.Instance.Players.GetClosestPlayer(other.collider.transform); 
            if (!this.GetComponentInParent<GhostSheepBehavior>().GetIsSheep()) {
                player.RemoveScore();
            }
        }
    }
}
