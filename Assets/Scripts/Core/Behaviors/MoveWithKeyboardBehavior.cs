using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    Arrows, 
    WASD
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public InputKeyboard inputKeyboard; 

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float horizontal = Input.GetAxis("Horizontal" + inputKeyboard.ToString());
        float vertical = Input.GetAxis("Vertical" + inputKeyboard.ToString());

        steering.linear = new Vector3(horizontal, 0, vertical)* agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));
        return steering;
    }

    public void ChangeControls(int idx)
    {
        inputKeyboard = (InputKeyboard) idx;
    }
}
