using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Minigame State]";
        StateMachine.MinigameController.gameObject.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateMachine.MinigameController.gameObject.SetActive(false);
    }

}
