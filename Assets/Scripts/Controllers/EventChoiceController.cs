using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventChoiceController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _eventTitleText;
    [SerializeField] GameObject _choiceUIPrefab;

    EventSO[] _allEvents;
    EventSO _currentEvent;

    private GameObject _hlgRef;

    private void Awake()
    {
        _allEvents = Resources.LoadAll<EventSO>("EventSO/");
        _hlgRef = transform.GetChild(0).GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        PickEvent();
        
    }

    private void OnDisable()
    {

    }

    private void PickEvent()
    {
        int index = Random.Range(0, _allEvents.Length);
        _currentEvent = _allEvents[index];

        _eventTitleText.text = _currentEvent.EventName;
        AddChoices(_currentEvent.Choices.Count);
    }

    private void AddChoices(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject test = Instantiate(_choiceUIPrefab, this.transform);
            test.transform.SetParent(_hlgRef.transform);
        }
    }

}
