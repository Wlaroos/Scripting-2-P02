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

    [SerializeField] MapController _mapController;
    [SerializeField] EventController _eventController;
    [SerializeField] MinigameController _minigameController;
    public MapController MapController => _mapController;
    public EventController EventController => _eventController;
    public MinigameController MinigameController => _minigameController;

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
        _mapController.gameObject.SetActive(false);
        _eventController.gameObject.SetActive(false);
        _minigameController.gameObject.SetActive(false);
    }

    public void SetStat(int index, int value)
    {
        _playerStats[index] = value;
        _uiController._statString[index] = "[" + value.ToString() + "]";
        _uiController.StatChange();
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
