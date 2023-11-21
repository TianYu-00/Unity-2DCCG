using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                ResumeGame();
                
            }
            else if (!gameIsPaused)
            {
                PauseGame();
                
            }
            
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        pausePanel.SetActive(true);
        Debug.Log("Paused");
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        pausePanel.SetActive(false);
        Debug.Log("Unpaused");
    }

    public void ClickResume()
    {
        ResumeGame();

    }

    public void ClickMainMenu()
    {
        if (FindObjectOfType<SceneLoading>() == null)
        {
            return;
        }
        else
        {
            FindObjectOfType<SceneLoading>().LoadScene("Mainmenu");
        }
        
        //SceneManager.LoadScene(0);
    }
}
