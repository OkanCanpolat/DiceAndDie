using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupriseCardContent : ScriptableObject
{
    [TextArea]
    public string description;
    public Sprite contentSprite;
    public AudioClip OnFlipClip;
    public AudioClip OnClickClip;
    public abstract void Action();
}
