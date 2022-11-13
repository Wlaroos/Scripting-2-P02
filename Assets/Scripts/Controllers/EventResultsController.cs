using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventResultsController : MonoBehaviour
{

    [SerializeField] GameSM _SMRef;
    [SerializeField] TextMeshProUGUI _rollResultText;
    [SerializeField] TextMeshProUGUI _eventResultText;
    [SerializeField] TextMeshProUGUI _statResultsText;
    [SerializeField] Image _eventResultImage;
    [SerializeField] Button _continueButton;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(() => ButtonClick());

       if (_SMRef.PlayerRoll > _SMRef.EnemyRoll)
        {
            _rollResultText.text = ("Your Roll: [<color=green>" + _SMRef.PlayerRoll + "</color>] --- Event Roll: [<color=red>" + _SMRef.EnemyRoll + "</color>]");
            _eventResultText.text = _SMRef.EventChoice.WinText;
            _statResultsText.text = ("");
            _eventResultImage.sprite = null;
        }
       else
        {
            _rollResultText.text = ("Your Roll: [<color=red>" + _SMRef.PlayerRoll + "</color>] --- Event Roll: [<color=green>" + _SMRef.EnemyRoll + "</color>]");
            _eventResultText.text = _SMRef.EventChoice.FailText;
            _statResultsText.text = ("");
            _eventResultImage.sprite = null;
        }
    }

    void ButtonClick()
    {
        _SMRef.OnStateExit();
        _SMRef.ChangeState<MapState>();
        _continueButton.onClick.RemoveAllListeners();
    }

}
