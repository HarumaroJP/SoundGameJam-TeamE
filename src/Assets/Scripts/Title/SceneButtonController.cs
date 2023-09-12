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
}
