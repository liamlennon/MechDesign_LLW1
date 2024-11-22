using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoading_Demo_Async : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(C_LoadSceneAsync(1, LoadSceneMode.Additive));
    }

    IEnumerator C_LoadSceneAsync(int sceneIndex, LoadSceneMode sceneMode)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, sceneMode);

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Progress: {asyncLoad.progress * 100}");
            yield return null;
            Debug.Log($"Progress: {asyncLoad.progress * 100}");
        }
    }
}
