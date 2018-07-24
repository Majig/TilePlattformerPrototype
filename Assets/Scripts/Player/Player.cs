using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    // Config Fields
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] Vector2 deathKick = new Vector2();

    // State
    private bool isAlive = true;
    private bool canClimb;

    // Cached component references
    private Rigidbody2D myRigidbody;
    private CapsuleCollider2D myBody;
    private BoxCollider2D myFeet;
    private Animator myAnimator;
    private GameSession myGameSession;
    private float gravityScaleAtStart;

    // Messages then methods

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myGameSession = FindObjectOfType<GameSession>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update ()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        Die();
        FlipSprite();
    }

    /// <summary>
    /// Uses CrossPlatformInputManager for the Horizontal Axis
    /// Sets myRigidBody.velocity to controlThrow * runSpeed without changing velocity on the y-Axis
    /// </summary>
    private void Run ()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");   // between -1 and +1
        var runVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = runVelocity;

        bool isRunning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", isRunning);
    }

    /// <summary>
    /// Uses CrossPlatformInputManager for Climbing Input
    /// Sets velocity according climbSpeed
    /// </summary>
    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("Climbing", false);  // stop falsly animating climbing
            myRigidbody.gravityScale = gravityScaleAtStart;   // reset gravityScale to initial value
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);

        myRigidbody.velocity = climbVelocity;

        bool isClimbing = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myRigidbody.gravityScale = 0f;
        myAnimator.SetBool("Climbing", isClimbing);
    }

    /// <summary>
    /// Uses CrossPlatformInputManager for Jump Input
    /// Sets myRigidBody.velocity to jumpHeight without changing velocity on the x-Axis
    /// </summary>
    private void Jump ()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {   
            var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    /// <summary>
    /// Checks collision with Enemy and Hazard layer
    /// Sets isAlive to false, applies death kick, plays Dead animation
    /// </summary>
    private void Die ()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            myAnimator.SetTrigger("Dead");
            myRigidbody.velocity = deathKick;
            isAlive = false;
            myGameSession.ProcessPlayerDeath();
        }
    }

    /// <summary>
    /// FlipSprite() checks if the Player moves along x-Axis.
    /// If so it sets transform.localScale.x to either -1 or 1 by using Mathf.Sign()
    /// </summary>
    private void FlipSprite()
    {
        bool playerMovesOnX = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerMovesOnX)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
