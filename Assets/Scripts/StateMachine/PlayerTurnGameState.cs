using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerTurnGameState : GameState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        Debug.Log("------------------");

        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        // hook into events

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Tick()
    {
        //
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);

        // unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnemyTurnGameState>();
    }
}
