using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState {
    walk,
    attack,
    interact
}


public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // set initial state
        currentState = PlayerState.walk;

        // initial direction
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack) {
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

        if (currentState == PlayerState.walk) {
            // move character
            UpdateAnimationAndMove();
        }
    }


    private IEnumerator AttackCoroutine() {
        currentState = PlayerState.attack;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.33f);

        currentState = PlayerState.walk;
    }

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
}
