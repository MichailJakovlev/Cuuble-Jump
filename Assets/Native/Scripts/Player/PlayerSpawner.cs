using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private CharacterSkinDB skinDB;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameState _gameState;

    void Start()
    {
        _gameState.StartGame();
        var skinModel = skinDB.skins.FirstOrDefault(m => m.name.ToString() == PlayerPrefs.GetString("SkinSelected", "Cat"))?.skinModel;
        GameObject spawnedSkin = Instantiate(skinModel, new Vector3(0,0,0), transform.rotation);
        spawnedSkin.layer = LayerMask.NameToLayer("Default");
        spawnedSkin.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        spawnedSkin.transform.SetParent(_parent, false);
    }

}
