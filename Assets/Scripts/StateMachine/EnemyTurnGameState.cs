using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyTurnGameState : GameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;

    private int _diceAmount = 2;

    public override void Enter()
    {
        StateMachine.UIController._stateMarkerTextUI.text = "[Enemy Turn State]";
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Tick()
    {
        //
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        StateMachine.DiceController.StartRoll(_diceAmount);
        EnemyTurnEnded?.Invoke();
    }

    public void EnemyDiceResolved()
    {
        // Turn Over
        EnemyTurnEnded?.Invoke();
        StateMachine.ChangeState<PlayerTurnGameState>();
    }

}
