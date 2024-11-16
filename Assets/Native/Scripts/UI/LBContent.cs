using UnityEngine;

public class LBContent : MonoBehaviour
{
    [SerializeField] private LeaderboardItem _itemPrefab;
    private GameObject item;

    public void Fill(string name, string score, string rank)
    {
        _itemPrefab._playerName.text = name;
        _itemPrefab._playerScore.text = score;
        _itemPrefab._playerRank.text = rank;
        item = Instantiate(_itemPrefab.gameObject, transform.position, Quaternion.identity, transform);
    }
}
