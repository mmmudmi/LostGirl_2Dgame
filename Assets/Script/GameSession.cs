using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] public int score = 0;
    [SerializeField] public int ammo = 0;


    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ammoText;

    void Awake() 
    {
        // ----------------
        // Sigleton pattern
        // ----------------
        // Count how GameSession object at this point
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there is a GameSession object already
        if(numGameSessions >= 2)  //  The first one and this one make it two.
        {
            Destroy(gameObject);  // Kill this one so the first one will be the only one.
        } 
        else // This is the first one
        {
            DontDestroyOnLoad(gameObject); // Make this object survive when we load a new scene
        }
    }

    void Start()
    {
        ResetEverything();
        livesText.text = playerLives.ToString(); // Make the UI show the initial value
        scoreText.text = score.ToString();       // Make the UI show the initial value
        ammoText.text = ammo.ToString(); 
    
    }


    public void AddToScore(int points) // This is public. When the coin is hit, it will call this.
    {
        score += points;
        scoreText.text = score.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void AddToLives(int lives) // This is public. When the coin is hit, it will call this.
    {
        playerLives += lives;
        livesText.text = playerLives.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void AddToAmmo(int ammoes) // This is public. When the refill is hit, it will call this.
    {
        ammo += ammoes;
        ammoText.text = ammo.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void DeductToAmmo(int ammoes) // This is public. When the refill is hit, it will call this.
    {
        ammo -= ammoes;
        ammoText.text = ammo.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void ResetEverything()
    {
            score = 0;
            ammo = 0;
            playerLives = 3;
            scoreText.text = score.ToString();
            ammoText.text = ammo.ToString();
            livesText.text = playerLives.ToString();
    }


    public void ProcessPlayerDeath() // This is public. When John dies, he will call this.
    {
        if(playerLives > 1) 
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            score = 0;
            ammo = 0;
            scoreText.text = score.ToString();
            ammoText.text = ammo.ToString();
            //FindObjectOfType<PlayerMovement>().resetAmmo(0);
            livesText.text = playerLives.ToString(); // if we change the value and we want UI to update
                                                     // we must do that manually
        }
        else // Game over.. need to start from the beginning
        {
            // Reset the ScenePersist
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            //go to GameOver Scene
            SceneManager.LoadScene(1); // assume that scene 0 is the first one or the menu
            Destroy(gameObject); // Destroy the game session.
        }
    }
}
