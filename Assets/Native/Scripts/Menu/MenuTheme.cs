using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuTheme : MonoBehaviour
{
    [SerializeField] private ThemeSkinDB skinDB;
    [SerializeField] private Transform _parent;
    [SerializeField] private List<GameObject> _platforms;
    private List<GameObject> copySkins;

    void Start()
    {
        GetSkin();
    }

    public void GetSkin()
    {
        var skinModels = skinDB.skins.FirstOrDefault(m => m.name.ToString() == PlayerPrefs.GetString("ThemeSelected", "Forest"))?.skinModel;
        copySkins = skinModels.ToList();
        GameObject spawnedSkin;
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            if (i > 0) {
                var randomSkin = copySkins[Random.Range(1, copySkins.Count)];
                spawnedSkin = Instantiate(randomSkin, new Vector3(0,0,0), transform.rotation);
                copySkins.Remove(randomSkin);
            }
            else
            {
                spawnedSkin = Instantiate(skinModels[i], new Vector3(0,0,0), transform.rotation);
            }
            spawnedSkin.layer = LayerMask.NameToLayer("UI");
            spawnedSkin.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
            spawnedSkin.transform.SetParent(_platforms[i].transform, false);
            spawnedSkin.transform.localScale = new Vector3(150,150,150);
            spawnedSkin.transform.Rotate(-7,-35,5);

        }
    }
    public void DestroySkin()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            Destroy(_platforms[i].transform.GetChild(0).gameObject);
        }
    }
}
