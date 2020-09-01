using UnityEngine;
using Boo.Lang;
using System;

[CreateAssetMenu(fileName = "SpriteAnimationSheet", menuName = "Sprite Animation")]
public class SpriteAnimationSheet : ScriptableObject
{

    public string sheetName;

    public enum AnimType { Eight_Directions, Four_Directions, One_Direction };
    public AnimType animType;

    public enum AnimMode { Loop, Once };
    public AnimMode animMode;

    [Range(1.0f, 144f)]
    public float framesPerSecond = 4f;

    // Iniatilize List Array
    [SerializeField]
    private FrameArray[] frames;

    [SerializeField]
    private List<FrameArray> _list;

    public Sprite sprite;

}

// Iniatilize List Array
[System.Serializable]
public class FrameArray
{
    
}
