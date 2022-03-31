using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    [SerializeField]
    private bool isSheep = true;
    [SerializeField]
    private Color sheepColor = Color.green;
    [SerializeField]
    private Color ghostColor = Color.red;

    float repeatRate = 0f;
    float fleeDistance = 5f;

    public void Awake()
    {
        base.Awake();
        isSheep = true;
    }

    public void Start() {
        this.gameObject.tag = "Sheep";
        this.tag = "Sheep";
        setLightning();
        repeatRate = Random.Range(5f, 10f);
        Invoke("SwitchState", repeatRate);
    }

    public override Steering GetSteering()
    {
        Vector3 playerPositionsSum = Vector3.zero;
        Vector3 avoidPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;

        Steering steering = new Steering();
        //implement your code here.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = FindClosestPlayer(players);

        playerPositionsSum += closestPlayer.transform.position;
        bool isPlayerinRange = false;

        foreach (GameObject p in players)
        {
            GameObject[] sheep = GameObject.FindGameObjectsWithTag("Sheep");
            Vector3 heading = p.transform.position - sheep[0].transform.position;
            float distance = heading.magnitude;
            if (distance < fleeDistance) {
                playerPositionsSum += p.transform.position;
                isPlayerinRange = true;
            }
        }

        // Computes the centroid of the position of every player, closest player taken twice into account
        avoidPosition = playerPositionsSum/(players.Length+1);

        // Sets the target position to move to according to the state of the sheep/ghost bot
        targetPosition = isSheep ? - ((avoidPosition-transform.position).normalized) : closestPlayer.transform.position-transform.position;

        //GameObject.Find("test").transform.position = new Vector3(targetPosition.x * agent.maxSpeed + transform.position.x, 1, targetPosition.z * agent.maxSpeed + transform.position.z);

        if (isPlayerinRange || !isSheep) {
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(new Vector3(targetPosition.x, 0, targetPosition.z) * agent.maxAccel, agent.maxAccel));
        }

        return steering;
    }

    private void SwitchState()
    {
        isSheep = !isSheep;
        setLightning();
        Invoke("SwitchState", repeatRate = Random.Range(2f, 5f));
    }

    private void setLightning()
    {
        Color color = isSheep? Color.green : ghostColor;
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, color, 0);
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

    public bool GetIsSheep() {
        return isSheep;
    }
}

