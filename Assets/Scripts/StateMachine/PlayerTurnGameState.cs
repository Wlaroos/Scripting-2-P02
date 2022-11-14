using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerTurnGameState : GameState
{
    [SerializeField] TextMeshProUGUI _playerTurnText = null;
    [SerializeField] TextMeshProUGUI _spacebarText = null;

    int _playerTurnCount = 0;

    bool _once = false;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Player Turn State]";
        Debug.Log("Player Turn: ...Entering");
        Debug.Log("------------------");

        _playerTurnText.gameObject.SetActive(true);
        _spacebarText.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnText.text = "Player Turn: " + _playerTurnCount.ToString();

        // hook into events

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Tick()
    {
        //
    }

    public override void Exit()
    {
        _playerTurnText.gameObject.SetActive(false);

        _once = false;

        // unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        if (!_once)
        {
            StateMachine.DiceController.StartRoll(StateMachine.PlayerStats[StateMachine.StatIndex] + StateMachine.ExtraDice);
            _spacebarText.gameObject.SetActive(false);
            _once = true;
        }
    }

}
