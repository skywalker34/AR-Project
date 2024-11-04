using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public void GotoScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

  
}
