using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LastLevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip passAudio;
    [SerializeField] AudioClip failAudio;
    [SerializeField] public int isPassAmonth = 100;
    [SerializeField] public bool isPass = false; // infront of the warp portical


    void OnTriggerEnter2D(Collider2D other)
    {
        if(FindObjectOfType<GameSession>().score >= isPassAmonth)
        {
            isPass = true;
            AudioSource.PlayClipAtPoint(passAudio, Camera.main.transform.position,.3f); 
            StartCoroutine(LoadWinLevel());
        }
        else 
        {
            AudioSource.PlayClipAtPoint(failAudio, Camera.main.transform.position,.3f); 
        }
    }

    IEnumerator LoadWinLevel()
    {
        // aka: come back to run the following like after the delay
        yield return new WaitForSecondsRealtime(levelLoadDelay); 

        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // int nextSceneIndex = currentSceneIndex + 1; 


        // if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
        //     nextSceneIndex = 0;
        //}

        // Before loading next level, have to destroy the ScenePersist object so that
        // the new one of the new level will be there to do the work
        FindObjectOfType<GameSession>().ResetEverything();
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        
        SceneManager.LoadScene(1);
    }

}
