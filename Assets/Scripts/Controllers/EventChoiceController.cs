using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventChoiceController : MonoBehaviour
{
    [SerializeField] GameSM _SMRef;

    [SerializeField] TextMeshProUGUI _eventTitleText;
    [SerializeField] GameObject _choiceUIPrefab;

    EventSO[] _allEvents;
    EventSO _currentEvent;

    private GameObject _hlgRef;

    private void Awake()
    {
        _allEvents = Resources.LoadAll<EventSO>("EventSO/");
        _hlgRef = transform.GetChild(1).GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        PickEvent();
        
    }

    private void OnDisable()
    {
        foreach (Transform child in _hlgRef.transform)
        {
            Destroy(child.gameObject);
        }

        _eventTitleText.text = "";
        _currentEvent = null;
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
        for (int i = amount - 1; i >= 0; i--)
        {
            // Populate the prefab with values from the scriptable object
            GameObject choice = Instantiate(_choiceUIPrefab, this.transform);
            choice.transform.SetParent(_hlgRef.transform);
            choice.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentEvent.Choices[i].ChoiceName;
            choice.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentEvent.Choices[i].ButtonText;
            choice.transform.GetChild(2).GetComponent<Image>().sprite = _currentEvent.Choices[i].ChoiceImage;

            if (_currentEvent.Choices[i].EnemyDiceAmount >= 1)
                choice.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
            if (_currentEvent.Choices[i].EnemyDiceAmount >= 3)
                choice.transform.GetChild(3).GetChild(4).gameObject.SetActive(true);
            if (_currentEvent.Choices[i].EnemyDiceAmount >= 5)
                choice.transform.GetChild(3).GetChild(5).gameObject.SetActive(true);

            // Allows me to hold onto the choice and button refs
            Button button = choice.transform.GetChild(1).GetComponent<Button>();
            int j = i;
            button.onClick.AddListener(() => ButtonClick(button, _currentEvent.Choices[j]));
        }
    }

    void ButtonClick(Button button, ChoiceSO choice)
    {
        _SMRef.SetEventChoice(choice);
        _SMRef.OnStateExit();
        _SMRef.ChangeState<EnemyTurnGameState>();
        button.onClick.RemoveAllListeners();
    }

}
