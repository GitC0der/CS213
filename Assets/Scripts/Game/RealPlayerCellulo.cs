using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayerCellulo : MonoBehaviour
{
    CelluloAgent agent;
    private bool isSheep;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CelluloAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSheep) AIisSheep();
        else AIisGhost();
    }

    public void AIisSheep()
    {
        isSheep = true;
        agent.ClearHapticFeedback();
        agent.SetCasualBackdriveAssistEnabled(true);
    }

    public void AIisGhost()
    {
        isSheep = false;
        agent.MoveOnStone();
    }
}
