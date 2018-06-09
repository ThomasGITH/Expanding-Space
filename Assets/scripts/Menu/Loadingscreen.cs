using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loadingscreen : MonoBehaviour {

    public GameObject loadingscreenObj;
    public Slider slider;
    public Text text;


    private bool load;

    public void LoadLevel()
    {
        StartCoroutine(LoadRoutine(1));
    }

    private IEnumerator LoadRoutine(int index)
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = false;

        loadingscreenObj.SetActive(true);

        while (!async.isDone)
        {

            float progress = Mathf.Clamp01(async.progress / .9f);
            Debug.Log("progress: " + progress);
            slider.value = progress;

            async.allowSceneActivation = true;
        }

    }
}

