using TMPro;
using UnityEngine;

public class TMPMenuCoins : MonoBehaviour
{
    public CoinCounter _coinCounter;

    [SerializeField]
    private TextMeshProUGUI _allCoinsTMP;

    [SerializeField]
    private TextMeshProUGUI _allCoinsShopTMP;

    void Start()
    {
        _coinCounter = GameObject.Find("Canvas").GetComponent<CoinCounter>();
        GetAllCoins();
    }

    public void GetAllCoins()
    {
        _allCoinsTMP.text = _coinCounter.PlayerPrefsCoinsGet().ToString();
        _allCoinsShopTMP.text = _coinCounter.PlayerPrefsCoinsGet().ToString();
    }
}
