using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private SkinDataBase skinDB;
    [SerializeField] private Transform _parent;

    void Start()
    {
        var skinModel = skinDB.skins.FirstOrDefault(m => m.name.ToString() == PlayerPrefs.GetString("SkinSelected"))?.skinModel;
        GameObject spawnedSkin = Instantiate(skinModel, new Vector3(0,0,0), transform.rotation);
        spawnedSkin.layer = LayerMask.NameToLayer("Default");
        spawnedSkin.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        spawnedSkin.transform.SetParent(_parent, false);

    }

}
