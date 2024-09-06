using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private Game _game;
    [SerializeField] private TMP_Text _levelNumberText;

    private void Awake()
    {
        _levelNumberText.text = $"{LEVEL_SCENE_SUBNAME} {SceneManager.GetActiveScene().buildIndex}";
    }
}
