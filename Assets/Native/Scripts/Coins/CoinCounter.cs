using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [HideInInspector]
    public int coins = 0;

    public void PlayerPrefsCoinsSet(int value)
    {
        PlayerPrefs.SetInt("coins", value);
    }

    public int PlayerPrefsCoinsGet()
    {
        return PlayerPrefs.GetInt("coins", 0);
    }
}
