using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    private int points = 0;

    public void incrementPoints()
    {
        ++points;
    }

    public void decrementPoints()
    {
        --points;
    }

    public int getPoints()
    {
        return points;
    }

    // public static void Main()
    // {
    //     RingTrigger rt = new RingTrigger();
    //     rt.GivePoints += rt_GivePoints; // register with an event
    //     rt.OnTriggerEnter();
    // }

    // event handler
    public static void rt_GivePoints()
    {
        //incrementPoints();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
