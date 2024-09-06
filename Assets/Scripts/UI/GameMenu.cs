using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private WinWindow _winWindow;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OpenPauseWindow);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OpenPauseWindow);
    }

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
    }

    public void OpenFailWindow()
    {
        _failWindow.gameObject.SetActive(true);
    }

    public void OpenWinWindow()
    {
        _winWindow.gameObject.SetActive(true);
    }
}
