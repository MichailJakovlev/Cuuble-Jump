using TMPro;
using UnityEditor;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public SkinDataBase skinDB;
    public TextMeshProUGUI priceTMP;
    public GameObject skinModel;
    private int selectedOption = 0;
    void Start()
    {
        UpdateSkin(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= skinDB.SkinCount)
        {
            selectedOption = 0;
        }

        UpdateSkin(selectedOption);
    }

    public void PreviousOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = skinDB.SkinCount - 1;
        }

        UpdateSkin(selectedOption);
    }

    private void UpdateSkin(int selectedOption)
    {
        Skin skin = skinDB.GetSkin(selectedOption);
        priceTMP.text = skin.price.ToString();
        // skinModel.transform = PrefabUtility.RecordPrefabInstancePropertyModifications(skin.skinModel);
        if (skinModel != null && skin.skinModel != null)
        {
            // PrefabUtility.ReplacePrefab(skinModel, skin.skinModel, ReplacePrefabOptions.ConnectToPrefab);
        }
    }
}
