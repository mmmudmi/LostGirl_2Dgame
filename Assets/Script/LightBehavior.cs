using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    
    [SerializeField] public bool IsWarpOpen = false;
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("isLit", false);

    }

    public void setIsLitTrue () {
        myAnimator.SetBool("isLit", true);
    }


    void Update()
    {

    }

}
