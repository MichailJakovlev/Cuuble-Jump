using TMPro;
using UnityEngine;

public class TextLocalization : MonoBehaviour
{
    [SerializeField] private string _ru;
    [SerializeField] private string _en;
    [SerializeField] private string _tr;
    [SerializeField] private string _ar;
    [SerializeField] private string _ja;
    [SerializeField] private string _de;
    [SerializeField] private string _es;

    [SerializeField] private TMP_FontAsset _arFont;
    [SerializeField] private TMP_FontAsset _jaFont;

    private void Start()
    {
        // Russian
        if (
            Localization._instance._currentLanguage == "ru"
            || Localization._instance._currentLanguage == "ru-RU"
            || Localization._instance._currentLanguage == "ru-KG"
            || Localization._instance._currentLanguage == "ru-KZ"
            || Localization._instance._currentLanguage == "ru-MD"
            || Localization._instance._currentLanguage == "ru-BY"
            || Localization._instance._currentLanguage == "ru-UA"
        )
        {
            GetComponent<TextMeshProUGUI>().text = _ru + GetComponent<TextMeshProUGUI>().text;
        }

        // Turkish
        else if (Localization._instance._currentLanguage == "tr" || Localization._instance._currentLanguage == "tr-TR")
        {
            GetComponent<TextMeshProUGUI>().text = _tr + GetComponent<TextMeshProUGUI>().text;
        }

        // Arabic
        else if (Localization._instance._currentLanguage == "ar" || Localization._instance._currentLanguage == "ar-AR")
        {
            GetComponent<TextMeshProUGUI>().font = _arFont;
            GetComponent<TextMeshProUGUI>().text = _ar + GetComponent<TextMeshProUGUI>().text;
        }

        // Japanese
        else if (Localization._instance._currentLanguage == "ja" || Localization._instance._currentLanguage == "ja-JA")
        {
            GetComponent<TextMeshProUGUI>().font = _jaFont;
            GetComponent<TextMeshProUGUI>().text = _ja + GetComponent<TextMeshProUGUI>().text;
        }

        // German
        else if (Localization._instance._currentLanguage == "de" || Localization._instance._currentLanguage == "de-DE")
        {
            GetComponent<TextMeshProUGUI>().text = _de + GetComponent<TextMeshProUGUI>().text;
        }

        // Spanish
        else if (Localization._instance._currentLanguage == "es" || Localization._instance._currentLanguage == "es-ES")
        {
            GetComponent<TextMeshProUGUI>().text = _es + GetComponent<TextMeshProUGUI>().text;
        }

        // English
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en + GetComponent<TextMeshProUGUI>().text;
        }
    }
}
