using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Humanoid
{
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        // set initial state
        currentState = HumanoidState.walk;

        // initial direction
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Attack") && currentState != HumanoidState.attack
                && currentState != HumanoidState.stagger) {
            StartCoroutine(AttackCoroutine());
        }
    }


    // FixedUpdate is called when physics update
    void FixedUpdate()
    {
        // reset change
        change = Vector3.zero;

        // get new change
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (currentState == HumanoidState.walk || currentState == HumanoidState.idle) {
            // move character
            UpdateAnimationAndMove();
        }
    }
 

    // MOVEMENT
    void UpdateAnimationAndMove() {
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

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }


    // COMBAT
    private IEnumerator AttackCoroutine() {
        currentState = HumanoidState.attack;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.33f);

        currentState = HumanoidState.walk;
    }

}
