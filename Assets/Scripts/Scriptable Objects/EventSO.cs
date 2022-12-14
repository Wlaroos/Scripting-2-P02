using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName = "EventSO", menuName = "NewSOEvent")]
public class EventSO : ScriptableObject
{
    [SerializeField] string _eventName;
    public string EventName { get => _eventName; }

    [SerializeField] private List<ChoiceSO> _choices = new List<ChoiceSO>();

    public List<ChoiceSO> Choices { get => _choices; set => _choices = value; }

    [SerializeField] private Sprite _eventImage;
    public Sprite EventImage { get => _eventImage; }

    // Creates a new choice scriptable object as a child
#if UNITY_EDITOR
    [ContextMenu("Make New")]
    private void MakeNewChoice()
    {
        ChoiceSO choice = ScriptableObject.CreateInstance<ChoiceSO>();
        choice.name = "New Choice";
        choice.Initialize(this);
        _choices.Add(choice);

        AssetDatabase.AddObjectToAsset(choice, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(choice);
    }

#endif

    // Delete all button, removes all sub choices
#if UNITY_EDITOR
    [ContextMenu("Delete All")]
    private void DeleteAll()
    {
        for (int i = _choices.Count; i-- > 0;)
        {
            ChoiceSO tmp = _choices[i];

            _choices.Remove(tmp);
            Undo.DestroyObjectImmediate(tmp);
        }
        AssetDatabase.SaveAssets();
    }

#endif

    // Sets _eventName to file name
#if UNITY_EDITOR
    private void OnValidate()
    {
        _eventName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
    }

#endif
}
