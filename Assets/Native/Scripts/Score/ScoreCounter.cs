using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
  [HideInInspector] public int score = 0;
  [SerializeField] private TextMeshProUGUI _scoreText;

  public void Count()
  {
    score++;
    _scoreText.text = score.ToString();
  }
}
