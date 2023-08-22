using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader instance;
    void Awake()
    {
        instance = this;

    }
    #endregion

    public GameObject loadingScreen;
    public Slider slider;
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Loading Scene")
        {
            LoadLevel(1);
        }
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;

            yield return null;
        }
    }
}
