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
using UnityEditor.UIElements;
using System;

[CustomEditor(typeof(SpriteAnimationSheet))]
public class ListEditor : Editor
{
    ReorderableList list;
    SpriteAnimationSheet spriteAnimationSheet;

    SerializedProperty sheetNameProperty;
    SerializedProperty animTypeProperty;
    SerializedProperty animModeProperty;
    SerializedProperty framesPerSecondProperty;
    SerializedProperty spriteArrayProperty;

    Sprites GetSprites;
    Sprite[] savedArray;
    FrameArray GetFrameArrays;


    int indexSize = 0;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("frames"), true, true, true, true);
        
        spriteAnimationSheet = (SpriteAnimationSheet)target;


        //for (int i = 0; i < 8; i++)
        //{
        //    spriteAnimationSheet.sprites[i].SpriteArray = new Sprite[8];
        //}




        sheetNameProperty = serializedObject.FindProperty("sheetName");
        animTypeProperty = serializedObject.FindProperty("animType");
        animModeProperty = serializedObject.FindProperty("animMode");
        framesPerSecondProperty = serializedObject.FindProperty("framesPerSecond");
        spriteArrayProperty = serializedObject.FindProperty("sprites");

        // test = serializedObject.FindProperty("sprites");

        // Get Frame and Sprite Arrays

        //spriteArray = GetSprites.spriteArray;

        if (target == null)
        {
            return;
        }



        // Draw the reordablelist
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.LabelField(rect, "Sprite #" + (index + 1f));
            rect.x += 65;
            EditorGUI.LabelField(rect, "Len: " + (spriteAnimationSheet.framesPerSecond / list.count));

            //Debug.Log("SpriteArray: " + spriteArray + index);

        };

        // Header of the list
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Frames");
        };

        // When frame is selected
        list.onSelectCallback = (ReorderableList l) =>
        {

        };


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        //base.OnInspectorGUI();
        spriteAnimationSheet = (SpriteAnimationSheet)target;

        EditorGUILayout.PropertyField(sheetNameProperty, new GUIContent("Name Field"));
        EditorGUILayout.PropertyField(animTypeProperty, new GUIContent("Animation Type"));
        EditorGUILayout.PropertyField(animModeProperty, new GUIContent("Animation Mode"));
        EditorGUILayout.PropertyField(framesPerSecondProperty, new GUIContent("FPS"));
        list.DoLayoutList();
        //Debug.Log(frameArray[1].sprite);

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.ObjectField(GetSprites.SpriteObject, typeof(Sprite), true);
        //EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(spriteArrayProperty, new GUIContent("Sprites"));



        //GetSprites.SpriteArray = new Sprite[list.count];


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