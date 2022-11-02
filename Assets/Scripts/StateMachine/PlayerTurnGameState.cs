using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerTurnGameState : GameState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI = null;

    [SerializeField] int _diceAmount = 1;
    int _diceResolved = 0;
    int _diceTotalScore = 0;
    List<Dice> _diceRolledList = new List<Dice>();

    bool _rolled = false;

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
        if (!_rolled)
        {
            _rolled = true;
            for (int i = 0; i < _diceAmount; i++)
            {
                Dice diceRef = Instantiate(StateMachine.Dice, new Vector3(0, -4, 3.5f), Quaternion.identity);
                diceRef.DiceValueReturned += OnDiceReturn;
                _diceRolledList.Add(diceRef);
                diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", UnityEngine.Random.ColorHSV());
                diceRef.RollDice();
            }
        }
    }

    void OnDiceReturn(int diceValue)
    {
        if (diceValue != -1)
        {
            _diceTotalScore += diceValue;
            _diceResolved++;
            Debug.Log("Roll " + _diceResolved + ": " + diceValue);

            if (_diceResolved == _diceAmount)
            {
                Debug.Log("Total Roll: " + _diceTotalScore);
                StateMachine.ChangeState<EnemyTurnGameState>();

                _diceTotalScore = 0;
                _diceResolved = 0;
                _rolled = false;
            }
        }
    }
}
