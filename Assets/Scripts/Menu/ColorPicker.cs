using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    CelluloAgent agent;
    private RealPlayerCellulo player;
    void Start()
    { 
        agent = GetComponent<CelluloAgent>();
        player = GetComponent<RealPlayerCellulo>();
    }

    public void BlueColor()
    {
        player.SetInitialColor(Color.blue);
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
    }

    public void GreenColor()
    {
        player.SetInitialColor(Color.green);

        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
    }

    public void YellowColor()
    {
        player.SetInitialColor(Color.yellow);

        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 0);
    }
}
