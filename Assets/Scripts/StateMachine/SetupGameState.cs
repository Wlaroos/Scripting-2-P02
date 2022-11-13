using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameState : GameState
{
    bool _activated = false;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Setup State]";
        Debug.Log("Setup: ...Entering");
        Debug.Log("------------------");
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;
    }

    public override void Tick()
    {
        // Add delays/input later
        if(_activated == false)
        {
            _activated = true;
            //StateMachine.ChangeState<PlayerTurnGameState>();
            StateMachine.ChangeState<InitialStatRollState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }

}
