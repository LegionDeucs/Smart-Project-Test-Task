using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneStaticData loadingScene;
    [SerializeField] private SceneStaticData metaScene;
    [SerializeField] private List<SceneStaticData> levelScenes;

    public void LoadLoadingScene(Action callback = null)
    {
        LoadScene(loadingScene, LoadSceneMode.Single, callback);
    }

    public async Task UnloadLoadingScene(Action callback = null)
    {
        await UnloadSceneAsynk(loadingScene, callback);
    }

    public async Task LoadLevelScene(int levelIndex, Action callback = null)
    {
        await LoadSceneAsynk(levelScenes[(int)Mathf.Repeat(levelIndex, levelScenes.Count)], LoadSceneMode.Additive, callback);
    }

    public async Task LoadMetaScene(Action callback = null)
    {
        await LoadSceneAsynk(metaScene, LoadSceneMode.Additive, callback);
    }

    public async Task UnloadMetaScene(Action callback)
    {
        await UnloadSceneAsynk(metaScene, callback);
    }

    private async Task LoadSceneAsynk(SceneStaticData sceneData, LoadSceneMode sceneLoadMode, Action callback = null)
    {
        await SceneManager.LoadSceneAsync(sceneData.BuildIndex, sceneLoadMode);

        callback?.Invoke();
    }

    private void LoadScene(SceneStaticData sceneData, LoadSceneMode sceneLoadMode, Action callback = null)
    {
        SceneManager.LoadScene(sceneData.BuildIndex, sceneLoadMode);
        callback?.Invoke();
    }

    private async Task UnloadSceneAsynk(SceneStaticData sceneData, Action callback)
    {
        await SceneManager.UnloadSceneAsync(sceneData.BuildIndex);
        callback?.Invoke();
    }
}
