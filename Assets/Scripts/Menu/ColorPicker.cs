using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : AgentBehaviour
{
    public int PlayerIdx;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialise()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Players players = GameManager.Instance.Players;
        Players.Player player1 = players.players[PlayerIdx];
    }

    public void BlueColor()
    {
        Initialise();
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
    }

    public void GreenColor()
    {
        Initialise();
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
    }

    public void YellowColor()
    {
        Initialise();
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 0);
    }
}
