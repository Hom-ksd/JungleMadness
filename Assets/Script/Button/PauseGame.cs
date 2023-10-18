using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public Canvas canvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1)
        {
            PauseGameButton();
        }
        else if(Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
        {
            ResumeGameButton();
        }
    }
    public void PauseGameButton()
    {
        Time.timeScale = 0;
        canvas.enabled = true;
    }
    public void ResumeGameButton()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
    }
}
