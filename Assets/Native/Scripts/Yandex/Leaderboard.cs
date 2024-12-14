using System.Runtime.InteropServices;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void SetScoreLeaderboard(int record);

    [DllImport("__Internal")]
    public static extern void GetScoreLeaderboard();

    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private LBContent _content;
    [HideInInspector] public bool _isAuthtorization = false;
    [HideInInspector] public bool _isLeaderboardClear;

    private void Awake()
    {
        _isLeaderboardClear = true;
    }

    [System.Serializable]
    public class PlayerJson
    {
        public int rank;
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class PlayerJsonArray
    {
        public PlayerJson[] entries;
    }

    public void GetPlayers(string lbAnswer)
    {
        PlayerJsonArray playerArray = JsonUtility.FromJson<PlayerJsonArray>(lbAnswer);

        for (int i = 0; i < playerArray.entries.Length; i++)
        {
            _content.Fill(playerArray.entries[i].playerName.ToString(), playerArray.entries[i].score.ToString(), playerArray.entries[i].rank.ToString());
        }
    }

    public void SetPlayerScore(int record)
    {
        if (_isAuthtorization == true)
        {
            SetScoreLeaderboard(record);
        }
    }

    public void OpenLeaderboard()
    {
        if (_isLeaderboardClear)
        {
            GetScoreLeaderboard();
            _isLeaderboardClear = false;
        }
    }

    public void AuthGetScore()
    {
        GetScoreLeaderboard();
    }

    public void CloseLeaderboard()
    {
        _leaderboardPanel.SetActive(false);
    }
}
