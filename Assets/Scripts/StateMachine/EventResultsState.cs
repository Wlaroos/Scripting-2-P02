using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventResultsState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Event Results State]";
        StateMachine.EventResultsController.gameObject.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateMachine.EventResultsController.gameObject.SetActive(false);
    }

}
