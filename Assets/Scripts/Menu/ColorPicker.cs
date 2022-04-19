using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : AgentBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlueColor()
    {
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
    }

    public void GreenColor()
    {
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
    }

    public void YellowColor()
    {
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 0);
    }
}
