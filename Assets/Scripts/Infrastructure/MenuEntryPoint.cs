using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEntryPoint : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private List<string> _sceneNames = new();
    [SerializeField] private SelectLevelWindow _selectLevelWindow;

    private void Awake()
    {
        _selectLevelWindow.SetLevelsNames(_sceneNames);
        SaveService.Initialize(_sceneNames);
    }

#if UNITY_EDITOR
    [ContextMenu("RefreshSceneList")]
    private void RefreshSceneList()
    {
        int extentionLength = 6;
        _sceneNames.Clear();

        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);

                if (name.StartsWith(LEVEL_SCENE_SUBNAME))
                    _sceneNames.Add(name.Substring(0, name.Length - extentionLength));
            }
        }
    }
#endif
}