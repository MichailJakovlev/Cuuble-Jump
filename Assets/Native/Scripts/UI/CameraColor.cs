using System.Collections;
using UnityEngine;

public class CameraColor : MonoBehaviour
{
    private Camera _camera;
    float _colorHue = 0;

    private void Awake()
    {   
        _camera =  gameObject.GetComponent<Camera>();
        StartCoroutine(HighColor());
    }

    IEnumerator HighColor()
    {
        for (int i = 0; i < 1000; i++)
        {
            _colorHue = (float)i / 1000;
            _camera.backgroundColor = Color.HSVToRGB(_colorHue, 0.4f, 0.35f);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(DownColor());
    }

    IEnumerator DownColor()
    {
        for(int i = 1000; i > 0; i--)
        {
            _colorHue = (float)i / 1000;
            _camera.backgroundColor = Color.HSVToRGB(_colorHue, 0.4f, 0.35f);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(HighColor());
    }
}
