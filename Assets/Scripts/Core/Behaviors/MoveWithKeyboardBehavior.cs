using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    Arrows, 
    WASD
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public InputKeyboard inputKeyboard;
    public AudioClip hitSound;

    //private GameObject cellulo;
    //CelluloAgent agent;
    private bool isAISheep;
    private bool hasGem = false;
    private float gemTimeEnd = 0.0f;
    private AudioSource audioSource;
    private Gem gem;
    private Color initialColor;
    private Color currentColor;
    
    void Start()
    {
        //cellulo = gameObject;
        agent = GetComponent<CelluloAgent>();
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
            ResetBlinking();
        } else if (hasGem) {
            Blink();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            return;
        Debug.Log("other.collider.transform.parent.gameObject: " + other.collider.transform.parent.gameObject + "   other.collider.gameObject: " + other.collider.gameObject);
        if (hasGem && other.collider.transform.parent.gameObject.CompareTag("Player")) {
            HitPlayer(GameManager.Instance.Players.GetClosestPlayer(other.collider.transform));
        }
    }

    private void Blink() {
        float timeRatio = 1 - ((gemTimeEnd - Time.time) / gem.GetTotalDuration());
        
		const float startSpeed = 15;    // Blinking speed of the lights in the beginning, >=0
		const float endSpeed = 110f;   // Blinking speed of the lights in end, >=0
		const float offSet = 2.27f;     // The higher the later the speed starts increasing, >=0
		//float colorID = (Mathf.Pow(timeRatio*5, 2.27f) + 10*timeRatio) % 2;    // Function that decides which color to display when the cellulo is blinking
		// Function of type f(x) = ax^k + bx that decides which color to display when the cellulo is blinking
		float colorID = ((endSpeed - startSpeed)/offSet*Mathf.Pow(timeRatio, offSet) + startSpeed*timeRatio) % 2;    
        
		currentColor = (colorID > 1) ? Color.red : Color.yellow;
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, currentColor, 0);
    }

    public void HitPlayer(Players.Player player) {
        player.RemoveScore(gem.GetPlayerHitMalus());
        player.GetOtherPlayer().AddScore(gem.GetPlayerHitMalus());
        audioSource.clip = hitSound;
        audioSource.Play();
        hasGem = false;
        ResetBlinking();
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
    
    public void SetInitialColor(Color color)
    {
        initialColor = color;
    }

    public Color GetInitialColor()
    {
        return initialColor;
    }

    private void ResetBlinking()
    {
        currentColor = initialColor;
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, initialColor, 0);
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float horizontal = Input.GetAxis("Horizontal" + inputKeyboard.ToString());
        float vertical = Input.GetAxis("Vertical" + inputKeyboard.ToString());

        steering.linear = new Vector3(horizontal, 0, vertical)* agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));
        return steering;
    }

    public void ChangeControls(int idx)
    {
        inputKeyboard = (InputKeyboard) idx;
    }
}
