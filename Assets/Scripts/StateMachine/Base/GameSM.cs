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

    private int _playerRoll;
    private int _enemyRoll;
    private int[] _stats = new int[] {0,0,0,0,0};

    void Start()
    {
        ChangeState<SetupGameState>();
    }

    public void SetStat(int index, int value)
    {
        _stats[index] = value;
    }

    public void SetPlayerRoll(int value)
    {
        _playerRoll = value;
    }

    public void SetEnemyRoll(int value)
    {
        _enemyRoll = value;
    }
}
