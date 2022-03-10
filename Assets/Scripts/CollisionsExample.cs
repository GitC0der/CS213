using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Detected collision between " + gameObject.name + " and " + collision.collider.name + " !");
        string s = string.Format(collision.contactCount == 1 ? "is {0} contact point !" : "are {0} contact points !", collision.contactCount);
        print("There " + s);
        print("Their relative velocity is " + collision.relativeVelocity + " !");
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Ground")
        {
            print(gameObject.name + " and " + collision.collider.name + " are still colliding !"); 
        }
    }
    void OnCollisionExit(Collision collision)
    {
        print(gameObject.name + " and " + collision.collider.name + " are no longer colliding !");
    }
}
