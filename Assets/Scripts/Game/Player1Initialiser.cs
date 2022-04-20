using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Initialiser : MonoBehaviour
{
    CelluloAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CelluloAgent>();
        agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
