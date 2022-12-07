using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventResultsController : MonoBehaviour
{

    [SerializeField] GameSM _SMRef;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] TextMeshProUGUI _rollResultText;
    [SerializeField] TextMeshProUGUI _eventResultText;
    [SerializeField] TextMeshProUGUI _statResultsText;
    [SerializeField] Image _eventResultImage;
    [SerializeField] Button _continueButton;

    [SerializeField] AudioClip _eventWinSFX;
    [SerializeField] AudioClip _eventFailSFX;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(() => ButtonClick());

       if (_SMRef.PlayerRoll >= _SMRef.EnemyRoll)
        {
            _rollResultText.text = ("Your Roll: [<color=green>" + _SMRef.PlayerRoll + "</color>] --- Event Roll: [<color=red>" + _SMRef.EnemyRoll + "</color>]");
            _eventResultText.text = _SMRef.EventChoice.WinText;
            _statResultsText.text = ("");
            _eventResultImage.sprite = _SMRef.EventChoice.WinChoiceImage;

            AudioManager.Instance.PlaySound2D(_eventWinSFX, .75f, UnityEngine.Random.Range(.8f, 1.2f));
        }
       else
        {
            _rollResultText.text = ("Your Roll: [<color=red>" + _SMRef.PlayerRoll + "</color>] --- Event Roll: [<color=green>" + _SMRef.EnemyRoll + "</color>]");
            _eventResultText.text = _SMRef.EventChoice.FailText;
            _statResultsText.text = ("");
            _eventResultImage.sprite = _SMRef.EventChoice.LoseChoiceImage;
            _SMRef.ChangePlayerHealth(-1);
            _healthText.text = ("Health: " + _SMRef.PlayerHealth);

            AudioManager.Instance.PlaySound2D(_eventFailSFX, .75f, UnityEngine.Random.Range(.8f, 1.2f));
        }
    }

    void ButtonClick()
    {
        _SMRef.OnStateExit();

        int mapPointCount = _SMRef.MapController.GetComponent<UILinePointsTest>().Images.Count;
        if (_SMRef.PlayerHealth > 0 && mapPointCount <= 0) _SMRef.ChangeState<WinState>();
        else if (_SMRef.PlayerHealth > 0 && mapPointCount > 0) _SMRef.ChangeState<MapState>();
        else _SMRef.ChangeState<LoseState>();

        _continueButton.onClick.RemoveAllListeners();
    }

}
