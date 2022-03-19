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
        Vector3 destination;
        destination.x = playerPositionsSum.x / players.Length;
        destination.y = playerPositionsSum.y / players.Length;
        destination.z = playerPositionsSum.z / players.Length;

        float maxDistanceDelta = 0.5f;
        Vector3 dst = Vector3.MoveTowards(this.transform.position, destination, maxDistanceDelta);

        steering.linear = dst;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));

        return steering;
    }



}
