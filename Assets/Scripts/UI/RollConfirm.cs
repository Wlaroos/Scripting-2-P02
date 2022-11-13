using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollConfirm : MonoBehaviour
{

    [SerializeField] GameSM _SMRef;
    [SerializeField] TextMeshProUGUI _confirmText;
    [SerializeField] Button _confirmButton;

    private void OnEnable()
    {
        switch (_SMRef.CurrentState)
        {
            case InitialStatRollState:
                _confirmButton.onClick.AddListener(() => StatConfirm());
                _confirmText.text = "You Rolled: "
                    + "[<color=#FFA600>" + _SMRef.PlayerStats[0] + "</color>]" 
                    + "[<color=#FFFF00>" + _SMRef.PlayerStats[1] + "</color>]"
                    + "[<color=#FF0000>" + _SMRef.PlayerStats[2] + "</color>]"
                    + "[<color=#00FFFF>" + _SMRef.PlayerStats[3] + "</color>]"
                    + "[<color=#991AFF>" + _SMRef.PlayerStats[4] + "</color>]";
                break;
            case EnemyTurnGameState:
                _confirmButton.onClick.AddListener(() => EnemyConfirm());
                _confirmText.text = "Event Rolled: [" + _SMRef.EnemyRoll + "]";
                break;
            case PlayerTurnGameState:
                _confirmButton.onClick.AddListener(() => PlayerConfirm());
                _confirmText.text = "You Rolled: [" + _SMRef.PlayerRoll + "]";
                break;
        }
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveAllListeners();
    }

    void StatConfirm()
    {
        _SMRef.OnStateExit();
        _SMRef.ChangeState<MapState>();
        gameObject.SetActive(false);
    }

    void PlayerConfirm()
    {
        _SMRef.OnStateExit();
        _SMRef.ChangeState<EnemyTurnGameState>();
        gameObject.SetActive(false);
    }

    void EnemyConfirm()
    {
        _SMRef.OnStateExit();
        _SMRef.ChangeState<EventResultsState>();
        gameObject.SetActive(false);
    }

}
