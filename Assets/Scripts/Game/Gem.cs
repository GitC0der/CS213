using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    /* ---- Parameters ---- **/
    public float spawnInterval = 20.0f;    // Fixed time between each spawn of a gem
    public float spawnDuration = 10.0f;   // Fixed time before the gem disappears
    public float powerUpDuration = 10.0f;  // Duration of the power up given by the gem
    public int hitMalus = 2;                 // Points awarded when a player that has a gem collides with another player

    public AudioClip grabSound;
    public AudioClip spawnSound;

    private float xMin = 5.00f;
    private float xMax = 23.00f;
    private float zMax = -5.40f;
    private float zMin = -16.41f;
    private Vector3 ringPosition = new Vector3(14.30f, 0.0f, -10.0f);
    private float ringClearanceRadius = 4.0f;
    private float celluloClearanceRadius = 3.0f;

    /* ---- Programming Variables ---- **/
    private float nextSpawnTime = 0.0f;
    private float nextDespawnTime = 0.0f;
    private AudioSource audioSource;
    private Collider collider; 

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        Disable();
        nextSpawnTime = spawnInterval;
        nextDespawnTime = spawnDuration;

        audioSource = (gameObject.GetComponent<AudioSource>() != null) ? gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextDespawnTime) {
            Enable();
            nextDespawnTime += spawnInterval;
        }
        if (Time.time > nextSpawnTime) {
            nextSpawnTime += spawnInterval;
            Spawn();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("other.collider.transform.parent.gameObject: " + other.collider.transform.parent.gameObject + "   other.collider.gameObject: " + other.collider.gameObject);
        if (other.collider.transform.parent.gameObject.CompareTag("Player")) {
            Players.Player player = GameManager.Instance.Players.GetClosestPlayer(other.collider.transform); 
            Grabbed(player.gameObject);
        }
    }

    public void Spawn() {
        Debug.Log("Gem has spawned");
        bool nearCellulo = false;
        bool nearRing = false;
        Vector3 gemPosition;
        List<Players.Player> players = GameManager.Instance.Players.players;
        List<GameObject> cellulos = new List<GameObject>();
        cellulos.Add(GameManager.Instance.Players.Get(0).gameObject);
        cellulos.Add(GameManager.Instance.Players.Get(1).gameObject);
        GameObject ghostSheep = GameObject.FindGameObjectWithTag("Sheep");
        ghostSheep = (ghostSheep == null) ? GameObject.FindGameObjectWithTag("Ghost"): ghostSheep;
        cellulos.Add(ghostSheep);

        do {
            gemPosition = new Vector3(Random.Range(xMin, xMax), 0.0f, Random.Range(zMin, zMax));
            nearRing = Vector3.Distance(gemPosition, ringPosition) < ringClearanceRadius;
            foreach (GameObject cellulo in cellulos) {
                nearCellulo = nearCellulo || Vector3.Distance(gemPosition, cellulo.transform.parent.position) < celluloClearanceRadius;
            }
        } while(nearCellulo || nearRing);

        this.transform.parent.position = gemPosition;
        audioSource.clip = spawnSound;
        audioSource.Play();
        Disable();
    }

    public void Despawn() {
        Debug.Log("Gem disappeared");
        Disable();
    }

    public void Grabbed(GameObject player) {
        audioSource.clip = grabSound;
        audioSource.Play();
        player.GetComponent<RealPlayerCellulo>().GrabGem();
    }

    public void Enable() {
        GameObject.FindGameObjectWithTag("Gem").SetActive(true);
        collider.enabled = true;
    }

    public void Disable() {
        GameObject.FindGameObjectWithTag("Gem").SetActive(false);
        collider.enabled = false;
    }

    public float GetTotalDuration() { return powerUpDuration; }

    public int getPlayerHitMalus() { return hitMalus; }
    
}
