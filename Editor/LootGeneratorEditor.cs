using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(LootGenerator))]
public class LootGeneratorEditor : Editor
{
    private SerializedProperty _maxTier;
    private SerializedProperty _ruleData;
    private SerializedProperty _generateRandom;

    private ReorderableList _list;

    private void OnEnable()
    {
        _maxTier = serializedObject.FindProperty("_maxTier");
        _ruleData = serializedObject.FindProperty("_ruleData");
        _generateRandom = serializedObject.FindProperty("_generateRandom");
        _list = new ReorderableList(serializedObject, _ruleData, true, false, false, true);

        _list.drawElementCallback = DrawListItems;
    }

    public override void OnInspectorGUI()
    {
        LootGenerator myTarget = (LootGenerator)target;

        EditorGUILayout.LabelField("Basic Properties:");
        EditorGUILayout.PropertyField(_maxTier);
        EditorGUILayout.PropertyField(_generateRandom);
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Rule")) myTarget.AddRuleData();

        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty type = element.FindPropertyRelative("Type");
        SerializedProperty item = element.FindPropertyRelative("Item");
        SerializedProperty pool = element.FindPropertyRelative("Pool");
        SerializedProperty amount = element.FindPropertyRelative("Amount");

        ShowProperty(type, rect, 0, 100);

        if (!CompareType(type, RuleType.Random))
        {
            ShowLabel("Item", rect, 120, 40);
            ShowProperty(item, rect, 160, 230);
        }

        if (CompareType(type, RuleType.Random))
        {
            ShowProperty(pool, rect, 120, 100);
        }

        if (CompareType(type, RuleType.Amount))
        {
            ShowLabel("Amount", rect, 400, 60);
            ShowProperty(amount, rect, 450, 30);
        }
    }

    private bool CompareType(SerializedProperty comparableType, RuleType statedType)
    {
        return (RuleType)comparableType.intValue == statedType;
    }

    private void ShowProperty(SerializedProperty property, Rect rect, float margin, float width)
    {
        EditorGUI.PropertyField(
            new Rect(rect.x + margin, rect.y, width, EditorGUIUtility.singleLineHeight),
            property,
            GUIContent.none
        );
    }

    private void ShowLabel(string label, Rect rect, float margin, float width)
    {
        EditorGUI.LabelField(new Rect(rect.x + margin, rect.y, width, EditorGUIUtility.singleLineHeight), label);
    }
}
