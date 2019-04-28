using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private bool doubleJumpUsed = false;
    private bool onWall = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    RobotEnergyController robotEnergy;
    Rigidbody2D rb;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
        robotEnergy = GetComponent<RobotEnergyController>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp ("Jump") && onWall) //wall jump
        {
            velocity.y = jumpTakeOffSpeed;
            velocity.x = jumpTakeOffSpeed;
            print("wall jump");
            //robotEnergy.WallJump();
        }
      	else if (Input.GetButtonDown ("Jump") && !doubleJumpUsed) { //double jump
            velocity.y = jumpTakeOffSpeed;
            doubleJumpUsed = true;
        }
        else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }

            robotEnergy.DoubleJump();
        }
        else if (Input.GetButtonDown (KeyCode.J.ToString()) && !grounded) { //dash
        	if(move.x > 0) {
        		move.x = 15;
        	} else {
        		move.x = -15;
        	}

        	robotEnergy.Dash();
        }
        else if (Input.GetButton (KeyCode.K.ToString())) {
        	if(velocity.y < 5) {
        		velocity.y += 0.75f;
        	}
        	robotEnergy.Jetpack();

        }
        else if(grounded) {
        	//replenish double jump
        	doubleJumpUsed = false;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite) 
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    void OnCollisionEnter2D(Collision2D other){
    	if (other.gameObject.tag == "Wall") {
    		rb.gravityScale = 0f;
    		gravityModifier = 0;
    		velocity.y = 0;
    		onWall = true;
 		}
 	}

 	void OnCollisionExit2D(Collision2D col) { 
 		if (col.gameObject.tag == "Wall") {
 			rb.gravityScale = 3f;
 			gravityModifier = 2;
 			onWall = false;
 		}
 	}
}
