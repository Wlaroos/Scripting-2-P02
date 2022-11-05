using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameState : GameState
{

    [SerializeField] int _testVar = 2;
    [SerializeField] int _testVar2 = 3;

    bool _activated = false;

    public override void Enter()
    {
        base.Enter();
        // Only here to stop warnings for "Not using the variable"
        _testVar = _testVar2;

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
        base.Exit();
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }

}
