using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialStatRollState : GameState
{

    [SerializeField] TextMeshProUGUI _spacebarText = null;

    bool _activated = false;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Initial Stat Roll State]";
        Debug.Log("ROLL FOR YOUR STATS!");
        Debug.Log("------------------");

        _spacebarText.gameObject.SetActive(true);

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
        _spacebarText.gameObject.SetActive(false);
    }

}
