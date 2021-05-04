using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : Singleton<Loading>
{

    private AsyncOperation async;
    public GameObject LoadingUi;
    public Slider Slider;

    public void LoadNextScene(string scene)
    {
        //LoadingUi.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }
}