using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MergedPlayerBehaviour : MonoBehaviour
{

    public Controls controls;
    public bool isPlayer1;

    //Dash
    private int dashSpeed = 4000;
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
    private const int MAX_BLOCK_VALUE = 50;
    private const int INITIAL_BLOCK_VALUE_DECREASE = 20;
    private int currentBlockDur = MAX_BLOCK_VALUE;
    private float maxBlockDur = 1;

    //Movement
    private float playerSpeed = 9;
    private float moveVelocity;

    //Player
    private bool onRightSide = true;
    private bool slidingoffhead = false;
    private bool groundpounding = false;
    public Collider2D[] attackHitboxes;
    public Animator animator;
    private int attackRange = 1;

    //Game
    public GameObject enemy;
    public Canvas canvas;
    private Rigidbody2D rb2d;
    private MergedPlayerBehaviour enemyScript;
    private bool gameOver = false;
    private bool won = false;
    private float gameOverTime;
    private float gameOverWait = 2f;

    //audio
    private AudioSource jumpSound;
    private AudioSource jabSound;
    private AudioSource groundPoundSound;
    private AudioSource dashSound;
    private AudioSource gameOverSound;
    private AudioSource fightMusic;

    // Use this for initialization
    void Start()
    {
        //initialise the sound sources
        AudioSource[] sounds = GetComponents<AudioSource>();
        this.fightMusic = GameObject.Find("FightMusic").GetComponent<AudioSource>();
        jumpSound = sounds[0];
        jabSound = sounds[1];
        groundPoundSound = sounds[2];
        dashSound = sounds[3];
        gameOverSound = sounds[4];
        //Initializes the players RigidBody, Animator and EnemyScript
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyScript = enemy.GetComponent<MergedPlayerBehaviour>();

        int character = 2; //Default character
        if(gameObject.tag == "Player1"){character = DataStore.PlayerOneCharacter;}
        else{character = DataStore.PlayerTwoCharacter;}

        Debug.Log(gameObject.name + " has selected " + character);
        if(character == 1) {
            this.dashLength = 0.15f;
            this.dashCooldown = 0.75f;
            this.jumpSpeed = 25;
            this.attackRange = 1;
            this.maxBlockDur = 1.5f;
            this.blockCD = 1.2f;
            this.playerSpeed = 7;
            doubleJump = true;
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Samurai/ata1");
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = !renderer.flipX;
        } else if(character == 3) {
            this.dashLength = 0.17f;
            this.dashCooldown = 1.1f;
            this.jumpSpeed = 32;
            this.attackRange = 2;
            this.maxBlockDur = 0.8f;
            this.blockCD = 1.0f;
            this.playerSpeed = 11;
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Munk/Munk");
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = !renderer.flipX;
        } else {
            //Nothing, you are the default
        }

        if(DataStore.controller1 && gameObject.tag == "Player1") {
            this.controls = Resources.Load<Controls>("Player1PS3Controller");
        } else if(DataStore.controller2 && gameObject.tag == "Player2") {
            this.controls = Resources.Load<Controls>("Player1PS3Controller");
        }
    }

    private void handleBlockMeter(){
        if (currentBlockDur <= 0)
        {
            nextBlock = Time.time + blockCD;
            currentBlockDur = MAX_BLOCK_VALUE;
            return;
        }

        if (blockIsOnCooldown() || currentBlockDur >= MAX_BLOCK_VALUE){
            return;
        }

        if(shieldUp){
            currentBlockDur -= 1;
        }
        else{
            currentBlockDur += 1;
        }

       
    }

    // Called every frame
    void Update()
    {
        if(!DataStore.ready){return;}
        handleBlockMeter();

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

        // walking animation
        if ( !dashing && ( Input.GetKeyDown(controls.left) || Input.GetKeyDown(controls.right) ) && !shieldUp && grounded)
        {
            animator.SetBool("Walking", true);    
        }
        if (!dashing && (Input.GetKeyUp(controls.left) || Input.GetKeyUp(controls.right)) && !shieldUp && grounded)
        {
            animator.SetBool("Walking", false);
        }


        //  Jump
        if (Input.GetKeyDown(controls.jump) && !shieldUp)
        {   //sets animator variables to true
            animator.SetBool("isJumping", true);
            animator.SetBool("Grounded", false);
            Jump();
        }
        //Melee
        //There are 3 melee cases - KeyUp, KeyDown, Key
        else if (Input.GetKeyUp(controls.jab) && shieldUp == false)
        {    //KeyUp is when releasing melee, turning off animation
            animator.SetBool("Jab_attack", false);
        }
        else if (Input.GetKeyDown(controls.jab) && shieldUp == false)
        {    //KeyDown is beginning of melee animation and launchAttack
            animator.SetBool("Jab_attack", true);
            this.jabSound.Play();
            if(attackRange == 2) {
                LaunchAttack(attackHitboxes[2]);
            } else {
                LaunchAttack(attackHitboxes[0]);
            }
        }
        //Block 
        /************Changed************/
        else if (Input.GetKey(controls.block))
        {
            Block();
        } else if (Input.GetKeyUp(controls.block)) {
            if (shieldUp)
            {
                //nextBlock = Time.time + blockCD;

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
        if(!DataStore.ready){return;}

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
            if (Input.GetKey(controls.left) && !shieldUp || (Input.GetKey(controls.left) && shieldUp && IsWalkingBackwardsKey(controls.left)) )
            {
                moveVelocity = -playerSpeed;
            }
            //move right
            if (Input.GetKey(controls.right) && !shieldUp || (Input.GetKey(controls.left) && shieldUp && IsWalkingBackwardsKey(controls.right)) )
            {
                moveVelocity = playerSpeed;
            }
            //Dash
            if (Input.GetKey(controls.dash) && Time.time > nextDash && !dashing)
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
            if (Input.GetKey(controls.groundPound) && !grounded)
            {
                groundpounding = true;
                GroundPound();
            }

            //player is in the air and can move left/right at half speed.
            //move left
            if (!dashing && Input.GetKey(controls.left) && !shieldUp)
            {
                moveVelocity = -playerSpeed;
            }
            //move right
            if (!dashing && Input.GetKey(controls.right) && !shieldUp)
            {
                moveVelocity = playerSpeed;
            }
            //Dash
            if (Input.GetKey(controls.dash) && Time.time > nextDash && !dashing)
            {
                //Set the time for next earliest dash
                nextDash = Time.time + dashCooldown;
                //Set dash attack variable in animator to true
                animator.SetBool("Dash_Attack", true);

                Dash();
                return;
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
            jumpCount = 0;
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
        if(!this.groundPoundSound.isPlaying)
            this.groundPoundSound.Play();
    }

    // this method will push the player off the other player's head
    private void SlideOffHead()
    {
        Debug.Log("Slide off head");
        slidingoffhead = true;
    }

    //Check to see if player grounded, update booleans if so
    private void checkGrounded() //TODO remove this and use ground collider instead
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

    private bool blockIsOnCooldown(){
        if(Time.time > nextBlock){
            return false;
        }
        return true;
    }

    private void Block()
    {
        if(shieldUp) {
            if(currentBlockDur <= 0) {
                
   
                //currentBlockDur = 100;
                shieldUp = false;
                nextBlock = Time.time + blockCD;
            }

        } else {
            if(!blockIsOnCooldown()) {
                shieldUp = true;

                // initial block uses heaps of meter so you cannot spam block
                currentBlockDur = currentBlockDur - INITIAL_BLOCK_VALUE_DECREASE;
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
                animator.SetBool("Dash_Attack", false);     //set dash attack variable in animator to false
                this.rb2d.velocity = new Vector2(0, 0);
            }
            else
            {
                if(attackRange == 2) {
                    LaunchAttack(attackHitboxes[2]);
                } else {
                    LaunchAttack(attackHitboxes[0]);
                }
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
            this.dashSound.Play();

        }
        if (shieldUp)
        {   //Turns shield off before dashing
            Block();
        }
        else if (dashDir)
        {
            //ensures that the only force acting during dash is horizontal force
            this.moveVelocity = 0;
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(-dashSpeed, 0));
        }
        else
        {
            this.moveVelocity = 0;
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(new Vector2(dashSpeed, 0));
        }

    }

    private void Jump()
    {
        //check player is grounded before jumping
        Debug.Log("Double: " + doubleJump + ", jumpCount: " + jumpCount);
        if (grounded || (doubleJump && jumpCount < 2))
        {
            this.jumpSound.Play();
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.SetBool("isJumping", true);
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
            child.gameObject.GetComponent<Image>().enabled = true;
            this.gameOverSound.Play();
            this.fightMusic.Stop();

            gameOverTime = Time.time;
            gameOver = true;
            enemyScript.SetGameOver(true);
        }
        else if (gameOver && Time.time > gameOverTime + gameOverWait)
        {
            //restart the current scene
            DataStore.ready = false;
            this.fightMusic.PlayDelayed(1.6f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (!won)
        {
            GameObject ChildGameObject = this.gameObject.transform.GetChild(1).gameObject;
            ChildGameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // This method returns if the key input represents walking backwards based on the direction 
    // the player is facing.
    private bool IsWalkingBackwardsKey(KeyCode input)
    {
        if (onRightSide && input == controls.right)
        {
            return true;
        }

        if (!onRightSide && input == controls.left)
        {
            return true;
        }
        return false;
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
