using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Humanoid
{
    public Signal playerHealthSignal;
    public Signal playerDiedSignal;
    public Signal playerCoinsSignal;
    public GameObject contextClue;
    public IntValue health;
    public VectorValue playerPositionStorage;
    private Vector3 moveChange;
    public Joystick joystick;

    // playerData stuff
    public IntValue healthLevel;
    public IntValue strenghtLevel;
    public IntValue speedLevel;
    public IntValue coins;
    public IntValue bestWave;


    private void SetupData() {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null) {
            // we found our save file
            healthLevel.initialValue = data.healthLevel;
            strenghtLevel.initialValue = data.strenghtLevel;
            speedLevel.initialValue = data.speedLevel;

            coins.initialValue = data.coins;
            bestWave.initialValue = data.bestWave;
        } else {
            healthLevel.initialValue = 1;
            strenghtLevel.initialValue = 1;
            speedLevel.initialValue = 1;

            coins.initialValue = 0;
            bestWave.initialValue = 0;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        
        // load player data
        SetupData();

        // initial health
        if (maxHealth.value % 2 != 0)
            Debug.LogWarning("Player max health is not even. Hearts may not show correctly");
        health.value = maxHealth.value;
        playerHealthSignal.Fire();

        // initial direction
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = playerPositionStorage.value;
    }

    // Update is called once per frame
    private void Update() {
        HumanoidState state = GetState();

        if (Input.GetButtonDown("Attack") && state != HumanoidState.attack
                && state != HumanoidState.stagger) {
            StartCoroutine(AttackCoroutine());
        }

        //for (int i = 0; i < Input.touchCount; i++) {
        //    Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //    Debug.DrawLine(transform.position, touchPos, Color.red);
        //}
    }


    // FixedUpdate is called when physics update
    private void FixedUpdate() {
        // reset change
        moveChange = Vector3.zero;

        // get new change
        //moveChange.x = Input.GetAxisRaw("Horizontal");
        //moveChange.y = Input.GetAxisRaw("Vertical");

        moveChange.x = joystick.Horizontal;;
        moveChange.y = joystick.Vertical;
        

        HumanoidState state = GetState();

        if (state == HumanoidState.walk || state == HumanoidState.idle) {
            // move character
            UpdateAnimationAndMove();
        }
    }
 

    // MOVEMENT
    private void UpdateAnimationAndMove() {
        if (moveChange != Vector3.zero) {
            MoveCharacter();

            // animator blend tree changes
            animator.SetFloat("moveX", moveChange.x);
            animator.SetFloat("moveY", moveChange.y);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        moveChange.Normalize();
        myRigidbody.MovePosition(
            transform.position + moveChange * speed.value * Time.deltaTime
        );
    }


    // COMBAT
    public void OnAttack() {
        HumanoidState state = GetState();

        if (state != HumanoidState.attack && state != HumanoidState.stagger) {
            StartCoroutine(AttackCoroutine());
        }
    }


    private IEnumerator AttackCoroutine() {
        ChangeState(HumanoidState.attack);
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.33f);

        ChangeState(HumanoidState.walk);
    }

    
    public override void TakeDamage(int damage) {
        
        health.value -= damage;
        playerHealthSignal.Fire();

        if (health.value <= 0) {
            OnDeath();
        }

    }


    public override void OnDeath() {
        ChangeState(HumanoidState.dead);
        
        health.value = 0;
        playerHealthSignal.Fire();

        // save data
        SaveSystem.SavePlayer(this);

        // fire death signal
        playerDiedSignal.Fire();
        gameObject.SetActive(false);
    }


    //MISC
    public void SetContextClue(Sprite clueSprite, bool visible) {
        if (enabled && clueSprite) {
            contextClue.GetComponent<SpriteRenderer>().sprite = clueSprite;
        }
        contextClue.SetActive(visible);
    }


    public void UpdateCoins(int amount) {
        coins.initialValue += amount;
        playerCoinsSignal.Fire();
    }


}
