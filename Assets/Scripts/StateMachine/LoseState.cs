using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] AudioClip _loseSFX;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Lose State]";

        AudioManager.Instance.PlaySound2D(_loseSFX, 1f, UnityEngine.Random.Range(.8f, 1.2f));

        StateMachine.LoseScreen.SetActive(true);
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public override void Exit()
    {
        StateMachine.LoseScreen.SetActive(false);
    }

}
