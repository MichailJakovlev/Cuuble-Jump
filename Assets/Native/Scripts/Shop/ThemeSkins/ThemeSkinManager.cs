using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSkinManager : MonoBehaviour
{
    public ThemeSkinDB skinDB;
    public TextMeshProUGUI priceTMP;
    private int selectedOption = 0;
    public Transform parent;

    public GameObject defaultUnlockButton;
    public GameObject reviewUnlockButton;
    public GameObject selectButton;
    public GameObject selectedItem;
    public Button coinsButton;
    public TMPMenuCoins _menuCoins;
    public MenuPlayer _menuPlayer;
    private List<string> unlockedSkins = new();
    private string selected;

    void Start()
    {
        PlayerPrefs.GetString("SkinSelected", "Cat");
        unlockedSkins = PlayerPrefs.GetString("UnlockedSkins").Split(',').ToList();
        unlockedSkins.Add(skinDB.skins[0].name.ToString());
        PlayerPrefs.GetString("UnlockedSkins", "Cat");
        SpawnSkins(selectedOption);
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
        ThemeSkin skin = skinDB.GetSkin(selectedOption);
        priceTMP.text = skin.price.ToString();
        SetActiveSkins(selectedOption);
        SetActiveButton(defaultUnlockButton, new GameObject[] { reviewUnlockButton, selectButton, selectedItem });
        IsUnlocked(skin);
        if (skin.isDefault)
        {
            SetActiveButton(selectButton, new GameObject[] { reviewUnlockButton, defaultUnlockButton, selectedItem });
            IsUnlocked(skin);
        }
        else if (skin.isReview)
        {
            SetActiveButton(reviewUnlockButton, new GameObject[] { selectButton, defaultUnlockButton, selectedItem });
            IsUnlocked(skin);
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
            GameObject spawnedSkin = Instantiate(skinDB.skins[i].skinModel[0]);
            spawnedSkin.transform.SetParent(parent, false);
            spawnedSkin.SetActive(false);
            spawnedSkin.layer = LayerMask.NameToLayer("Texture Renderer");
            spawnedSkin.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Texture Renderer");
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

    public void UnlockSkinCoins()
    {
        ThemeSkin skin = skinDB.GetSkin(selectedOption);
        unlockedSkins.Add(skin.name.ToString());
        string result = string.Join(", ", unlockedSkins);
        PlayerPrefs.SetString("UnlockedSkins", result);
        PlayerPrefs.Save();
        int skinPrice = skin.price;
        if (PlayerPrefs.GetInt("coins") >= skinPrice)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - skinPrice);
            _menuCoins.GetAllCoins();
        }
        IsUnlocked(skin);
    }
    public void UnlockSkinAd()
    {
        ThemeSkin skin = skinDB.GetSkin(selectedOption);
        unlockedSkins.Add(skin.name.ToString());
        string result = string.Join(", ", unlockedSkins);
        PlayerPrefs.SetString("UnlockedSkins", result);
        PlayerPrefs.Save();
        IsUnlocked(skin);
    }

    public void UnlockSkinReview()
    {
        ThemeSkin skin = skinDB.GetSkin(selectedOption);
        unlockedSkins.Add(skin.name.ToString());
        string result = string.Join(", ", unlockedSkins);
        PlayerPrefs.SetString("UnlockedSkins", result);
        PlayerPrefs.Save();
        IsUnlocked(skin);
    }

    private void IsUnlocked(ThemeSkin skin)
    {
        string unlocked = unlockedSkins.Find(m => m.Contains(skin.name.ToString()));

        if (PlayerPrefs.GetInt("coins") < skin.price)
        {
            coinsButton.interactable = false;
        }

        if (unlocked != null)
        {
            SetActiveButton(selectButton, new GameObject[] { selectedItem, reviewUnlockButton, defaultUnlockButton });
            IsSelected(skin);
        }
    }

    public void SelectSkin()
    {
        ThemeSkin skin = skinDB.GetSkin(selectedOption);
        PlayerPrefs.SetString("SkinSelected", skin.name.ToString());
        PlayerPrefs.Save();
        _menuPlayer.GetSkin();
        _menuPlayer.DestroySkin();
        IsSelected(skin);
    }
    private void IsSelected(ThemeSkin skin)
    {
        selected = PlayerPrefs.GetString("SkinSelected", "Cat");
        if (selected == skin.name.ToString())
        {
            SetActiveButton(selectedItem, new GameObject[] { reviewUnlockButton, defaultUnlockButton, selectButton });
        }
    }
}
