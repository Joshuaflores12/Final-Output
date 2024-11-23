using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Shooter_Tubbies;

    public void StartButton()
    {
        SceneManager.LoadScene(Shooter_Tubbies);
    }

    public void ExitButton() 
    {
        Application.Quit();
    }
}
