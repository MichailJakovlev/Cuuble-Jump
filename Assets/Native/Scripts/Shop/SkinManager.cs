using TMPro;
using UnityEditor;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public SkinDataBase skinDB;
    public TextMeshProUGUI priceTMP;
    private int selectedOption = 0;
    public Transform parent;

    public GameObject defaultUnlockButton;
    public GameObject reviewUnlockButton;
    public GameObject selectButton;

    void Start()
    {
        UpdateSkin(selectedOption);
        SpawnSkins(selectedOption);
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
        SetActiveSkins(selectedOption);
        SetActiveButton(defaultUnlockButton, new GameObject[] { reviewUnlockButton, selectButton });
        if (skin.isDefault)
        {
            SetActiveButton(selectButton, new GameObject[] { reviewUnlockButton, defaultUnlockButton });
        }
        if (skin.isReview)
        {
            SetActiveButton(reviewUnlockButton, new GameObject[] { selectButton, defaultUnlockButton });
        }
    }

    private void SetActiveSkins(int selectedOption)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(false);
            if (selectedOption == i)
            {
                parent.GetChild(i).gameObject.SetActive(true);
            }

        }
    }

    private void SpawnSkins(int selectedOption)
    {
        for (int i = 0; i < skinDB.SkinCount; i++)
        {
            GameObject spawnedSkin = Instantiate(skinDB.skins[i].skinModel,
                new Vector3(-2.29999995f,-25.8999996f,-35.0999985f),
                new Quaternion(-0.0558543317f,-0.33958146f,0.0260453075f,0.938555539f));

            spawnedSkin.transform.SetParent(parent, false);
            spawnedSkin.transform.localScale = new Vector3(40,40,40);
            spawnedSkin.SetActive(false);
            if (selectedOption == i)
            {
                spawnedSkin.SetActive(true);
            }
        }
    }

    private void SetActiveButton(GameObject activeButton, GameObject[] disableButtons)
    {
        activeButton.SetActive(true);
        foreach (GameObject disableButton in disableButtons)
        {
            disableButton.SetActive(false);
        }
    }
}
