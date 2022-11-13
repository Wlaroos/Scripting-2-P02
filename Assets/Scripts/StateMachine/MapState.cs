using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Map State]";
        StateMachine.MapController.gameObject.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateMachine.MapController.gameObject.SetActive(false);
    }

}
