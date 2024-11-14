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
            GetComponent<TextMeshProUGUI>().text = _ru;
        }

        // Turkish
        else if (Localization._instance._currentLanguage == "tr" || Localization._instance._currentLanguage == "tr-TR")
        {
            GetComponent<TextMeshProUGUI>().text = _tr;
        }

        // Arabic
        else if (Localization._instance._currentLanguage == "ar" || Localization._instance._currentLanguage == "ar-AR")
        {
            GetComponent<TextMeshProUGUI>().text = _ar;
        }

        // Japanese
        else if (Localization._instance._currentLanguage == "ja" || Localization._instance._currentLanguage == "ja-JA")
        {
            GetComponent<TextMeshProUGUI>().text = _ja;
        }

        // German
        else if (Localization._instance._currentLanguage == "de" || Localization._instance._currentLanguage == "de-DE")
        {
            GetComponent<TextMeshProUGUI>().text = _de;
        }

        // Spanish
        else if (Localization._instance._currentLanguage == "es" || Localization._instance._currentLanguage == "es-ES")
        {
            GetComponent<TextMeshProUGUI>().text = _es;
        }

        // English
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
    }
}
