using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    // Update is called once per frame
    private void FixedUpdate()
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
        if (other.gameObject.CompareTag("Player") && other.isTrigger) {
            
            float knockTime = GetKnockTime();
            float knockThrust = GetKnockThrust();

            Vector2 knockVector = other.GetComponent<Rigidbody2D>().transform.position - transform.position;
            knockVector = knockVector.normalized * knockThrust;

            other.GetComponent<Humanoid>().Knockback(knockVector, knockTime);
        }
    }
    

}
