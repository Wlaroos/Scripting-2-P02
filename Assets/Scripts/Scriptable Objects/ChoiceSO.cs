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
    [TextArea(3, 10)]
    [SerializeField] private string _choiceText;
    [SerializeField] private Sprite _choiceImage;
    [SerializeField] private int _statIndex;

    public EventSO MyEvent { get => _myEvent; set => _myEvent = value; }

    public string ChoiceName { get => _choiceName; }
    public string ChoiceText { get => _choiceText; }
    public Sprite ChoiceImage { get => _choiceImage; }
    public int StatIndex { get => _statIndex; }

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