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
    private float ringClearanceRadius = 5.0f;
    private float celluloClearanceRadius = 4.0f;

    /* ---- Programming Variables ---- **/
    private float nextSpawnTime = 0.0f;
    private float nextDespawnTime = 0.0f;
    private AudioSource audioSource;
    private Collider collider;
    private bool isEnabled = false;
    private List<GameObject> cellulos;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        Disable();
        nextSpawnTime = spawnInterval;
        nextDespawnTime = spawnDuration;

        audioSource = (gameObject.GetComponent<AudioSource>() != null) ? gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        
        // Creates a list containing all Cellulos
        cellulos = new List<GameObject>
        {
            GameManager.Instance.Players.Get(0).gameObject,
            GameManager.Instance.Players.Get(1).gameObject
        };
        GameObject ghostSheep = GameObject.FindGameObjectWithTag("Sheep");
        ghostSheep = (ghostSheep == null) ? GameObject.FindGameObjectWithTag("Ghost"): ghostSheep;
        cellulos.Add(ghostSheep);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(string.Format("Time = {0} | despawn =  {1} | spawn = {2} | interval = {3}", Time.time, nextDespawnTime, nextSpawnTime, spawnInterval));
        if (Time.time > nextDespawnTime) {
            Despawn();
            nextDespawnTime += spawnInterval;
        }
        if (Time.time > nextSpawnTime) {
            nextSpawnTime += spawnInterval;
            Spawn();
        }
    }

    /*
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
    **/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("other.collider.transform.parent.gameObject: " + other.transform.parent.gameObject + "   other.collider.gameObject: " + other.gameObject);
        if (other.transform.parent.gameObject.CompareTag("Player")) {
            Players.Player player = GameManager.Instance.Players.GetClosestPlayer(other.transform); 
            Grabbed(player.gameObject);
        }
    }

    public void Spawn() {
        bool nearCellulo;
        bool nearRing;
        Vector3 gemPosition;
        do
        {
            gemPosition = new Vector3(Random.Range(xMin, xMax), 0.5f, Random.Range(zMin, zMax));
            nearRing = Vector3.Distance(gemPosition, ringPosition) < ringClearanceRadius;
            nearCellulo = false;
            foreach (GameObject cellulo in cellulos) {
                nearCellulo = nearCellulo || Vector3.Distance(gemPosition, cellulo.transform.position) < celluloClearanceRadius;
            }

        } while(nearCellulo || nearRing);

        Enable();
        transform.position = gemPosition;
        audioSource.clip = spawnSound;
        audioSource.Play();
        Debug.Log("Gem has spawned in" + transform.position);

    }

    public void Despawn() {
        if (isEnabled) Debug.Log("Gem disappeared");
        Disable();
    }

    public void Grabbed(GameObject player)
    {
        Debug.Log("Gem grabbed");
        Disable();
        audioSource.clip = grabSound;
        audioSource.Play();
        player.GetComponent<RealPlayerCellulo>().GrabGem();
    }

    public void Enable()
    {
        isEnabled = true;
        //GameObject.FindGameObjectWithTag("Gem").SetActive(true);
        collider.enabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
        //GameObject.FindGameObjectWithTag("Gem").SetActive(false);
        // Dirty solution that works better than the above
        transform.position = new Vector3(9999, 9999, 9999);
        collider.enabled = false;
    }

    public float GetTotalDuration() { return powerUpDuration; }

    public int GetPlayerHitMalus() { return hitMalus; }
    
}
