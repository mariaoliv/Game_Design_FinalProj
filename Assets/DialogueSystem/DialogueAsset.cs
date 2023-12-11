using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    [TextArea]
    //public int      length;     // Number of lines
    public string[] lines;      // The line being spoken
    public string[] speakers;   // Who is speaking
    public string[] emotion;    // Emotion for sprite
    public float[]  speed;      // How fast the dialogue progresses
}
