using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayerCellulo : MonoBehaviour
{
    public AudioClip hitSound;

    CelluloAgent agent;
    private bool alreadyHit = false;
    private bool isAISheep;
    private bool hasGem = false;
    private float gemTimeEnd = 0.0f;
    private AudioSource audioSource;
    private Gem gem;
    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CelluloAgent>();
        //gem = GameObject.FindGameObjectWithTag("Gem").GetComponent<Gem>();
        gem = FindObjectOfType(typeof(Gem)) as Gem;
        audioSource = (gameObject.GetComponent<AudioSource>() != null) ? gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAISheep) {
            AIisSheep();
        } else { 
            AIisGhost();
        }

        if (hasGem && Time.time > gemTimeEnd) {
            hasGem = false;
        } else if (hasGem) {
            Blink();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("other.collider.transform.parent.gameObject: " + other.collider.transform.parent.gameObject + "   other.collider.gameObject: " + other.collider.gameObject);
        if (hasGem && !alreadyHit && other.collider.transform.parent.gameObject.CompareTag("Player")) {
            HitPlayer(GameManager.Instance.Players.GetClosestPlayer(other.collider.transform));
        }
    }

    private void Blink() {
        float timeRatio = 1 - ((gemTimeEnd - Time.time) / gem.GetTotalDuration());
        float colorID = (Mathf.Pow(timeRatio*5, 2.3f) + 2*timeRatio) % 2;    // Function that decides which color to display when the cellulo is blinking
        if (colorID > 1) {
            agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
        } else {
            agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 1);
        }
    }

    public void HitPlayer(Players.Player player) {
        player.GetOtherPlayer().RemoveScore(gem.GetPlayerHitMalus());
        player.AddScore(gem.GetPlayerHitMalus());
        audioSource.clip = hitSound;
        audioSource.Play();
        alreadyHit = true;
    }

    public void GrabGem() {
        hasGem = true;
        gemTimeEnd = Time.time + gem.GetTotalDuration();
    }

    public bool HasGem() {
        return hasGem;
    }

    public void AIisSheep()
    {
        isAISheep = true;
        agent.ClearHapticFeedback();
        agent.SetCasualBackdriveAssistEnabled(true);
    }

    public void AIisGhost()
    {
        isAISheep = false;
        agent.MoveOnStone();
    }
    
}
