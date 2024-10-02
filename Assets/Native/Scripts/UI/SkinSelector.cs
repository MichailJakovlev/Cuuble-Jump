using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
  [SerializeField] private Button previousButton;
  [SerializeField] private Button nextButton;
  private int currentSkin;

  void Start()
  {
    Change(0);
  }

  private void Awake()
  {
    Select(0);
  }

  private void Select(int _index)
  {
    previousButton.interactable = (_index != 0);
    nextButton.interactable = (_index != transform.childCount - 1);
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(i == _index);
    }
  }

  public void Change(int _change)
  {
    currentSkin += _change;
    Select(currentSkin);
  }
}
