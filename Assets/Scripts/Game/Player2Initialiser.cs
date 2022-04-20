using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Initialiser : MonoBehaviour
{
    CelluloAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CelluloAgent>();
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
