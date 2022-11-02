using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{

    [SerializeField] Dice _diceRef;
    [SerializeField] GameSM _SMRef;
    [SerializeField] Vector3 _throwPosition = new Vector3(0, -4, 3.5f);

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
                //diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", UnityEngine.Random.ColorHSV());
                diceRef.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.cyan);
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
                // Set Player Roll in GameSM script (May be a better way to do this?)

                _SMRef._playerRoll = _diceTotalScore;
                _SMRef.ChangeState<EnemyTurnGameState>();

                // Reset values
                _diceRolledList.Clear();
                _diceTotalScore = 0;
                _diceResolved = 0;
                _rolled = false;
            }
        }
    }

}
