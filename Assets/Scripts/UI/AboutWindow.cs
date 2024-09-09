using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutWindow : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(Close);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}