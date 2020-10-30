using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = HumanoidState.idle;
        
        target = GameObject.FindWithTag("Player").transform;

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsInChaseRadius()) {

            animator.SetBool("wakeUp", true);

            if (!IsInAttackRadius()) {
                Vector3 direction = MoveTowardsTarget();
                
                // determine which animation to play
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                    if (direction.x > 0) {
                        animator.SetFloat("moveX", 1f);
                        animator.SetFloat("moveY", 0f);
                    } else {
                        animator.SetFloat("moveX", -1f);
                        animator.SetFloat("moveY", 0f);
                    }

                } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
                    if (direction.y > 0) {
                        animator.SetFloat("moveX", 0f);
                        animator.SetFloat("moveY", 1f);
                    } else {
                        animator.SetFloat("moveX", 0f);
                        animator.SetFloat("moveY", -1f);
                    }
                }


            } else {
                Debug.Log("Attack");
            }
        } else {
            animator.SetBool("wakeUp", false);
        }
    }


    // MOVEMENT


    // COMBAT
    private void OnTriggerEnter2D(Collider2D other) {
        // check if player
        if (other.gameObject.CompareTag("Player")) {
            
            Vector2 knockVector = other.GetComponent<Rigidbody2D>().transform.position - transform.position;
            knockVector = knockVector.normalized * thrust;

            other.GetComponent<Humanoid>().Knockback(knockVector, knockTime);
        }
    }
    

}
