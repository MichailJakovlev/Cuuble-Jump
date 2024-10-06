using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
  [field: SerializeField] public GameObject Model { get; private set; }
  [field: SerializeField, Range(50, 1000)] public int Price { get; private set; }
}
