using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : GameState
{
    [SerializeField] AudioClip _winSFX;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Win State]";

        AudioManager.Instance.PlaySound2D(_winSFX, 1f, UnityEngine.Random.Range(.8f, 1.2f));

        StateMachine._WinScreen.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public override void Exit()
    {
        StateMachine._WinScreen.SetActive(true);
    }

}
