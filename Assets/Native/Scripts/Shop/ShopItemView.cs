using UnityEngine;

public class ShopItemView : MonoBehaviour
{
  [SerializeField] private IntValueView _priceView;

  public ShopItem Item { get; private set; }
  public bool IsLock { get; private set; }

  public int Price => Item.Price;
  public GameObject Model => Item.Model;

  public void Initialize(ShopItem item)
  {
    Item = item;
    _priceView.Show(item.Price);
  }
}
