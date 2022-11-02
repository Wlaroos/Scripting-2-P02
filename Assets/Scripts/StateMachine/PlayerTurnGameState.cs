using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerTurnGameState : GameState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI = null;

    // Amount of dice to roll
    [SerializeField] int _diceAmount = 1;

    // Increments as dice stop moving and return their values
    int _diceResolved = 0;

    // Sum of all rolls
    int _diceTotalScore = 0;

    // List of all dice objects for the turn
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

            // Insantiate and roll however many dice
            for (int i = 0; i < _diceAmount; i++)
            {
                Dice diceRef = Instantiate(StateMachine.Dice, new Vector3(0, -4, 3.5f), Quaternion.identity);

                // Subscribe to the event of the dice that was just spawned
                diceRef.DiceValueReturned += OnDiceReturn;

                _diceRolledList.Add(diceRef);
                diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", UnityEngine.Random.ColorHSV());
                diceRef.RollDice();
            }
        }
    }

    void OnDiceReturn(int diceValue)
    {
        // Does nothing if dice is stuck. Dice is rerolled internally.
        if (diceValue != -1)
        {
            // Adds score
            _diceTotalScore += diceValue;

            // Unsubscribe from the event of the dice in the list that just returned their value
            _diceRolledList[_diceResolved].DiceValueReturned -= OnDiceReturn;

            _diceResolved++;
            Debug.Log("Roll " + _diceResolved + ": " + diceValue);

            if (_diceResolved == _diceAmount)
            {
                Debug.Log("Total Roll: " + _diceTotalScore);
                // Set Player Roll in GameSM script (May be a better way to do this?)
                StateMachine._playerRoll = _diceTotalScore;
                StateMachine.ChangeState<EnemyTurnGameState>();

                // Reset values
                _diceRolledList.Clear();
                _diceTotalScore = 0;
                _diceResolved = 0;
                _rolled = false;
            }
        }
    }
}
