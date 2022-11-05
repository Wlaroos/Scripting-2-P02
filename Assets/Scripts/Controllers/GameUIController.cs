using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUIController : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI _stateMarkerTextUI = null;
    [SerializeField] public TextMeshProUGUI _playerStatTextUI = null;
    public string[] _statString = new string[] {"[]", "[]", "[]", "[]", "[]" };
    [SerializeField] TextMeshProUGUI _enemyThinkingTextUI = null;

    private void OnEnable()
    {
        EnemyTurnGameState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnGameState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void Start()
    {
        // make sure text is disabled on start
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    private void OnEnemyTurnBegan()
    {
        _enemyThinkingTextUI.gameObject.SetActive(true);
    }

    private void OnEnemyTurnEnded()
    {
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    public void StatChange()
    {
        _playerStatTextUI.text = "";
        for (int i = 0; i < _statString.Length; i++)
        {
            _playerStatTextUI.text += _statString[i] + "\n";
        }
    }

}
