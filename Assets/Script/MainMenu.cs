using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip clickAudio;

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(clickAudio, Camera.main.transform.position,.3f); 

    }

    public void PlayEasy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void PlayHard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 7);
    }
    public void ToMainMenu()
    {
        FindObjectOfType<GameSession>().ResetEverything();
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame()
    {
        Debug.Log("Quit the game ...");
        Application.Quit();
    }


}
