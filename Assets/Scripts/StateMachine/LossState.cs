using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Loss State]";
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
