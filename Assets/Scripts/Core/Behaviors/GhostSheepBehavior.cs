using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class GhostSheepBehavior : AgentBehaviour
{
    [SerializeField]
    private bool isSheep = true;
    [SerializeField]
    private Color sheepColor = Color.grey;
    [SerializeField]
    private Color ghostColor = Color.red;

    GameObject Sheep;
    private AudioSource audioSource;

    public AudioClip wolf_audioClip;
    public AudioClip sheep_audioClip;

    float repeatRate = 0f;
    float fleeDistance = 5f;

    float minStateDuration = 2f;
    float maxStateDuration = 6f;

    [SerializeField]
    private RealPlayerCellulo Player1;
    [SerializeField]
    private RealPlayerCellulo Player2;

    public new void Awake()
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

        audioSource = (gameObject.GetComponent<AudioSource>() != null) ? gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSheep) {
            Player1.AIisSheep();
            Player2.AIisSheep();
        } else {
            Player1.AIisGhost();
            Player2.AIisGhost();
        }
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
        int playerInRange = (closestPlayer.transform.position - Sheep.transform.position).magnitude < fleeDistance ? 1 : 0;

        foreach (Players.Player p in players)
        {
            Vector3 heading = p.gameObject.transform.position - Sheep.transform.position;
            float distance = heading.magnitude;
            if (distance < fleeDistance) {
                playerPositionsSum += p.gameObject.transform.position;
                playerInRange += 1;
            }
        }

        // Computes the centroid of the position of every player, closest player taken twice into account
        avoidPosition = playerPositionsSum/(playerInRange);

        // Sets the target position to move to according to the state of the sheep/ghost bot
        targetPosition = isSheep ? - ((avoidPosition-transform.position).normalized) : closestPlayer.transform.position-transform.position;

        if (playerInRange > 0 || !isSheep) {
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(new Vector3(targetPosition.x, 0, targetPosition.z) * agent.maxAccel, agent.maxAccel));
        }

        return steering;
    }

    private void SwitchState()
    {
        isSheep = !isSheep;
        audioSource.clip = isSheep ? sheep_audioClip : wolf_audioClip;
        audioSource.Play();
        setLightning();
        Invoke("SwitchState", repeatRate = Random.Range(minStateDuration, maxStateDuration));
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

