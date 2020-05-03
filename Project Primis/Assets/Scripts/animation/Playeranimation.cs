using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        move = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onground", move.onGround);
        anim.SetBool("onWall", move.onWall);
        //anim.SetBool("onRightWall", move.onRightWall);
        anim.SetBool("wallClimb", move.isclimbing);
        //anim.SetBool("wallSlide", move.iswallsliding);
        anim.SetBool("canMove", move.canmove);
        anim.SetBool("isDashing", move.isdashing);

    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (move.isclimbing|| move.iswallsliding)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
        
    }

    
}

