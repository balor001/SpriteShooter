using UnityEngine;
using Boo.Lang;
using System;
using UnityEngine.UI;
using System.Runtime.Remoting.Messaging;

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
    public FrameArray[] frames;

    [SerializeField]
    public List<FrameArray> _list;

    [SerializeField]
    public Sprites sprites;
}

// Iniatilize List Array
[Serializable]
public class FrameArray : SpriteAnimationSheet
{


    public FrameArray(float FramesPerSecond)
    {
        framesPerSecond = FramesPerSecond;
    }
}

[Serializable]
public class Sprites : List<FrameArray>
{
    public Sprite spriteArray;
}