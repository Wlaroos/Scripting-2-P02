using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{
    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Lose State]";
        StateMachine.LoseScreen.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateMachine.LoseScreen.SetActive(false);
    }

}
