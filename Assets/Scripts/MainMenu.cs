using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Level(int levelIndex)
    {
        SceneManager.LoadSceneAsync(levelIndex);
    }


    public void Quit()
    {
        Application.Quit();

    }
    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }



}
