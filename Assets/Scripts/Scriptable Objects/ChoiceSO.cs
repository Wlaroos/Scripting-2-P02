using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChoiceSO : ScriptableObject
{
    [SerializeField] private EventSO _myEvent;

    [SerializeField] private string _choiceName;
    [SerializeField] private string _buttonText;
    [TextArea(3, 10)]
    [SerializeField] private string _winText;
    [TextArea(3, 10)]
    [SerializeField] private string _failText;
    [SerializeField] private Sprite _choiceImage;
    [SerializeField] private int _statIndex;
    [SerializeField] private int _enemyDiceAmount;

    public EventSO MyEvent { get => _myEvent; set => _myEvent = value; }

    public string ChoiceName { get => _choiceName; }
    public string ButtonText { get => _buttonText; }
    public string WinText { get => _winText; }
    public string FailText { get => _failText; }
    public Sprite ChoiceImage { get => _choiceImage; }
    public int StatIndex { get => _statIndex; }
    public int EnemyDiceAmount { get => _enemyDiceAmount; }

#if UNITY_EDITOR
    public void Initialize(EventSO myEvent)
    {
        _myEvent = myEvent;
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Rename to name")]
    private void Rename()
    {
        this.name = _choiceName;
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Delete this")]
    private void DeleteThis()
    {
        _myEvent.Choices.Remove(this);
        Undo.DestroyObjectImmediate(this);
        AssetDatabase.SaveAssets();
    }
#endif


}
