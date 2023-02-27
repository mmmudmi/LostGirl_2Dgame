using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    
    Rigidbody2D rgbd;
    PlayerMovement player;
    CapsuleCollider2D myCapsuleCollider;
    float xSpeed;
    //public int currentAmmo;



    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    //currentAmmo = FindObjectOfType<GameSession>().ammo;
    }

    void Update()
    {
        rgbd.velocity = new Vector2(xSpeed, 0f);
        FlipSprite();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //Destroy(other.gameObject); // Destroy to enemy
            //FindObjectOfType<EnemyMovement>().DeductToLives(1);
            other.gameObject.GetComponent<EnemyMovement>().DeductToLives(1);
        }
        // if(FindObjectOfType<EnemyMovement>().lives <= 0){
        //     Destroy(gameObject);
        // }
        
        // if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("light")))
        // {
        //     other.gameObject.GetComponent<GameSession>().AddToLight();
        //     other.gameObject.GetComponent<LightBehavior>().setIsLitTrue();
        // }
        Destroy(gameObject); // Destroy the bullet
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(rgbd.velocity.x) > Mathf.Epsilon; 
        if(playHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(rgbd.velocity.x), 1f);
        }
    }
}


