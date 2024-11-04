using System.Linq;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] private CharacterSkinDB skinDB;
    [SerializeField] private Transform _parent;

    void Start()
    {
        GetSkin();
    }

    public void GetSkin()
    {
        var skinModel = skinDB.skins.FirstOrDefault(m => m.name.ToString() == PlayerPrefs.GetString("SkinSelected", "Cat"))?.skinModel;
        GameObject spawnedSkin = Instantiate(skinModel, new Vector3(0,0,0), transform.rotation);
        spawnedSkin.layer = LayerMask.NameToLayer("UI");
        spawnedSkin.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
        spawnedSkin.transform.SetParent(_parent, false);
        spawnedSkin.transform.localScale = new Vector3(150,150,150);
        spawnedSkin.transform.Rotate(-7,-35,5);
    }

    public void DestroySkin()
    {
        Destroy(_parent.GetChild(0).gameObject);
    }

}
