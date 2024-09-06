using TMPro;
using UnityEngine;

public abstract class CountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    protected virtual void OnCountChanged(int count)
    {
        _text.text = count.ToString();
    }
}
