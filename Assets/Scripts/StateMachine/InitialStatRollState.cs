using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialStatRollState : GameState
{

    [SerializeField] TextMeshProUGUI _spacebarText = null;

    bool _once = false;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Initial Stat Roll State]";
        Debug.Log("ROLL FOR YOUR STATS!");
        Debug.Log("------------------");

        _spacebarText.gameObject.SetActive(true);

        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }


    void OnPressedConfirm()
    {
        if (!_once)
        {
            StateMachine.DiceController.StartRoll(5);
            _spacebarText.gameObject.SetActive(false);
            _once = true;
        }
    }

}
