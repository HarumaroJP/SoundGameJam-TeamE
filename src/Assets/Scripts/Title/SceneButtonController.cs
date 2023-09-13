using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void SceneButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void QuitButton()
    {
        #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
        #else
           Application.Quit();
        #endif
    }
    public void CreditsButton(GameObject Credits)
    {
        Credits.SetActive(true);
    }
    public void URLButton(string URL)
    {
        Application.OpenURL(URL);
    }
    public void CreditsQuitButton(GameObject Credits)
    {
        Credits.SetActive(false);
    }
}
