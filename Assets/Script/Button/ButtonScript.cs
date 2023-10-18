using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitMenuButton()
    {
        Destroy(scene);
        SceneManager.LoadSceneAsync(0);
    }

    public void NextStage(int sceneNumber)
    {
        Destroy(scene);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void TryAgain(int sceneNumber)
    {
        Destroy(scene);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
