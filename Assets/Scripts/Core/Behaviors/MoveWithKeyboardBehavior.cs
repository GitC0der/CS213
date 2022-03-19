using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows =0, 
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public InputKeyboard inputKeyboard; 

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        //implement your code here (to be verified!)
        float horizontalArrows = Input.GetAxis("HorizontalArrows");
        float verticalArrows = Input.GetAxis("VerticalArrows");

        float horizontalWASD = Input.GetAxis("HorizontalWASD");
        float verticalWASD = Input.GetAxis("VerticalWASD");

        steering.linear = new Vector3(inputKeyboard == 0 ? horizontalArrows : horizontalWASD, 0, inputKeyboard == 0 ? verticalArrows : verticalWASD)* agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));
        return steering;
    }

}
