using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    // [DllImport("__Internal")]
    // public static extern void SetScoreLeaderboard(int record);

    // [DllImport("__Internal")]
    // public static extern void GetScoreLeaderboard();

    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;

    [System.Serializable]
    public class TestPlayer
    {
        public string name;
        public int score;
    }

    TestPlayer player = new TestPlayer() { name = "Michail Jakovlev", score = 35};
   
    public void OpenLeaderboard()
    {
        _leaderboardPanel.SetActive(true);
        string playerJson = JsonUtility.ToJson(player);
        Debug.Log(playerJson);

        TestPlayer deserialize = JsonUtility.FromJson<TestPlayer>(playerJson);
        _name.text = deserialize.name;
        _score.text = deserialize.score.ToString();

    }

    public void CloseLeaderboard()
    {
        _leaderboardPanel.SetActive(false);
    }

    public void SetPlayerScore(int record)
    {
       // SetScoreLeaderboard(record);
    }
}
