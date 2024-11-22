using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class SceneLoading_Demo_UniTask : MonoBehaviour
{
    async void Start()
    {
        await UniTask.WhenAll(
            LoadScene_UniTask(1, true, false),
            LoadScene_UniTask(2, true, true),
            LoadScene_UniTask(2, true, false)
            );;
    }

    public async UniTask LoadScene_UniTask(int id, bool isAddative, bool SetActiveScene)
    {
        LoadSceneMode sceneMode = isAddative ? LoadSceneMode.Additive : LoadSceneMode.Single;
        await SceneManager.LoadSceneAsync(id, sceneMode);
        if (SetActiveScene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(id));
        }
    }
}
