using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] public int lives = 3;
    [SerializeField] public int maxLives = 3;
    [SerializeField] AudioClip deadAudio;

    public HealthBarBehavior Healthbar;

    
    Rigidbody2D rgbd2D;
    Animator myAnimator;
    
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        Healthbar.SetHealth(lives, maxLives);

    }

    void Update()
    {
        rgbd2D.velocity = new Vector2 (moveSpeed,0f);

    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rgbd2D.velocity.x)),1f);
    }

    public void DeductToLives(int live) // This is public. When the refill is hit, it will call this.
    {
        lives -= live;

        Healthbar.SetHealth(lives, maxLives);
        if(lives <= 0) {
            AudioSource.PlayClipAtPoint(deadAudio, Camera.main.transform.position,.3f); 
            Destroy(gameObject);
        }
    }


}
