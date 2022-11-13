using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Win State]";
        //StateMachine.MapController.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        //StateMachine.MapController.SetActive(false);
    }

}
