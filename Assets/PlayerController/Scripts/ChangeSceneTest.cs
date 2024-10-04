using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
    IEnumerator LoadSceneInBack()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!load.isDone) 
        {
            
            yield return null;  
        }
    }
}
