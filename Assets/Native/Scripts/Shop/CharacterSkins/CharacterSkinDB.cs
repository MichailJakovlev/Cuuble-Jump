using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkinDB", menuName = "ScriptableObject/CharacterSkinDB")]
public class CharacterSkinDB : ScriptableObject
{
    public CharacterSkin[] skins;

    public int SkinCount
    {
        get
        {
            return skins.Length;
        }
    }

    public CharacterSkin GetSkin(int index)
    {
        return skins[index];
    }
}
