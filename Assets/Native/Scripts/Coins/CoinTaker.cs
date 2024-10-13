using UnityEngine;

public class CoinTaker : MonoBehaviour
{
  public CoinCounter _coinCounter;

  void Start()
  {
    // _coinCounter.PlayerPrefsCoinsSet(0);
    _coinCounter = GameObject.Find("Canvas").GetComponent<CoinCounter>();
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Coin"))
    {
      _coinCounter.coins++;
      _coinCounter.PlayerPrefsCoinsSet(_coinCounter.PlayerPrefsCoinsGet() + 1);
      other.gameObject.SetActive(false);
    }
  }
}
