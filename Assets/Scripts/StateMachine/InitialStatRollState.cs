using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialStatRollState : GameState
{

    bool _activated = false;

    public override void Enter()
    {
        // Only here to stop warnings for "Not using the variable"

        Debug.Log("ROLL FOR YOUR STATS!");
        Debug.Log("------------------");
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Tick()
    {
        // Add delays/input later
        if(_activated == false)
        {
            _activated = true;
        }
    }

    public override void Exit()
    {
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        _activated = false;
    }


    void OnPressedConfirm()
    {
        StateMachine.DiceController.StartRoll(5);
    }

}
