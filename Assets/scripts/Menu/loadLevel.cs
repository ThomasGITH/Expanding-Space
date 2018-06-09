//This script lets you load a Scene asynchronously. It uses an asyncOperation to calculate the progress and outputs the current progress to Text (could also be used to make progress bars).

//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text) and attach them both to the Inspector of your GameObject
//In Play Mode, press your Button to load the Scene, and the Text changes depending on progress. Press the space key to activate the Scene.
//Note: The progress may look like it goes straight to 100% if your Scene doesn’t have a lot to load.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadLevel : MonoBehaviour
{
    public GameObject loadingscreen;
    public Text m_Text;
    public Button m_Button;
    public Slider slider;

    void Start()
    {
        //Call the LoadButton() function when the user clicks this Button
        m_Button.onClick.AddListener(LoadButton);
    }

    void LoadButton()
    {
        //Start loading the Scene asynchronously and output the progress bar
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadingscreen.SetActive(true);
        yield return null;

        int sceneIndex = 1;
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Menu")
        {
            sceneIndex = 1;
        }
        else if (currentScene.name == "GameOver")
        {
            sceneIndex = 0;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);

        while (!asyncOperation.isDone)
        {
            
            float progress = (asyncOperation.progress * 100);
            m_Text.text = (Mathf.RoundToInt(progress)) + "%";
            slider.value = asyncOperation.progress;


            
            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = 1f;
                //m_Text.text = "100%";

                if(sceneIndex != 0)
                {
                    m_Text.text = "Press SPACE  to continue";
                    if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
                }
                else { m_Text.text = "100%";

                    asyncOperation.allowSceneActivation = true;
                }


            }

            yield return null;
        }
    }
}