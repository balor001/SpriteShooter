//using UnityEngine;
//using UnityEditor;
//using UnityEditorInternal;
//
//[CustomEditor(typeof(SpriteAnimationSprites))]
//public class ListEditor : Editor
//{
//    
//    // Iniatilize ReordableList
//    private ReorderableList list;
//
//    private void OnEnable()
//    {
//        // Create new Reordablelist
//        list = new ReorderableList(serializedObject, serializedObject.FindProperty("_frames"), false, true, true, true);
//
//        // Draw the reordablelist
//        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
//        {
//            var element = list.serializedProperty.GetArrayElementAtIndex(index);
//            rect.y += 2;
//            EditorGUI.LabelField(rect, "Frame #" + (index + 1f));
//            rect.x += 65;
//            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("frame"), GUIContent.none);
//        };
//
//        // Header of the list
//        list.drawHeaderCallback = (Rect rect) =>
//        {
//            EditorGUI.LabelField(rect, "Frames");
//        };
//
//    }
//
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI(); // DISPLAYS OTHER PROPERTIES ON THE LIST
//        serializedObject.Update();
//        list.DoLayoutList();
//        serializedObject.ApplyModifiedProperties();
//
//    }
//}
//

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(SpriteAnimationSheet))]
public class ListEditor : Editor
{
    ReorderableList list;
    SpriteAnimationSheet spriteAnimationSheet;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("frames"), true, true, true, true);

        if (target == null)
        {
            return;
        }
        spriteAnimationSheet = (SpriteAnimationSheet)target;

        // Draw the reordablelist
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.LabelField(rect, "Sprite #" + (index + 1f));
            rect.x += 65;
            EditorGUI.LabelField(rect, "Len: " + (0));
        };

        // Header of the list
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Frames");
        };

        // When frame is selected
        list.onSelectCallback = list =>
        {

        };
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //base.OnInspectorGUI();
        spriteAnimationSheet = (SpriteAnimationSheet)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("sheetName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("animType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("animMode"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("framesPerSecond"));
        list.DoLayoutList();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));

        switch (spriteAnimationSheet.animType)
        {
            case SpriteAnimationSheet.AnimType.Eight_Directions:

                break;
            case SpriteAnimationSheet.AnimType.Four_Directions:
                
                break;
            case SpriteAnimationSheet.AnimType.One_Direction:
                
                break;
            default:
                break;
        }



        switch (spriteAnimationSheet.animMode)
        {
            case SpriteAnimationSheet.AnimMode.Loop:

                break;
            case SpriteAnimationSheet.AnimMode.Once:

                break;
            default:
                break;
        }



        serializedObject.ApplyModifiedProperties();

    }


}