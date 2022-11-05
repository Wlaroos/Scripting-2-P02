using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;

    [SerializeField] GameUIController _uiController;
    public GameUIController UIController => _uiController;

    [SerializeField] DiceController _diceController;
    public DiceController DiceController => _diceController;

    [SerializeField] Dice _dice;
    public Dice Dice => _dice;

    private int _playerRoll;
    public int PlayerRoll => _playerRoll;

    private int _enemyRoll;
    public int EnemyRoll => _enemyRoll;

    private int[] _playerStats = new int[] {0,0,0,0,0};
    public int[] PlayerStats => _playerStats;

    void Start()
    {
        ChangeState<SetupGameState>();
    }

    public void SetStat(int index, int value)
    {
        _playerStats[index] = value;
    }

    public void SetPlayerRoll(int value)
    {
        _playerRoll = value;
    }

    public void SetEnemyRoll(int value)
    {
        _enemyRoll = value;
    }

    public void OnStateExit()
    {
        foreach (Transform child in _diceController.transform)
        {
                GameObject.Destroy(child.gameObject);
        }
    }
}
