using UnityEngine;

public class CoinTaker : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Coin"))
    {
      Destroy(other.gameObject);
    }
  }
}
