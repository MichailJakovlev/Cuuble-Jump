using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
  [SerializeField] private ScoreCounter _scoreCounter;

  void Start()
  {
    PlayerPrefs.GetInt("Score", 0);
  }

  public void Save()
  {
    PlayerPrefs.SetInt("Score", _scoreCounter.score);
    PlayerPrefs.Save();
  }
}
