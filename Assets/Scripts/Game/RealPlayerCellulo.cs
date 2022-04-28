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

    void AIisSheep()
    {
        isSheep = true;
        agent.SetCasualBackdriveAssistEnabled(true);
    }

    void AIisGhost()
    {
        isSheep = false;
        agent.MoveOnStone();
        agent.SetCasualBackdriveAssistEnabled(false);
    }
}
