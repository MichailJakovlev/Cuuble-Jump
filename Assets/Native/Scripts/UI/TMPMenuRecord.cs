using TMPro;
using UnityEngine;

public class TMPMenuRecord : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _recordTMP;

  private void Start()
  {
    _recordTMP.text = PlayerPrefs.GetInt("Record", 0).ToString();
  }
}
