using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern string GetLang();

    [SerializeField] TextMeshProUGUI _languageText;
    [HideInInspector] public string _currentLanguage;

    public static Localization _instance;

    private void Awake()
    {
        // if (_instance == null)
        // {
        //     _instance = this;
        //     DontDestroyOnLoad(gameObject);

        //     _currentLanguage = GetLang();
        //     _languageText.text = _currentLanguage;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }
}
