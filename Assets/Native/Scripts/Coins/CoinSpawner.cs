using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
  public GameObject coin;
  void Awake()
  {
    RandomSpawn();
  }

  void RandomSpawn()
  {
    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Platform");
    foreach (GameObject gameObject in gameObjects)
    {
      int rand = Random.Range(0, 10);
      if (rand < 3)
      {
        Vector3 coinPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        Instantiate(coin, coinPosition, Quaternion.identity);
        // print($"{gameObject.name} {gameObject.transform.position}");
      }
    }
  }
}
