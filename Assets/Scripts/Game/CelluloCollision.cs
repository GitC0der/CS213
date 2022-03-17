using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluloCollision : MonoBehaviour
{
    private float interval;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        interval = 0f;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        print("Detected collider trigger between " + gameObject.name + " and " + other.name + " !");
        print(gameObject.name + " velocity is " + gameObject.GetComponent<Rigidbody>().velocity.magnitude + " !");
        if (other.name == "Star")
        {
            print("Vibrate!");
            gameObject.GetComponent<CelluloAgent>().SetSimpleVibrate(1f, 1f, 1f, 50, 0);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Ground")
        {
            if (other.name == "Square")
            {
                interval += Time.deltaTime;
                if (interval >= 1)
                {
                    interval = 0f;
                    print("Color to " + index + " !");
                    gameObject.GetComponent<CelluloAgent>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.clear, index);
                    gameObject.GetComponent<CelluloAgent>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, index);
                    index += 1;
                    index %= 6;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        print(gameObject.name + " and " + other.name + " are no longer colliding !");
        print(gameObject.name + " velocity is " + gameObject.GetComponent<Rigidbody>().velocity.magnitude + " !");
        if (other.name == "Star")
        {
            print("NOT Vibrate!");
            gameObject.GetComponent<CelluloAgent>().SetSimpleVibrate(0f, 0f, 0f, 0, 0);
        }
        if (other.name == "Square")
        {
            print("NOT Color!");
            gameObject.GetComponent<CelluloAgent>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.clear, index);
        }
    }
}
