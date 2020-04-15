using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;     //needed for tweening
using Assets.Scripts.game.sfx;  // used for dash ffect
public class Movement : MonoBehaviour
{
    //basic vars
    private Rigidbody2D rb;
    private improvedjump improvedjump;
    private SpriteRenderer sr;
    private GhostingContainer gh;
    private Playeranimation anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        improvedjump = GetComponent<improvedjump>();
        sr = GetComponent<SpriteRenderer>();
        gh = GetComponent<GhostingContainer>();
        anim = GetComponent<Playeranimation>();
    }



    #region collisons

    [Header("Collsionsbools")]
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    
    private int wallSide;

    [Space]

    [Header("Collisonareas")]
    public float bottomcollisionRadius = 0.25f;
    public float sidecollisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    [Space]

    [Header("Layers")]
    public LayerMask groundLayer; //layer that can be detected, make sure player is not in that layer


    private void Collisionbool()
    {
        //uses a circlecasts on each side to check bools
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, bottomcollisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, sidecollisionRadius, groundLayer)
              || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, sidecollisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, sidecollisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, sidecollisionRadius, groundLayer);
        
        //which side player has a wall  
        wallSide = onRightWall ? -1 : 1;
        // -1 = right
        //  1 = left
    }


    private void OnDrawGizmos()
    {
        //draws the circles for easier adjusting in scene 
        Gizmos.color = debugCollisionColor;


        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, bottomcollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, sidecollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, sidecollisionRadius);
    }



    #endregion 



    #region update



    private void Update()
    {
        //player input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");


        anim.SetHorizontalMovement(x, y, rb.velocity.y);
        Vector2 dir = new Vector2(x, y);
  

        //wallclimbing
        wallclimb = onWall && Input.GetAxis("Vertical") > 0;
        if (wallclimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, y * speed);
        }


        //walking
        Walk(dir);


        //jump input
        if (Input.GetKeyDown(InputManager.IM.jump))
        {
            anim.SetTrigger("jump");
            //if on ground then normal jump
            if (onGround)
            {
                Jump(Vector2.up, false);
            }
            //if on a wall then walljump
            if (onWall && !onGround)
            {
                Walljump();
            }
        }


        //for landing effects
        if (onGround && !groundtouch)
        {
            GroundTouch();
            groundtouch = true;
        }

        if (!onGround && groundtouch)
        {
            groundtouch = false;
        }


        //always do collision checking
        Collisionbool();


        //if on a wall then slide down
        if (onWall && !onGround && !isclimbing)
        {
            if (x != 0)
            {
                iswallsliding = true;
                Wallslide();
            }
        }
        if (!onWall || onGround)
            iswallsliding = false;


        //if right click then dash
        if (Input.GetKeyDown(InputManager.IM.dash) && hasdashed == false)
        {
            if (xRaw != 0 || yRaw != 0)
            {
                Dash(xRaw , yRaw);
            }
        }


        //so one can dash again
        if(onWall || onGround)
        {
            isdashing = false;
            hasdashed = false;
        }
        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }

        if(onGround && dir == Vector2.zero)
        {
            anim.SetTrigger("idle");
        }
    }



    #endregion



    #region movement

    [Space]

    [Header("Movementvalues")]
    public float speed;
    public float jumpforce;
    public float slidespeed;
    public float walljumplerp;
    public float climbspeed;
    public float dashspeed;
    public float dragincrease;
    public int side = 1;

    [Space]

    [Header("Movementbools")]
    public bool canmove;
    [HideInInspector]
    public bool iswallsliding;
    private bool walljumped;
    private bool groundtouch;
    [HideInInspector]
    public bool isclimbing;
    private bool wallclimb;
    [HideInInspector]
    public bool isdashing;
    private bool hasdashed;
    


    //walking 
    private void Walk(Vector2 dir)
    {
        if (!canmove)
        {
            return;
        }

        if (isdashing)
        {
            return;
        }

        if (isclimbing)
        {
            return;
        }

        if (!walljumped)
        {
            //use velocity for movement
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            //if from walljump
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), walljumplerp * Time.deltaTime);
        }
    }


    //jumping
    private void Jump(Vector2 dir, bool wall )
    {
        //jumps in direction of dir in this case
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpforce;
        
    }

    
    //a method to diable movement for a time period
    IEnumerator DisableMovement(float time)
    {
        canmove = false;
        yield return new WaitForSeconds(time);
        canmove = true;
    }


    //slides down when against a wall
    void Wallslide()
    {
        if (wallSide != side)
        {
            anim.Flip(side * -1);
        }
        if (!canmove)
        {
            return;
        }
        if (!isclimbing)
        {
            return;
        }

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && onRightWall) || (rb.velocity.x < 0 && onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slidespeed);  
    }

 
    //the ability to jump of a wall
    void Walljump()
    {
        if ((side == 1 && onRightWall) || side == -1 && !onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        //jump the oposite way from the wall
        Vector2 wallDir = onRightWall ? Vector2.left : Vector2.right;
        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        walljumped = true;
        if(wallSide == 1)
        {
            Createwalljumparticles(-walljumpparticles.transform.localScale.x);
        }
        else
        {
            Createwalljumparticles(walljumpparticles.transform.localScale.x);
        }
        //-1 right
    }


    //landing effects
    void GroundTouch()
    {
        isdashing = false;
        hasdashed = false;
        Createdust();
        Camera.main.transform.DOShakePosition(.2f, .1f, 4, 5, false, true);
        side = anim.sr.flipX ? -1 : 1;
    }


    //dashing
    private void Dash(float x, float y)
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        

        hasdashed = true;
        anim.SetTrigger("dash");
        gh.Init(20, 0.01f, sr, 1f);


        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashspeed;
        StartCoroutine(DashWait());
    }


    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        DOVirtual.Float(dragincrease, 0, .8f,Drag);
        

        rb.gravityScale = 0;
        improvedjump.enabled = false;
        walljumped = true;
        isdashing = true;

        yield return new WaitForSeconds(.3f);

     
        rb.gravityScale = 1;
        improvedjump.enabled = true;
        walljumped = false;
        isdashing = false;
    }


    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (onGround)
            hasdashed = false;
            gh.StopEffect();
    }


    //so the drag can be set directly
    void Drag(float x)
    {
        rb.drag = x;
    }
    


    #endregion



    #region polish



    [Header("polish")]
    public ParticleSystem dust;
    public ParticleSystem walljumpparticles;


    void Createdust()
    {
        dust.Play();
    }

  
    void Createwalljumparticles(float x)
    {
        walljumpparticles.Play();
        walljumpparticles.gameObject.transform.localScale = new Vector3(x, walljumpparticles.gameObject.transform.localScale.y, 1);
    }



    #endregion
}