using UnityEngine;
using UnityEditor;
using Boo.Lang;
using UnityEditorInternal;

[CreateAssetMenu(fileName = "SpriteAnimation", menuName = "SpriteAnimation", order = 999)]
public class SpriteAnimationEditor : ScriptableObject
{
    public string animationName;

    public enum AnimType { Eight_Directions, Four_Directions };
    public AnimType animType;

    public enum AnimMode { Loop, Once };
    public AnimMode animMode;

    [Range(1.0f, 144f)]
    public float fps = 4f;

    // Iniatilize List Array
    [SerializeField]
    private GOArray[] _sprites;

    [SerializeField]
    private List<GOArray> _list;


}

// Iniatilize List Array
[System.Serializable]
public class GOArray
{
    public Sprite sprite;
}

[CustomEditor(typeof(SpriteAnimationEditor))]
public class ListEditor : Editor
{
    // Iniatilize ReordableList
    private ReorderableList list;

    private void OnEnable()
    {
        // Create new Reordablelist
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("_sprites"), false, true, true, true);

        // Draw the reordablelist
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.LabelField(rect, "Sprite #" + (index + 1f));
            rect.x += 65;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("sprite"), GUIContent.none);
        };

        // Header of the list
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Frames");
        };


    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // DISPLAYS OTHER PROPERTIES ON THE LIST
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }


}