using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Humanoid
{
    private Vector3 change;

    // Start is called before the first frame update
    private void Start() {
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
        change = Vector3.zero;

        // get new change
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        HumanoidState state = GetState();

        if (state == HumanoidState.walk || state == HumanoidState.idle) {
            // move character
            UpdateAnimationAndMove();
        }
    }
 

    // MOVEMENT
    private void UpdateAnimationAndMove() {
        if (change != Vector3.zero) {
            MoveCharacter();

            // animator blend tree changes
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
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

}
