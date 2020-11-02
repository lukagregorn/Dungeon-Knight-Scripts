using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Humanoid
{
    public Signal playerHealthSignal;
    public IntValue health;
    private Vector3 moveChange;

    // Start is called before the first frame update
    private void Start() {
        // initial health
        health.initialValue = maxHealth.initialValue;

        // initial direction
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    private void Update() {
        HumanoidState state = GetState();

        if (Input.GetButtonDown("Attack") && state != HumanoidState.attack
                && state != HumanoidState.stagger) {
            StartCoroutine(AttackCoroutine());
        }
    }


    // FixedUpdate is called when physics update
    private void FixedUpdate() {
        // reset change
        moveChange = Vector3.zero;

        // get new change
        moveChange.x = Input.GetAxisRaw("Horizontal");
        moveChange.y = Input.GetAxisRaw("Vertical");

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
            transform.position + moveChange * speed.initialValue * Time.deltaTime
        );
    }


    // COMBAT
    private IEnumerator AttackCoroutine() {
        ChangeState(HumanoidState.attack);
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.33f);

        ChangeState(HumanoidState.walk);
    }

    
    public override void TakeDamage(int damage) {
        health.initialValue -= damage;
        if (health.initialValue <= 0) {
            ChangeState(HumanoidState.dead);
            gameObject.SetActive(false);

            return;
        }

        playerHealthSignal.Fire();
    }

}
