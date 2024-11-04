using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThemeSkinDB", menuName = "ScriptableObject/ThemeSkinDB")]
public class ThemeSkinDB : ScriptableObject
{
    public ThemeSkin[] skins;

    public int SkinCount
    {
        get
        {
            return skins.Length;
        }
    }

    public ThemeSkin GetSkin(int index)
    {
        return skins[index];
    }
}
