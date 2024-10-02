using TMPro;
using UnityEngine;

public class TMPCoinsGameOver : MonoBehaviour
{
    public CoinCounter _coinCounter;
    [SerializeField] private TextMeshProUGUI _textCoins;

    void Awake()
    {
        _coinCounter = GameObject.Find("Canvas").GetComponent<CoinCounter>();
        _textCoins.text = $"+{_coinCounter.coins}";
    }
}
