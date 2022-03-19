using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    
    public void Start() {
        this.gameObject.tag = "Sheep";
    }

    public override Steering GetSteering()
    {
        
        Steering steering = new Steering();
        //implement your code here.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Vector3 playerPositionsSum = new Vector3(0,0,0);
        foreach(GameObject p in players)
        {
            playerPositionsSum += p.transform.position;
        }
        Vector3 avoidPosition;
        avoidPosition.x = playerPositionsSum.x / players.Length;
        avoidPosition.y = playerPositionsSum.y / players.Length;
        avoidPosition.z = playerPositionsSum.z / players.Length;

        // Position currently NOT avoided but sought after!

        float maxDistanceDelta = 0.5f;
        Vector3 dst = Vector3.MoveTowards(this.transform.position, avoidPosition, maxDistanceDelta);

        steering.linear = dst;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));

        return steering;
    }



}
