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

    [SerializeField] GameObject _mapController;
    [SerializeField] EventChoiceController _eventChoiceController;
    [SerializeField] GameObject _minigameController;
    [SerializeField] EventResultsController _eventResultsController;
    public GameObject MapController => _mapController;
    public EventChoiceController EventChoiceController => _eventChoiceController;
    public GameObject MinigameController => _minigameController;
    public EventResultsController EventResultsController => _eventResultsController;

    [SerializeField] Dice _dice;
    public Dice Dice => _dice;

    private int _playerRoll;
    public int PlayerRoll => _playerRoll;

    private int _enemyRoll;
    public int EnemyRoll => _enemyRoll;

    private int[] _playerStats = new int[] {0,0,0,0,0};
    public int[] PlayerStats => _playerStats;

    private int _statIndex;
    public int StatIndex => _statIndex;

    private int _extraDice;
    public int ExtraDice => _extraDice;

    private int _enemyDiceAmount;
    public int EnemyDiceAmount => _enemyDiceAmount;

    private ChoiceSO _eventChoice;
    public ChoiceSO EventChoice => _eventChoice;

    void Start()
    {
        ChangeState<SetupGameState>();
        _mapController.gameObject.SetActive(false);
        _eventChoiceController.gameObject.SetActive(false);
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

    public void SetExtraDice(int value)
    {
        _extraDice = value;
    }

    public void SetEventChoice(ChoiceSO value)
    {
        _eventChoice = value;
        _enemyDiceAmount = value.EnemyDiceAmount;
        _statIndex = value.StatIndex;
    }

    public void OnStateExit()
    {
        foreach (Transform child in _diceController.transform)
        {
                GameObject.Destroy(child.gameObject);
        }
    }
}
