using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataBase", menuName = "ScriptableObject/SkinDataBase")]
public class SkinDataBase : ScriptableObject
{
    public Skin[] skins;

    public int SkinCount
    {
        get
        {
            return skins.Length;
        }
    }

    public Skin GetSkin(int index)
    {
        return skins[index];
    }
}
