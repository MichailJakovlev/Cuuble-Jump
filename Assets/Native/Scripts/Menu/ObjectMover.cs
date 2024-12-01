using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : MonoBehaviour
{
    public RectTransform _canvas;
    public RectTransform _skinView;
    public GameObject _shopUI;
    public GameObject _leaderboardUI;
    [SerializeField, HideInInspector] float _speed = 1;

    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _closeLeaderboardButton;

    float currentTmie;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    public void MoveOutOfCanvasShop()
    {
        Vector3 targetPosition =
            new(
                _skinView.localPosition.x,
                _canvas.rect.height + _skinView.rect.height * 2,
                _skinView.localPosition.z
            );
        StartCoroutine(MoveToTarget(_skinView, targetPosition));
        _shopUI.SetActive(true);
        StartCoroutine(SmoothAlpha(_shopUI));
    }

    public void MoveOutOfCanvasLeaderboard()
    {
        Vector3 targetPosition =
            new(
                _skinView.localPosition.x,
                _canvas.rect.height + _skinView.rect.height * 2,
                _skinView.localPosition.z
            );
        StartCoroutine(MoveToTarget(_skinView, targetPosition));
        _leaderboardUI.SetActive(true);
        StartCoroutine(SmoothAlpha(_leaderboardUI));
    }

    public void MoveToCanvas()
    {
        _skinView.gameObject.SetActive(true);
        StartCoroutine(
            MoveToTarget(
                _skinView,
                new Vector3(_skinView.localPosition.x, startPosition.y, _skinView.localPosition.z)
            )
        );
    }

    private IEnumerator MoveToTarget(RectTransform _object, Vector3 _targetPosition)
    {
        currentTmie = Time.deltaTime;
        while (_object.localPosition != _targetPosition)
        {
            currentTmie += (float)Math.Sqrt(Time.deltaTime);

            _shopButton.interactable = false;
            _closeShopButton.interactable = false;
            _leaderboardButton.interactable = false;
            _closeLeaderboardButton.interactable = false;

            _object.localPosition = Vector3.MoveTowards(
                _object.localPosition,
                _targetPosition,
                currentTmie * _speed * 2.5f
            );
            yield return null;
            _shopButton.interactable = true;
            _closeShopButton.interactable = true;
            _leaderboardButton.interactable = true;
            _closeLeaderboardButton.interactable = true;
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
