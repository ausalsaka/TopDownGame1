using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Start()
    {
        //pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    public void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);

            
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        
    }


}
