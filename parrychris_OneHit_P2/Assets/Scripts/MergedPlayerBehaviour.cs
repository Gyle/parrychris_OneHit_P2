﻿using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MergedPlayerBehaviour : MonoBehaviour
{

    public KeyCode left, right, jump, dash, block, jab, groundPound;
    public bool isPlayer1;

    //Dash
    private int dashSpeed = 5000;
    private float dashCooldown = 1;
    private float dashLength = 0.2f;
    private bool dashing = false;
    private bool dashDir = false;
    private float nextDash = 1;
    private float dashStop;

    //Jumping
    private float jumpSpeed = 30;
    private bool grounded = true;
    private bool doubleJump = false;
    private int jumpCount = 0;

    //Blocking
    private bool shieldUp = false;
    private float blockCD = 1;
    private float nextBlock = 0;
    private float currentBlockDur = 0;
    private float maxBlockDur = 1;

    //Movement
    private float playerSpeed = 9;
    private float moveVelocity;

    //Player
    public string character = "default";
    private bool onRightSide = true;
    private bool slidingoffhead = false;
    private bool groundpounding = false;
    public Collider2D[] attackHitboxes;
    public Animator animator;

    //Game
    public GameObject enemy;
    public Canvas canvas;
    private Rigidbody2D rb2d;
    private MergedPlayerBehaviour enemyScript;
    private bool gameOver = false;
    private bool won = false;
    private float gameOverTime;
    private float gameOverWait = 2f;

    // Use this for initialization
    void Start()
    {
        //Initializes the players RigidBody, Animator and EnemyScript
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyScript = enemy.GetComponent<MergedPlayerBehaviour>();

        if(character == "fast") {
            Debug.Log(this.gameObject.name + " has selected " + character);
            this.playerSpeed = 12;
            doubleJump = true;
        }
    }

    // Called every frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        //Check if player is on ground, set variable if so
        checkGrounded();

        if (dashing)
        {
            Dash();
        }

        // Ground Pound
        if (groundpounding)
        {
            LaunchAttack(attackHitboxes[1]);
        }


        //  Jump
        if (Input.GetKeyDown(this.jump) && !shieldUp)
        {   //sets animator variables to true
            animator.SetBool("isJumping", true);
            animator.SetBool("Grounded", false);
            Jump();
        }
        //Melee
        //There are 3 melee cases - KeyUp, KeyDown, Key
        else if (Input.GetKeyUp(this.jab) && shieldUp == false)
        {    //KeyUp is when releasing melee, turning off animation
            animator.SetBool("Jab_attack", false);
        }
        else if (Input.GetKeyDown(this.jab) && shieldUp == false)
        {    //KeyDown is beginning of melee animation and launchAttack
            animator.SetBool("Jab_attack", true);
            LaunchAttack(attackHitboxes[0]);
        }
        else if (Input.GetKey(this.jab) && shieldUp == false)
        {    //Key is when holding attack
             //May remove this option in future
            LaunchAttack(attackHitboxes[0]);
        }
        //Block 
        /************Changed************/
        else if (Input.GetKey(this.block))
        {
            Block();
        } else if (Input.GetKeyUp(this.block)) {
        	if(shieldUp) {
        		nextBlock = Time.time + blockCD;
        	}
        	shieldUp = false;
        	animator.SetBool("Block", shieldUp);
        	GameObject ChildGameObject = this.gameObject.transform.GetChild(0).gameObject;
        	ChildGameObject.GetComponent<SpriteRenderer>().enabled = shieldUp;
        }
        /************Changed************/
        //Restart button (for development)
        else if (Input.GetKey(KeyCode.Delete))
        {
            GameOver();
        }
    }


    // Called every frame 
    void FixedUpdate()
    {
        if (gameOver)
        {
            GameOver();
            return;
        }
        if (grounded)
        {
            moveVelocity = 0;
            //set Grounded variable in Animator to true
            animator.SetBool("Grounded", true);

            //move left
            if (Input.GetKey(this.left) && !shieldUp)
            {
                moveVelocity = -playerSpeed;
            }
            //move right
            if (Input.GetKey(this.right) && !shieldUp)
            {
                moveVelocity = playerSpeed;
            }
            //Dash
            if (Input.GetKey(this.dash) && Time.time > nextDash && !dashing)
            {
                //Set the time for next earliest dash
                nextDash = Time.time + dashCooldown;
                //Set dash attack variable in animator to true
                animator.SetBool("Dash_Attack", true);

                Dash();
                return;
            }
            //Move the player by moving iuts Rigidbody component
            rb2d.velocity = new Vector2(moveVelocity,
                rb2d.velocity.y);
        }
        else if(!grounded){
            // perform ground pound movement if in the air
            if (Input.GetKey(this.groundPound) && !grounded)
            {
                groundpounding = true;
                GroundPound();
            }

            //player is in the air and can move left/right at half speed.
            //move left
            if (Input.GetKey(this.left) && !shieldUp)
            {
                moveVelocity = -playerSpeed;
            }
            //move right
            if (Input.GetKey(this.right) && !shieldUp)
            {
                moveVelocity = playerSpeed;
            }
            rb2d.velocity = new Vector2(moveVelocity,
                rb2d.velocity.y);
        }
        // if(slidingoffhead){
        //     //Move the player by moving iuts Rigidbody component
        //     rb2d.velocity = new Vector2(-5f,0f);
        // }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //check if floor or other player
        if (coll.transform.tag.Contains("Ground"))
        {
            slidingoffhead = false;
            grounded = true;
            groundpounding = false;
        }
        else if (coll.transform.tag.Contains("Head"))
        {
            grounded = true;
            SlideOffHead();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //check if floor or other player
        if (coll.transform.tag.Contains("Ground"))
        {
            grounded = false;
        }
    }

    // this method accelerates the player faster toward the ground
    private void GroundPound()
    {
        rb2d.velocity = new Vector2(
                rb2d.velocity.x, -jumpSpeed);
    }

    // this method will push the player off the other player's head
    private void SlideOffHead()
    {
        Debug.Log("Slide off head");
        slidingoffhead = true;
    }

    //Check to see if player grounded, update booleans if so
    private void checkGrounded()
    {
        if (gameObject.transform.position.y < 1.35)
        {
            animator.SetBool("Grounded", true);
            grounded = true;
            jumpCount = 0;
        }
        //this code seems to make the character bounce off the other ones head, not sure why...
        //else{
        //    animator.SetBool("Grounded", false);
        //    grounded = false;
        //}
    }

    private void Block()
    {
    	if(shieldUp) {
    		if(Time.time > currentBlockDur) {
    			shieldUp = false;
    			nextBlock = Time.time + blockCD;
    		}

    	} else {
    		if(Time.time > nextBlock) {
    			shieldUp = true;
        		currentBlockDur = Time.time + maxBlockDur;
    		} else {
    			return;
    		}
        	
    	}
    	//set animator variable Block to true
    	animator.SetBool("Block", shieldUp);
        //Activate blue shield sprite
        GameObject ChildGameObject = this.gameObject.transform.GetChild(0).gameObject;
        ChildGameObject.GetComponent<SpriteRenderer>().enabled = shieldUp;
    }

    private void Dash()
    {
        if (dashing)
        {
            //Checkif player should stop dashing
            if (Time.time > dashStop)
            {
                dashing = false;
                //Set grounded here to fix a bug of player getting stuck after dashing
                grounded = true;
                animator.SetBool("Dash_Attack", false);     //set dash attack variable in animator to false
                this.rb2d.velocity = new Vector2(0, 0);
            }
            else
            {
                LaunchAttack(attackHitboxes[0]);
            }
        }
        else //If not dashing, start dash
        {
            if (onRightSide)
            {
                dashDir = true;
            }
            else
            {
                dashDir = false;
            }
            dashing = true;
            //Set time dash should stop
            dashStop = Time.time + dashLength;

        }
        if (shieldUp)
        {   //Turns shield off before dashing
            Block();
        }
        else if (dashDir)
        {
            rb2d.AddForce(new Vector2(-dashSpeed, 0));
        }
        else
        {
            rb2d.AddForce(new Vector2(dashSpeed, 0));
        }

    }

    private void Jump()
    {
        //check player is grounded before jumping
        Debug.Log("Double: " + doubleJump + ", jumpCount: " + jumpCount);
        if (grounded || (doubleJump && jumpCount < 2))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.SetBool("isJumping", false);
            grounded = false;
            jumpCount++;
        }
    }

    //Main attack method
    //Used for melee and dash attacks
    private void LaunchAttack(Collider2D col)
    {
        //Find all colliders overlapping players 'melee' collider
        Collider2D[] cols = Physics2D.OverlapBoxAll(
            col.bounds.center,
            col.bounds.extents,
            0,
            LayerMask.GetMask("Hitbox"));

        foreach (Collider2D c in cols)
        {
            if (c.transform.parent.parent == transform || enemyScript.getShieldUp() == true)
            {
                continue;
            }
            //PlayerOne hit successful
            //Debug.Log("Player One Wins!");
            won = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        if (!gameOver)
        {
            //Turn on win message - player 1 win message must be first child of canvas.
            int childIndex = this.isPlayer1 ? 0 : 1;
            GameObject child = canvas.transform.GetChild(childIndex).gameObject;
            child.gameObject.GetComponent<Text>().enabled = true;

            gameOverTime = Time.time;
            gameOver = true;
            enemyScript.SetGameOver(true);
        }
        else if (gameOver && Time.time > gameOverTime + gameOverWait)
        {
            SceneManager.LoadScene("FinalMainScene", LoadSceneMode.Single);
        }
        else if (!won)
        {
            GameObject ChildGameObject = this.gameObject.transform.GetChild(1).gameObject;
            ChildGameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetGameOver(bool game)
    {
        gameOver = game;
        gameOverTime = Time.time;
    }

    public bool getOnRightSide()
    {
        return onRightSide;
    }

    public bool getShieldUp()
    {
        return shieldUp;
    }

    public void setOnRightSide(bool isOnRightSide)
    {
        this.onRightSide = isOnRightSide;
    }

    public void setShieldUp(bool shield)
    {
        shieldUp = shield;
    }
}
