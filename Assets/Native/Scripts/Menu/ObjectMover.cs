using System;
using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
  public RectTransform _canvas;
  public RectTransform _skinView;
  public GameObject _shopUI;
  [SerializeField] float _speed = 10f;

  float currentTmie;

  Vector3 startPosition;
  void Start()
  {
    startPosition = transform.localPosition;
  }

  // private void OnRectTransformDimensionsChange()
  // {
  //   MoveOutOfCanvas();
  // }

  public void MoveOutOfCanvas()
  {
    Vector3 targetPosition = new (_skinView.localPosition.x, _canvas.rect.height + _skinView.rect.height * 2, _skinView.localPosition.z);
    StartCoroutine(MoveToTarget(_skinView, targetPosition));
    _shopUI.SetActive(true);
    StartCoroutine(SmoothAlpha(_shopUI));
  }

  public void MoveToCanvas()
  {
    _skinView.gameObject.SetActive(true);
    StartCoroutine(MoveToTarget(_skinView, new Vector3(
      _skinView.localPosition.x,
      startPosition.y,
      _skinView.localPosition.z)));
  }

  private IEnumerator MoveToTarget(RectTransform _object, Vector3 _targetPosition)
  {
    currentTmie = 1;
    while (_object.localPosition != _targetPosition)
    {
      currentTmie += (float)Math.Sqrt(Time.deltaTime);
      _object.localPosition = Vector3.MoveTowards(_object.localPosition, _targetPosition, currentTmie * _speed);
      yield return null;
    }
  }

  private IEnumerator SmoothAlpha(GameObject _object)
  {
    _object.GetComponent<CanvasGroup>().alpha = 0;
    while (_object.GetComponent<CanvasGroup>().alpha < 1)
    {
      _object.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
      yield return null;
    }
  }
}
