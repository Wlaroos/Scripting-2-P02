using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName = "EventSO", menuName = "NewSOEvent")]
public class EventSO : ScriptableObject
{
    [SerializeField] string _eventName;

    [SerializeField] private List<ChoiceSO> _choices = new List<ChoiceSO>();

    public List<ChoiceSO> Choices { get => _choices; set => _choices = value; }


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
}
