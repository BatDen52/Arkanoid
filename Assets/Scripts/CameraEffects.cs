using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private Image _damageScreen;

    private Color _endColor;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _damageScreen.raycastTarget = false;

        _endColor = _damageScreen.color;
        Color startColor = _damageScreen.color;
        startColor.a = 0;
        _damageScreen.color = startColor;
    }

    public void PlayDie()
    {
        transform.DOShakePosition(1, 7).SetUpdate(true);
        _damageScreen.DOColor(_endColor, 0.4f).SetUpdate(true).SetLoops(2, LoopType.Yoyo);
    }

    public void PlayPush()
    {
        _camera.DOOrthoSize(Camera.main.orthographicSize * 0.97f, 0.15f).SetLoops(2, LoopType.Yoyo);
    }
}
