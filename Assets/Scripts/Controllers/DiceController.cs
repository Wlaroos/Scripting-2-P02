using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{

    [SerializeField] Dice _diceRef;
    [SerializeField] GameSM _SMRef;
    [SerializeField] Vector3 _throwPosition = new Vector3(0, -4, 3.5f);

    private Color[] _statColors = new Color[] {new Color(1,0,0), new Color(1,.65f,0), new Color(1,1,0), new Color(0,0,1), new Color(.6f,0.1f,1), };

    // Amount of dice to roll
    [SerializeField] int _diceAmount = 1;

    // Increments as dice stop moving and return their values
    int _diceResolved = 0;

    // Sum of all rolls
    int _diceTotalScore = 0;

    // List of all dice objects for the turn
    List<Dice> _diceRolledList = new List<Dice>();

    bool _rolled = false;

    public void StartRoll(int diceAmount)
    {
        if (!_rolled)
        {
            _rolled = true;

            // Insantiate and roll however many dice
            for (int i = 0; i < diceAmount; i++)
            {
                Dice diceRef = Instantiate(_diceRef, _throwPosition, Quaternion.identity);

                // Subscribe to the event of the dice that was just spawned
                diceRef.DiceValueReturned += OnDiceReturn;

                _diceRolledList.Add(diceRef);

                switch (_SMRef.CurrentState)
                {
                    case InitialStatRollState:
                        diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", _statColors[i]);
                        break;
                    case EnemyTurnGameState:
                        diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        break;
                    case PlayerTurnGameState:
                        diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.cyan);
                        break;
                }

                //diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", UnityEngine.Random.ColorHSV());

                diceRef.RollDice();
            }
        }
    }

    void OnDiceReturn(int diceValue, Color diceColor)
    {
        // Does nothing if dice is stuck. Dice is rerolled internally.
        if (diceValue != -1)
        {
            // Adds score
            if(_SMRef.CurrentState == _SMRef.GetComponent<InitialStatRollState>())
            {
                switch (diceColor)
                {
                    case Color when _statColors[0] == diceColor:
                        _SMRef.SetStat(0, diceValue);
                        break;
                    case Color when _statColors[1] == diceColor:
                        _SMRef.SetStat(1, diceValue);
                        break;
                    case Color when _statColors[2] == diceColor:
                        _SMRef.SetStat(2, diceValue);
                        break;
                    case Color when _statColors[3] == diceColor:
                        _SMRef.SetStat(3, diceValue);
                        break;
                    case Color when _statColors[4] == diceColor:
                        _SMRef.SetStat(4, diceValue);
                        break;
                }
            }
            _diceTotalScore += diceValue;

            // Unsubscribe from the event of the dice in the list that have returned their value
            foreach (Dice dice in _diceRolledList)
            {
                if (dice.DiceValue > 0)
                {
                    dice.DiceValueReturned -= OnDiceReturn;
                }
            }

            _diceResolved++;
            Debug.Log("Roll " + _diceResolved + ": " + diceValue);

            if (_diceResolved == _diceRolledList.Count)
            {
                Debug.Log("Total Roll: " + _diceTotalScore);

                switch (_SMRef.CurrentState)
                {
                    case InitialStatRollState:
                        //_SMRef._playerRoll = _diceTotalScore;
                        _SMRef.ChangeState<PlayerTurnGameState>();
                        break;
                    case EnemyTurnGameState:
                        _SMRef.SetEnemyRoll(_diceTotalScore);
                        _SMRef.ChangeState<EnemyTurnGameState>();
                        break;
                    case PlayerTurnGameState:
                        _SMRef.SetPlayerRoll(_diceTotalScore);
                        _SMRef.ChangeState<EnemyTurnGameState>();
                        break;
                }

                // Reset values
                _diceRolledList.Clear();
                _diceTotalScore = 0;
                _diceResolved = 0;
                _rolled = false;
            }
        }
    }

}
