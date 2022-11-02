using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;

    [SerializeField] DiceController _diceController;
    public DiceController DiceController => _diceController;

    [SerializeField] Dice _dice;
    public Dice Dice => _dice;

    public int _playerRoll;
    public int _enemyRoll;

    void Start()
    {
        ChangeState<SetupGameState>();
    }
}
