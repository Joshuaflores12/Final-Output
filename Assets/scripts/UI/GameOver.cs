using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;
    [SerializeField] private GameObject Throwable;
    private void Start()
    {
        if(gameoverUI != null) 
        {
            gameoverUI.SetActive(false);
        }
    }
    void Update()
    {
        if (PlayerManager != null && PlayerManager.HP <= 0) 
        {
            GameOverUI();

            Primary.SetActive(false);

            Secondary.SetActive(false);

            Throwable.SetActive(false);

            GameUI.SetActive(false);
        }
    }

    public void GameOverUI() 
    {
        if (gameoverUI != null) 
        {
            gameoverUI.SetActive(true);
        }

    }
    public void TryAgain() 
    {
        
        SceneManager.LoadScene("Shooter Tubbies");
    }

    public void Mainmenu() 
    {
        
        SceneManager.LoadScene("Main Menu");
    }


}
