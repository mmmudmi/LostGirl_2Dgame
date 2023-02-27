using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using TMPro;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 4.0f;
    [SerializeField] float climbSpeed = 2.0f;

    [SerializeField] Vector2 deathKick = new Vector2(0,2f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip deadAudio;

    Vector2 moveInput;
    Rigidbody2D rgbd2D;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    CircleCollider2D myCircleCollider;
    float gravityScaleAtStart;




    bool isAlive = true;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        gravityScaleAtStart = rgbd2D.gravityScale;
        //ammoText.text = ammo.ToString(); 

    }

    void Update()
    {
        if(!isAlive) { return; };

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) { return; };
        // if (ammo > 0)
        // {
        //DeductToAmmo(1);
        if(FindObjectOfType<GameSession>().ammo > 0)
        {
        FindObjectOfType<GameSession>().DeductToAmmo(1);
        Instantiate(bullet, gun.position, transform.rotation);
        myAnimator.SetTrigger("Shooting");
        }

    }

    void OnMove(InputValue value)
    {
        if(!isAlive) { return; };
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) { return; };
        //if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(!myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed)
        {
            rgbd2D.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x*runSpeed , rgbd2D.velocity.y);
        rgbd2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) >  Mathf.Epsilon;
        if(playerHasHorizontalSpeed) 
        {
            myAnimator.SetBool("isRunning", true);

        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon; 
        if(playHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(rgbd2D.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        // if not touching the ladder
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            rgbd2D.gravityScale = gravityScaleAtStart; 
            myAnimator.SetBool("isClimbing", false);
            return; 
        }

        Vector2 climbVelocity = new Vector2 (rgbd2D.velocity.x,moveInput.y*climbSpeed);
        rgbd2D.velocity = climbVelocity;
        rgbd2D.gravityScale = 0f;

        // check vertical Speed
        bool playHasVerticalSpeed = Mathf.Abs(rgbd2D.velocity.y) > Mathf.Epsilon; 

        myAnimator.SetBool("isClimbing", playHasVerticalSpeed);
    }

    void Die() 
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            AudioSource.PlayClipAtPoint(deadAudio, Camera.main.transform.position,.3f); 
            myAnimator.SetTrigger("Dying");
            rgbd2D.velocity = deathKick;

             StartCoroutine(informGameSession());
        }    
    }

     IEnumerator informGameSession()
    {
        // aka: come back to run the following like after the delay
        yield return new WaitForSecondsRealtime(1f); 
        // inform the GameSession
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

//     public void AddToAmmo(int ammoes) // This is public. When the refill is hit, it will call this.
//     {
//         ammo += ammoes;
//         ammoText.text = ammo.ToString();  // if we change the value and we want UI to update
//                                             // we must do that manually
//     }

//     void DeductToAmmo(int ammoes) // This is public. When the refill is hit, it will call this.
//     {
//         ammo -= ammoes;
//         ammoText.text = ammo.ToString();  // if we change the value and we want UI to update
//                                             // we must do that manually
//     }

//     public void resetAmmo(int ammoes)
//     {
//         ammo = ammoes;
//         ammoText.text = ammo.ToString();
//     }
}
