using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class GhostSheepBehavior : AgentBehaviour
{
    [SerializeField]
    private bool isSheep = true;
    [SerializeField]
    private Color sheepColor = Color.green;
    [SerializeField]
    private Color ghostColor = Color.red;

    GameObject Sheep;

    public AudioSource[] sounds;
    public AudioClip wolf;
    public AudioClip bah;

    float repeatRate = 0f;
    float fleeDistance = 5f;

    public void Awake()
    {
        base.Awake();
        isSheep = true;
    }

    public void Start() {
        Sheep = GameObject.FindGameObjectWithTag("Sheep");
        this.tag = "Sheep";
        setLightning();
        repeatRate = Random.Range(5f, 10f);
        Invoke("SwitchState", repeatRate);
        sounds = GetComponents<AudioSource>();
        sounds[0].playOnAwake = false;
        sounds[0].clip = wolf;
        sounds[1].playOnAwake = false;
        sounds[1].clip = bah;
    }

    public override Steering GetSteering()
    {
        Vector3 playerPositionsSum = Vector3.zero;
        Vector3 avoidPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;

        Steering steering = new Steering();
        //implement your code here.

        List<Players.Player> players = GameManager.Instance.Players.players;
        GameObject closestPlayer = GameManager.Instance.Players.GetClosestPlayer(transform).gameObject;

        playerPositionsSum = (closestPlayer.transform.position - Sheep.transform.position).magnitude<fleeDistance? closestPlayer.transform.position + playerPositionsSum : Vector3.zero + playerPositionsSum;
        bool isPlayerinRange = false;

        foreach (Players.Player p in players)
        {
            Vector3 heading = p.gameObject.transform.position - Sheep.transform.position;
            float distance = heading.magnitude;
            if (distance < fleeDistance) {
                playerPositionsSum += p.gameObject.transform.position;
                isPlayerinRange = true;
            }
        }

        // Computes the centroid of the position of every player, closest player taken twice into account
        avoidPosition = playerPositionsSum/(players.Count+1);

        // Sets the target position to move to according to the state of the sheep/ghost bot
        targetPosition = isSheep ? - ((avoidPosition-transform.position).normalized) : closestPlayer.transform.position-transform.position;

        if (isPlayerinRange || !isSheep) {
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(new Vector3(targetPosition.x, 0, targetPosition.z) * agent.maxAccel, agent.maxAccel));
        }

        return steering;
    }

    private void SwitchState()
    {
        if (isSheep) sounds[0].Play();
        else sounds[1].Play();
        isSheep = !isSheep;
        setLightning();
        Invoke("SwitchState", repeatRate = Random.Range(2f, 5f));
    }

    private void setLightning()
    {
        Color color = isSheep? sheepColor : ghostColor;
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, color, 0);
    }

    public bool GetIsSheep() {
        return isSheep;
    }
}

