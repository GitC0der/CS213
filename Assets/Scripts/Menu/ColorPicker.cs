using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    CelluloAgent agent;
    void Start()
    { 
        agent = GetComponent<CelluloAgent>();
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
